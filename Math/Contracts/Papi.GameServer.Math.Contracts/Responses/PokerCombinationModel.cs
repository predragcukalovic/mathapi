namespace Papi.GameServer.Math.Contracts.Responses
{
    public enum Win
    {
        RoyalFlush = 8,
        StraightFlush = 7,
        FourOfAKind = 6,
        FullHouse = 5,
        Flush = 4,
        Straight = 3,
        ThreeOfAKind = 2,
        TwoPairs = 1,
        None = 0
    }

    public class PokerCombinationModel
    {
        public int Win { get; set; }
        public Win WinType { get; set; }
        public bool IsFirstDeal { get; set; }
        public byte[] CardSign { get; set; }
        public byte[] CardValue { get; set; }
        public byte[] CardHand { get; set; }
        public byte[] HoldCards { get; set; }
        public byte[] FrontedData { get; set; }
    }
}