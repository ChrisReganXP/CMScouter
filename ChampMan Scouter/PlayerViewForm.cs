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

        public IIntrinsicMasker Masker { get; set; }

        public PlayerViewForm(PlayerView player, IIntrinsicMasker masker)
        {
            Player = player;
            Masker = masker;
            InitializeComponent();
            InitialiseControls();
        }

        private void InitialiseControls()
        {
            ucPersonalDetails.SetPlayer(this.Player);
            ucScouting.SetPlayer(this.Player);
            ucTechnical.SetPlayer(this.Player, Masker);
            ucMental.SetPlayer(this.Player, Masker);
            ucPhysical.SetPlayer(this.Player, Masker);
            ucSetPieces.SetPlayer(this.Player, Masker);
            ucGoalkeeping.SetPlayer(this.Player, Masker);
        }
    }
}
