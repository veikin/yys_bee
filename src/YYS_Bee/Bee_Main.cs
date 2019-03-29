using System;
using System.Linq;
using System.Windows.Forms;
using YYS_Bee.Imp;
using YYS_Bee.Model;
using YYS_Bee.WinApi;

namespace YYS_Bee
{
    public partial class Bee_Main : Form
    {
        private Job job;
        private GameSetting setting;

        public Bee_Main()
        {
            InitializeComponent();
        }

        private void Bee_Main_Load(object sender, EventArgs e)
        {
            //if (DateTime.Now > Convert.ToDateTime("2018-06-01"))
            //{
            //    this.Close();
            //    this.Dispose();
            //}

            
            var list=TemplatesManager.GetAllGameSettings();
            if(list!=null && list.Count>0)
                setting = list[0];

            templates.DataSource = list;
            //templates.ValueMember = "FileName";
            templates.DisplayMember = "Name";

            templates.SelectedIndexChanged += new EventHandler(templates_SelectedIndexChanged);

            templates.SelectedIndex = 0;
            templates_SelectedIndexChanged(null, null);

            




        }
       
        //声明一个delegate（委托）类型：testDelegate，该类型可以搭载返回值为空，参数只有一个(long型)的方法。
        public delegate void testDelegate(string msg);

        //声明一个testDelegate类型的对象。该对象代表了返回值为空，参数只有一个(long型)的方法。它可以搭载N个方法。
        public testDelegate mainThread;
        private void AlertMsg(string msg)
        {
            //Control.CheckForIllegalCrossThreadCalls = false;
            //判断该方法是否被主线程调用，也就是创建labMessage1控件的线程，当控件的InvokeRequired属性为ture时，说明是被主线程以外的线程调用。如果不加判断，会造成异常
            if (this.textBox1.InvokeRequired)
            {
                //为新对象的mainThread对象搭载方法
                mainThread = new testDelegate(AlertMsg);
                //this指窗体，在这调用窗体的Invoke方法，也就是用窗体的创建线程来执行mainThread对象委托的方法，再加上需要的参数(i)
                this.Invoke(mainThread, new object[] { msg });
            }
            else
            {
                textBox1.AppendText(msg + Environment.NewLine);
            }
        }

        private void Bee_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (job != null)
                job.Stop();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox1.SelectionStart = this.textBox1.Text.Length;
            this.textBox1.SelectionLength = 0;
            this.textBox1.ScrollToCaret();

            if (this.textBox1.Text.Length > 1000)
            {
                this.textBox1.Clear();
                this.textBox1.Text = "日志清除。。\r\n";
            }
        }

        private WindowInfo GetCurrentSelectWindoInfo()
        {
            var currentH = GetCurrentSelectWindoHandle();
            var winInfo = WindowTools.GetWindowInfo(currentH);
            return winInfo;

        }
        private WindowHandle GetCurrentSelectWindoHandle()
        {
            string winHashCode = wins.Text;
            var winis = WindowTools.GetWindowHandles(windowName.Text);
            var currentH = winis.Where(n => n.hWnd.GetHashCode() == int.Parse(winHashCode)).FirstOrDefault();
            return currentH;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var winInfo = GetCurrentSelectWindoInfo();
                if (setting != null)
                {
                    WindowTools.SetWindowSize(winInfo, setting);
                }
                string appBaseDir = AppTools.GetAppBaseDir();
                //截图
                var image = Win.CopyScreen(winInfo.hWnd.hWnd);
                var path = ImageTools.SaveImage(appBaseDir + "Temp", DateTime.Now.ToString("yyyy-MM-dd-HHmmssfff") + ".png", image);
                AlertMsg("截图成功：" + path);
            }
            catch (Exception ex)
            {
                AlertMsg("截图异常：" + ex.Message);
            }
        }

        private void templates_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            setting =(GameSetting)templates.SelectedValue;
            //setting=TemplatesManager.GetGameSettings(fileName);
            windowHeight.Text = setting.WindowHeight.ToString();
            windowWidth.Text = setting.WindowWidth.ToString();
            windowName.Text = setting.WindowName;
            try
            {
                //初始化窗口信息
                var winis = WindowTools.GetWindowHandles(windowName.Text);
                foreach (var item in winis)
                {
                    AlertMsg("找到"+item.szWindowName+"["+item.hWnd.GetHashCode()+"]");
                }
                wins.DataSource = winis.Select(n=>n.hWnd.GetHashCode()).ToList();
                //templates.ValueMember = "FileName";
                //wins.DisplayMember = "hWnd";
            }
            catch (Exception ex)
            {
                AlertMsg(string.Format("获取{0}窗口信息失败：{1}", setting.WindowName,ex.Message));
            }
            AlertMsg(setting.Describe);
            AlertMsg("===开启功能==");
            foreach (var gameIndexTemplate in setting.Templates)
            {
                if (gameIndexTemplate.Enabled)
                    AlertMsg(gameIndexTemplate.TemplateName);
            }
            AlertMsg("============");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (button2.Text== "关闭辅助")
                {
                    button2.Enabled = false;
                    if (job != null)
                        job.Stop();
                    IsSoftwareSimulation.Enabled = true;
                    templates.Enabled = true;
                    wins.Enabled = true;
                    button2.Enabled = true;
                    button2.Text = "开启辅助";
                }
                else
                {
                    button2.Enabled = false;
                    //为新对象的mainThread对象搭载方法
                    setting.IsSoftwareSimulation = IsSoftwareSimulation.Checked;
                    job = new Job(AlertMsg, setting, GetCurrentSelectWindoHandle());
                    job.Run();

                    IsSoftwareSimulation.Enabled = false;
                    templates.Enabled = false;
                    wins.Enabled = false;
                    button2.Enabled = true;
                    button2.Text = "关闭辅助";
                }
            }
            catch (Exception ex)
            {
                AlertMsg("异常：" + ex.Message);
                IsSoftwareSimulation.Enabled = true;
                templates.Enabled = true;
                button2.Enabled = true;
            }

        }

        private void IsSoftwareSimulation_CheckedChanged(object sender, EventArgs e)
        {
            if (IsSoftwareSimulation.Checked)
                AlertMsg("软件模拟可以不控制鼠标，但还不稳定且有可能会被游戏反作弊检测到，目前仅用于测试。");
        }

        private void templates_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
