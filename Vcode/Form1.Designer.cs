namespace Vcode
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
            this.btnVerify = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.btnHTTP = new System.Windows.Forms.Button();
            this.btnGen = new System.Windows.Forms.Button();
            this.txtRst = new System.Windows.Forms.TextBox();
            this.txtCodeCopy = new System.Windows.Forms.TextBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(24, 84);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(75, 23);
            this.btnVerify.TabIndex = 0;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(12, 23);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(100, 21);
            this.txtResult.TabIndex = 1;
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(375, 23);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(100, 50);
            this.picBox.TabIndex = 2;
            this.picBox.TabStop = false;
            // 
            // btnHTTP
            // 
            this.btnHTTP.Location = new System.Drawing.Point(24, 129);
            this.btnHTTP.Name = "btnHTTP";
            this.btnHTTP.Size = new System.Drawing.Size(75, 23);
            this.btnHTTP.TabIndex = 3;
            this.btnHTTP.Text = "HTTP";
            this.btnHTTP.UseVisualStyleBackColor = true;
            this.btnHTTP.Click += new System.EventHandler(this.btnHTTP_Click);
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(105, 84);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(75, 23);
            this.btnGen.TabIndex = 4;
            this.btnGen.Text = "Generate";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // txtRst
            // 
            this.txtRst.Location = new System.Drawing.Point(12, 52);
            this.txtRst.Name = "txtRst";
            this.txtRst.Size = new System.Drawing.Size(100, 21);
            this.txtRst.TabIndex = 5;
            // 
            // txtCodeCopy
            // 
            this.txtCodeCopy.Location = new System.Drawing.Point(118, 23);
            this.txtCodeCopy.Name = "txtCodeCopy";
            this.txtCodeCopy.Size = new System.Drawing.Size(100, 21);
            this.txtCodeCopy.TabIndex = 6;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(118, 52);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(251, 21);
            this.txtPath.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.txtCodeCopy);
            this.Controls.Add(this.txtRst);
            this.Controls.Add(this.btnGen);
            this.Controls.Add(this.btnHTTP);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnVerify);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Button btnHTTP;
        private System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.TextBox txtRst;
        private System.Windows.Forms.TextBox txtCodeCopy;
        private System.Windows.Forms.TextBox txtPath;
    }
}

