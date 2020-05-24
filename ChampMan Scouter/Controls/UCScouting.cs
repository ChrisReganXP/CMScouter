using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CMScouter.UI;
using System.ComponentModel.Design;
using CMScouter.UI.Raters;

namespace ChampMan_Scouter.Controls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class UCScouting : UserControl
    {
        private PlayerView player;

        public UCScouting()
        {
            InitializeComponent();
        }

        public void SetPlayer(PlayerView Player)
        {
            player = Player;
            SetLabels();
        }
        private void SetLabels()
        {
            lblBestPosition.Text = $"{player.ScoutRatings.BestPosition.BestRole().Role.ToString()} : {player.ScoutRatings.BestPosition.BestRole().Rating}";
            lblScoutRoleDebug.Text = string.Empty;

            player.ScoutRatings.Debug.RatingsDebug.ForEach(x =>
            {
                lblScoutRoleDebug.Text += GetRoleDebugLine(x);
            });
        }

        private string GetRoleDebugLine(RatingRoleDebug x)
        {
            return $"{x.Position} {x.Role} - Mental:{x.Mental} Physical:{x.Physical} Technical:{x.Technical} OffField:{x.OffField} ({x.OffFieldBonus})" + Environment.NewLine;
        }
    }
}
