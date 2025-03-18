namespace Papi.GameServer.Math.Contracts.Requests
{
    public class NextDealRequest
    {
        public string CardHand { get; set; }
        public int Bet { get; set; }
        public string CardsToHold { get; set; }
        public bool IsHoldingAllowed { get; set; }
    }
}