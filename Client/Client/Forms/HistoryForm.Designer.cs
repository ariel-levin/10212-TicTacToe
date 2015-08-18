namespace Client
{
    partial class HistoryForm
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
            this.cbGames = new System.Windows.Forms.ComboBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblSelect = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbGames
            // 
            this.cbGames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGames.FormattingEnabled = true;
            this.cbGames.Location = new System.Drawing.Point(29, 41);
            this.cbGames.Name = "cbGames";
            this.cbGames.Size = new System.Drawing.Size(405, 21);
            this.cbGames.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(301, 98);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(133, 31);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Location = new System.Drawing.Point(26, 25);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(78, 13);
            this.lblSelect.TabIndex = 2;
            this.lblSelect.Text = "Select a game:";
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(29, 98);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(133, 31);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "Show History";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // HistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 154);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lblSelect);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.cbGames);
            this.Name = "HistoryForm";
            this.ShowIcon = false;
            this.Text = "Game History";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HistoryForm_FormClosing);
            this.Load += new System.EventHandler(this.HistoryForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbGames;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.Button btnSelect;
    }
}