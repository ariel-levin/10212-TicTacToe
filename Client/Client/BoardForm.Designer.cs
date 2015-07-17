namespace Client
{
    partial class BoardForm
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
            this.boardElementHost = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            // 
            // boardElementHost
            // 
            this.boardElementHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boardElementHost.Location = new System.Drawing.Point(0, 0);
            this.boardElementHost.Name = "boardElementHost";
            this.boardElementHost.Size = new System.Drawing.Size(581, 450);
            this.boardElementHost.TabIndex = 0;
            this.boardElementHost.Text = "boardElementHost";
            this.boardElementHost.Child = null;
            // 
            // BoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 450);
            this.Controls.Add(this.boardElementHost);
            this.Name = "BoardForm";
            this.Text = "BoardForm";
            this.Load += new System.EventHandler(this.BoardForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost boardElementHost;



    }
}