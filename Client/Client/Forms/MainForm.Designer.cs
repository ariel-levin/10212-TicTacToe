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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.championshipMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newChampMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerChampMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.online3MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.online4MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.online5MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offlineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offline3MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offline4MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offline5MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queriesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblCurrentPlayer = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.userMenuItem,
            this.championshipMenuItem,
            this.gameMenuItem,
            this.queriesMenuItem,
            this.helpMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(556, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "File";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // userMenuItem
            // 
            this.userMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginMenuItem,
            this.registerMenuItem,
            this.logoutMenuItem});
            this.userMenuItem.Name = "userMenuItem";
            this.userMenuItem.Size = new System.Drawing.Size(42, 20);
            this.userMenuItem.Text = "User";
            // 
            // loginMenuItem
            // 
            this.loginMenuItem.Name = "loginMenuItem";
            this.loginMenuItem.Size = new System.Drawing.Size(116, 22);
            this.loginMenuItem.Text = "Login";
            this.loginMenuItem.Click += new System.EventHandler(this.loginMenuItem_Click);
            // 
            // registerMenuItem
            // 
            this.registerMenuItem.Name = "registerMenuItem";
            this.registerMenuItem.Size = new System.Drawing.Size(116, 22);
            this.registerMenuItem.Text = "Register";
            this.registerMenuItem.Click += new System.EventHandler(this.registerMenuItem_Click);
            // 
            // logoutMenuItem
            // 
            this.logoutMenuItem.Name = "logoutMenuItem";
            this.logoutMenuItem.Size = new System.Drawing.Size(116, 22);
            this.logoutMenuItem.Text = "Logout";
            this.logoutMenuItem.Click += new System.EventHandler(this.logoutMenuItem_Click);
            // 
            // championshipMenuItem
            // 
            this.championshipMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newChampMenuItem,
            this.registerChampMenuItem});
            this.championshipMenuItem.Name = "championshipMenuItem";
            this.championshipMenuItem.Size = new System.Drawing.Size(97, 20);
            this.championshipMenuItem.Text = "Championship";
            // 
            // newChampMenuItem
            // 
            this.newChampMenuItem.Name = "newChampMenuItem";
            this.newChampMenuItem.Size = new System.Drawing.Size(211, 22);
            this.newChampMenuItem.Text = "New Championship";
            this.newChampMenuItem.Click += new System.EventHandler(this.newChampMenuItem_Click);
            // 
            // registerChampMenuItem
            // 
            this.registerChampMenuItem.Name = "registerChampMenuItem";
            this.registerChampMenuItem.Size = new System.Drawing.Size(211, 22);
            this.registerChampMenuItem.Text = "Register to Championship";
            this.registerChampMenuItem.Click += new System.EventHandler(this.registerChampMenuItem_Click);
            // 
            // gameMenuItem
            // 
            this.gameMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineMenuItem,
            this.offlineMenuItem,
            this.historyMenuItem});
            this.gameMenuItem.Name = "gameMenuItem";
            this.gameMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameMenuItem.Text = "Game";
            // 
            // onlineMenuItem
            // 
            this.onlineMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.online3MenuItem,
            this.online4MenuItem,
            this.online5MenuItem});
            this.onlineMenuItem.Name = "onlineMenuItem";
            this.onlineMenuItem.Size = new System.Drawing.Size(112, 22);
            this.onlineMenuItem.Text = "Online";
            // 
            // online3MenuItem
            // 
            this.online3MenuItem.Name = "online3MenuItem";
            this.online3MenuItem.Size = new System.Drawing.Size(91, 22);
            this.online3MenuItem.Text = "3x3";
            this.online3MenuItem.Click += new System.EventHandler(this.online3MenuItem_Click);
            // 
            // online4MenuItem
            // 
            this.online4MenuItem.Name = "online4MenuItem";
            this.online4MenuItem.Size = new System.Drawing.Size(91, 22);
            this.online4MenuItem.Text = "4x4";
            this.online4MenuItem.Click += new System.EventHandler(this.online4MenuItem_Click);
            // 
            // online5MenuItem
            // 
            this.online5MenuItem.Name = "online5MenuItem";
            this.online5MenuItem.Size = new System.Drawing.Size(91, 22);
            this.online5MenuItem.Text = "5x5";
            this.online5MenuItem.Click += new System.EventHandler(this.online5MenuItem_Click);
            // 
            // offlineMenuItem
            // 
            this.offlineMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.offline3MenuItem,
            this.offline4MenuItem,
            this.offline5MenuItem});
            this.offlineMenuItem.Name = "offlineMenuItem";
            this.offlineMenuItem.Size = new System.Drawing.Size(112, 22);
            this.offlineMenuItem.Text = "Offline";
            // 
            // offline3MenuItem
            // 
            this.offline3MenuItem.Name = "offline3MenuItem";
            this.offline3MenuItem.Size = new System.Drawing.Size(91, 22);
            this.offline3MenuItem.Text = "3x3";
            this.offline3MenuItem.Click += new System.EventHandler(this.offline3MenuItem_Click);
            // 
            // offline4MenuItem
            // 
            this.offline4MenuItem.Name = "offline4MenuItem";
            this.offline4MenuItem.Size = new System.Drawing.Size(91, 22);
            this.offline4MenuItem.Text = "4x4";
            this.offline4MenuItem.Click += new System.EventHandler(this.offline4MenuItem_Click);
            // 
            // offline5MenuItem
            // 
            this.offline5MenuItem.Name = "offline5MenuItem";
            this.offline5MenuItem.Size = new System.Drawing.Size(91, 22);
            this.offline5MenuItem.Text = "5x5";
            this.offline5MenuItem.Click += new System.EventHandler(this.offline5MenuItem_Click);
            // 
            // historyMenuItem
            // 
            this.historyMenuItem.Name = "historyMenuItem";
            this.historyMenuItem.Size = new System.Drawing.Size(112, 22);
            this.historyMenuItem.Text = "History";
            this.historyMenuItem.Click += new System.EventHandler(this.historyMenuItem_Click);
            // 
            // queriesMenuItem
            // 
            this.queriesMenuItem.Name = "queriesMenuItem";
            this.queriesMenuItem.Size = new System.Drawing.Size(59, 20);
            this.queriesMenuItem.Text = "Queries";
            this.queriesMenuItem.Click += new System.EventHandler(this.queriesMenuItem_Click);
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
            // lblCurrentPlayer
            // 
            this.lblCurrentPlayer.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPlayer.Location = new System.Drawing.Point(363, 33);
            this.lblCurrentPlayer.Name = "lblCurrentPlayer";
            this.lblCurrentPlayer.Size = new System.Drawing.Size(181, 26);
            this.lblCurrentPlayer.TabIndex = 7;
            this.lblCurrentPlayer.Text = "logged out";
            this.lblCurrentPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 367);
            this.Controls.Add(this.lblCurrentPlayer);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Tic Tac Toe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.Label lblCurrentPlayer;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem championshipMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newChampMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerChampMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineMenuItem;
        private System.Windows.Forms.ToolStripMenuItem online3MenuItem;
        private System.Windows.Forms.ToolStripMenuItem online4MenuItem;
        private System.Windows.Forms.ToolStripMenuItem online5MenuItem;
        private System.Windows.Forms.ToolStripMenuItem offlineMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offline3MenuItem;
        private System.Windows.Forms.ToolStripMenuItem offline4MenuItem;
        private System.Windows.Forms.ToolStripMenuItem offline5MenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queriesMenuItem;
    }
}

