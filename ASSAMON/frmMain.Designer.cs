
namespace ASSAMON
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstccount = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSW = new System.Windows.Forms.Button();
            this.btnStartSelected = new System.Windows.Forms.Button();
            this.btnCloseAll = new System.Windows.Forms.Button();
            this.btnCloseSelected = new System.Windows.Forms.Button();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.btnTimer = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslblVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSSlblTimer = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlsddbStartUpType = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnTest = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstccount);
            this.groupBox1.Location = new System.Drawing.Point(3, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 96);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account";
            // 
            // lstccount
            // 
            this.lstccount.FormattingEnabled = true;
            this.lstccount.ItemHeight = 12;
            this.lstccount.Location = new System.Drawing.Point(6, 12);
            this.lstccount.Name = "lstccount";
            this.lstccount.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstccount.Size = new System.Drawing.Size(288, 76);
            this.lstccount.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSW);
            this.groupBox2.Controls.Add(this.btnStartSelected);
            this.groupBox2.Controls.Add(this.btnCloseAll);
            this.groupBox2.Controls.Add(this.btnCloseSelected);
            this.groupBox2.Controls.Add(this.btnLoadData);
            this.groupBox2.Controls.Add(this.btnTimer);
            this.groupBox2.Location = new System.Drawing.Point(310, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(462, 96);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作";
            // 
            // btnSW
            // 
            this.btnSW.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSW.Location = new System.Drawing.Point(179, 20);
            this.btnSW.Name = "btnSW";
            this.btnSW.Size = new System.Drawing.Size(75, 23);
            this.btnSW.TabIndex = 5;
            this.btnSW.Text = "声望挂机";
            this.btnSW.UseVisualStyleBackColor = true;
            this.btnSW.Click += new System.EventHandler(this.btnSW_Click);
            // 
            // btnStartSelected
            // 
            this.btnStartSelected.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStartSelected.Location = new System.Drawing.Point(179, 59);
            this.btnStartSelected.Name = "btnStartSelected";
            this.btnStartSelected.Size = new System.Drawing.Size(75, 23);
            this.btnStartSelected.TabIndex = 4;
            this.btnStartSelected.Text = "5次挂机";
            this.btnStartSelected.UseVisualStyleBackColor = true;
            this.btnStartSelected.Click += new System.EventHandler(this.btnStartSelected_Click);
            // 
            // btnCloseAll
            // 
            this.btnCloseAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCloseAll.Location = new System.Drawing.Point(95, 59);
            this.btnCloseAll.Name = "btnCloseAll";
            this.btnCloseAll.Size = new System.Drawing.Size(75, 23);
            this.btnCloseAll.TabIndex = 3;
            this.btnCloseAll.Text = "关闭所有";
            this.btnCloseAll.UseVisualStyleBackColor = true;
            this.btnCloseAll.Click += new System.EventHandler(this.btnCloseAll_Click);
            // 
            // btnCloseSelected
            // 
            this.btnCloseSelected.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCloseSelected.Location = new System.Drawing.Point(95, 21);
            this.btnCloseSelected.Name = "btnCloseSelected";
            this.btnCloseSelected.Size = new System.Drawing.Size(75, 23);
            this.btnCloseSelected.TabIndex = 2;
            this.btnCloseSelected.Text = "关闭选中";
            this.btnCloseSelected.UseVisualStyleBackColor = true;
            this.btnCloseSelected.Click += new System.EventHandler(this.btnCloseSelected_Click);
            // 
            // btnLoadData
            // 
            this.btnLoadData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLoadData.Location = new System.Drawing.Point(12, 59);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(75, 23);
            this.btnLoadData.TabIndex = 1;
            this.btnLoadData.Text = "加载数据";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // btnTimer
            // 
            this.btnTimer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTimer.Location = new System.Drawing.Point(12, 22);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Size = new System.Drawing.Size(75, 23);
            this.btnTimer.TabIndex = 0;
            this.btnTimer.Text = "开启Timeer";
            this.btnTimer.UseVisualStyleBackColor = true;
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslblVersion,
            this.tSSlblTimer,
            this.tlsddbStartUpType});
            this.statusStrip1.Location = new System.Drawing.Point(0, 301);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslblVersion
            // 
            this.tsslblVersion.Name = "tsslblVersion";
            this.tsslblVersion.Size = new System.Drawing.Size(0, 17);
            // 
            // tSSlblTimer
            // 
            this.tSSlblTimer.Name = "tSSlblTimer";
            this.tSSlblTimer.Size = new System.Drawing.Size(0, 17);
            // 
            // tlsddbStartUpType
            // 
            this.tlsddbStartUpType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlsddbStartUpType.Image = ((System.Drawing.Image)(resources.GetObject("tlsddbStartUpType.Image")));
            this.tlsddbStartUpType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsddbStartUpType.Name = "tlsddbStartUpType";
            this.tlsddbStartUpType.Size = new System.Drawing.Size(13, 20);
            this.tlsddbStartUpType.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tlsddbStartUpType_DropDownItemClicked);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(697, 275);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 5;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 323);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMain";
            this.Text = "Stonage Main";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstccount;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTimer;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Button btnCloseSelected;
        private System.Windows.Forms.Button btnCloseAll;
        private System.Windows.Forms.Button btnStartSelected;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslblVersion;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnSW;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel tSSlblTimer;
        private System.Windows.Forms.ToolStripDropDownButton tlsddbStartUpType;
    }
}