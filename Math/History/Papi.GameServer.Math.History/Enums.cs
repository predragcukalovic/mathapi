namespace Papi.GameServer.Math.History
{
    #region Enums

    public enum GameTypeEnum
    {
        ReelSpin = 0x01,
        FreeSpin = 0x02,
        DoubleUp = 0x05,
        Roulette = 0x07,
        Poker = 0x08,
        Bonus = 0x06,
        Plinko = 0x09,
        PromotionSpin = 0x0A
    }
    public enum GameTypeEnumV3
    {
        Slot = 0,
        Gamble = 1,
        Respin = 2,
        FreeSpin = 3,
        Bonus = 4,
        Error = 5,
        Plinko = 6,
        Roulette = 7,
        Poker = 8,
        PromotionSpin = 9
    }

    public enum CardTypeV3
    {
        None = -1,
        Black = 2,
        Red = 1
    }

    #endregion
}
