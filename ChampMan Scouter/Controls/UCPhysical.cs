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
    public partial class UCPhysical : BaseAttributeControl
    {
        public UCPhysical()
        {
            InitializeComponent();
        }

        protected override void SetLabels()
        {
            SetAttributeLabels(lblAcceleration, lblAccVal, player.Attributes.Acceleration);
            SetAttributeLabels(lblAgility, lblAgiVal, player.Attributes.Agility);
            SetAttributeLabels(lblBalance, lblBalVal, player.Attributes.Balance);
            SetAttributeLabels(lblInjuryProneness, lblInjVal, player.Attributes.InjuryProneness, false, true);
            SetAttributeLabels(lblJumping, lblJumpVal, player.Attributes.Jumping);
            SetAttributeLabels(lblNaturalFitness, lblNaFiVal, player.Attributes.NaturalFitness);
            SetAttributeLabels(lblPace, lblPaceVal, player.Attributes.Pace);
            SetAttributeLabels(lblStamina, lblStaVal, player.Attributes.Stamina);
            SetAttributeLabels(lblStrength, lblStreVal, player.Attributes.Strength);
        }
    }
}
