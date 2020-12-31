using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.UI
{
    public class ContractView
    {
        public string SquadStatus { get; set; }

        public string TransferStatus { get; set; }

        public ReleaseClauseType ReleaseClause { get; set; }

        public int ReleaseValue { get; set; }
    }
}
