namespace ChampMan_Scouter.Controls
{
    public partial class UCSetPieces : BaseAttributeControl
    {
        public UCSetPieces()
        {
            InitializeComponent();
        }

        protected override void SetLabels()
        {
            SetAttributeLabels(lblCorners, lblCorVal, player.Attributes.Corners, false);
            SetAttributeLabels(lblFreeKicks, lblFreeVal, player.Attributes.FreeKicks, false);
            SetAttributeLabels(lblPenalties, lblPenVal, player.Attributes.Penalties, true);
            SetAttributeLabels(lblThrowIns, lblThrVal, player.Attributes.ThrowIns, true);
        }
    }
}
