using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMScouter.UI.Raters
{
    public class PositionRatings
    {
        public PlayerType Position { get; set; }

        public List<PositionRating> Ratings { get; set; }

        public PositionRatings()
        {
            Ratings = new List<PositionRating>();
        }

        public PositionRating BestRole()
        {
            return Ratings.OrderByDescending(x => x.Rating).First();
        }
    }

    public class PositionRating
    {
        public Roles Role { get; set; }

        public byte Rating { get; set; }

        public RatingRoleDebug Debug { get; set; }
    }

    public class RatingResults
    {
        public PositionRatings BestPosition
        {
            get {
                var pos = new PositionRatings[12];
                pos[0] = Goalkeeper;
                pos[1] = RightBack;
                pos[2] = CentreHalf;
                pos[3] = LeftBack;
                pos[4] = DefensiveMidfielder;
                pos[5] = RightMidfielder;
                pos[6] = CentreMidfielder;
                pos[7] = LeftMidfielder;
                pos[8] = RightWinger;
                pos[9] = AttackingMidfielder;
                pos[10] = LeftWinger;
                pos[11] = CentreForward;

                return pos.OrderByDescending(x => x.BestRole().Rating).First();
            }
        }

        public PositionRatings Goalkeeper { get; set; }

        public PositionRatings RightBack { get; set; }

        public PositionRatings CentreHalf { get; set; }

        public PositionRatings LeftBack { get; set; }

        public PositionRatings RightWingBack { get; set; }

        public PositionRatings DefensiveMidfielder { get; set; }

        public PositionRatings LeftWingBack { get; set; }

        public PositionRatings RightMidfielder { get; set; }

        public PositionRatings CentreMidfielder { get; set; }

        public PositionRatings LeftMidfielder { get; set; }

        public PositionRatings RightWinger { get; set; }

        public PositionRatings AttackingMidfielder { get; set; }

        public PositionRatings LeftWinger { get; set; }

        public PositionRatings CentreForward { get; set; }
    }

    public class RatingRoleDebug
    {
        public string OffField { get; set; }

        public string Position { get; set; }

        public Roles Role { get; set; }

        public string Mental { get; set; }

        public string MentalDetail { get; set; }

        public string Physical { get; set; }

        public string PhysicalDetail { get; set; }

        public string Technical { get; set; }

        public string TechnicalDetail { get; set; }

    }
}
