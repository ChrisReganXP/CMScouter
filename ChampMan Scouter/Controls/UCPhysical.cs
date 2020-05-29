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
    public partial class UCPhysical : UserControl
    {
        private PlayerView player;

        public UCPhysical()
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
            SetAttributeLabelValue(lblAccVal, player.Attributes.Acceleration);
            SetAttributeLabelValue(lblAgiVal, player.Attributes.Agility);
            SetAttributeLabelValue(lblBalVal, player.Attributes.Balance);
            SetAttributeLabelValue(lblInjVal, player.Attributes.InjuryProneness);
            SetAttributeLabelValue(lblJumpVal, player.Attributes.Jumping);
            SetAttributeLabelValue(lblNaFiVal, player.Attributes.NaturalFitness);
            SetAttributeLabelValue(lblPaceVal, player.Attributes.Pace);
            SetAttributeLabelValue(lblStaVal, player.Attributes.Stamina);
            SetAttributeLabelValue(lblStreVal, player.Attributes.Strength);
        }

        private void SetAttributeLabelValue(Label label, byte val)
        {
            label.Text = val.ToString();
        }
    }
}
