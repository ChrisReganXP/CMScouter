namespace ChampMan_Scouter.Controls
{
    partial class UCPersonalDetails
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbxPersonal = new System.Windows.Forms.GroupBox();
            this.lblDescribedPosition = new System.Windows.Forms.Label();
            this.lblSecondNationality = new System.Windows.Forms.Label();
            this.lblNationality = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblAlternateName = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblCA = new System.Windows.Forms.Label();
            this.lblCAVal = new System.Windows.Forms.Label();
            this.lblPA = new System.Windows.Forms.Label();
            this.lblPAVal = new System.Windows.Forms.Label();
            this.gbxPersonal.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxPersonal
            // 
            this.gbxPersonal.Controls.Add(this.lblPAVal);
            this.gbxPersonal.Controls.Add(this.lblPA);
            this.gbxPersonal.Controls.Add(this.lblCAVal);
            this.gbxPersonal.Controls.Add(this.lblCA);
            this.gbxPersonal.Controls.Add(this.lblDescribedPosition);
            this.gbxPersonal.Controls.Add(this.lblSecondNationality);
            this.gbxPersonal.Controls.Add(this.lblNationality);
            this.gbxPersonal.Controls.Add(this.lblAge);
            this.gbxPersonal.Controls.Add(this.lblAlternateName);
            this.gbxPersonal.Controls.Add(this.lblFullName);
            this.gbxPersonal.Location = new System.Drawing.Point(4, 3);
            this.gbxPersonal.Name = "gbxPersonal";
            this.gbxPersonal.Size = new System.Drawing.Size(147, 166);
            this.gbxPersonal.TabIndex = 0;
            this.gbxPersonal.TabStop = false;
            this.gbxPersonal.Text = "Personal";
            // 
            // lblDescribedPosition
            // 
            this.lblDescribedPosition.AutoSize = true;
            this.lblDescribedPosition.Location = new System.Drawing.Point(4, 104);
            this.lblDescribedPosition.Name = "lblDescribedPosition";
            this.lblDescribedPosition.Size = new System.Drawing.Size(58, 13);
            this.lblDescribedPosition.TabIndex = 5;
            this.lblDescribedPosition.Text = "AM/F RLC";
            // 
            // lblSecondNationality
            // 
            this.lblSecondNationality.AutoSize = true;
            this.lblSecondNationality.Location = new System.Drawing.Point(6, 79);
            this.lblSecondNationality.Name = "lblSecondNationality";
            this.lblSecondNationality.Size = new System.Drawing.Size(84, 13);
            this.lblSecondNationality.TabIndex = 4;
            this.lblSecondNationality.Text = "(Second Nation)";
            // 
            // lblNationality
            // 
            this.lblNationality.AutoSize = true;
            this.lblNationality.Location = new System.Drawing.Point(4, 66);
            this.lblNationality.Name = "lblNationality";
            this.lblNationality.Size = new System.Drawing.Size(56, 13);
            this.lblNationality.TabIndex = 3;
            this.lblNationality.Text = "Nationality";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(68, 104);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(75, 13);
            this.lblAge.TabIndex = 2;
            this.lblAge.Text = "Age Years Old";
            // 
            // lblAlternateName
            // 
            this.lblAlternateName.AutoSize = true;
            this.lblAlternateName.Location = new System.Drawing.Point(6, 40);
            this.lblAlternateName.Name = "lblAlternateName";
            this.lblAlternateName.Size = new System.Drawing.Size(86, 13);
            this.lblAlternateName.TabIndex = 1;
            this.lblAlternateName.Text = "(Alternate Name)";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(6, 27);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(117, 13);
            this.lblFullName.TabIndex = 0;
            this.lblFullName.Text = "Full Name or Known As";
            // 
            // lblCA
            // 
            this.lblCA.AutoSize = true;
            this.lblCA.Location = new System.Drawing.Point(7, 130);
            this.lblCA.Name = "lblCA";
            this.lblCA.Size = new System.Drawing.Size(21, 13);
            this.lblCA.TabIndex = 6;
            this.lblCA.Text = "CA";
            // 
            // lblCAVal
            // 
            this.lblCAVal.AutoSize = true;
            this.lblCAVal.Location = new System.Drawing.Point(32, 130);
            this.lblCAVal.Name = "lblCAVal";
            this.lblCAVal.Size = new System.Drawing.Size(25, 13);
            this.lblCAVal.TabIndex = 7;
            this.lblCAVal.Text = "200";
            // 
            // lblPA
            // 
            this.lblPA.AutoSize = true;
            this.lblPA.Location = new System.Drawing.Point(74, 130);
            this.lblPA.Name = "lblPA";
            this.lblPA.Size = new System.Drawing.Size(21, 13);
            this.lblPA.TabIndex = 8;
            this.lblPA.Text = "PA";
            // 
            // lblPAVal
            // 
            this.lblPAVal.AutoSize = true;
            this.lblPAVal.Location = new System.Drawing.Point(99, 130);
            this.lblPAVal.Name = "lblPAVal";
            this.lblPAVal.Size = new System.Drawing.Size(25, 13);
            this.lblPAVal.TabIndex = 9;
            this.lblPAVal.Text = "200";
            // 
            // UCPersonalDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxPersonal);
            this.Name = "UCPersonalDetails";
            this.Size = new System.Drawing.Size(157, 172);
            this.gbxPersonal.ResumeLayout(false);
            this.gbxPersonal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxPersonal;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblAlternateName;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblSecondNationality;
        private System.Windows.Forms.Label lblNationality;
        private System.Windows.Forms.Label lblDescribedPosition;
        private System.Windows.Forms.Label lblPA;
        private System.Windows.Forms.Label lblCAVal;
        private System.Windows.Forms.Label lblCA;
        private System.Windows.Forms.Label lblPAVal;
    }
}
