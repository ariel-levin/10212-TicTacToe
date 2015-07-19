namespace Client
{
    partial class RegisterForm
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
            this.clbAdvisors = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // clbAdvisors
            // 
            this.clbAdvisors.FormattingEnabled = true;
            this.clbAdvisors.Location = new System.Drawing.Point(39, 266);
            this.clbAdvisors.Name = "clbAdvisors";
            this.clbAdvisors.Size = new System.Drawing.Size(282, 124);
            this.clbAdvisors.TabIndex = 0;
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 524);
            this.Controls.Add(this.clbAdvisors);
            this.Name = "RegisterForm";
            this.Text = "RegisterForm";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbAdvisors;
    }
}