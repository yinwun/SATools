namespace ASSAREG
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.btnCreateAcct = new System.Windows.Forms.Button();
            this.btnCreateChar = new System.Windows.Forms.Button();
            this.BtnCopy = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.gpASSA = new System.Windows.Forms.GroupBox();
            this.lstbASSAGroupList = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstbASSAList = new System.Windows.Forms.ListBox();
            this.rtxtOutput = new System.Windows.Forms.RichTextBox();
            this.gpOutput = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.gpASSA.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gpOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNum);
            this.groupBox1.Controls.Add(this.btnCreateAcct);
            this.groupBox1.Controls.Add(this.btnCreateChar);
            this.groupBox1.Controls.Add(this.BtnCopy);
            this.groupBox1.Controls.Add(this.btnGenerate);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(93, 448);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "信息";
            // 
            // txtNum
            // 
            this.txtNum.Location = new System.Drawing.Point(6, 137);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(75, 21);
            this.txtNum.TabIndex = 1;
            this.txtNum.Text = "4";
            // 
            // btnCreateAcct
            // 
            this.btnCreateAcct.Location = new System.Drawing.Point(6, 79);
            this.btnCreateAcct.Name = "btnCreateAcct";
            this.btnCreateAcct.Size = new System.Drawing.Size(75, 23);
            this.btnCreateAcct.TabIndex = 3;
            this.btnCreateAcct.Text = "3.创建账号";
            this.btnCreateAcct.UseVisualStyleBackColor = true;
            this.btnCreateAcct.Click += new System.EventHandler(this.btnCreateAcct_Click);
            // 
            // btnCreateChar
            // 
            this.btnCreateChar.Location = new System.Drawing.Point(6, 108);
            this.btnCreateChar.Name = "btnCreateChar";
            this.btnCreateChar.Size = new System.Drawing.Size(75, 23);
            this.btnCreateChar.TabIndex = 2;
            this.btnCreateChar.Text = "4.创建人物";
            this.btnCreateChar.UseVisualStyleBackColor = true;
            this.btnCreateChar.Click += new System.EventHandler(this.btnCreateChar_Click);
            // 
            // BtnCopy
            // 
            this.BtnCopy.Location = new System.Drawing.Point(6, 49);
            this.BtnCopy.Name = "BtnCopy";
            this.BtnCopy.Size = new System.Drawing.Size(75, 23);
            this.BtnCopy.TabIndex = 1;
            this.BtnCopy.Text = "2.复制文件";
            this.BtnCopy.UseVisualStyleBackColor = true;
            this.BtnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(6, 20);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "1.生成账号";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // gpASSA
            // 
            this.gpASSA.Controls.Add(this.lstbASSAGroupList);
            this.gpASSA.Location = new System.Drawing.Point(111, 12);
            this.gpASSA.Name = "gpASSA";
            this.gpASSA.Size = new System.Drawing.Size(136, 448);
            this.gpASSA.TabIndex = 1;
            this.gpASSA.TabStop = false;
            this.gpASSA.Text = "ASSA分组列表";
            // 
            // lstbASSAGroupList
            // 
            this.lstbASSAGroupList.FormattingEnabled = true;
            this.lstbASSAGroupList.ItemHeight = 12;
            this.lstbASSAGroupList.Location = new System.Drawing.Point(7, 21);
            this.lstbASSAGroupList.Name = "lstbASSAGroupList";
            this.lstbASSAGroupList.Size = new System.Drawing.Size(123, 424);
            this.lstbASSAGroupList.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstbASSAList);
            this.groupBox2.Location = new System.Drawing.Point(253, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(141, 448);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ASSA列表";
            // 
            // lstbASSAList
            // 
            this.lstbASSAList.FormattingEnabled = true;
            this.lstbASSAList.ItemHeight = 12;
            this.lstbASSAList.Location = new System.Drawing.Point(6, 20);
            this.lstbASSAList.Name = "lstbASSAList";
            this.lstbASSAList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstbASSAList.Size = new System.Drawing.Size(123, 424);
            this.lstbASSAList.TabIndex = 1;
            // 
            // rtxtOutput
            // 
            this.rtxtOutput.Location = new System.Drawing.Point(6, 18);
            this.rtxtOutput.Name = "rtxtOutput";
            this.rtxtOutput.Size = new System.Drawing.Size(324, 422);
            this.rtxtOutput.TabIndex = 3;
            this.rtxtOutput.Text = "";
            // 
            // gpOutput
            // 
            this.gpOutput.Controls.Add(this.rtxtOutput);
            this.gpOutput.Location = new System.Drawing.Point(401, 14);
            this.gpOutput.Name = "gpOutput";
            this.gpOutput.Size = new System.Drawing.Size(336, 446);
            this.gpOutput.TabIndex = 4;
            this.gpOutput.TabStop = false;
            this.gpOutput.Text = "输出";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 469);
            this.Controls.Add(this.gpOutput);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gpASSA);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASSA生成账号";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gpASSA.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.gpOutput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button BtnCopy;
        private System.Windows.Forms.Button btnCreateChar;
        private System.Windows.Forms.Button btnCreateAcct;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.GroupBox gpASSA;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstbASSAList;
        private System.Windows.Forms.ListBox lstbASSAGroupList;
        private System.Windows.Forms.RichTextBox rtxtOutput;
        private System.Windows.Forms.GroupBox gpOutput;
    }
}

