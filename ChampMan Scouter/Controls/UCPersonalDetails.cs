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
    public partial class UCPersonalDetails : UserControl
    {
        private PlayerView player;

        public UCPersonalDetails()
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
            lblAge.Text = player.Age.ToString();
            lblFullName.Text = player.GetKnownName();
            lblAlternateName.Text = player.GetAlternateName();
            lblDescribedPosition.Text = player.Positions.DescribedPosition;
            lblNationality.Text = player.Nationality;
            lblSecondNationality.Text = player.SecondaryNationality;

            lblCAVal.Text = player.CurrentAbility.ToString();
            lblPAVal.Text = player.PotentialAbility.ToString();
        }
    }
}
