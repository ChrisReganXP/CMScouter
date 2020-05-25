using System.Threading;

namespace ChampMan_Scouter
{
    partial class ScoutingForm
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
            this.components = new System.ComponentModel.Container();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.mainMenuFile = new System.Windows.Forms.MenuItem();
            this.mainMenu_Open = new System.Windows.Forms.MenuItem();
            this.mainMenu_Exit = new System.Windows.Forms.MenuItem();
            this.ofdOpenSaveFile = new System.Windows.Forms.OpenFileDialog();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlClubList = new System.Windows.Forms.Panel();
            this.lblClubNameSearch = new System.Windows.Forms.Label();
            this.cbxClubName = new System.Windows.Forms.ComboBox();
            this.ddlSearchTypes = new System.Windows.Forms.ComboBox();
            this.lblSearchFor = new System.Windows.Forms.Label();
            this.dgvPlayers = new System.Windows.Forms.DataGridView();
            this.pnlPlayerSearch = new System.Windows.Forms.Panel();
            this.ddlPlayerType = new System.Windows.Forms.ComboBox();
            this.tbxMaxValue = new System.Windows.Forms.TextBox();
            this.lblPlayerType = new System.Windows.Forms.Label();
            this.lblMaxValue = new System.Windows.Forms.Label();
            this.tabSearchPanels = new System.Windows.Forms.TabControl();
            this.tabSearch_Club = new System.Windows.Forms.TabPage();
            this.tabSearch_Player = new System.Windows.Forms.TabPage();
            this.cbxEUNational = new System.Windows.Forms.CheckBox();
            this.lblMaxAge = new System.Windows.Forms.Label();
            this.tbxMaxAge = new System.Windows.Forms.TextBox();
            this.lblBasedIn = new System.Windows.Forms.Label();
            this.ddlPlaysInRegion = new System.Windows.Forms.ComboBox();
            this.pnlSearch.SuspendLayout();
            this.pnlClubList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayers)).BeginInit();
            this.pnlPlayerSearch.SuspendLayout();
            this.tabSearchPanels.SuspendLayout();
            this.tabSearch_Club.SuspendLayout();
            this.tabSearch_Player.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mainMenuFile});
            // 
            // mainMenuFile
            // 
            this.mainMenuFile.Index = 0;
            this.mainMenuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mainMenu_Open,
            this.mainMenu_Exit});
            this.mainMenuFile.Text = "File";
            // 
            // mainMenu_Open
            // 
            this.mainMenu_Open.Index = 0;
            this.mainMenu_Open.Text = "Open Save Game";
            this.mainMenu_Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // mainMenu_Exit
            // 
            this.mainMenu_Exit.Index = 1;
            this.mainMenu_Exit.Text = "Exit";
            this.mainMenu_Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // ofdOpenSaveFile
            // 
            this.ofdOpenSaveFile.DefaultExt = "sav";
            this.ofdOpenSaveFile.Filter = "Save Game Files (*.sav)|*.sav";
            this.ofdOpenSaveFile.RestoreDirectory = true;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.tabSearchPanels);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.ddlSearchTypes);
            this.pnlSearch.Controls.Add(this.lblSearchFor);
            this.pnlSearch.Location = new System.Drawing.Point(13, 26);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(775, 135);
            this.pnlSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(676, 45);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlClubList
            // 
            this.pnlClubList.Controls.Add(this.lblClubNameSearch);
            this.pnlClubList.Controls.Add(this.cbxClubName);
            this.pnlClubList.Location = new System.Drawing.Point(6, -1);
            this.pnlClubList.Name = "pnlClubList";
            this.pnlClubList.Size = new System.Drawing.Size(202, 79);
            this.pnlClubList.TabIndex = 2;
            // 
            // lblClubNameSearch
            // 
            this.lblClubNameSearch.AutoSize = true;
            this.lblClubNameSearch.Location = new System.Drawing.Point(59, 10);
            this.lblClubNameSearch.Name = "lblClubNameSearch";
            this.lblClubNameSearch.Size = new System.Drawing.Size(59, 13);
            this.lblClubNameSearch.TabIndex = 1;
            this.lblClubNameSearch.Text = "Club Name";
            // 
            // cbxClubName
            // 
            this.cbxClubName.FormattingEnabled = true;
            this.cbxClubName.Location = new System.Drawing.Point(32, 36);
            this.cbxClubName.Name = "cbxClubName";
            this.cbxClubName.Size = new System.Drawing.Size(121, 21);
            this.cbxClubName.TabIndex = 0;
            // 
            // ddlSearchTypes
            // 
            this.ddlSearchTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSearchTypes.FormattingEnabled = true;
            this.ddlSearchTypes.Location = new System.Drawing.Point(7, 45);
            this.ddlSearchTypes.Name = "ddlSearchTypes";
            this.ddlSearchTypes.Size = new System.Drawing.Size(121, 21);
            this.ddlSearchTypes.TabIndex = 1;
            this.ddlSearchTypes.SelectedIndexChanged += new System.EventHandler(this.ddlSearchTypes_SelectedIndexChanged);
            // 
            // lblSearchFor
            // 
            this.lblSearchFor.AutoSize = true;
            this.lblSearchFor.Location = new System.Drawing.Point(32, 19);
            this.lblSearchFor.Name = "lblSearchFor";
            this.lblSearchFor.Size = new System.Drawing.Size(68, 13);
            this.lblSearchFor.TabIndex = 0;
            this.lblSearchFor.Text = "Search Type";
            // 
            // dgvPlayers
            // 
            this.dgvPlayers.AllowUserToAddRows = false;
            this.dgvPlayers.AllowUserToDeleteRows = false;
            this.dgvPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlayers.Location = new System.Drawing.Point(15, 167);
            this.dgvPlayers.Name = "dgvPlayers";
            this.dgvPlayers.ReadOnly = true;
            this.dgvPlayers.RowHeadersVisible = false;
            this.dgvPlayers.Size = new System.Drawing.Size(768, 271);
            this.dgvPlayers.TabIndex = 1;
            this.dgvPlayers.Visible = false;
            this.dgvPlayers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlayers_CellContentClick);
            // 
            // pnlPlayerSearch
            // 
            this.pnlPlayerSearch.Controls.Add(this.ddlPlaysInRegion);
            this.pnlPlayerSearch.Controls.Add(this.lblBasedIn);
            this.pnlPlayerSearch.Controls.Add(this.tbxMaxAge);
            this.pnlPlayerSearch.Controls.Add(this.lblMaxAge);
            this.pnlPlayerSearch.Controls.Add(this.cbxEUNational);
            this.pnlPlayerSearch.Controls.Add(this.lblMaxValue);
            this.pnlPlayerSearch.Controls.Add(this.lblPlayerType);
            this.pnlPlayerSearch.Controls.Add(this.tbxMaxValue);
            this.pnlPlayerSearch.Controls.Add(this.ddlPlayerType);
            this.pnlPlayerSearch.Location = new System.Drawing.Point(6, -1);
            this.pnlPlayerSearch.Name = "pnlPlayerSearch";
            this.pnlPlayerSearch.Size = new System.Drawing.Size(494, 106);
            this.pnlPlayerSearch.TabIndex = 4;
            // 
            // ddlPlayerType
            // 
            this.ddlPlayerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPlayerType.Location = new System.Drawing.Point(15, 29);
            this.ddlPlayerType.Name = "ddlPlayerType";
            this.ddlPlayerType.Size = new System.Drawing.Size(121, 21);
            this.ddlPlayerType.TabIndex = 0;
            // 
            // tbxMaxValue
            // 
            this.tbxMaxValue.Location = new System.Drawing.Point(157, 29);
            this.tbxMaxValue.Name = "tbxMaxValue";
            this.tbxMaxValue.Size = new System.Drawing.Size(100, 20);
            this.tbxMaxValue.TabIndex = 1;
            // 
            // lblPlayerType
            // 
            this.lblPlayerType.AutoSize = true;
            this.lblPlayerType.Location = new System.Drawing.Point(34, 7);
            this.lblPlayerType.Name = "lblPlayerType";
            this.lblPlayerType.Size = new System.Drawing.Size(63, 13);
            this.lblPlayerType.TabIndex = 2;
            this.lblPlayerType.Text = "Player Type";
            // 
            // lblMaxValue
            // 
            this.lblMaxValue.AutoSize = true;
            this.lblMaxValue.Location = new System.Drawing.Point(173, 7);
            this.lblMaxValue.Name = "lblMaxValue";
            this.lblMaxValue.Size = new System.Drawing.Size(57, 13);
            this.lblMaxValue.TabIndex = 3;
            this.lblMaxValue.Text = "Max Value";
            // 
            // tabSearchPanels
            // 
            this.tabSearchPanels.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabSearchPanels.Controls.Add(this.tabSearch_Club);
            this.tabSearchPanels.Controls.Add(this.tabSearch_Player);
            this.tabSearchPanels.ItemSize = new System.Drawing.Size(0, 1);
            this.tabSearchPanels.Location = new System.Drawing.Point(143, 4);
            this.tabSearchPanels.Name = "tabSearchPanels";
            this.tabSearchPanels.SelectedIndex = 0;
            this.tabSearchPanels.Size = new System.Drawing.Size(511, 114);
            this.tabSearchPanels.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabSearchPanels.TabIndex = 5;
            this.tabSearchPanels.TabStop = false;
            // 
            // tabSearch_Club
            // 
            this.tabSearch_Club.Controls.Add(this.pnlClubList);
            this.tabSearch_Club.Location = new System.Drawing.Point(4, 5);
            this.tabSearch_Club.Name = "tabSearch_Club";
            this.tabSearch_Club.Padding = new System.Windows.Forms.Padding(3);
            this.tabSearch_Club.Size = new System.Drawing.Size(503, 105);
            this.tabSearch_Club.TabIndex = 0;
            this.tabSearch_Club.Text = "tabPage1";
            this.tabSearch_Club.UseVisualStyleBackColor = true;
            // 
            // tabSearch_Player
            // 
            this.tabSearch_Player.BackColor = System.Drawing.SystemColors.Control;
            this.tabSearch_Player.Controls.Add(this.pnlPlayerSearch);
            this.tabSearch_Player.Location = new System.Drawing.Point(4, 5);
            this.tabSearch_Player.Name = "tabSearch_Player";
            this.tabSearch_Player.Padding = new System.Windows.Forms.Padding(3);
            this.tabSearch_Player.Size = new System.Drawing.Size(503, 105);
            this.tabSearch_Player.TabIndex = 1;
            this.tabSearch_Player.Text = "tabPage2";
            // 
            // cbxEUNational
            // 
            this.cbxEUNational.AutoSize = true;
            this.cbxEUNational.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbxEUNational.Location = new System.Drawing.Point(31, 73);
            this.cbxEUNational.Name = "cbxEUNational";
            this.cbxEUNational.Size = new System.Drawing.Size(83, 17);
            this.cbxEUNational.TabIndex = 5;
            this.cbxEUNational.Text = "EU National";
            this.cbxEUNational.UseVisualStyleBackColor = true;
            // 
            // lblMaxAge
            // 
            this.lblMaxAge.AutoSize = true;
            this.lblMaxAge.Location = new System.Drawing.Point(176, 61);
            this.lblMaxAge.Name = "lblMaxAge";
            this.lblMaxAge.Size = new System.Drawing.Size(49, 13);
            this.lblMaxAge.TabIndex = 6;
            this.lblMaxAge.Text = "Max Age";
            // 
            // tbxMaxAge
            // 
            this.tbxMaxAge.Location = new System.Drawing.Point(164, 80);
            this.tbxMaxAge.Name = "tbxMaxAge";
            this.tbxMaxAge.Size = new System.Drawing.Size(73, 20);
            this.tbxMaxAge.TabIndex = 7;
            // 
            // lblBasedIn
            // 
            this.lblBasedIn.AutoSize = true;
            this.lblBasedIn.Location = new System.Drawing.Point(305, 7);
            this.lblBasedIn.Name = "lblBasedIn";
            this.lblBasedIn.Size = new System.Drawing.Size(49, 13);
            this.lblBasedIn.TabIndex = 8;
            this.lblBasedIn.Text = "Based In";
            // 
            // ddlPlaysInRegion
            // 
            this.ddlPlaysInRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPlaysInRegion.FormattingEnabled = true;
            this.ddlPlaysInRegion.Location = new System.Drawing.Point(275, 29);
            this.ddlPlaysInRegion.Name = "ddlPlaysInRegion";
            this.ddlPlaysInRegion.Size = new System.Drawing.Size(121, 21);
            this.ddlPlaysInRegion.TabIndex = 9;
            // 
            // ScoutingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvPlayers);
            this.Controls.Add(this.pnlSearch);
            this.Menu = this.mainMenu;
            this.Name = "ScoutingForm";
            this.Text = "CM0102 Scouter";
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlClubList.ResumeLayout(false);
            this.pnlClubList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayers)).EndInit();
            this.pnlPlayerSearch.ResumeLayout(false);
            this.pnlPlayerSearch.PerformLayout();
            this.tabSearchPanels.ResumeLayout(false);
            this.tabSearch_Club.ResumeLayout(false);
            this.tabSearch_Player.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem mainMenuFile;
        private System.Windows.Forms.MenuItem mainMenu_Open;
        private System.Windows.Forms.MenuItem mainMenu_Exit;
        private System.Windows.Forms.OpenFileDialog ofdOpenSaveFile;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblSearchFor;
        private System.Windows.Forms.ComboBox ddlSearchTypes;
        private System.Windows.Forms.Panel pnlClubList;
        private System.Windows.Forms.ComboBox cbxClubName;
        private System.Windows.Forms.Label lblClubNameSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvPlayers;
        private System.Windows.Forms.Panel pnlPlayerSearch;
        private System.Windows.Forms.Label lblPlayerType;
        private System.Windows.Forms.TextBox tbxMaxValue;
        private System.Windows.Forms.ComboBox ddlPlayerType;
        private System.Windows.Forms.Label lblMaxValue;
        private System.Windows.Forms.TabControl tabSearchPanels;
        private System.Windows.Forms.TabPage tabSearch_Club;
        private System.Windows.Forms.TabPage tabSearch_Player;
        private System.Windows.Forms.CheckBox cbxEUNational;
        private System.Windows.Forms.Label lblMaxAge;
        private System.Windows.Forms.TextBox tbxMaxAge;
        private System.Windows.Forms.Label lblBasedIn;
        private System.Windows.Forms.ComboBox ddlPlaysInRegion;
    }
}