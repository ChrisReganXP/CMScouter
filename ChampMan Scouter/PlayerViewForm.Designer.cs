namespace ChampMan_Scouter
{
    partial class PlayerViewForm
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
            this.ucSetPieces = new ChampMan_Scouter.Controls.UCSetPieces();
            this.ucPhysical = new ChampMan_Scouter.Controls.UCPhysical();
            this.ucMental = new ChampMan_Scouter.Controls.UCMental();
            this.ucTechnical = new ChampMan_Scouter.Controls.UCTechnical();
            this.ucScouting = new ChampMan_Scouter.Controls.UCScouting();
            this.ucPersonalDetails = new ChampMan_Scouter.Controls.UCPersonalDetails();
            this.SuspendLayout();
            // 
            // ucSetPieces
            // 
            this.ucSetPieces.Location = new System.Drawing.Point(745, 13);
            this.ucSetPieces.Name = "ucSetPieces";
            this.ucSetPieces.Size = new System.Drawing.Size(153, 210);
            this.ucSetPieces.TabIndex = 5;
            // 
            // ucPhysical
            // 
            this.ucPhysical.Location = new System.Drawing.Point(593, 13);
            this.ucPhysical.Name = "ucPhysical";
            this.ucPhysical.Size = new System.Drawing.Size(153, 210);
            this.ucPhysical.TabIndex = 4;
            // 
            // ucMental
            // 
            this.ucMental.Location = new System.Drawing.Point(443, 11);
            this.ucMental.Name = "ucMental";
            this.ucMental.Size = new System.Drawing.Size(144, 210);
            this.ucMental.TabIndex = 3;
            // 
            // ucTechnical
            // 
            this.ucTechnical.Location = new System.Drawing.Point(177, 12);
            this.ucTechnical.Name = "ucTechnical";
            this.ucTechnical.Size = new System.Drawing.Size(260, 222);
            this.ucTechnical.TabIndex = 2;
            // 
            // ucScouting
            // 
            this.ucScouting.Location = new System.Drawing.Point(12, 241);
            this.ucScouting.Name = "ucScouting";
            this.ucScouting.Size = new System.Drawing.Size(776, 448);
            this.ucScouting.TabIndex = 1;
            // 
            // ucPersonalDetails
            // 
            this.ucPersonalDetails.Location = new System.Drawing.Point(13, 13);
            this.ucPersonalDetails.Name = "ucPersonalDetails";
            this.ucPersonalDetails.Size = new System.Drawing.Size(158, 172);
            this.ucPersonalDetails.TabIndex = 0;
            // 
            // PlayerViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 701);
            this.Controls.Add(this.ucSetPieces);
            this.Controls.Add(this.ucPhysical);
            this.Controls.Add(this.ucMental);
            this.Controls.Add(this.ucTechnical);
            this.Controls.Add(this.ucScouting);
            this.Controls.Add(this.ucPersonalDetails);
            this.Name = "PlayerViewForm";
            this.Text = "PlayerView";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.UCPersonalDetails ucPersonalDetails;
        private Controls.UCScouting ucScouting;
        private Controls.UCTechnical ucTechnical;
        private Controls.UCMental ucMental;
        private Controls.UCPhysical ucPhysical;
        private Controls.UCSetPieces ucSetPieces;
    }
}