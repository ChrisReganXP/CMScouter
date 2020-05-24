using CMScouter.UI.Raters;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.UI
{
    internal interface IPlayerRater
    {
        RatingResults GetRatings(Player item);
        bool PlaysPosition(PlayerType type, PlayerData player);
    }
}
