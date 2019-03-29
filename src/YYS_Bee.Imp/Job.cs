using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using YYS_Bee.Configs;
using YYS_Bee.Model;
using YYS_Bee.WinApi;

namespace YYS_Bee.Imp
{
    public class Job
    {
        /// <summary>
        /// 成功点击执行次数
        /// </summary>
        private int _suClickCount;

        private readonly GameSetting _gameSetting;
        private  SystemActionLock systemActionLock=new SystemActionLock();
        public Job(Action<string> alert,GameSetting setting)
        {
            _alert = alert;
            _gameSetting = setting;
            ConfigRepository.ConfigInit(setting);
        }

        public List<Task> Tasks;
        private readonly Action<string> _alert;
        //任务控制变量  
        private CancellationTokenSource _cts;
        public void Run()
        {
            
            _alert("开启检测！");
            _cts = new CancellationTokenSource();
            Tasks = new List<Task>();
            //实时获取最新模板
            List<GameIndexTemplate> templates = ConfigRepository.GetCurrentTemplates();

            foreach (var template in templates)
            {
                //未启用
                if (!template.Enabled)
                    continue;
                var task = new Task(()=> { Task(template); });
                Tasks.Add(task);
                task.Start();
            }
        }
        public void Stop()
        {
            if(_cts!=null)
                _cts.Cancel();
            _alert("停止检测！");
        }

        private void Task(GameIndexTemplate template)
        {
            while (!_cts.IsCancellationRequested)
            {
                try
                {
                    _alert(template.TemplateName + ":检测开始");
                    System.Diagnostics.Stopwatch sp = new System.Diagnostics.Stopwatch();
                    sp.Start();
                    //获取窗体信息
                    var windowHanle = WindowTools.GetWindowHandle(_gameSetting.WindowName);
                    //窗体大小非指定大小时改变其大小

                    var info = WindowTools.GetWindowInfo(windowHanle);

                    //if (info.Width != _gameSetting.WindowWidth || info.Height != _gameSetting.WindowHeigth)
                    //{
                    //    WinApi.Win.MoveWindow(info.hWnd.hWnd, info.X, info.Y, _gameSetting.WindowWidth,
                    //        _gameSetting.WindowHeigth, true);
                    //}

                    //搜索模板中图片位置
                    Rectangle result = Check(info, template);
                    if (result.IsEmpty)
                    {
                        _alert(template.TemplateName + ":运行耗时" + sp.ElapsedMilliseconds + "ms");
                        continue;
                    }
                    //根据搜索结果点击结果区域
                    Click(info, template, new List<Rectangle>() { result });
                    sp.Stop();
                    _alert(template.TemplateName + ":运行耗时" + sp.ElapsedMilliseconds + "ms");
                }
                catch (Exception ex)
                {
                    _alert("异常:" + ex.Message);
                }
                finally {
                    Thread.Sleep((int)(template.Interval * 1000));
                }
            }
        }

        private Rectangle Check(WindowInfo info, GameIndexTemplate template)
        {
            FileConfig fileConfig= ConfigRepository.fileConfig;
            //获取结束图片模板
            string imagePath = fileConfig.GetTemplateIndexImagePath(template.TemplateImageName);
            Bitmap templateImage = (Bitmap)Image.FromFile(imagePath);

            //置顶
            Win.SetWindowPos(info.hWnd.hWnd, -1, 0, 0, 0, 0, 1 | 2);
            //var image = Win.GetWindowCapture(windowHanle.hWnd);
            //截图
            var image = Win.CopyScreen(info.Width, info.Height, info.X,info.Y);

            var s_bmp = image;
            var p_bmp = templateImage;

            var bmp_temp_path = fileConfig.WindowImageTempDir;

            string timespan = "";// DateTime.Now.ToString("yyyyMMddHHmmssfff");

            var s_bmp_temp_name= template.TemplateId + "_s.png";
            var p_bmp_temp_name = template.TemplateId + "_p.png";

            //存储截图
            ImageTools.SaveImage(bmp_temp_path + "\\" + timespan, s_bmp_temp_name, s_bmp);
            ImageTools.SaveImage(bmp_temp_path + "\\" + timespan, p_bmp_temp_name, templateImage);
            Stopwatch sp=new Stopwatch();
            sp.Start();
            long matchTime;
            Rectangle result = ImageTools.Match(
                Path.Combine(bmp_temp_path + "\\" + timespan, s_bmp_temp_name),
                imagePath,
                out matchTime);
            sp.Stop();
            _alert("图片匹配耗时："+sp.ElapsedMilliseconds+"ms");

            if (!result.IsEmpty )//&& (result.X >= 0 && result.Y >= 0 && result.Width > 0 && result.Height > 0))
            {
                //计算与模板大小差
                if (result.Width < p_bmp.Width || result.Height < p_bmp.Height)
                {
                    return new Rectangle();
                }

                if (result.Width - p_bmp.Width > 20 || result.Height - p_bmp.Height > 20)
                {
                    return new Rectangle();
                }
                //找到位置
                _alert(string.Format("找到位置：\r\nx={0}\r\ny={1}\r\nw={2}\r\nh={3}", result.X, result.Y, result.Width, result.Height));
                Graphics g = Graphics.FromImage(s_bmp);
                g.DrawRectangle(new Pen(Color.Red, 2), result);
                ImageTools.SaveImage(bmp_temp_path + "\\" + timespan, template.TemplateId + "_ss.png", s_bmp);
                return result;
            }
            return new Rectangle();
        }

