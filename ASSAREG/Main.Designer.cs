namespace ASSAREG
{
    partial class Main
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
            this.btnCWD = new System.Windows.Forms.Button();
            this.btnNewACT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCWD
            // 
            this.btnCWD.Location = new System.Drawing.Point(98, 90);
            this.btnCWD.Name = "btnCWD";
            this.btnCWD.Size = new System.Drawing.Size(133, 23);
            this.btnCWD.TabIndex = 0;
            this.btnCWD.Text = "CHang Pass Word";
            this.btnCWD.UseVisualStyleBackColor = true;
            this.btnCWD.Click += new System.EventHandler(this.btnCWD_Click);
            // 
            // btnNewACT
            // 
            this.btnNewACT.Location = new System.Drawing.Point(98, 201);
            this.btnNewACT.Name = "btnNewACT";
            this.btnNewACT.Size = new System.Drawing.Size(133, 23);
            this.btnNewACT.TabIndex = 1;
            this.btnNewACT.Text = "New Account";
            this.btnNewACT.UseVisualStyleBackColor = true;
            this.btnNewACT.Click += new System.EventHandler(this.btnNewACT_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnNewACT);
            this.Controls.Add(this.btnCWD);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCWD;
        private System.Windows.Forms.Button btnNewACT;
    }
}