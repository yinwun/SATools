namespace BuildSWSRC
{
    partial class SWSRCGenrate
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.rtbInput = new System.Windows.Forms.RichTextBox();
            this.gbASSA = new System.Windows.Forms.GroupBox();
            this.rtbASSA = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbXZ = new System.Windows.Forms.RichTextBox();
            this.gbASSA.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(553, 782);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "生成";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // rtbInput
            // 
            this.rtbInput.Location = new System.Drawing.Point(12, 634);
            this.rtbInput.Name = "rtbInput";
            this.rtbInput.Size = new System.Drawing.Size(1134, 141);
            this.rtbInput.TabIndex = 1;
            this.rtbInput.Text = "";
            // 
            // gbASSA
            // 
            this.gbASSA.Controls.Add(this.rtbASSA);
            this.gbASSA.Location = new System.Drawing.Point(12, 12);
            this.gbASSA.Name = "gbASSA";
            this.gbASSA.Size = new System.Drawing.Size(560, 616);
            this.gbASSA.TabIndex = 2;
            this.gbASSA.TabStop = false;
            this.gbASSA.Text = "ASSA";
            // 
            // rtbASSA
            // 
            this.rtbASSA.Location = new System.Drawing.Point(6, 20);
            this.rtbASSA.Name = "rtbASSA";
            this.rtbASSA.Size = new System.Drawing.Size(548, 590);
            this.rtbASSA.TabIndex = 0;
            this.rtbASSA.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtbXZ);
            this.groupBox2.Location = new System.Drawing.Point(586, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(560, 616);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "XZ";
            // 
            // rtbXZ
            // 
            this.rtbXZ.Location = new System.Drawing.Point(6, 20);
            this.rtbXZ.Name = "rtbXZ";
            this.rtbXZ.Size = new System.Drawing.Size(548, 590);
            this.rtbXZ.TabIndex = 1;
            this.rtbXZ.Text = "";
            // 
            // SWSRCGenrate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 816);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbASSA);
            this.Controls.Add(this.rtbInput);
            this.Controls.Add(this.btnGenerate);
            this.Name = "SWSRCGenrate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SWSRCGenrate";
            this.gbASSA.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.RichTextBox rtbInput;
        private System.Windows.Forms.GroupBox gbASSA;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtbASSA;
        private System.Windows.Forms.RichTextBox rtbXZ;
    }
}