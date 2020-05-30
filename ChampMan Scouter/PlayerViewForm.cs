using CMScouter.UI;
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
    public partial class PlayerViewForm : Form
    {
        public PlayerView Player { get; set; }

        public IPlayerRater Rater { get; set; }

        public PlayerViewForm(PlayerView player, IPlayerRater rater)
        {
            Player = player;
            Rater = rater;
            InitializeComponent();
            InitialiseControls();
        }

        private void InitialiseControls()
        {
            ucPersonalDetails.SetPlayer(this.Player);
            ucScouting.SetPlayer(this.Player);
            ucTechnical.SetPlayer(this.Player, Rater);
            ucMental.SetPlayer(this.Player, Rater);
            ucPhysical.SetPlayer(this.Player, Rater);
            ucSetPieces.SetPlayer(this.Player, Rater);
            ucGoalkeeping.SetPlayer(this.Player, Rater);
        }
    }
}
