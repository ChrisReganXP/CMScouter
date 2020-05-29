namespace ChampMan_Scouter.Controls
{
    partial class UCScouting
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
            this.gbxScouting = new System.Windows.Forms.GroupBox();
            this.lblScoutRoleDebug = new System.Windows.Forms.Label();
            this.lblBestPosition = new System.Windows.Forms.Label();
            this.gbxScouting.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxScouting
            // 
            this.gbxScouting.Controls.Add(this.lblScoutRoleDebug);
            this.gbxScouting.Controls.Add(this.lblBestPosition);
            this.gbxScouting.Location = new System.Drawing.Point(3, 6);
            this.gbxScouting.Name = "gbxScouting";
            this.gbxScouting.Size = new System.Drawing.Size(752, 550);
            this.gbxScouting.TabIndex = 0;
            this.gbxScouting.TabStop = false;
            this.gbxScouting.Text = "Scouting";
            // 
            // lblScoutRoleDebug
            // 
            this.lblScoutRoleDebug.AutoSize = true;
            this.lblScoutRoleDebug.Location = new System.Drawing.Point(20, 64);
            this.lblScoutRoleDebug.Name = "lblScoutRoleDebug";
            this.lblScoutRoleDebug.Size = new System.Drawing.Size(64, 13);
            this.lblScoutRoleDebug.TabIndex = 1;
            this.lblScoutRoleDebug.Text = "Role Debug";
            // 
            // lblBestPosition
            // 
            this.lblBestPosition.AutoSize = true;
            this.lblBestPosition.Location = new System.Drawing.Point(17, 31);
            this.lblBestPosition.Name = "lblBestPosition";
            this.lblBestPosition.Size = new System.Drawing.Size(68, 13);
            this.lblBestPosition.TabIndex = 0;
            this.lblBestPosition.Text = "Best Position";
            // 
            // UCScouting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxScouting);
            this.Name = "UCScouting";
            this.Size = new System.Drawing.Size(758, 566);
            this.gbxScouting.ResumeLayout(false);
            this.gbxScouting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxScouting;
        private System.Windows.Forms.Label lblBestPosition;
        private System.Windows.Forms.Label lblScoutRoleDebug;
    }
}
