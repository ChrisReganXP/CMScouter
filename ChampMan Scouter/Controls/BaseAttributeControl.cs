using CMScouter.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChampMan_Scouter.Controls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class BaseAttributeControl : UserControl
    {
        protected PlayerView player;
        protected IPlayerRater rater = new DefaultRater();

        protected virtual void SetLabels() { }

        public void SetPlayer(PlayerView Player, IPlayerRater Rater)
        {
            player = Player;
            rater = Rater;
            SetLabels();
        }

        protected void SetAttributeLabels(Label textLabel, Label valueLabel, byte value, bool IsIntrinsic = false, bool IsInverted = false)
        {
            byte maskedValue = 0;
            if (IsIntrinsic)
            {
                maskedValue = value;
                value = rater.GetIntrinsicMask(value);
            }

            Color color = GetAttributeColor(IsInverted ? (byte)(21 - value) : value);

            //textLabel.ForeColor = color;
            valueLabel.ForeColor = color;
            valueLabel.Text = value.ToString();
            if (maskedValue > 0)
            {
                valueLabel.Text += $" ({maskedValue})";
            }
        }

        private static Color GetAttributeColor(byte value)
        {
            if (value >= 18)
            {
                return Color.Teal;
            }

            if (value >= 15)
            {
                return Color.DarkSeaGreen;
            }

            if (value >= 12)
            {
                return Color.DarkOliveGreen;
            }

            if (value >= 8)
            {
                return Color.Black;
            }

            if (value >= 5)
            {
                return Color.Maroon;
            }

            return Color.Red;
        }
    }
}
