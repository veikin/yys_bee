namespace YYS_Bee
{
    partial class Bee_Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bee_Main));
            this.KaiQiCheckBox = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.windowWidth = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.windowHeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.windowName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // KaiQiCheckBox
            // 
            this.KaiQiCheckBox.AutoSize = true;
            this.KaiQiCheckBox.Location = new System.Drawing.Point(231, 23);
            this.KaiQiCheckBox.Name = "KaiQiCheckBox";
            this.KaiQiCheckBox.Size = new System.Drawing.Size(48, 16);
            this.KaiQiCheckBox.TabIndex = 1;
            this.KaiQiCheckBox.Text = "开启";
            this.KaiQiCheckBox.UseVisualStyleBackColor = true;
            this.KaiQiCheckBox.CheckedChanged += new System.EventHandler(this.KaiQiCheckBox_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.ForeColor = System.Drawing.Color.Green;
            this.textBox1.Location = new System.Drawing.Point(15, 72);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(291, 185);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // windowWidth
            // 
            this.windowWidth.Enabled = false;
            this.windowWidth.Location = new System.Drawing.Point(91, 18);
            this.windowWidth.Name = "windowWidth";
            this.windowWidth.Size = new System.Drawing.Size(42, 21);
            this.windowWidth.TabIndex = 5;
            this.windowWidth.Text = "773";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "窗口大小";
            // 
            // windowHeight
            // 
            this.windowHeight.Enabled = false;
            this.windowHeight.Location = new System.Drawing.Point(156, 18);
            this.windowHeight.Name = "windowHeight";
            this.windowHeight.Size = new System.Drawing.Size(42, 21);
            this.windowHeight.TabIndex = 7;
            this.windowHeight.Text = "466";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "x";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "窗口名称";
            // 
            // windowName
            // 
            this.windowName.Location = new System.Drawing.Point(91, 46);
            this.windowName.Name = "windowName";
            this.windowName.Size = new System.Drawing.Size(107, 21);
            this.windowName.TabIndex = 10;
            this.windowName.Text = "阴阳师-网易游戏";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(229, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "qq:326794739";
            // 
            // Bee_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 263);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.windowName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.windowHeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.windowWidth);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.KaiQiCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Bee_Main";
            this.Text = "yys_胡萝卜吃兔子啦";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Bee_Main_FormClosing);
            this.Load += new System.EventHandler(this.Bee_Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox KaiQiCheckBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox windowWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox windowHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox windowName;
        private System.Windows.Forms.Label label4;
    }
}

