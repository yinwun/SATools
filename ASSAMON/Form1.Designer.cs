namespace ASSAMON
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstccount = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCloseS = new System.Windows.Forms.Button();
            this.btnTimer = new System.Windows.Forms.Button();
            this.btnLoadInfo = new System.Windows.Forms.Button();
            this.btnCloseALL = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.BtnStartALL = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnFlight = new System.Windows.Forms.Button();
            this.btnClearn = new System.Windows.Forms.Button();
            this.btnClickMe = new System.Windows.Forms.Button();
            this.lblver = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstccount);
            this.groupBox1.Location = new System.Drawing.Point(11, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(763, 171);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "帐号";
            // 
            // lstccount
            // 
            this.lstccount.FormattingEnabled = true;
            this.lstccount.ItemHeight = 12;
            this.lstccount.Location = new System.Drawing.Point(6, 20);
            this.lstccount.Name = "lstccount";
            this.lstccount.Size = new System.Drawing.Size(751, 148);
            this.lstccount.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCloseS);
            this.groupBox3.Controls.Add(this.btnTimer);
            this.groupBox3.Controls.Add(this.btnLoadInfo);
            this.groupBox3.Controls.Add(this.btnCloseALL);
            this.groupBox3.Controls.Add(this.btnRefresh);
            this.groupBox3.Controls.Add(this.BtnStartALL);
            this.groupBox3.Controls.Add(this.btnStart);
            this.groupBox3.Location = new System.Drawing.Point(12, 361);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(762, 47);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "操作";
            // 
            // btnCloseS
            // 
            this.btnCloseS.Location = new System.Drawing.Point(436, 18);
            this.btnCloseS.Name = "btnCloseS";
            this.btnCloseS.Size = new System.Drawing.Size(75, 23);
            this.btnCloseS.TabIndex = 8;
            this.btnCloseS.Text = "关闭选中";
            this.btnCloseS.UseVisualStyleBackColor = true;
            this.btnCloseS.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnTimer
            // 
            this.btnTimer.Location = new System.Drawing.Point(193, 18);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Size = new System.Drawing.Size(75, 23);
            this.btnTimer.TabIndex = 7;
            this.btnTimer.Text = "开启Timer";
            this.btnTimer.UseVisualStyleBackColor = true;
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // btnLoadInfo
            // 
            this.btnLoadInfo.Location = new System.Drawing.Point(274, 18);
            this.btnLoadInfo.Name = "btnLoadInfo";
            this.btnLoadInfo.Size = new System.Drawing.Size(75, 23);
            this.btnLoadInfo.TabIndex = 6;
            this.btnLoadInfo.Text = "加载";
            this.btnLoadInfo.UseVisualStyleBackColor = true;
            this.btnLoadInfo.Click += new System.EventHandler(this.btnLoadInfo_Click);
            // 
            // btnCloseALL
            // 
            this.btnCloseALL.Location = new System.Drawing.Point(517, 18);
            this.btnCloseALL.Name = "btnCloseALL";
            this.btnCloseALL.Size = new System.Drawing.Size(75, 23);
            this.btnCloseALL.TabIndex = 5;
            this.btnCloseALL.Text = "关闭所有";
            this.btnCloseALL.UseVisualStyleBackColor = true;
            this.btnCloseALL.Click += new System.EventHandler(this.btnCloseALL_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(355, 18);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // BtnStartALL
            // 
            this.BtnStartALL.Location = new System.Drawing.Point(598, 18);
            this.BtnStartALL.Name = "BtnStartALL";
            this.BtnStartALL.Size = new System.Drawing.Size(75, 23);
            this.BtnStartALL.TabIndex = 1;
            this.BtnStartALL.Text = "启动所有";
            this.BtnStartALL.UseVisualStyleBackColor = true;
            this.BtnStartALL.Click += new System.EventHandler(this.BtnStartALL_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(679, 18);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "启动选中";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblver);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.btnFlight);
            this.groupBox2.Controls.Add(this.btnClearn);
            this.groupBox2.Controls.Add(this.btnClickMe);
            this.groupBox2.Location = new System.Drawing.Point(13, 415);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(755, 45);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "整理";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(354, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 23);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // btnFlight
            // 
            this.btnFlight.Location = new System.Drawing.Point(6, 16);
            this.btnFlight.Name = "btnFlight";
            this.btnFlight.Size = new System.Drawing.Size(75, 23);
            this.btnFlight.TabIndex = 8;
            this.btnFlight.Text = "族战";
            this.btnFlight.UseVisualStyleBackColor = true;
            this.btnFlight.Click += new System.EventHandler(this.btnFlight_Click);
            // 
            // btnClearn
            // 
            this.btnClearn.Location = new System.Drawing.Point(87, 16);
            this.btnClearn.Name = "btnClearn";
            this.btnClearn.Size = new System.Drawing.Size(75, 23);
            this.btnClearn.TabIndex = 9;
            this.btnClearn.Text = "整理";
            this.btnClearn.UseVisualStyleBackColor = true;
            this.btnClearn.Click += new System.EventHandler(this.btnClearn_Click);
            // 
            // btnClickMe
            // 
            this.btnClickMe.Location = new System.Drawing.Point(168, 16);
            this.btnClickMe.Name = "btnClickMe";
            this.btnClickMe.Size = new System.Drawing.Size(75, 23);
            this.btnClickMe.TabIndex = 4;
            this.btnClickMe.Text = "CLick Me";
            this.btnClickMe.UseVisualStyleBackColor = true;
            this.btnClickMe.Click += new System.EventHandler(this.btnClickMe_Click);
            // 
            // lblver
            // 
            this.lblver.AutoSize = true;
            this.lblver.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblver.Location = new System.Drawing.Point(627, 16);
            this.lblver.Name = "lblver";
            this.lblver.Size = new System.Drawing.Size(45, 20);
            this.lblver.TabIndex = 11;
            this.lblver.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 472);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ASSAMON";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox lstccount;
        private System.Windows.Forms.Button BtnStartALL;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCloseALL;
        private System.Windows.Forms.Button btnLoadInfo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnTimer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnFlight;
        private System.Windows.Forms.Button btnClearn;
        private System.Windows.Forms.Button btnClickMe;
        private System.Windows.Forms.Button btnCloseS;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblver;
    }
}

