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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.windowWidth = new System.Windows.Forms.TextBox();
            this.windowHeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.windowName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.IsSoftwareSimulation = new System.Windows.Forms.CheckBox();
            this.templates = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.wins = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.ForeColor = System.Drawing.Color.Lime;
            this.textBox1.Location = new System.Drawing.Point(12, 152);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(294, 131);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // windowWidth
            // 
            this.windowWidth.Location = new System.Drawing.Point(196, 39);
            this.windowWidth.Name = "windowWidth";
            this.windowWidth.ReadOnly = true;
            this.windowWidth.Size = new System.Drawing.Size(42, 21);
            this.windowWidth.TabIndex = 5;
            // 
            // windowHeight
            // 
            this.windowHeight.Location = new System.Drawing.Point(261, 38);
            this.windowHeight.Name = "windowHeight";
            this.windowHeight.ReadOnly = true;
            this.windowHeight.Size = new System.Drawing.Size(42, 21);
            this.windowHeight.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "x";
            // 
            // windowName
            // 
            this.windowName.Location = new System.Drawing.Point(12, 39);
            this.windowName.Name = "windowName";
            this.windowName.ReadOnly = true;
            this.windowName.Size = new System.Drawing.Size(147, 21);
            this.windowName.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(10, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "欢迎加入本寮：咖啡";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(196, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "截图";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // IsSoftwareSimulation
            // 
            this.IsSoftwareSimulation.AutoSize = true;
            this.IsSoftwareSimulation.Location = new System.Drawing.Point(12, 98);
            this.IsSoftwareSimulation.Name = "IsSoftwareSimulation";
            this.IsSoftwareSimulation.Size = new System.Drawing.Size(96, 16);
            this.IsSoftwareSimulation.TabIndex = 13;
            this.IsSoftwareSimulation.Text = "软件模拟鼠标";
            this.IsSoftwareSimulation.UseVisualStyleBackColor = true;
            this.IsSoftwareSimulation.CheckedChanged += new System.EventHandler(this.IsSoftwareSimulation_CheckedChanged);
            // 
            // templates
            // 
            this.templates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.templates.FormattingEnabled = true;
            this.templates.Location = new System.Drawing.Point(12, 12);
            this.templates.Name = "templates";
            this.templates.Size = new System.Drawing.Size(293, 20);
            this.templates.TabIndex = 14;
            this.templates.SelectedIndexChanged += new System.EventHandler(this.templates_SelectedIndexChanged_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(198, 123);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "开启辅助";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // wins
            // 
            this.wins.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wins.FormattingEnabled = true;
            this.wins.Location = new System.Drawing.Point(13, 67);
            this.wins.Name = "wins";
            this.wins.Size = new System.Drawing.Size(293, 20);
            this.wins.TabIndex = 16;
            // 
            // Bee_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 294);
            this.Controls.Add(this.wins);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.templates);
            this.Controls.Add(this.IsSoftwareSimulation);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.windowName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.windowHeight);
            this.Controls.Add(this.windowWidth);
            this.Controls.Add(this.textBox1);
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox windowWidth;
        private System.Windows.Forms.TextBox windowHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox windowName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox IsSoftwareSimulation;
        private System.Windows.Forms.ComboBox templates;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox wins;
    }
}

