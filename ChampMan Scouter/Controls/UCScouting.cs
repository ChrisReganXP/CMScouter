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
using CMScouterFunctions.DataClasses;

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

            player.ScoutRatings.Goalkeeper.Ratings.ForEach(x =>
            {
                lblScoutRoleDebug.Text += GetRoleDebugLine(x.Debug, PlayerType.GoalKeeper, x.Rating);
            });

            player.ScoutRatings.RightBack.Ratings.ForEach(x =>
            {
                lblScoutRoleDebug.Text += GetRoleDebugLine(x.Debug, PlayerType.RightBack, x.Rating);
            });

            player.ScoutRatings.CentreHalf.Ratings.ForEach(x =>
            {
                lblScoutRoleDebug.Text += GetRoleDebugLine(x.Debug, PlayerType.CentreHalf, x.Rating);
            });

            player.ScoutRatings.LeftBack.Ratings.ForEach(x =>
            {
                lblScoutRoleDebug.Text += GetRoleDebugLine(x.Debug, PlayerType.LeftBack, x.Rating);
            });

            player.ScoutRatings.RightWingBack.Ratings.ForEach(x =>
            {
                lblScoutRoleDebug.Text += GetRoleDebugLine(x.Debug, PlayerType.RightWingBack, x.Rating);
            });

            player.ScoutRatings.DefensiveMidfielder.Ratings.ForEach(x =>
            {
                lblScoutRoleDebug.Text += GetRoleDebugLine(x.Debug, PlayerType.DefensiveMidfielder, x.Rating);
            });

            player.ScoutRatings.LeftWingBack.Ratings.ForEach(x =>
            {
                lblScoutRoleDebug.Text += GetRoleDebugLine(x.Debug, PlayerType.LeftWingBack, x.Rating);
            });

            player.ScoutRatings.RightMidfielder.Ratings.ForEach(x =>
            {
                lblScoutRoleDebug.Text += GetRoleDebugLine(x.Debug, PlayerType.RightMidfielder, x.Rating);
            });

            player.ScoutRatings.CentreMidfielder.Ratings.ForEach(x =>
            {
                lblScoutRoleDebug.Text += GetRoleDebugLine(x.Debug, PlayerType.CentralMidfielder, x.Rating);
            });

            player.ScoutRatings.LeftMidfielder.Ratings.ForEach(x =>
            {
                lblScoutRoleDebug.Text += GetRoleDebugLine(x.Debug, PlayerType.LeftMidfielder, x.Rating);
            });

            player.ScoutRatings.RightWinger.Ratings.ForEach(x =>
            {
                lblScoutRoleDebug.Text += GetRoleDebugLine(x.Debug, PlayerType.RightWinger, x.Rating);
            });

            player.ScoutRatings.AttackingMidfielder.Ratings.ForEach(x =>
            {
                lblScoutRoleDebug.Text += GetRoleDebugLine(x.Debug, PlayerType.AttackingMidfielder, x.Rating);
            });

            player.ScoutRatings.LeftWinger.Ratings.ForEach(x =>
            {
                lblScoutRoleDebug.Text += GetRoleDebugLine(x.Debug, PlayerType.LeftWinger, x.Rating);
            });

            player.ScoutRatings.CentreForward.Ratings.ForEach(x =>
            {
                lblScoutRoleDebug.Text += GetRoleDebugLine(x.Debug, PlayerType.CentreForward, x.Rating);
            });
        }

        private string GetRoleDebugLine(RatingRoleDebug debug, PlayerType type, byte rating)
        {
            return $"{type,-20} {debug.Role} {rating} - Mental:{debug.Mental} Physical:{debug.Physical} Technical:{debug.Technical} Familiarity:{debug.Position} OffField:{debug.OffField}" + Environment.NewLine;
        }
    }
}
