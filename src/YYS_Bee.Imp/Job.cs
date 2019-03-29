using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YYS_Bee.Model;
using YYS_Bee.WinApi;

namespace YYS_Bee.Imp
{
    public class Job
    {
        /// <summary>
        /// 成功点击执行次数
        /// </summary>
        private ConcurrentDictionary<string, int> concurrentDictionary;

        private readonly GameSetting _gameSetting;
        //private SystemActionLock systemActionLock = new SystemActionLock();
        private static object mylock = new object();
        private static bool _debug = false;
        private Action<string> _sysLog;

        private ConcurrentQueue<string> _logQueue;
        private WindowHandle _windowHandle;
        public Job(Action<string> alert, GameSetting setting, WindowHandle windowHandle)
        {
            _sysLog = alert;
            _gameSetting = setting;
            _windowHandle = windowHandle;
        }

        public List<Task> Tasks;
        private void _alert(string msg, bool show = false)
        {
            //if (_debug || show)
            //    _sysLog(msg);
            if (_debug || show)
                _logQueue.Enqueue("["+_windowHandle.hWnd.GetHashCode()+"]"+msg);
        }
        //任务控制变量  
        private CancellationTokenSource _cts;
        public void Run()
        {
            _logQueue = new ConcurrentQueue<string>();
            concurrentDictionary = new ConcurrentDictionary<string, int>();
            //_alert("开启检测！",true);
            _cts = new CancellationTokenSource();
            Tasks = new List<Task>();
            //实时获取最新模板
            List<GameIndexTemplate> templates = _gameSetting.Templates;
            //获取窗体信息
            var windowHanle = _windowHandle;
            //激活窗体
            KeyMouseHelper.ActivateWindow(windowHanle.hWnd);
            //置顶
            Win.SetWindowPos(windowHanle.hWnd, -1, 0, 0, 0, 0, 1 | 2);
            foreach (var template in templates)
            {
                //未启用
                if (!template.Enabled)
                    continue;

                var task = new Task(() =>
                {
                    Task(template, windowHanle);
                });
                Tasks.Add(task);
                task.Start();
            }
            //日志线程-从日志队列里拿日志输出
            var taskLog = new Task(() =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    string msg;
                    while (_logQueue.TryDequeue(out msg))
                    {
                        _sysLog(msg);
                    }
                    Thread.Sleep(1000);

                }
            });
            Tasks.Add(taskLog);
            taskLog.Start();
        }
        public void Stop()
        {
            if (_cts != null)
                _cts.Cancel();
            //_alert("停止检测！", true);
        }



        private void Task(GameIndexTemplate template, WindowHandle windowHanle)
        {
            while (!_cts.IsCancellationRequested)
            {
                try
                {
                    //_alert(template.TemplateName + ":检测开始");
                    System.Diagnostics.Stopwatch sp = new System.Diagnostics.Stopwatch();
                    sp.Start();

                    //窗体信息
                    var info = WindowTools.GetWindowInfo(windowHanle);
                    if (WindowTools.SetWindowSize(info, _gameSetting))
                    {
                        //_alert(string.Format("窗体大小发生变化，重新设置大小为{0}x{1}！", _gameSetting.WindowWidth,_gameSetting.WindowHeight), true);
                    }
                    //搜索模板中指定位置是否匹配到色值
                    bool result = Check(info, template);
                    if (!result)
                    {
                        //_alert(template.TemplateName + ":运行耗时" + sp.ElapsedMilliseconds + "ms");
                        continue;
                    }

                    lock (mylock)
                    {
                        //根据搜索结果点击结果区域
                        Click(info, template);
                        //计数
                        concurrentDictionary.AddOrUpdate(template.TemplateId, 1, (key, value) => value + 1);
                        sp.Stop();
                        //_alert(template.TemplateName + ":运行耗时" + sp.ElapsedMilliseconds + "ms");
                        _alert("==次数统计" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "==", true);
                        foreach (var i in concurrentDictionary)
                        {
                            _alert(i.Key + "=" + i.Value, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _alert("异常:" + ex.Message, true);
                }
                finally
                {
                    Thread.Sleep((int)(template.Interval * 1000));
                }
            }
        }
        /// <summary>
        /// 检测是否命中
        /// </summary>
        /// <param name="info"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        private bool Check(WindowInfo info, GameIndexTemplate template)
        {
            //截图
            using (Bitmap image = Win.CopyScreen(info.hWnd.hWnd))
            {
                //ImageTools.SaveImage(AppTools.GetAppBaseDir() + "Temp", "current"+DateTime.Now.ToString("yyyyMMddHHmmss")+".png", image);
                foreach (var item in template.CheckPosition)
                {
                    Color color= image.GetPixel(item.X, item.Y);
                
                    string rgb = string.Format("{0},{1},{2}", color.R,color.G,color.B);
                    //_alert(string.Format("x={0},y={1},color={2},config={3}", item.X, item.Y, rgb, item.ColorValue));
                    if (rgb != item.ColorValue)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// 点击计算与操作
        /// </summary>
        /// <param name="info"></param>
        /// <param name="template"></param>
        private void Click(WindowInfo info, GameIndexTemplate template)
        {
            var clickConfigs = template.ClickConfig.OrderBy(n => n.Order).ToList();
            //系统操作锁-如果有其他任务在操作就会等待
            //systemActionLock.LineUp(template.TemplateId);
            foreach (var clickConfig in clickConfigs)
            {
                //计算窗体范围
                int x = info.X;
                int y = info.Y;
                //窗体最大范围
                int x_max = x + info.Width;
                int y_max = y + info.Height;

                #region 计算一个结束后的随机落点
                int randomX = 0;
                int randomY = 0;
                if (clickConfig.ClickEndShowRandomPosition && !_gameSetting.IsSoftwareSimulation)
                {
                    Random random = new Random();
                    //随机到游戏所在区域之外
                    var temp = random.Next(1, 101);
                    if (temp > 50)
                    {
                        randomX = x - temp;
                    }
                    else
                    {
                        randomX = x_max + temp;
                    }
                    var temp1 = random.Next(1, 101);
                    if (temp1 > 50)
                    {
                        randomY = y - temp1;
                    }
                    else
                    {
                        randomY = y_max + temp1;
                    }
                }
                #endregion

                //屏幕定位
                //_alert(string.Format("模板位置偏移后：\r\nx={0}\r\ny={1}\r\nx_max={2}\r\ny_max={3}", x, y, x_max, y_max));
                //计算配置的点击区域
                int z_x = RandomTools.Get(clickConfig.X, clickConfig.X_Max);
                int z_y = RandomTools.Get(clickConfig.Y, clickConfig.Y_Max);

                if (!_gameSetting.IsSoftwareSimulation)
                {
                    //移动鼠标到这个位置
                    KeyMouseHelper.MouseMove1(x + z_x, y + z_y);
                }
                else
                {
                    //移动鼠标到这个位置
                    KeyMouseHelper.SoftwareSimulationMouseMove(z_x, z_y, info.hWnd.hWnd);
                }
                //点击
                MouseClick(clickConfig.ClickSub, z_x, z_y, info);
                #region 随机落点
                if (clickConfig.ClickEndShowRandomPosition && !_gameSetting.IsSoftwareSimulation)
                {
                    //_alert(string.Format("自动移动到随机位置：random_x={0}\r\nrandom_y={1}", randomX, randomY));
                    //硬件模拟时才随机落点
                    //移动鼠标到这个位置
                    KeyMouseHelper.MouseMove1(randomX, randomY);
                }
                #endregion
            }
            //使用完毕后出队
            //systemActionLock.Dequeue();
        }
        /// <summary>
        /// 点击索引点击组中的事件
        /// </summary>
        /// <param name="ClickSub"></param>
        private void MouseClick(ClickSub[] ClickSub, int x_Relative, int y_Relative, WindowInfo info)
        {

            Random ran = new Random();
            foreach (var click in ClickSub)
            {
                if (click.IsRandom)
                {
                    if (ran.Next(1, 3) > 1)
                        continue;
                }
                //需要休眠
                if (click.Sleeped)
                {
                    int time = RandomTools.Get(click.IntervalMin, click.IntervalMax);
                    //_alert("延迟：" + time + "ms");
                    Thread.Sleep(time);
                }
                //_alert("点击");
                Thread.Sleep(20);
                if (!_gameSetting.IsSoftwareSimulation)
                {
                    //点击
                    KeyMouseHelper.MouseLeftClick();
                }
                else
                {
                    KeyMouseHelper.SoftwareSimulationMouseLeftClick(x_Relative, y_Relative, info.hWnd.hWnd);
                }
            }
        }
    }
}
