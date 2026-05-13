namespace assign3
{
    partial class Form1
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
            this.hmbutton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // hmbutton
            // 
            this.hmbutton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.hmbutton.Location = new System.Drawing.Point(21, 12);
            this.hmbutton.Name = "hmbutton";
            this.hmbutton.Size = new System.Drawing.Size(100, 52);
            this.hmbutton.TabIndex = 0;
            this.hmbutton.Text = "Home";
            this.hmbutton.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(150, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 52);
            this.button1.TabIndex = 1;
            this.button1.Text = "Inventory";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.hmbutton);
            this.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Name = "Form1";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button hmbutton;
        private System.Windows.Forms.Button button1;
    }
}

