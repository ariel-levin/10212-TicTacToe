namespace Client
{
    partial class Board4Form
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
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // boardElementHost
            // 
            this.boardElementHost.Dock = System.Windows.Forms.DockStyle.Top;
            this.boardElementHost.Location = new System.Drawing.Point(0, 0);
            this.boardElementHost.Margin = new System.Windows.Forms.Padding(2);
            this.boardElementHost.Name = "boardElementHost";
            this.boardElementHost.Size = new System.Drawing.Size(391, 347);
            this.boardElementHost.TabIndex = 0;
            this.boardElementHost.Text = "boardElementHost";
            this.boardElementHost.Child = null;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(290, 373);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 37);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Board4Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 435);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.boardElementHost);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Board4Form";
            this.ShowIcon = false;
            this.Text = "Board 4x4";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Board4Form_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost boardElementHost;
        private System.Windows.Forms.Button btnExit;



    }
}