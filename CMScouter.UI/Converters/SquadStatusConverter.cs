﻿using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.UI.Converters
{
    public static class SquadStatusConverter
    {
        public static string ConvertSquadStatus(byte rangeStatus)
        {
            var lastSquadStatusString = string.Empty;

            foreach (SquadStatus status in (SquadStatus[])Enum.GetValues(typeof(SquadStatus)))
            {
                lastSquadStatusString = status.ToName();

                if (rangeStatus < (int)status)
                {
                    break;
                }
            }

            return lastSquadStatusString;
        }
    }
}
