using System;

namespace GameCrownOfSecret
{
    public class BonusDataCrownOfSecret
    {
        public SpecialSymbolsCrownOfSecret SpecialSymbols { get; set; }
        public bool WasRespinUsed { get; set; }
        public int NumberOfActiveReels { get; set; }

        public static BonusDataCrownOfSecret FromByteArray(byte[] addArray)
        {
            if (addArray == null || addArray.Length < 5)
            {
                throw new ArgumentException("addArray must have at least 5 elements.");
            }

            return new BonusDataCrownOfSecret
            {
                SpecialSymbols = new SpecialSymbolsCrownOfSecret
                {
                    X2 = addArray[0],
                    X3 = addArray[1],
                    Respin = addArray[2]
                },
                NumberOfActiveReels = addArray[3],
                WasRespinUsed = addArray[4] != 0,
            };
        }

    }

    public class SpecialSymbolsCrownOfSecret
    {
        public int X2 { get; set; }
        public int X3 { get; set; }
        public int Respin { get; set; }
    }
}
