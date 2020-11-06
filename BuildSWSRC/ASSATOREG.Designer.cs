namespace BuildSWSRC
{
    partial class ASSATOREG
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
            this.rtxtInput = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtxtOutput = new System.Windows.Forms.RichTextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnASSALogon = new System.Windows.Forms.Button();
            this.btnPrisionBreak = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxtInput
            // 
            this.rtxtInput.Location = new System.Drawing.Point(6, 20);
            this.rtxtInput.Name = "rtxtInput";
            this.rtxtInput.Size = new System.Drawing.Size(361, 367);
            this.rtxtInput.TabIndex = 0;
            this.rtxtInput.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtxtInput);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 393);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输入";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtxtOutput);
            this.groupBox2.Location = new System.Drawing.Point(399, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(389, 393);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输出";
            // 
            // rtxtOutput
            // 
            this.rtxtOutput.Location = new System.Drawing.Point(6, 20);
            this.rtxtOutput.Name = "rtxtOutput";
            this.rtxtOutput.Size = new System.Drawing.Size(377, 367);
            this.rtxtOutput.TabIndex = 0;
            this.rtxtOutput.Text = "";
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(353, 415);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 3;
            this.btnConvert.Text = "转换";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnASSALogon
            // 
            this.btnASSALogon.Location = new System.Drawing.Point(463, 415);
            this.btnASSALogon.Name = "btnASSALogon";
            this.btnASSALogon.Size = new System.Drawing.Size(75, 23);
            this.btnASSALogon.TabIndex = 4;
            this.btnASSALogon.Text = "ASSAM";
            this.btnASSALogon.UseVisualStyleBackColor = true;
            this.btnASSALogon.Click += new System.EventHandler(this.btnASSALogon_Click);
            // 
            // btnPrisionBreak
            // 
            this.btnPrisionBreak.Location = new System.Drawing.Point(559, 415);
            this.btnPrisionBreak.Name = "btnPrisionBreak";
            this.btnPrisionBreak.Size = new System.Drawing.Size(75, 23);
            this.btnPrisionBreak.TabIndex = 5;
            this.btnPrisionBreak.Text = "越狱";
            this.btnPrisionBreak.UseVisualStyleBackColor = true;
            this.btnPrisionBreak.Click += new System.EventHandler(this.btnPrisionBreak_Click);
            // 
            // ASSATOREG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnPrisionBreak);
            this.Controls.Add(this.btnASSALogon);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ASSATOREG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ASSATOREG";
            this.Load += new System.EventHandler(this.ASSATOREG_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtInput;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtxtOutput;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnASSALogon;
        private System.Windows.Forms.Button btnPrisionBreak;
    }
}