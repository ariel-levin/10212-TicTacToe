namespace Client
{
    partial class NewChampForm
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
            this.lblCity = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.tbCity = new System.Windows.Forms.TextBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblHasEnded = new System.Windows.Forms.Label();
            this.cbHasEnded = new System.Windows.Forms.CheckBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblPicture = new System.Windows.Forms.Label();
            this.btnPicture = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblPicturePath = new System.Windows.Forms.Label();
            this.cbPicture = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(22, 25);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(27, 13);
            this.lblCity.TabIndex = 0;
            this.lblCity.Text = "City:";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(19, 69);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(58, 13);
            this.lblStartDate.TabIndex = 1;
            this.lblStartDate.Text = "Start Date:";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(19, 147);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(55, 13);
            this.lblEndDate.TabIndex = 2;
            this.lblEndDate.Text = "End Date:";
            // 
            // tbCity
            // 
            this.tbCity.Location = new System.Drawing.Point(55, 22);
            this.tbCity.Name = "tbCity";
            this.tbCity.Size = new System.Drawing.Size(228, 20);
            this.tbCity.TabIndex = 3;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(83, 63);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 4;
            // 
            // lblHasEnded
            // 
            this.lblHasEnded.AutoSize = true;
            this.lblHasEnded.Location = new System.Drawing.Point(19, 107);
            this.lblHasEnded.Name = "lblHasEnded";
            this.lblHasEnded.Size = new System.Drawing.Size(66, 13);
            this.lblHasEnded.TabIndex = 5;
            this.lblHasEnded.Text = "Has Ended?";
            // 
            // cbHasEnded
            // 
            this.cbHasEnded.AutoSize = true;
            this.cbHasEnded.Location = new System.Drawing.Point(91, 106);
            this.cbHasEnded.Name = "cbHasEnded";
            this.cbHasEnded.Size = new System.Drawing.Size(15, 14);
            this.cbHasEnded.TabIndex = 6;
            this.cbHasEnded.UseVisualStyleBackColor = true;
            this.cbHasEnded.CheckedChanged += new System.EventHandler(this.cbHasEnded_CheckedChanged);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Enabled = false;
            this.dtpEndDate.Location = new System.Drawing.Point(83, 141);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpEndDate.TabIndex = 7;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblPicture
            // 
            this.lblPicture.AutoSize = true;
            this.lblPicture.Location = new System.Drawing.Point(19, 187);
            this.lblPicture.Name = "lblPicture";
            this.lblPicture.Size = new System.Drawing.Size(43, 13);
            this.lblPicture.TabIndex = 8;
            this.lblPicture.Text = "Picture:";
            // 
            // btnPicture
            // 
            this.btnPicture.Enabled = false;
            this.btnPicture.Location = new System.Drawing.Point(22, 218);
            this.btnPicture.Name = "btnPicture";
            this.btnPicture.Size = new System.Drawing.Size(75, 23);
            this.btnPicture.TabIndex = 9;
            this.btnPicture.Text = "Browse";
            this.btnPicture.UseVisualStyleBackColor = true;
            this.btnPicture.Click += new System.EventHandler(this.btnPicture_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(22, 274);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(117, 36);
            this.btnSubmit.TabIndex = 10;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(166, 274);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(117, 36);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblPicturePath
            // 
            this.lblPicturePath.Location = new System.Drawing.Point(103, 223);
            this.lblPicturePath.Name = "lblPicturePath";
            this.lblPicturePath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPicturePath.Size = new System.Drawing.Size(180, 18);
            this.lblPicturePath.TabIndex = 12;
            this.lblPicturePath.Text = "...";
            // 
            // cbPicture
            // 
            this.cbPicture.AutoSize = true;
            this.cbPicture.Location = new System.Drawing.Point(69, 187);
            this.cbPicture.Name = "cbPicture";
            this.cbPicture.Size = new System.Drawing.Size(15, 14);
            this.cbPicture.TabIndex = 13;
            this.cbPicture.UseVisualStyleBackColor = true;
            this.cbPicture.CheckedChanged += new System.EventHandler(this.cbPicture_CheckedChanged);
            // 
            // NewChampForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 333);
            this.Controls.Add(this.cbPicture);
            this.Controls.Add(this.lblPicturePath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnPicture);
            this.Controls.Add(this.lblPicture);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.cbHasEnded);
            this.Controls.Add(this.lblHasEnded);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.tbCity);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.lblCity);
            this.Name = "NewChampForm";
            this.ShowIcon = false;
            this.Text = "New Championship";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewChampForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.TextBox tbCity;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblHasEnded;
        private System.Windows.Forms.CheckBox cbHasEnded;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblPicture;
        private System.Windows.Forms.Button btnPicture;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblPicturePath;
        private System.Windows.Forms.CheckBox cbPicture;
    }
}