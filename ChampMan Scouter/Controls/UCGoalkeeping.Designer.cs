namespace ChampMan_Scouter.Controls
{
    partial class UCGoalkeeping
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
            this.lblHandling = new System.Windows.Forms.Label();
            this.lblOneOnOnes = new System.Windows.Forms.Label();
            this.lblReflexes = new System.Windows.Forms.Label();
            this.lblRefVal = new System.Windows.Forms.Label();
            this.lblOoOVal = new System.Windows.Forms.Label();
            this.lblHanVal = new System.Windows.Forms.Label();
            this.gbxGoalkeeping = new System.Windows.Forms.GroupBox();
            this.gbxGoalkeeping.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHandling
            // 
            this.lblHandling.AutoSize = true;
            this.lblHandling.Location = new System.Drawing.Point(15, 27);
            this.lblHandling.Name = "lblHandling";
            this.lblHandling.Size = new System.Drawing.Size(49, 13);
            this.lblHandling.TabIndex = 0;
            this.lblHandling.Text = "Handling";
            // 
            // lblOneOnOnes
            // 
            this.lblOneOnOnes.AutoSize = true;
            this.lblOneOnOnes.Location = new System.Drawing.Point(15, 50);
            this.lblOneOnOnes.Name = "lblOneOnOnes";
            this.lblOneOnOnes.Size = new System.Drawing.Size(70, 13);
            this.lblOneOnOnes.TabIndex = 1;
            this.lblOneOnOnes.Text = "One on Ones";
            // 
            // lblReflexes
            // 
            this.lblReflexes.AutoSize = true;
            this.lblReflexes.Location = new System.Drawing.Point(15, 72);
            this.lblReflexes.Name = "lblReflexes";
            this.lblReflexes.Size = new System.Drawing.Size(48, 13);
            this.lblReflexes.TabIndex = 2;
            this.lblReflexes.Text = "Reflexes";
            // 
            // lblRefVal
            // 
            this.lblRefVal.AutoSize = true;
            this.lblRefVal.Location = new System.Drawing.Point(82, 72);
            this.lblRefVal.Name = "lblRefVal";
            this.lblRefVal.Size = new System.Drawing.Size(41, 13);
            this.lblRefVal.TabIndex = 10;
            this.lblRefVal.Text = "label14";
            // 
            // lblOoOVal
            // 
            this.lblOoOVal.AutoSize = true;
            this.lblOoOVal.Location = new System.Drawing.Point(82, 50);
            this.lblOoOVal.Name = "lblOoOVal";
            this.lblOoOVal.Size = new System.Drawing.Size(41, 13);
            this.lblOoOVal.TabIndex = 9;
            this.lblOoOVal.Text = "label15";
            // 
            // lblHanVal
            // 
            this.lblHanVal.AutoSize = true;
            this.lblHanVal.Location = new System.Drawing.Point(82, 27);
            this.lblHanVal.Name = "lblHanVal";
            this.lblHanVal.Size = new System.Drawing.Size(19, 13);
            this.lblHanVal.TabIndex = 8;
            this.lblHanVal.Text = "20";
            // 
            // gbxGoalkeeping
            // 
            this.gbxGoalkeeping.Controls.Add(this.lblOneOnOnes);
            this.gbxGoalkeeping.Controls.Add(this.lblHandling);
            this.gbxGoalkeeping.Controls.Add(this.lblReflexes);
            this.gbxGoalkeeping.Controls.Add(this.lblHanVal);
            this.gbxGoalkeeping.Controls.Add(this.lblOoOVal);
            this.gbxGoalkeeping.Controls.Add(this.lblRefVal);
            this.gbxGoalkeeping.Location = new System.Drawing.Point(3, 3);
            this.gbxGoalkeeping.Name = "gbxGoalkeeping";
            this.gbxGoalkeeping.Size = new System.Drawing.Size(140, 107);
            this.gbxGoalkeeping.TabIndex = 26;
            this.gbxGoalkeeping.TabStop = false;
            this.gbxGoalkeeping.Text = "Goalkeeping";
            // 
            // UCGoalkeeping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxGoalkeeping);
            this.Name = "UCGoalkeeping";
            this.Size = new System.Drawing.Size(153, 115);
            this.gbxGoalkeeping.ResumeLayout(false);
            this.gbxGoalkeeping.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHandling;
        private System.Windows.Forms.Label lblOneOnOnes;
        private System.Windows.Forms.Label lblReflexes;
        private System.Windows.Forms.Label lblRefVal;
        private System.Windows.Forms.Label lblOoOVal;
        private System.Windows.Forms.Label lblHanVal;
        private System.Windows.Forms.GroupBox gbxGoalkeeping;
    }
}
