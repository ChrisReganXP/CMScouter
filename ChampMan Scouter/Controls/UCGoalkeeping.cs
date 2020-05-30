namespace ChampMan_Scouter.Controls
{
    public partial class UCGoalkeeping : BaseAttributeControl
    {
        public UCGoalkeeping()
        {
            InitializeComponent();
        }

        protected override void SetLabels()
        {
            SetAttributeLabels(lblHandling, lblHanVal, player.Attributes.Handling, true);
            SetAttributeLabels(lblOneOnOnes, lblOoOVal, player.Attributes.OneOnOnes, true);
            SetAttributeLabels(lblReflexes, lblRefVal, player.Attributes.Reflexes, true);
        }
    }
}
