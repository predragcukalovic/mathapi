using Papi.GameServer.Math.History;

namespace V4Converter.Mappers
{
    public static class GameTypeEnumV3Mapper
    {
        public static int GetV3GameType(int gameType)
        {
            switch (gameType)
            {
                case (int)GameTypeEnum.ReelSpin:
                    return (int)GameTypeEnumV3.Slot;
                case (int)GameTypeEnum.FreeSpin:
                    return (int)GameTypeEnumV3.FreeSpin;
                case (int)GameTypeEnum.DoubleUp:
                    return (int)GameTypeEnumV3.Gamble;
                case (int)GameTypeEnum.Bonus:
                    return (int)GameTypeEnumV3.Bonus;
                case (int)GameTypeEnum.Plinko:
                    return (int)GameTypeEnumV3.Plinko;
                case (int)GameTypeEnum.PromotionSpin:
                    return (int)GameTypeEnumV3.PromotionSpin;
                default:
                    return (int)GameTypeEnumV3.Error;
            }
        }
    }
}
