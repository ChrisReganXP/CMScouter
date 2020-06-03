using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampMan_Scouter.Controls
{
    public partial class UCPersonality : BaseAttributeControl
    {
        public UCPersonality()
        {
            InitializeComponent();
        }

        protected override void SetLabels()
        {
            SetAttributeLabels(lblAdaptability, lblAdaVal, player.Attributes.Adaptability);
            SetAttributeLabels(lblAmbition, lblAmbVal, player.Attributes.Ambition);
            SetAttributeLabels(lblDetermination, lblDetVal, player.Attributes.Determination);
            SetAttributeLabels(lblLoyalty, lblLoyVal, player.Attributes.Loyalty);
            SetAttributeLabels(lblPressure, lblPreVal, player.Attributes.Pressure);
            SetAttributeLabels(lblProfessionalism, lblProVal, player.Attributes.Professionalism);
            SetAttributeLabels(lblSportsmanship, lblSpoVal, player.Attributes.Sportsmanship);
            SetAttributeLabels(lblTemperament, lblTemVal, player.Attributes.Temperament);
        }
    }
}
