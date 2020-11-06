namespace BuildSWSRC
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
            this.btnCreate = new System.Windows.Forms.Button();
            this.rtxtOutPut = new System.Windows.Forms.RichTextBox();
            this.rtbInput = new System.Windows.Forms.RichTextBox();
            this.btnSWSRC = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(217, 238);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "创建";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // rtxtOutPut
            // 
            this.rtxtOutPut.Location = new System.Drawing.Point(14, 166);
            this.rtxtOutPut.Name = "rtxtOutPut";
            this.rtxtOutPut.Size = new System.Drawing.Size(643, 66);
            this.rtxtOutPut.TabIndex = 1;
            this.rtxtOutPut.Text = "";
            // 
            // rtbInput
            // 
            this.rtbInput.Location = new System.Drawing.Point(14, 3);
            this.rtbInput.Name = "rtbInput";
            this.rtbInput.Size = new System.Drawing.Size(643, 157);
            this.rtbInput.TabIndex = 2;
            this.rtbInput.Text = "";
            // 
            // btnSWSRC
            // 
            this.btnSWSRC.Location = new System.Drawing.Point(473, 238);
            this.btnSWSRC.Name = "btnSWSRC";
            this.btnSWSRC.Size = new System.Drawing.Size(103, 23);
            this.btnSWSRC.TabIndex = 3;
            this.btnSWSRC.Text = "生成声望脚本";
            this.btnSWSRC.UseVisualStyleBackColor = true;
            this.btnSWSRC.Click += new System.EventHandler(this.btnSWSRC_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(582, 238);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 4;
            this.btnConvert.Text = "转换创建";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 273);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.btnSWSRC);
            this.Controls.Add(this.rtbInput);
            this.Controls.Add(this.rtxtOutPut);
            this.Controls.Add(this.btnCreate);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.RichTextBox rtxtOutPut;
        private System.Windows.Forms.RichTextBox rtbInput;
        private System.Windows.Forms.Button btnSWSRC;
        private System.Windows.Forms.Button btnConvert;
    }
}

