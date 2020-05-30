namespace ChampMan_Scouter.Controls
{
    public partial class UCTechnical : BaseAttributeControl
    {
        public UCTechnical()
        {
            InitializeComponent();
        }

        protected override void SetLabels()
        {
            SetAttributeLabels(lblAnticipation, lblAntVal, player.Attributes.Anticipation, true);
            SetAttributeLabels(lblCreativity, lblCreVal, player.Attributes.Creativity, true);
            SetAttributeLabels(lblCrossing, lblCrossingVal, player.Attributes.Crossing, true);
            SetAttributeLabels(lblDecisions, lblDecVal, player.Attributes.Decisions, true);
            SetAttributeLabels(lblDribbling, lblDribVal, player.Attributes.Dribbling, true);
            SetAttributeLabels(lblFinishing, lblFinVal, player.Attributes.Finishing, true);
            SetAttributeLabels(lblHeading, lblHeaVal, player.Attributes.Heading, true);
            SetAttributeLabels(lblLongShots, lblLonVal, player.Attributes.LongShots, true);
            SetAttributeLabels(lblMarking, lblMarVal, player.Attributes.Marking, true);
            SetAttributeLabels(lblOffTheBall, lblOffVal, player.Attributes.OffTheBall, true);
            SetAttributeLabels(lblPassing, lblPasVal, player.Attributes.Passing, true);
            SetAttributeLabels(lblPositioning, lblPosVal, player.Attributes.Positioning, true);
            SetAttributeLabels(lblTackling, lblTackVal, player.Attributes.Tackling, true);
            SetAttributeLabels(lblTechnique, lblTecVal, player.Attributes.Technique);
        }
    }
}
