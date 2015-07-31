namespace Client
{
    partial class MainForm
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
            this.btnBoard4 = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRegisterUser = new System.Windows.Forms.Button();
            this.btnNewChamp = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblCurrentPlayer = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnRegToChamp = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBoard4
            // 
            this.btnBoard4.Location = new System.Drawing.Point(121, 161);
            this.btnBoard4.Name = "btnBoard4";
            this.btnBoard4.Size = new System.Drawing.Size(75, 23);
            this.btnBoard4.TabIndex = 0;
            this.btnBoard4.Text = "Board 4x4";
            this.btnBoard4.UseVisualStyleBackColor = true;
            this.btnBoard4.Click += new System.EventHandler(this.btnBoard4_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(556, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip";
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "Help";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutMenuItem.Text = "About";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // btnRegisterUser
            // 
            this.btnRegisterUser.Location = new System.Drawing.Point(275, 120);
            this.btnRegisterUser.Name = "btnRegisterUser";
            this.btnRegisterUser.Size = new System.Drawing.Size(90, 23);
            this.btnRegisterUser.TabIndex = 4;
            this.btnRegisterUser.Text = "Register User";
            this.btnRegisterUser.UseVisualStyleBackColor = true;
            this.btnRegisterUser.Click += new System.EventHandler(this.btnRegisterUser_Click);
            // 
            // btnNewChamp
            // 
            this.btnNewChamp.Location = new System.Drawing.Point(275, 181);
            this.btnNewChamp.Name = "btnNewChamp";
            this.btnNewChamp.Size = new System.Drawing.Size(90, 49);
            this.btnNewChamp.TabIndex = 5;
            this.btnNewChamp.Text = "New Championship";
            this.btnNewChamp.UseVisualStyleBackColor = true;
            this.btnNewChamp.Click += new System.EventHandler(this.btnNewChamp_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(426, 206);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblCurrentPlayer
            // 
            this.lblCurrentPlayer.Location = new System.Drawing.Point(399, 35);
            this.lblCurrentPlayer.Name = "lblCurrentPlayer";
            this.lblCurrentPlayer.Size = new System.Drawing.Size(145, 16);
            this.lblCurrentPlayer.TabIndex = 7;
            this.lblCurrentPlayer.Text = "logged out";
            this.lblCurrentPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnLogout
            // 
            this.btnLogout.Enabled = false;
            this.btnLogout.Location = new System.Drawing.Point(426, 247);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 8;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnRegToChamp
            // 
            this.btnRegToChamp.Location = new System.Drawing.Point(275, 247);
            this.btnRegToChamp.Name = "btnRegToChamp";
            this.btnRegToChamp.Size = new System.Drawing.Size(90, 46);
            this.btnRegToChamp.TabIndex = 9;
            this.btnRegToChamp.Text = "Register to Championship";
            this.btnRegToChamp.UseVisualStyleBackColor = true;
            this.btnRegToChamp.Click += new System.EventHandler(this.btnRegToChamp_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 367);
            this.Controls.Add(this.btnRegToChamp);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblCurrentPlayer);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnNewChamp);
            this.Controls.Add(this.btnRegisterUser);
            this.Controls.Add(this.btnBoard4);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Tic Tac Toe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBoard4;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.Button btnRegisterUser;
        private System.Windows.Forms.Button btnNewChamp;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblCurrentPlayer;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnRegToChamp;
    }
}

