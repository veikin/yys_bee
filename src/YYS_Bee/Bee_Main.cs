using System;
using System.Windows.Forms;
using YYS_Bee.Imp;
using YYS_Bee.Model;

namespace YYS_Bee
{
    public partial class Bee_Main : Form
    {
        private Job job;

        public Bee_Main()
        {
            InitializeComponent();
        }

        private void Bee_Main_Load(object sender, EventArgs e)
        {
            //if (DateTime.Now > Convert.ToDateTime("2018-05-01"))
            //{
            //    this.Close();
            //    this.Dispose();
            //}

        }
        /// <summary>
        /// 开启按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KaiQiCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!KaiQiCheckBox.Checked)
            {
                if (job!=null)
                    job.Stop();
            }
            else
            {
                int height, width;
                int.TryParse(windowHeight.Text, out height);
                int.TryParse(windowWidth.Text, out width);
                job=new Job(AlertMsg,new GameSetting()
                {
                    WindowName = windowName.Text,
                    WindowHeigth = height,
                    WindowWidth = width
                });
                job.Run();
            }
        }

        private void AlertMsg(string msg)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            textBox1.AppendText(msg + Environment.NewLine);
        }

        private void Bee_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (job != null)
                job.Stop();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox1.SelectionStart = this.textBox1.Text.Length;
            this.textBox1.SelectionLength = 0;
            this.textBox1.ScrollToCaret();

            if (this.textBox1.Text.Length > 10000)
            {
                this.textBox1.Text = "日志清除。。";
            }
        }
    }
}
