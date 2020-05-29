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

        public PlayerViewForm(PlayerView player)
        {
            Player = player;
            InitializeComponent();
            InitialiseControls();
        }

        private void InitialiseControls()
        {
            ucPersonalDetails.SetPlayer(this.Player);
            ucScouting.SetPlayer(this.Player);
            ucTechnical.SetPlayer(this.Player);
            ucMental.SetPlayer(this.Player);
            ucPhysical.SetPlayer(this.Player);
            ucSetPieces.SetPlayer(this.Player);
            ucGoalkeeping.SetPlayer(this.Player);
        }
    }
}
