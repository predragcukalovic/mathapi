using Papi.GameServer.Utils.Enums;
using MathCombination.CombinationData;
using RNGUtils.RandomData;
using V4Converter.DTOs;

namespace V4Converter
{
    public class ExtraRowMapper
    {
        public static object getExtraRow(V3MapperParams v3MapperParams, bool forTop)
        {
            switch (v3MapperParams.GameId)
            {
                case Games.WildLuckyClover:
                    return ExtraRowWildLuckyClover(v3MapperParams.Combination, v3MapperParams.IsCurrentGameGratis, v3MapperParams.NumberOfReels, forTop);
                case Games.HeatClassic5:
                    return DuplicateRow(v3MapperParams, forTop);
                default:
                    return GetRowDefault(v3MapperParams);
            }
        }

        public static object GetExtraRow(V3MapperParams v3MapperParams, bool forTop)
        {
            switch (v3MapperParams.GameConfig.ExtraRowStrategy)
            {
                case "WLC":
                    return ExtraRowWildLuckyClover(v3MapperParams.Combination, v3MapperParams.IsCurrentGameGratis, v3MapperParams.NumberOfReels, forTop);
                case "Duplicate":
                    return DuplicateRow(v3MapperParams, forTop);
                case "DuplicateOrRandom":
                    return GetRowDuplicateOrRandom(v3MapperParams, forTop);
                case "Included":
                    return IncludedRow(v3MapperParams, forTop);
                case "IncludedMiddle":
                    return IncludedMiddle(v3MapperParams, forTop);
                case "MysticJungle":
                    return MysticJungle(v3MapperParams, forTop);
                case "IntervalForBonus":
                    return IntervalForBonus(v3MapperParams);
                default:
                    return GetRowDefault(v3MapperParams);
            }
        }

        private static object IntervalForBonus(V3MapperParams v3MapperParams)
        {
            GameConfig gameConfig = v3MapperParams.GameConfig;
            int[] row = new int[v3MapperParams.NumberOfReels];
            var min = (v3MapperParams.IsCurrentGameGratis || v3MapperParams.Combination.GratisGame) ? 5 : 1;
            for (var i = 0; i < row.Length; i++)
            {
                row[i] = (int)SoftwareRng.Next(min, gameConfig.ExtraRowStrategyHigh);
            }
            return row;
        }

        private static object IncludedRow(V3MapperParams v3MapperParams, bool forTop)
        {
            int[] row = new int[v3MapperParams.NumberOfReels];
            int rowIndex = forTop ? v3MapperParams.NumberOfRows + 1 : v3MapperParams.NumberOfRows;
            for (var i = 0; i < row.Length; i++)
            {
                row[i] = v3MapperParams.Combination.Matrix[i, rowIndex];
            }
            return row;
        }

        private static object IncludedMiddle(V3MapperParams v3MapperParams, bool forTop)
        {
            int[] row = new int[v3MapperParams.NumberOfReels];
            int rowIndex = forTop ? 0 : v3MapperParams.NumberOfRows + 1;
            for (var i = 0; i < row.Length; i++)
            {
                row[i] = v3MapperParams.Combination.Matrix[i, rowIndex];
            }
            return row;
        }

        private static object MysticJungle(V3MapperParams v3MapperParams, bool forTop)
        {
            ICombination combination = v3MapperParams.Combination;
            int[] row = new int[v3MapperParams.NumberOfReels];
            int rowIndex = forTop ? 0 : v3MapperParams.NumberOfRows + 1;
            for (var i = 0; i < row.Length; i++)
            {
                row[i] = combination.Matrix[i, rowIndex] + (combination.Matrix[i, rowIndex] == 9 ? combination.WinFor2 : 0);
            }
            return row;
        }

        private static object ExtraRowWildLuckyClover(ICombination combination, bool isCurrentGameGratis, int numberOfReels, bool forTop)
        {
            int[] row = new int[numberOfReels];
            if (forTop)
            {
                for (var i = 0; i < row.Length; i++)
                {
                    row[i] = (byte)((isCurrentGameGratis && combination.Matrix[i, 5] == combination.AdditionalInformation) ? combination.Matrix[i, 5] + 4 : combination.Matrix[i, 5]);
                }
            }
            else
            {
                for (var i = 0; i < row.Length; i++)
                {
                    row[i] = (byte)((isCurrentGameGratis && combination.Matrix[i, 4] == combination.AdditionalInformation) ? combination.Matrix[i, 4] + 4 : combination.Matrix[i, 4]);
                }
            }
            return row;
        }

        private static object DuplicateRow(V3MapperParams v3MapperParams, bool forTop)
        {
            int[] row = new int[v3MapperParams.NumberOfReels];
            if (forTop)
            {
                for (var i = 0; i < row.Length; i++)
                {
                    row[i] = v3MapperParams.Combination.Matrix[i, 0];
                }
            }
            else
            {
                for (var i = 0; i < row.Length; i++)
                {
                    row[i] = v3MapperParams.Combination.Matrix[i, v3MapperParams.NumberOfRows - 1];
                }
            }
            return row;
        }

        private static object GetRowDefault(V3MapperParams v3MapperParams)
        {
            GameConfig gameConfig = v3MapperParams.GameConfig;
            int[] row = new int[v3MapperParams.NumberOfReels];
            for (var i = 0; i < row.Length; i++)
            {
                row[i] = (int)SoftwareRng.Next(gameConfig.ExtraRowStrategyLow, gameConfig.ExtraRowStrategyHigh);
            }
            return row;
        }

        private static object GetRowDuplicateOrRandom(V3MapperParams v3MapperParams, bool forTop)
        {
            GameConfig gameConfig = v3MapperParams.GameConfig;
            int[] row = new int[v3MapperParams.NumberOfReels];
            int numberOfRows = v3MapperParams.NumberOfRows;
            if (forTop)
            {
                for (var i = 0; i < row.Length; i++)
                {
                    if (v3MapperParams.Matrix[i, 0] < gameConfig.ExtraRowStrategyLow || v3MapperParams.Matrix[i, 0] == gameConfig.ExtraRowStrategyHigh)
                    {
                        row[i] = (int)SoftwareRng.Next(gameConfig.ExtraRowStrategyLow, gameConfig.ExtraRowStrategyHigh);
                    }
                    else
                    {
                        row[i] = v3MapperParams.Matrix[i, 0];
                    }
                }
            }
            else
            {
                for (var i = 0; i < row.Length; i++)
                {
                    if (v3MapperParams.Matrix[i, numberOfRows - 1] < gameConfig.ExtraRowStrategyLow || v3MapperParams.Matrix[i, numberOfRows - 1] == gameConfig.ExtraRowStrategyHigh)
                    {
                        row[i] = (int)SoftwareRng.Next(gameConfig.ExtraRowStrategyLow, gameConfig.ExtraRowStrategyHigh);
                    }
                    else
                    {
                        row[i] = v3MapperParams.Matrix[i, numberOfRows - 1];
                    }
                }
            }
            return row;
        }
    }
}