        private void Click(WindowInfo info, GameIndexTemplate template, List<Rectangle> rectangles)
        {
            //计算范围
            int x = info.X;
            int y = info.Y;

            int x_max = x + info.Width;
            int y_max = y + info.Height;

            int randomX = 0;
            int randomY = 0;
            Random random=new Random();
            if (template.ClickConfig.ClickEndShowRandomPosition)
            {
                //随机到游戏所在区域之外
                 var  temp= random.Next(1, 101);
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
            //系统操作锁-如果有其他任务在操作就会等待
            systemActionLock.LineUp(template.TemplateId);

            //屏幕定位
            _alert(string.Format("屏幕定位：\r\nx={0}\r\ny={1}\r\nx_max={2}\r\ny_max={3}", x, y, x_max, y_max));
            foreach (var rectangle in rectangles)
            {
                if (rectangle.IsEmpty)
                    continue;

               
                //使用模板的位置
                if (template.ClickConfig.UseTemplateRange)
                {
                    x = info.X + rectangle.X-10;
                    y = info.Y + rectangle.Y+10;
                    x_max = x + rectangle.Width-10;
                    y_max = y + rectangle.Height-10;

                    //屏幕定位
                    _alert(string.Format("使用模板位置：\r\nx={0}\r\n\r\ny={1}\r\nx_max={2}\r\ny_max={3}", x, y, x_max, y_max));
                }

                x = x + template.ClickConfig.X_Deviation;
                y = y + template.ClickConfig.Y_Deviation;
                x_max = x_max + template.ClickConfig.Max_X_Deviation;
                y_max = y_max + template.ClickConfig.Max_Y_Deviation;
                _alert(string.Format("模板位置偏移值：\r\nx={0}\r\ny={1}\r\nx_max={2}\r\ny_max={3}", template.ClickConfig.X_Deviation, template.ClickConfig.Y_Deviation, template.ClickConfig.Max_X_Deviation, template.ClickConfig.Max_Y_Deviation));
                //屏幕定位
                _alert(string.Format("模板位置偏移后：\r\nx={0}\r\ny={1}\r\nx_max={2}\r\ny_max={3}", x, y, x_max, y_max));

                int z_x = RandomTools.Get(x, x_max);
                int z_y = RandomTools.Get(y, y_max);
                //移动鼠标到这个位置
                WinApi.KeyMouseHelper.MouseMove1(z_x, z_y);
                Random ran=new Random();
                foreach (var click in template.ClickConfig.ClickSub)
                {
                    if (click.IsRandom)
                    {
                        if(ran.Next(1,3)>1)
                            continue;
                    }
                    //需要休眠
                    if (click.Sleeped)
                    {
                        int time = RandomTools.Get(click.IntervalMin, click.IntervalMax);
                        _alert("延迟："+time+"ms");
                        Thread.Sleep(time);
                    }
                    _alert("点击：\r\nx=" + z_x + "\r\ny=" + z_y);
                    WinApi.KeyMouseHelper.ActivateWindow(info.hWnd.hWnd);
                    Thread.Sleep(20);
                    //点击
                    WinApi.KeyMouseHelper.MouseLeftClick();
                }

                _suClickCount++;
                _alert("成功自动执行次数：" + _suClickCount);
                if (template.ClickConfig.ClickEndShowRandomPosition)
                {
                    _alert(string.Format("自动移动到随机位置：random_x={0}\r\nrandom_y={1}", randomX, randomY));
                    //移动鼠标到这个位置
                    WinApi.KeyMouseHelper.MouseMove1(randomX, randomY);
                }

            }
            //使用完毕后出队
            systemActionLock.Dequeue();
        }
    }
}
