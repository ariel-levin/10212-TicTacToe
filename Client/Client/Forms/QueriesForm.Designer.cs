namespace Client
{
    partial class QueriesForm
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
            this.tableElementHost = new System.Windows.Forms.Integration.ElementHost();
            this.cbQuery = new System.Windows.Forms.ComboBox();
            this.lblQuery = new System.Windows.Forms.Label();
            this.lblSubQuery = new System.Windows.Forms.Label();
            this.cbSubQuery = new System.Windows.Forms.ComboBox();
            this.lblDelType = new System.Windows.Forms.Label();
            this.cbDelType = new System.Windows.Forms.ComboBox();
            this.groupDelete = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cbDelay = new System.Windows.Forms.CheckBox();
            this.groupDelete.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableElementHost
            // 
            this.tableElementHost.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableElementHost.Location = new System.Drawing.Point(0, 160);
            this.tableElementHost.Name = "tableElementHost";
            this.tableElementHost.Size = new System.Drawing.Size(646, 377);
            this.tableElementHost.TabIndex = 21;
            this.tableElementHost.Text = "elementHost1";
            this.tableElementHost.Child = null;
            // 
            // cbQuery
            // 
            this.cbQuery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQuery.FormattingEnabled = true;
            this.cbQuery.Items.AddRange(new object[] {
            "1 - Display all Players",
            "2 - Display all Games",
            "3 - Display all championships",
            "4 - Players games",
            "5 - Players championships",
            "6 - Games players",
            "7 - Games advisors",
            "8 - Championship players",
            "9 - Players number of games",
            "10 - City championships"});
            this.cbQuery.Location = new System.Drawing.Point(100, 41);
            this.cbQuery.Name = "cbQuery";
            this.cbQuery.Size = new System.Drawing.Size(205, 21);
            this.cbQuery.TabIndex = 23;
            this.cbQuery.SelectedIndexChanged += new System.EventHandler(this.cbQuery_SelectedIndexChanged);
            // 
            // lblQuery
            // 
            this.lblQuery.AutoSize = true;
            this.lblQuery.Location = new System.Drawing.Point(23, 44);
            this.lblQuery.Name = "lblQuery";
            this.lblQuery.Size = new System.Drawing.Size(71, 13);
            this.lblQuery.TabIndex = 24;
            this.lblQuery.Text = "Select Query:";
            // 
            // lblSubQuery
            // 
            this.lblSubQuery.AutoSize = true;
            this.lblSubQuery.Location = new System.Drawing.Point(37, 99);
            this.lblSubQuery.Name = "lblSubQuery";
            this.lblSubQuery.Size = new System.Drawing.Size(57, 13);
            this.lblSubQuery.TabIndex = 25;
            this.lblSubQuery.Text = "SubQuery:";
            // 
            // cbSubQuery
            // 
            this.cbSubQuery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubQuery.FormattingEnabled = true;
            this.cbSubQuery.Location = new System.Drawing.Point(100, 96);
            this.cbSubQuery.Name = "cbSubQuery";
            this.cbSubQuery.Size = new System.Drawing.Size(205, 21);
            this.cbSubQuery.TabIndex = 26;
            this.cbSubQuery.SelectedIndexChanged += new System.EventHandler(this.cbSubQuery_SelectedIndexChanged);
            // 
            // lblDelType
            // 
            this.lblDelType.AutoSize = true;
            this.lblDelType.Location = new System.Drawing.Point(6, 26);
            this.lblDelType.Name = "lblDelType";
            this.lblDelType.Size = new System.Drawing.Size(34, 13);
            this.lblDelType.TabIndex = 27;
            this.lblDelType.Text = "Type:";
            // 
            // cbDelType
            // 
            this.cbDelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDelType.FormattingEnabled = true;
            this.cbDelType.Items.AddRange(new object[] {
            "Single row",
            "Multi row"});
            this.cbDelType.Location = new System.Drawing.Point(46, 23);
            this.cbDelType.Name = "cbDelType";
            this.cbDelType.Size = new System.Drawing.Size(106, 21);
            this.cbDelType.TabIndex = 28;
            this.cbDelType.SelectedIndexChanged += new System.EventHandler(this.cbDelType_SelectedIndexChanged);
            // 
            // groupDelete
            // 
            this.groupDelete.Controls.Add(this.btnDelete);
            this.groupDelete.Controls.Add(this.lblDelType);
            this.groupDelete.Controls.Add(this.cbDelType);
            this.groupDelete.Location = new System.Drawing.Point(336, 20);
            this.groupDelete.Name = "groupDelete";
            this.groupDelete.Size = new System.Drawing.Size(265, 59);
            this.groupDelete.TabIndex = 29;
            this.groupDelete.TabStop = false;
            this.groupDelete.Text = "Delete";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(171, 23);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 21);
            this.btnDelete.TabIndex = 29;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(416, 96);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(83, 30);
            this.btnUpdate.TabIndex = 30;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(508, 96);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(83, 30);
            this.btnExit.TabIndex = 31;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbDelay
            // 
            this.cbDelay.AutoSize = true;
            this.cbDelay.Location = new System.Drawing.Point(346, 101);
            this.cbDelay.Name = "cbDelay";
            this.cbDelay.Size = new System.Drawing.Size(53, 17);
            this.cbDelay.TabIndex = 32;
            this.cbDelay.Text = "Delay";
            this.cbDelay.UseVisualStyleBackColor = true;
            // 
            // QueriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 537);
            this.Controls.Add(this.cbDelay);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.groupDelete);
            this.Controls.Add(this.cbSubQuery);
            this.Controls.Add(this.lblSubQuery);
            this.Controls.Add(this.lblQuery);
            this.Controls.Add(this.cbQuery);
            this.Controls.Add(this.tableElementHost);
            this.Name = "QueriesForm";
            this.ShowIcon = false;
            this.Text = "Queries";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QueriesForm_FormClosing);
            this.Load += new System.EventHandler(this.QueriesForm_Load);
            this.groupDelete.ResumeLayout(false);
            this.groupDelete.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost tableElementHost;
        private System.Windows.Forms.ComboBox cbQuery;
        private System.Windows.Forms.Label lblQuery;
        private System.Windows.Forms.Label lblSubQuery;
        private System.Windows.Forms.ComboBox cbSubQuery;
        private System.Windows.Forms.Label lblDelType;
        private System.Windows.Forms.ComboBox cbDelType;
        private System.Windows.Forms.GroupBox groupDelete;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox cbDelay;
    }
}