namespace ChampMan_Scouter.Controls
{
    partial class UCSetPieces
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
            this.lblCorners = new System.Windows.Forms.Label();
            this.lblFreeKicks = new System.Windows.Forms.Label();
            this.lblPenalties = new System.Windows.Forms.Label();
            this.lblThrowIns = new System.Windows.Forms.Label();
            this.lblThrVal = new System.Windows.Forms.Label();
            this.lblPenVal = new System.Windows.Forms.Label();
            this.lblFreeVal = new System.Windows.Forms.Label();
            this.lblCorVal = new System.Windows.Forms.Label();
            this.gbxSetPieces = new System.Windows.Forms.GroupBox();
            this.gbxSetPieces.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCorners
            // 
            this.lblCorners.AutoSize = true;
            this.lblCorners.Location = new System.Drawing.Point(15, 27);
            this.lblCorners.Name = "lblCorners";
            this.lblCorners.Size = new System.Drawing.Size(43, 13);
            this.lblCorners.TabIndex = 0;
            this.lblCorners.Text = "Corners";
            // 
            // lblFreeKicks
            // 
            this.lblFreeKicks.AutoSize = true;
            this.lblFreeKicks.Location = new System.Drawing.Point(15, 50);
            this.lblFreeKicks.Name = "lblFreeKicks";
            this.lblFreeKicks.Size = new System.Drawing.Size(57, 13);
            this.lblFreeKicks.TabIndex = 1;
            this.lblFreeKicks.Text = "Free Kicks";
            // 
            // lblPenalties
            // 
            this.lblPenalties.AutoSize = true;
            this.lblPenalties.Location = new System.Drawing.Point(15, 72);
            this.lblPenalties.Name = "lblPenalties";
            this.lblPenalties.Size = new System.Drawing.Size(50, 13);
            this.lblPenalties.TabIndex = 2;
            this.lblPenalties.Text = "Penalties";
            // 
            // lblThrowIns
            // 
            this.lblThrowIns.AutoSize = true;
            this.lblThrowIns.Location = new System.Drawing.Point(15, 96);
            this.lblThrowIns.Name = "lblThrowIns";
            this.lblThrowIns.Size = new System.Drawing.Size(60, 13);
            this.lblThrowIns.TabIndex = 3;
            this.lblThrowIns.Text = "Thrown Ins";
            // 
            // lblThrVal
            // 
            this.lblThrVal.AutoSize = true;
            this.lblThrVal.Location = new System.Drawing.Point(82, 96);
            this.lblThrVal.Name = "lblThrVal";
            this.lblThrVal.Size = new System.Drawing.Size(41, 13);
            this.lblThrVal.TabIndex = 11;
            this.lblThrVal.Text = "label13";
            // 
            // lblPenVal
            // 
            this.lblPenVal.AutoSize = true;
            this.lblPenVal.Location = new System.Drawing.Point(82, 72);
            this.lblPenVal.Name = "lblPenVal";
            this.lblPenVal.Size = new System.Drawing.Size(41, 13);
            this.lblPenVal.TabIndex = 10;
            this.lblPenVal.Text = "label14";
            // 
            // lblFreeVal
            // 
            this.lblFreeVal.AutoSize = true;
            this.lblFreeVal.Location = new System.Drawing.Point(82, 50);
            this.lblFreeVal.Name = "lblFreeVal";
            this.lblFreeVal.Size = new System.Drawing.Size(41, 13);
            this.lblFreeVal.TabIndex = 9;
            this.lblFreeVal.Text = "label15";
            // 
            // lblCorVal
            // 
            this.lblCorVal.AutoSize = true;
            this.lblCorVal.Location = new System.Drawing.Point(82, 27);
            this.lblCorVal.Name = "lblCorVal";
            this.lblCorVal.Size = new System.Drawing.Size(19, 13);
            this.lblCorVal.TabIndex = 8;
            this.lblCorVal.Text = "20";
            // 
            // gbxSetPieces
            // 
            this.gbxSetPieces.Controls.Add(this.lblFreeKicks);
            this.gbxSetPieces.Controls.Add(this.lblCorners);
            this.gbxSetPieces.Controls.Add(this.lblPenalties);
            this.gbxSetPieces.Controls.Add(this.lblThrowIns);
            this.gbxSetPieces.Controls.Add(this.lblCorVal);
            this.gbxSetPieces.Controls.Add(this.lblThrVal);
            this.gbxSetPieces.Controls.Add(this.lblFreeVal);
            this.gbxSetPieces.Controls.Add(this.lblPenVal);
            this.gbxSetPieces.Location = new System.Drawing.Point(3, 3);
            this.gbxSetPieces.Name = "gbxSetPieces";
            this.gbxSetPieces.Size = new System.Drawing.Size(140, 198);
            this.gbxSetPieces.TabIndex = 26;
            this.gbxSetPieces.TabStop = false;
            this.gbxSetPieces.Text = "Set Pieces";
            // 
            // UCSetPieces
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxSetPieces);
            this.Name = "UCSetPieces";
            this.Size = new System.Drawing.Size(153, 210);
            this.gbxSetPieces.ResumeLayout(false);
            this.gbxSetPieces.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCorners;
        private System.Windows.Forms.Label lblFreeKicks;
        private System.Windows.Forms.Label lblPenalties;
        private System.Windows.Forms.Label lblThrowIns;
        private System.Windows.Forms.Label lblThrVal;
        private System.Windows.Forms.Label lblPenVal;
        private System.Windows.Forms.Label lblFreeVal;
        private System.Windows.Forms.Label lblCorVal;
        private System.Windows.Forms.GroupBox gbxSetPieces;
    }
}
