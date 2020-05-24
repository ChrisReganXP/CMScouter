using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Design;
using CMScouter.UI;

namespace ChampMan_Scouter.Controls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class UCSetPieces : UserControl
    {
        private PlayerView player;

        public UCSetPieces()
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
            SetAttributeLabelValue(lblCorVal, player.Attributes.Corners);
            SetAttributeLabelValue(lblFreeVal, player.Attributes.FreeKicks);
            SetAttributeLabelValue(lblPenVal, player.Attributes.Penalties);
            SetAttributeLabelValue(lblThrVal, player.Attributes.ThrowIns);
        }

        private void SetAttributeLabelValue(Label label, byte val)
        {
            label.Text = val.ToString();
        }
    }
}
