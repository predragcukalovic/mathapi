using System;
using System.Collections.Generic;
using System.Text;

namespace Papi.GameServer.Math.Contracts.Responses
{
    public class BuyBonusTypesResponse
    {
        public BuyBonusTypeConfig[] BuyBonusTypes { get; set; }
    }

    public class BuyBonusTypeConfig
    {
        public int Type { get; set; }
        public int PriceMultiplier { get; set; }
        public int Lines { get; set; }
        public int Rtp { get; set; }
    }
}
