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
    public partial class UCMental : UserControl
    {
        private PlayerView player;

        public UCMental()
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
            SetAttributeLabelValue(lblAggVal, player.Attributes.Aggression);
            SetAttributeLabelValue(lblBraVal, player.Attributes.Bravery);
            SetAttributeLabelValue(lblConsVal, player.Attributes.Consistency);
            SetAttributeLabelValue(lblDirtVal, player.Attributes.Dirtiness);
            SetAttributeLabelValue(lblFlaVal, player.Attributes.Flair);
            SetAttributeLabelValue(lblImpVal, player.Attributes.ImportantMatches);
            SetAttributeLabelValue(lblInfVal, player.Attributes.Influence);
            SetAttributeLabelValue(lblTeamVal, player.Attributes.Teamwork);
            SetAttributeLabelValue(lblVersVal, player.Attributes.Versitility);
            SetAttributeLabelValue(lblWorkVal, player.Attributes.WorkRate);
        }

        private void SetAttributeLabelValue(Label label, byte val)
        {
            label.Text = val.ToString();
        }
    }
}
