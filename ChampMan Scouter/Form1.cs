using ChampMan_Scouter.Controls;
using CMScouter.UI;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChampMan_Scouter
{
    public partial class Form1 : Form
    {
        public List<PlayerView> playersToDisplay;
        private CMScouterUI cmsUI;

        public Form1()
        {
            InitializeComponent();
            InitialiseFields();
        }

        private void InitialiseFields()
        {
            cbxClub.Items.Add("OLDHAM");
            cbxClub.Items.Add("OLDHAM ATHLETIC");
            cbxClub.Items.Add("LIVERPOOL");
            cbxClub.Items.Add("BARNSLEY FC");
            cbxClub.Items.Add("PLYMOUTH ARGYLE");
            cbxClub.Items.Add("IPSWICH TOWN");
            cbxClub.Items.Add("SCUNTHORPE UNITED");
            cbxClub.Items.Add("SHREWSBURY TOWN");
            cbxClub.Items.Add("BURTON ALBION");
            cbxClub.SelectedIndex = 0;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            DialogResult fileLoadResult = ofdSaveFileDialog.ShowDialog();
            LoadSaveGameFile(fileLoadResult, ofdSaveFileDialog);
        }

        private void LoadSaveGameFile(DialogResult result, OpenFileDialog ofdSaveFileDialog)
        {
            if (result == DialogResult.OK)
            {
                cmsUI = new CMScouterUI(ofdSaveFileDialog.FileName);
                DisplaySaveGameData();
            }
        }

        private void DisplaySaveGameData()
        {
            if (cmsUI == null)
            {
                return;
            }

            PlayerType? scoutedPosition = null;
            //scoutedPosition = PlayerType.CentreHalf;

            // Euro scouting
            // ScoutingRequest request = new ScoutingRequest() { NumberOfResults = 1000, EUNationality = true, MaxValue = 1, MaxAge = 31/*, PlaysInRegion = "UK"*/ };

            //Club scouting
            ScoutingRequest request = new ScoutingRequest() 
            { 
                ClubName = cbxClub.Text as string,
            };

            if (scoutedPosition.HasValue)
            {
                request.PlayerType = scoutedPosition;
            }

            var players = cmsUI.GetScoutResults(request);

            //var players = cmsUI.GetPlayersBySecondName("KETTINGS").ToList();
            //var players = cmsUI.GetPlayersAtClub(cbxClub.SelectedItem as string).OrderByDescending(x => x.BestRating).ToList();
            //var players = cmsUI.GetHighestAbilityGKs(20).OrderByDescending(x => x.CurrentAbility).ToList();
            //var players = cmsUI.GetScoutResults(PlayerType.RightBack, 100000, 100, true).ToList();
            //var players = cmsUI.GetPlayersByNationality("ENGLAND").Where(p => p.GK == 20).OrderByDescending(x => x.GKRating).ToList();
            //var players = cmsUI.GetPlayerByPlayerId(new List<int>() { 1416, 46988 }).ToList();
            //var players = cmsUI.GetBestFreeTransfers(100).OrderByDescending(x => x.BestRating).ToList();
            //var players = cmsUI.GetScoutResults(PlayerType.CentreHalf, 150000, "ENGLAND", 50);
            //var players = cmsUI.GetYouthProspects(100, true);

            dgvPlayers.RowHeadersVisible = false;
            dgvPlayers.AutoGenerateColumns = false;
            dgvPlayers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgvPlayers.Columns.Clear();

            var buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "View";
            buttonColumn.Name = "ViewButton";
            buttonColumn.Text = "View";
            buttonColumn.Width = 25;
            dgvPlayers.Columns.Add(buttonColumn);

            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "PlayerId", "Id"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(80, "FirstName", "First Name"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(80, "SecondName", "Surname"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(100, "ClubName", "Club"));

            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Age", "Age"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(80, "Nationality", "Nation"));

            dgvPlayers.Columns.Add(CreateGridViewColumn(30, "CurrentAbility", "CA"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(30, "PotentialAbility", "PA"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(80, "Value", "Value", format: "c0"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(50, "WagePerWeek", "Wage"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(50, "DescribedPosition", "Position"));

            dgvPlayers.Columns.Add(CreateGridViewColumn(30, GetScoutedPosition(scoutedPosition), "Rating"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(30, GetScoutedRole(scoutedPosition), "Role"));

            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "BestRating", "Best Rating"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "BestRole", "Best Role"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Contract", "ContractExpiryDate"));

            /*
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "GKRating", "GK"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "DFBRating", "DFB"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "AFBRating", "AFB"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "CBRating", "CB"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "DMRating", "DM"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "CMRating", "CM"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "WMRating", "WM"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "WGRating", "WG"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "AMRating", "AM"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "TargetManRating", "TM"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "PoacherRating", "ST"));


            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "BasePhysicalRating", "PHYS"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "BaseMentalRating", "MENT"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "BaseAttackingRating", "ATT"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "BaseDefendingRating", "DEF"));*/

            bool showDefending = false;
            bool showAttacking = false;
            //bool showGKDebug = false;
            bool showWGDebug = false;
            bool showTMDebug = false;
            bool showSTDebug = false;

            if (showWGDebug)
            {
                dgvPlayers.Columns.Add(CreateGridViewColumn(90, "Winger_Debug_Mental", "Mental"));
                dgvPlayers.Columns.Add(CreateGridViewColumn(90, "Winger_Debug_Physical", "Physical"));
                dgvPlayers.Columns.Add(CreateGridViewColumn(90, "Winger_Debug_Technical", "Technical"));
            }

            if (showTMDebug)
            {
                dgvPlayers.Columns.Add(CreateGridViewColumn(40, "TargetManRating_Debug_Mental", "TM_M"));
                dgvPlayers.Columns.Add(CreateGridViewColumn(40, "TargetManRating_Debug_Physical", "TM_P"));
                dgvPlayers.Columns.Add(CreateGridViewColumn(40, "TargetManRating_Debug_Technical", "TM_T"));
            }

            if (showSTDebug)
            {
                dgvPlayers.Columns.Add(CreateGridViewColumn(40, "PoacherRating_Debug_Mental", "ST_M"));
                dgvPlayers.Columns.Add(CreateGridViewColumn(40, "PoacherRating_Debug_Physical", "ST_P"));
                dgvPlayers.Columns.Add(CreateGridViewColumn(40, "PoacherRating_Debug_Technical", "ST_T"));
            }

            if (showDefending)
            {
                dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Marking", "Mark"));
                dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Tackling", "Tack"));
                dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Positioning ", "Posi"));
            }

            if (showAttacking)
            {
                dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Creativity", "Cre"));
                dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Crossing", "Cro"));
                dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Dribbling", "Drib"));
                dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Finishing", "Fin"));
                dgvPlayers.Columns.Add(CreateGridViewColumn(40, "LongShots", "Long"));
                dgvPlayers.Columns.Add(CreateGridViewColumn(40, "OffTheBall", "Off"));
                dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Passing", "Pas"));
            }

            /*
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Agility", "Agil"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Anticipation", "Anti"));

            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Positioning", "Pos"));

            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Handling", "Hand"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "OneOnOnes", "1on1"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Reflexes", "Refl")); 

            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Anticipation", "Anti"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Crossing", "Cro"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(40, "Finishing", "Fin"));*/

            dgvPlayers.DataSource = players;

            DisplayDistribution(players.ToList());
        }

        private string GetScoutedRole(PlayerType? scoutedPosition)
        {
            if (scoutedPosition == null)
            {
                return "BestRole";
            }

            switch (scoutedPosition)
            {
                case PlayerType.GoalKeeper:
                    return "GKRole";

                case PlayerType.RightBack:
                    return "RBRole";

                case PlayerType.CentreHalf:
                    return "CBRole";

                case PlayerType.LeftBack:
                    return "LBRole";

                case PlayerType.RightWingBack:
                    return "RWBRole";

                case PlayerType.DefensiveMidfielder:
                    return "DMRole";

                case PlayerType.LeftWingBack:
                    return "LWBRole";

                case PlayerType.RightMidfielder:
                    return "RMRole";

                case PlayerType.CentralMidfielder:
                    return "CMRole";

                case PlayerType.LeftMidfielder:
                    return "LMRole";

                case PlayerType.RightWinger:
                    return "RWRole";

                case PlayerType.AttackingMidfielder:
                    return "AMRole";

                case PlayerType.LeftWinger:
                    return "LWRole";

                case PlayerType.CentreForward:
                    return "CFRole";
            }

            return "";
        }

        private string GetScoutedPosition(PlayerType? scoutedPosition)
        {
            if (scoutedPosition == null)
            {
                return "BestRating";
            }

            switch (scoutedPosition)
            {
                case PlayerType.GoalKeeper:
                    return "GKRating";

                case PlayerType.RightBack:
                    return "RBRating";

                case PlayerType.CentreHalf:
                    return "CBRating";

                case PlayerType.LeftBack:
                    return "LBRating";

                case PlayerType.RightWingBack:
                    return "RWBRating";

                case PlayerType.DefensiveMidfielder:
                    return "DMRating";

                case PlayerType.LeftWingBack:
                    return "LWBRating";

                case PlayerType.RightMidfielder:
                    return "RMRating";

                case PlayerType.CentralMidfielder:
                    return "CMRating";

                case PlayerType.LeftMidfielder:
                    return "LMRating";

                case PlayerType.RightWinger:
                    return "RWRating";

                case PlayerType.AttackingMidfielder:
                    return "AMRating";

                case PlayerType.LeftWinger:
                    return "LWRating";

                case PlayerType.CentreForward:
                    return "CFRating";

                default:
                    return "?";
            }
        }

        private AttributeDistribution CalculatateDistribution(List<PlayerView> players, PropertyInfo prop, bool isIntrinsic = true)
        {
            var totalPlayers = players.Count;
            if (totalPlayers == 0)
            {
                return new AttributeDistribution();
            }

            var minFin = (byte)players.Select(x => x.Attributes).Min(p => prop.GetValue(p));
            var maxFin = (byte)players.Select(x => x.Attributes).Max(p => prop.GetValue(p));

            decimal[] finDistrib = new decimal[11];
            byte[] boundaries = new byte[12];

            if (isIntrinsic)
            {
                boundaries = new byte[12]{ 0, 32, 64, 96, 102, 108, 114, 120, 128, 160, 192, 225 };
            }
            else
            {
                boundaries = new byte[12] { 0, 4, 8, 11, 13, 14, 15, 16, 17, 18, 19, 20 };
            }

            for (int i = 1; i <= 11; i++)
            {
                var total = players.Select(x => x.Attributes).Where(p => (boundaries[(i - 1)]) <= (byte)prop.GetValue(p) && (byte)prop.GetValue(p) < boundaries[i]).Count();
                finDistrib[i - 1] = (((decimal)total / totalPlayers) * 100);
            }

            List<AttributeDistribution> attributes = new List<AttributeDistribution>();
            return new AttributeDistribution()
            {
                Attribute = prop.Name,
                HighestValue = maxFin,
                LowestValue = minFin,
                FirstSpan = finDistrib[0],
                SecondSpan = finDistrib[1],
                ThirdSpan = finDistrib[2],
                FourthSpan = finDistrib[3],
                FifthSpan = finDistrib[4],
                SixthSpan = finDistrib[5],
                SeventhSpan = finDistrib[6],
                EigthSpan = finDistrib[7],
                NinthSpan = finDistrib[8],
                TenthSpan = finDistrib[9],
                EleventhSpan = finDistrib[10],
            };
        }

        private void DisplayDistribution(List<PlayerView> players)
        {
            List<AttributeDistribution> attributes = new List<AttributeDistribution>();
            
            attributes.Add(CalculatateDistribution(players, typeof(PlayerAttributeView).GetProperty("Tackling")));

            attributes.Add(CalculatateDistribution(players, typeof(PlayerAttributeView).GetProperty("Marking")));

            attributes.Add(CalculatateDistribution(players, typeof(PlayerAttributeView).GetProperty("Positioning")));

            attributes.Add(CalculatateDistribution(players, typeof(PlayerAttributeView).GetProperty("Heading")));

            attributes.Add(CalculatateDistribution(players, typeof(PlayerAttributeView).GetProperty("Jumping"), false));

            attributes.Add(CalculatateDistribution(players, typeof(PlayerAttributeView).GetProperty("Strength"), false));

            dgvDistributions.RowHeadersVisible = false;
            dgvDistributions.AutoGenerateColumns = false;
            dgvDistributions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // { 0, 32, 64, 96, 102, 108, 114, 120, 128, 160, 192, 225 };
            dgvDistributions.Columns.Clear();
            dgvDistributions.Columns.Add(CreateGridViewColumn(90, "Attribute", "Attribute"));
            dgvDistributions.Columns.Add(CreateGridViewColumn(60, "HighestValue", "Highest"));
            dgvDistributions.Columns.Add(CreateGridViewColumn(60, "LowestValue", "Lowest"));
            dgvDistributions.Columns.Add(CreateGridViewColumn(60, "FirstSpan", "0-32", true));
            dgvDistributions.Columns.Add(CreateGridViewColumn(60, "SecondSpan", "33-64", true));
            dgvDistributions.Columns.Add(CreateGridViewColumn(60, "ThirdSpan", "65-96", true));
            dgvDistributions.Columns.Add(CreateGridViewColumn(60, "FourthSpan", "97-102", true));
            dgvDistributions.Columns.Add(CreateGridViewColumn(60, "FifthSpan", "103-108", true));
            dgvDistributions.Columns.Add(CreateGridViewColumn(60, "SixthSpan", "109-114", true));
            dgvDistributions.Columns.Add(CreateGridViewColumn(60, "SeventhSpan", "115-120", true));
            dgvDistributions.Columns.Add(CreateGridViewColumn(60, "EigthSpan", "121-128", true));
            dgvDistributions.Columns.Add(CreateGridViewColumn(60, "NinthSpan", "129-160", true));
            dgvDistributions.Columns.Add(CreateGridViewColumn(60, "TenthSpan", "161-192", true));
            dgvDistributions.Columns.Add(CreateGridViewColumn(60, "EleventhSpan", "193-256", true));

            dgvDistributions.DataSource = attributes;
        }

        private DataGridViewTextBoxColumn CreateGridViewColumn(int width, string propertyName, string headerText, bool isDecimal = false, string format = null)
        {
            var c =  new DataGridViewTextBoxColumn() { Width = width, DataPropertyName = propertyName, HeaderText = headerText, ReadOnly = true, Resizable = DataGridViewTriState.False, SortMode = DataGridViewColumnSortMode.Automatic };

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

        private void dgvPlayers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                PlayerView player = senderGrid.Rows[e.RowIndex].DataBoundItem as PlayerView;
                PlayerViewForm details = new PlayerViewForm(player);
                DialogResult result = details.ShowDialog();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DisplaySaveGameData();
        }
    }

    public class AttributeDistribution
    {
        public string Attribute { get; set; }

        public byte LowestValue { get; set; }

        public byte HighestValue { get; set; }

        public decimal FirstSpan { get; set; }

        public decimal SecondSpan { get; set; }

        public decimal ThirdSpan { get; set; }

        public decimal FourthSpan { get; set; }

        public decimal FifthSpan { get; set; }

        public decimal SixthSpan { get; set; }

        public decimal SeventhSpan { get; set; }

        public decimal EigthSpan { get; set; }

        public decimal NinthSpan { get; set; }

        public decimal TenthSpan { get; set; }

        public decimal EleventhSpan { get; set; }

        public decimal TwelfthSpan { get; set; }
    }
}
