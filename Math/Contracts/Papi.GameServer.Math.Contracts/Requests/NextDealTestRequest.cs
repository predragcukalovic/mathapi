using Papi.GameServer.Math.Contracts.Responses;

namespace Papi.GameServer.Math.Contracts.Requests
{
    public class NextDealTestRequest
    {
        public int Bet { get; set; }
        public string CardsToHold { get; set; }
        public bool IsHoldingAllowed { get; set; }
        public Win TestCombination { get; set; }
    }
}