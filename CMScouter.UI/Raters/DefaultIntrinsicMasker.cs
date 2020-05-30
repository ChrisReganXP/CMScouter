using System;

namespace CMScouter.UI.Raters
{
    public class DefaultIntrinsicMasker : IIntrinsicMasker
    {
        public byte GetIntrinsicMask(byte val)
        {
            // based on GK attribs
            /*
            if (val < 32) return 1;
            if (val < 50) return 2;
            if (val < 65) return 3;
            if (val < 81) return 4;
            if (val < 88) return 5;
            if (val < 94) return 6;

            if (val < 99) return 7;
            if (val < 104) return 8;
            if (val < 107) return 9;

            if (val < 108) return 10;
            if (val < 109) return 11;
            if (val < 110) return 12;

            if (val < 111) return 13;

            if (val < 114) return 14;
            if (val < 118) return 15;
            if (val < 122) return 16;

            if (val < 128) return 17;
            if (val < 135) return 18;
            if (val < 150) return 19;*/

            if (val < 64) return 1;
            if (val < 72) return 2;
            if (val < 80) return 3;
            if (val < 87) return 4;
            if (val < 92) return 5;
            if (val < 96) return 6;

            if (val < 99) return 7;
            if (val < 102) return 8;
            if (val < 107) return 9;

            if (val < 111) return 10;
            if (val < 114) return 11;
            if (val < 119) return 12;

            if (val < 123) return 13;

            if (val < 128) return 14;
            if (val < 134) return 15;
            if (val < 142) return 16;

            if (val < 150) return 17;
            if (val < 160) return 18;
            if (val < 180) return 19;

            return 20;
        }
    }
}
