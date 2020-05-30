using System.ComponentModel;
using System.ComponentModel.Design;

namespace ChampMan_Scouter.Controls
{
    public partial class UCMental : BaseAttributeControl
    {
        public UCMental()
        {
            InitializeComponent();
        }

        protected override void SetLabels()
        {
            SetAttributeLabels(lblAggression, lblAggVal, player.Attributes.Aggression);
            SetAttributeLabels(lblBravery, lblBraVal, player.Attributes.Bravery);
            SetAttributeLabels(lblConsistency, lblConsVal, player.Attributes.Consistency);
            SetAttributeLabels(lblDirtyness, lblDirtVal, player.Attributes.Dirtiness, false, true);
            SetAttributeLabels(lblFlair, lblFlaVal, player.Attributes.Flair);
            SetAttributeLabels(lblImpMat, lblImpVal, player.Attributes.ImportantMatches);
            SetAttributeLabels(lblInfluence, lblInfVal, player.Attributes.Influence);
            SetAttributeLabels(lblTeamwork, lblTeamVal, player.Attributes.Teamwork);
            SetAttributeLabels(lblVersitility, lblVersVal, player.Attributes.Versitility);
            SetAttributeLabels(lblWorkRate, lblWorkVal, player.Attributes.WorkRate);
        }
    }
}
