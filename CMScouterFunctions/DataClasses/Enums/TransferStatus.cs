using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CMScouterFunctions.DataClasses
{
    public enum TransferStatus
    {
        [Description("Not Set")]
        NotSet = 4,

        [Description("Listed")]
        TransferListed = 5,

        [Description("For Loan")]
        ListedForLoan = 6,

        [Description("Listed By Request")]
        ListedByRequest = 12,
    }
}
