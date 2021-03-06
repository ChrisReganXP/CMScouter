﻿using CMScouter.UI;
using CMScouter.UI.Raters;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChampMan_Scouter
{
    public partial class ScoutingForm : Form
    {
        private CMScouterUI cmsUI;

        private const string PlayerSearch = "Player Search";
        private const string ClubSearch = "Club Search";

        #region Initialise

        public ScoutingForm()
        {
            InitializeComponent();
            PopulateInitialItems();
            CustomiseInitialItems();
        }

        private void PopulateInitialItems()
        {
            ddlSearchTypes.Items.Add(PlayerSearch);
            ddlSearchTypes.Items.Add(ClubSearch);
            ddlSearchTypes.SelectedIndex = 0;

            ddlPlayerType.Items.Add("<All>");
            ddlPlayerType.SelectedIndex = 0;
            foreach (var type in Enum.GetNames(typeof(PlayerType)))
            {
                ddlPlayerType.Items.Add(type);
            }

            ddlContractStatus.ValueMember = "Value";
            ddlContractStatus.DisplayMember = "Text";
            var contractStatusList = new[] { new { Value = -1, Text = "<All>" }, new { Value = 6, Text = "Expires 6 Months" }, new { Value = 12, Text = "Expires 12 Months" } };
            ddlContractStatus.DataSource = contractStatusList;
            ddlContractStatus.SelectedIndex = 0;
        }

        private void CustomiseInitialItems()
        {
            pnlSearch.Hide();

            dgvPlayers.Visible = false;
            dgvPlayers.AutoGenerateColumns = false;
        }

        # endregion

        #region Menu Events

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            DialogResult fileLoadResult = ofdOpenSaveFile.ShowDialog();
            LoadSaveGameFile(fileLoadResult, ofdOpenSaveFile);
        }

        #endregion

        #region Load Data

        private void LoadSaveGameFile(DialogResult result, OpenFileDialog ofdSaveFileDialog)
        {
            if (result == DialogResult.OK)
            {
                CustomiseInitialItems();
                cmsUI = new CMScouterUI(ofdSaveFileDialog.FileName);
                PopulateSearchControls();
                DisplayInitialSearchOptions();
            }
        }

        private void PopulateSearchControls()
        {
            var clubNames = cmsUI.GetClubs().Select(x => x.Name).ToList();
            clubNames.Sort();
            cbxClubName.Items.AddRange(clubNames.ToArray());
        }

        private void DisplayInitialSearchOptions()
        {
            ddlSearchTypes.SelectedIndex = 0;
            pnlSearch.Show();

            tabSearchPanels.SelectedTab = tabSearch_Player;
            //pnlClubList.Hide();
            //pnlPlayerSearch.Show();

            AddNationsToSelect();
            AddPlaysInOptions();
        }

        private void AddPlaysInOptions()
        {
            ddlPlaysIn.ValueMember = "Value";
            ddlPlaysIn.DisplayMember = "Text";
            var playsInLocationList = new List<object>
            {
                new { Value = "-1", Text = "<All>" },
                new { Value = "-2", Text = "---- Regions ----" },
                new { Value = "UKI", Text = "UK & Ireland" },
                new { Value = "SCA", Text = "Scandinavia" },
                new { Value = "OCE", Text = "Oceania" },
                new { Value = "-3", Text = "---- Competitions ----" },
            };

            var nations = cmsUI.GetAllNations();
            var clubComps = cmsUI.GetAllClubCompetitions().OrderBy(x => x.LongName);

            foreach (var comp in clubComps)
            {
                var x = new { Value = comp.Id.ToString(), Text = comp.LongName };
                playsInLocationList.Add(x);
            }

            ddlPlaysIn.DataSource = playsInLocationList;
            ddlPlaysIn.SelectedIndex = 0;
        }

        private void AddNationsToSelect()
        {
            ddlNationality.DisplayMember = "Name";
            ddlNationality.ValueMember = "Id";

            var nationList = cmsUI.GetAllNations().Select(x => new { x.Id, x.Name}).OrderBy(x => x.Name).ToList();
            var all = new { Id = -1, Name = "<All>" };
            nationList.Insert(0, all);

            ddlNationality.DataSource = nationList;
            ddlNationality.SelectedIndex = 0;
        }

        #endregion

        # region Search Changes

        private void ddlSearchTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySearchPanel(((ComboBox)sender).Text);
        }

        private void DisplaySearchPanel(string selectedText)
        {
            switch (selectedText)
            {
                case PlayerSearch:
                    tabSearchPanels.SelectedTab = tabSearch_Player;
                    //pnlClubList.Hide();
                    //pnlPlayerSearch.Show();
                    break;

                case ClubSearch:
                    tabSearchPanels.SelectedTab = tabSearch_Club;
                    //pnlPlayerSearch.Hide();
                    //pnlClubList.Show();
                    break;
            }
        }

        #endregion

        #region Execute Search

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (tabSearchPanels.SelectedTab == tabSearch_Club)
            {
                SearchForClub();
                return;
            }

            if (tabSearchPanels.SelectedTab == tabSearch_Player)
            {
                SearchForPlayer();
                return;
            }
        }

        private void SearchForPlayer()
        {
            int maxValue;
            if (!int.TryParse(tbxMaxValue.Text, out maxValue))
            {
                maxValue = int.MaxValue;
            }

            byte maxAge;
            if (!byte.TryParse(tbxMaxAge.Text, out maxAge))
            {
                maxAge = 255;
            }
            

            PlayerType castType;
            PlayerType? type;
            if (!Enum.TryParse(ddlPlayerType.Text, out castType))
            {
                type = null;
            }
            else
            {
                type = castType;
            }

            int? nationId = (int)ddlNationality.SelectedValue == -1 ? (int?)null : (int)ddlNationality.SelectedValue;

            string selectedPlaysInValue = (string)ddlPlaysIn.SelectedValue;
            string playsInRegion = null;
            int? playsInDivision = null;
            if (!string.IsNullOrWhiteSpace(selectedPlaysInValue) && selectedPlaysInValue.Length == 3 && selectedPlaysInValue.ToList().All(Char.IsLetter))
            {
                playsInRegion = (string)ddlPlaysIn.Text;
            }
            else
            {
                if (selectedPlaysInValue.ToList().All(Char.IsDigit))
                {
                    int parsedID = 0;
                    if (int.TryParse(selectedPlaysInValue, out parsedID))
                    {
                        playsInDivision = parsedID;
                    }
                }
            }

            ScoutingRequest request = new ScoutingRequest()
            {
                PlayerType = type,
                MaxValue = maxValue,
                EUNationalityOnly = cbxEUNational.Checked,
                MaxAge = maxAge,
                NumberOfResults = 200,
                PlaysInRegion = playsInRegion,
                PlaysInDivision = playsInDivision,
                Nationality = nationId,
                ContractStatus = (short)(int)ddlContractStatus.SelectedValue,
            };

            var playerList = cmsUI.GetScoutResults(request);
            DisplayPlayerList(playerList, request);
        }

        private void SearchForClub()
        {
            ScoutingRequest request = new ScoutingRequest()
            {
                ClubName = cbxClubName.Text,
            };
            
            var playerList = cmsUI.GetScoutResults(request);
            DisplayPlayerList(playerList, request);
        }

        #endregion

        #region Show Players

        private void DisplayPlayerList(List<PlayerView> playerList, ScoutingRequest request)
        {
            dgvPlayers.Columns.Clear();

            var buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "View";
            buttonColumn.Name = "ViewButton";
            buttonColumn.Text = "View";
            buttonColumn.Width = 40;
            buttonColumn.UseColumnTextForButtonValue = true;
            dgvPlayers.Columns.Add(buttonColumn);

            dgvPlayers.Columns.Add(CreateGridViewColumn(50, "PlayerId", "Id"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(120, "Name", "Name"));

            if (request.PlayerType != null)
            {
                dgvPlayers.Columns.Add(CreateGridViewColumn(30, "ScoutedRating", "Pos Rat"));
                dgvPlayers.Columns.Add(CreateGridViewColumn(30, "ScoutedRole", "Role Rat"));
            }

            dgvPlayers.Columns.Add(CreateGridViewColumn(30, "BestRating", "Rat"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(30, "BestPosition", "Pos"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(30, "BestRole", "Role"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(70, "DescribedPosition", "Position"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(70, "Value", "Value", format: "c0"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(70, "SquadStatus", "Squad"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(70, "TransferStatus", "Transfer"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(70, "ContractExpiryDate", "Contract"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(70, "ReleaseValue", "Release", format: "c0"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(100, "ClubName", "Club"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(50, "Wage", "Wage", format: "c0"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(30, "Age", "Age"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(70, "Nationality", "Nation"));

            DataTable dtPlayers = CreateDataTable(playerList, request);
            dgvPlayers.DataSource = dtPlayers;
            dgvPlayers.Visible = true;
        }

        private DataTable CreateDataTable(List<PlayerView> playerList, ScoutingRequest request)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn() { ColumnName = "PlayerId", DataType = typeof(int), DefaultValue = null, Unique = true });
            dt.Columns.Add(new DataColumn() { ColumnName = "Name", DataType = typeof(string), DefaultValue = null });

            if (request.PlayerType != null)
            {
                dt.Columns.Add(new DataColumn() { ColumnName = "ScoutedRating", DataType = typeof(byte), DefaultValue = null });
                dt.Columns.Add(new DataColumn() { ColumnName = "ScoutedRole", DataType = typeof(string), DefaultValue = null });
            }

            dt.Columns.Add(new DataColumn() { ColumnName = "BestRating", DataType = typeof(byte), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "BestPosition", DataType = typeof(string), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "BestRole", DataType = typeof(string), DefaultValue = null });

            dt.Columns.Add(new DataColumn() { ColumnName = "ClubName", DataType = typeof(string), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "DescribedPosition", DataType = typeof(string), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "Value", DataType = typeof(int), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "Wage", DataType = typeof(int), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "Age", DataType = typeof(short), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nationality", DataType = typeof(string), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "ContractExpiryDate", DataType = typeof(string), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "SquadStatus", DataType = typeof(string), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "TransferStatus", DataType = typeof(string), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "ReleaseValue", DataType = typeof(int), DefaultValue = null });

            foreach (var player in playerList)
            {
                DataRow dr = dt.NewRow();
                dr["PlayerId"] = player.PlayerId;
                dr["Name"] = player.GetKnownName();

                if (request.PlayerType != null)
                {
                    dr["ScoutedRating"] = GetScoutedPositionRating(request.PlayerType, player.ScoutRatings);
                    dr["ScoutedRole"] = GetScoutedRole(request.PlayerType, player.ScoutRatings);
                }

                dr["BestRating"] = player.ScoutRatings.BestPosition.BestRole().Rating;
                dr["BestPosition"] = GetScoutedPosition(player.ScoutRatings.BestPosition.Position);
                dr["BestRole"] = player.ScoutRatings.BestPosition.BestRole().Role;

                dr["ClubName"] = player.ClubName;
                dr["DescribedPosition"] = player.Positions.DescribedPosition;
                dr["Value"] = player.Value;
                dr["Wage"] = player.WagePerWeek;
                dr["Age"] = player.Age;
                dr["Nationality"] = player.Nationality;
                dr["ContractExpiryDate"] = player.ContractExpiryDate == null ? string.Empty : player.ContractExpiryDate.Value.ToShortDateString();
                dr["SquadStatus"] = player.Contract.SquadStatus;
                dr["TransferStatus"] = player.Contract.TransferStatus?.ToString();
                dr["ReleaseValue"] = player.Contract.ReleaseValue;

                dt.Rows.Add(dr);
            }

            return dt;
        }

        private string GetScoutedPosition(PlayerType position)
        {
            switch (position)
            {
                case PlayerType.GoalKeeper:
                    return "GK";

                case PlayerType.RightBack:
                    return "RB";

                case PlayerType.CentreHalf:
                    return "CD";

                case PlayerType.LeftBack:
                    return "LB";

                case PlayerType.RightWingBack:
                    return "RWB";

                case PlayerType.DefensiveMidfielder:
                    return "DM";

                case PlayerType.LeftWingBack:
                    return "LWB";

                case PlayerType.RightMidfielder:
                    return "RM";

                case PlayerType.CentralMidfielder:
                    return "CM";

                case PlayerType.LeftMidfielder:
                    return "LM";

                case PlayerType.RightWinger:
                    return "RW";

                case PlayerType.AttackingMidfielder:
                    return "AM";

                case PlayerType.LeftWinger:
                    return "LW";

                case PlayerType.CentreForward:
                    return "CF";

                default:
                    return "";
            }
        }

        private DataGridViewTextBoxColumn CreateGridViewColumn(int width, string propertyName, string headerText, bool isDecimal = false, string format = null)
        {
            var c = new DataGridViewTextBoxColumn() { Width = width, DataPropertyName = propertyName, HeaderText = headerText, ReadOnly = true, Resizable = DataGridViewTriState.False, SortMode = DataGridViewColumnSortMode.Automatic };

            if (isDecimal)
            {
                c.DefaultCellStyle.Format = "0.00";
            }

            if (!string.IsNullOrWhiteSpace(format))
            {
                c.DefaultCellStyle.Format = format;
            }

            return c;
        }

        private string GetScoutedPositionRating(PlayerType? scoutedPosition, RatingResults ratings)
        {
            if (scoutedPosition == null)
            {
                return ratings.BestPosition.BestRole().Role.ToString();
            }

            switch (scoutedPosition)
            {
                case PlayerType.GoalKeeper:
                    return ratings.Goalkeeper.BestRole().Rating.ToString();

                case PlayerType.RightBack:
                    return ratings.RightBack.BestRole().Rating.ToString();

                case PlayerType.CentreHalf:
                    return ratings.CentreHalf.BestRole().Rating.ToString();

                case PlayerType.LeftBack:
                    return ratings.LeftBack.BestRole().Rating.ToString();

                case PlayerType.RightWingBack:
                    return ratings.RightWingBack.BestRole().Rating.ToString();

                case PlayerType.DefensiveMidfielder:
                    return ratings.DefensiveMidfielder.BestRole().Rating.ToString();

                case PlayerType.LeftWingBack:
                    return ratings.LeftWingBack.BestRole().Rating.ToString();

                case PlayerType.RightMidfielder:
                    return ratings.RightMidfielder.BestRole().Rating.ToString();

                case PlayerType.CentralMidfielder:
                    return ratings.CentreMidfielder.BestRole().Rating.ToString();

                case PlayerType.LeftMidfielder:
                    return ratings.LeftMidfielder.BestRole().Rating.ToString();

                case PlayerType.RightWinger:
                    return ratings.RightWinger.BestRole().Rating.ToString();

                case PlayerType.AttackingMidfielder:
                    return ratings.AttackingMidfielder.BestRole().Rating.ToString();

                case PlayerType.LeftWinger:
                    return ratings.LeftWinger.BestRole().Rating.ToString();

                case PlayerType.CentreForward:
                    return ratings.CentreForward.BestRole().Rating.ToString();

                default:
                    return ratings.BestPosition.BestRole().Rating.ToString();
            }
        }

        private string GetScoutedRole(PlayerType? scoutedPosition, RatingResults ratings)
        {
            if (scoutedPosition == null)
            {
                return ratings.BestPosition.BestRole().Role.ToString();
            }

            switch (scoutedPosition)
            {
                case PlayerType.GoalKeeper:
                    return ratings.Goalkeeper.BestRole().Role.ToString();

                case PlayerType.RightBack:
                    return ratings.RightBack.BestRole().Role.ToString();

                case PlayerType.CentreHalf:
                    return ratings.CentreHalf.BestRole().Role.ToString();

                case PlayerType.LeftBack:
                    return ratings.LeftBack.BestRole().Role.ToString();

                case PlayerType.RightWingBack:
                    return ratings.RightWingBack.BestRole().Role.ToString();

                case PlayerType.DefensiveMidfielder:
                    return ratings.DefensiveMidfielder.BestRole().Role.ToString();

                case PlayerType.LeftWingBack:
                    return ratings.LeftWingBack.BestRole().Role.ToString();

                case PlayerType.RightMidfielder:
                    return ratings.RightMidfielder.BestRole().Role.ToString();

                case PlayerType.CentralMidfielder:
                    return ratings.CentreMidfielder.BestRole().Role.ToString();

                case PlayerType.LeftMidfielder:
                    return ratings.LeftMidfielder.BestRole().Role.ToString();

                case PlayerType.RightWinger:
                    return ratings.RightWinger.BestRole().Role.ToString();

                case PlayerType.AttackingMidfielder:
                    return ratings.AttackingMidfielder.BestRole().Role.ToString();

                case PlayerType.LeftWinger:
                    return ratings.LeftWinger.BestRole().Role.ToString();

                case PlayerType.CentreForward:
                    return ratings.CentreForward.BestRole().Role.ToString();

                default:
                    return ratings.BestPosition.BestRole().Role.ToString();
            }
        }

        private void dgvPlayers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                int PlayerId = (int)((DataRowView)senderGrid.Rows[e.RowIndex].DataBoundItem).Row[0];

                PlayerView player = cmsUI.GetPlayerByPlayerId(new List<int>() { PlayerId }).First();
                PlayerViewForm details = new PlayerViewForm(player, cmsUI.IntrinsicMasker);
                DialogResult result = details.ShowDialog();
            }
        }

        #endregion
    }
}
