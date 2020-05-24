﻿using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.UI
{
    public class ScoutingRequest
    {
        public string ClubName { get; set; }

        public byte MinAge { get; set; }

        public byte MaxAge { get; set; }

        public bool EUNationality { get; set; }

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public PlayerType? PlayerType { get; set; }

        public string PlaysInCountry { get; set; }

        public string PlaysInRegion { get; set; }

        public short NumberOfResults { get; set; }
    }
}