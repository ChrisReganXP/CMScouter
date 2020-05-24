namespace ChampMan_Scouter
{
    partial class Form1
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
            this.ofdSaveFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.dgvPlayers = new System.Windows.Forms.DataGridView();
            this.dgvDistributions = new System.Windows.Forms.DataGridView();
            this.cbxClub = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistributions)).BeginInit();
            this.SuspendLayout();
            // 
            // ofdSaveFileDialog
            // 
            this.ofdSaveFileDialog.DefaultExt = "sav";
            this.ofdSaveFileDialog.Filter = "Save Game Files (*.sav)|*.sav";
            this.ofdSaveFileDialog.RestoreDirectory = true;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(21, 12);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(118, 23);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Load Save Game";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // dgvPlayers
            // 
            this.dgvPlayers.AllowUserToAddRows = false;
            this.dgvPlayers.AllowUserToDeleteRows = false;
            this.dgvPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlayers.Location = new System.Drawing.Point(21, 201);
            this.dgvPlayers.Name = "dgvPlayers";
            this.dgvPlayers.ReadOnly = true;
            this.dgvPlayers.Size = new System.Drawing.Size(876, 347);
            this.dgvPlayers.TabIndex = 1;
            this.dgvPlayers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlayers_CellContentClick);
            // 
            // dgvDistributions
            // 
            this.dgvDistributions.AllowUserToAddRows = false;
            this.dgvDistributions.AllowUserToDeleteRows = false;
            this.dgvDistributions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDistributions.Location = new System.Drawing.Point(21, 41);
            this.dgvDistributions.Name = "dgvDistributions";
            this.dgvDistributions.Size = new System.Drawing.Size(876, 150);
            this.dgvDistributions.TabIndex = 2;
            // 
            // cbxClub
            // 
            this.cbxClub.FormattingEnabled = true;
            this.cbxClub.Location = new System.Drawing.Point(228, 12);
            this.cbxClub.Name = "cbxClub";
            this.cbxClub.Size = new System.Drawing.Size(148, 21);
            this.cbxClub.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(442, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 575);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.cbxClub);
            this.Controls.Add(this.dgvDistributions);
            this.Controls.Add(this.dgvPlayers);
            this.Controls.Add(this.btnOpenFile);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistributions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdSaveFileDialog;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.DataGridView dgvPlayers;
        private System.Windows.Forms.DataGridView dgvDistributions;
        private System.Windows.Forms.ComboBox cbxClub;
        private System.Windows.Forms.Button btnRefresh;
    }
}

