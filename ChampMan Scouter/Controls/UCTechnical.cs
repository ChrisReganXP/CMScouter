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
    public partial class UCTechnical : UserControl
    {
        private PlayerView player;

        public UCTechnical()
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
            SetAttributeLabelValue(lblAntVal, player.Attributes.Anticipation);
            SetAttributeLabelValue(lblCreVal, player.Attributes.Creativity);
            SetAttributeLabelValue(lblCrossingVal, player.Attributes.Crossing);
            SetAttributeLabelValue(lblDecVal, player.Attributes.Decisions);
            SetAttributeLabelValue(lblDribVal, player.Attributes.Dribbling);
            SetAttributeLabelValue(lblFinVal, player.Attributes.Finishing);
            SetAttributeLabelValue(lblHeaVal, player.Attributes.Heading);
            SetAttributeLabelValue(lblLonVal, player.Attributes.LongShots);
            SetAttributeLabelValue(lblMarVal, player.Attributes.Marking);
            SetAttributeLabelValue(lblOffVal, player.Attributes.OffTheBall);
            SetAttributeLabelValue(lblPasVal, player.Attributes.Passing);
            SetAttributeLabelValue(lblPosVal, player.Attributes.Positioning);
            SetAttributeLabelValue(lblTackVal, player.Attributes.Tackling);
            SetAttributeLabelValue(lblTecVal, $"({player.Attributes.Technique})");
        }

        private void SetAttributeLabelValue(Label label, byte val)
        {
            label.Text = val.ToString();
        }

        private void SetAttributeLabelValue(Label label, string val)
        {
            label.Text = val;
        }
    }
}
