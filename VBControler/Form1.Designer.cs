namespace VBControler
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
            this.btnRefreh = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvVMS = new System.Windows.Forms.ListView();
            this.btnCloseSelected = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnCloseSelected);
            this.groupBox1.Controls.Add(this.btnRefreh);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1574, 52);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ControlPanel";
            // 
            // btnRefreh
            // 
            this.btnRefreh.Location = new System.Drawing.Point(6, 20);
            this.btnRefreh.Name = "btnRefreh";
            this.btnRefreh.Size = new System.Drawing.Size(75, 23);
            this.btnRefreh.TabIndex = 0;
            this.btnRefreh.Text = "刷新";
            this.btnRefreh.UseVisualStyleBackColor = true;
            this.btnRefreh.Click += new System.EventHandler(this.btnRefreh_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.lvVMS);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.groupBox2.Location = new System.Drawing.Point(12, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(399, 1412);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "VMList";
            // 
            // lvVMS
            // 
            this.lvVMS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvVMS.Location = new System.Drawing.Point(7, 20);
            this.lvVMS.Name = "lvVMS";
            this.lvVMS.Size = new System.Drawing.Size(386, 1386);
            this.lvVMS.TabIndex = 0;
            this.lvVMS.UseCompatibleStateImageBehavior = false;
            this.lvVMS.View = System.Windows.Forms.View.Details;
            // 
            // btnCloseSelected
            // 
            this.btnCloseSelected.Location = new System.Drawing.Point(88, 20);
            this.btnCloseSelected.Name = "btnCloseSelected";
            this.btnCloseSelected.Size = new System.Drawing.Size(75, 23);
            this.btnCloseSelected.TabIndex = 1;
            this.btnCloseSelected.Text = "关闭选中";
            this.btnCloseSelected.UseVisualStyleBackColor = true;
            this.btnCloseSelected.Click += new System.EventHandler(this.btnCloseSelected_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1599, 1495);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VBBox";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRefreh;
        private System.Windows.Forms.ListView lvVMS;
        private System.Windows.Forms.Button btnCloseSelected;
    }
}

