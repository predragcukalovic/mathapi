using Papi.GameServer.Utils.Enums;
using GameCrownOfSecret;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.GameCloverCash;
using RNGUtils.RandomData;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using V4Converter.DTOs;
using V4Converter.Mappers;

namespace V4Converter
{
    public class ExtraDataMapper
    {
        public static object getExtraData(V3MapperParams v3MapperParams)
        {
            return ExtraDataObjectBuilder(v3MapperParams);
            if (Enum.GetName(typeof(Games), v3MapperParams.GameId).StartsWith("Unicorn"))
            {
                return GetExtraDataUnicorn(v3MapperParams);
            }
            else
            {
                return GetExtraDataRegular(v3MapperParams);
            }
        }
        private static object GetExtraDataRegular(V3MapperParams v3MapperParams)
        {
            ICombination combination = v3MapperParams.Combination;
            switch (v3MapperParams.GameId)
            {
                case Games.BonusEpicCrown:
                case Games.RedstoneWildHeartBeat:
                case Games.RedstoneFearOfDark:
                case Games.CloverBlast5:
                case Games.PiratesPapi:
                case Games.BlowFruits40:
                case Games.CoinSplash:
                case Games.Redstone40HotJoker:
                case Games.VeryHot5Extreme:
                case Games.RedstoneChilliDouble:
                case Games.EpicClover40:
                case Games.EpicClover100:
                case Games.BurstingHot5:
                case Games.FireClover5:
                case Games.FireCloverDice5:
                case Games.VeryHot5:
                case Games.BurstingHot5Admiral:
                case Games.WildHeart5:
                case Games.AdmiralHot5:
                case Games.VeryHot5Christmas:
                case Games.CrownFire5:
                case Games.Money5:
                case Games.WinningClover5:
                case Games.HotLine1X:
                case Games.RedstoneChilliHot5:
                case Games.RedstoneJollyPresents:
                case Games.RedstoneHotRushCrownBurst:
                case Games.MozzartHot5:
                case Games.RedstoneLadyTripleFortune:
                case Games.BurstingHot40:
                case Games.VeryHot40:
                case Games.VeryHot40Dice:
                case Games.FireClover40:
                case Games.BurstingHot40Mozzart:
                case Games.FireDice40:
                case Games.GermaniaHot40:
                case Games.VeryHot40Christmas:
                case Games.MozzartHot40:
                case Games.VeryHot20:
                case Games.TopHot20:
                case Games.Redstone20WildCrown:
                case Games.GoldenCrown:
                case Games.WildCrown10:
                case Games.GoldenCrownMaxbet:
                case Games.WildDiamond10:
                case Games.WildDice10:
                case Games.WinXtip:
                case Games.SuperCasinoCrown:
                case Games.WildPumpkin10:
                case Games.GoldenCrownChristmas:
                case Games.WildSanta10:
                case Games.EpicCrown10:
                case Games.GoldenVegas:
                case Games.GrandpashabetCrown:
                case Games.CrownOfMozzart:
                case Games.RedstoneSlotRoyale:
                case Games.GoldenCrown20BalkanBet:
                case Games.GoldenCrown40:
                case Games.EpicCrown5:
                case Games.WildJokerHot:
                case Games.FashionNight:
                case Games.BonusBells:
                case Games.CrystalHot40Max:
                case Games.VeryHot40Respin:
                case Games.WildHot40Blow:
                case Games.WildHot40BlowDice:
                case Games.WinningClover5Extreme:
                case Games.TripleFieldsOfLuck:
                case Games.DeepJungle:
                case Games.PlayNetFortuneCloverX2:
                case Games.PlayNetDiamondHeartX2:
                case Games.BetwoonHot5:
                    return new
                    {
                        upperRow = ExtraRowMapper.getExtraRow(v3MapperParams, true),
                        bottomRow = ExtraRowMapper.getExtraRow(v3MapperParams, false),
                        wildExpand = WildExpandMapper.getWildExpand(new WildExpandParams(v3MapperParams.GameId, v3MapperParams.Matrix, v3MapperParams.GameConfig, v3MapperParams.Combination.PositionFor2))
                    };
                case Games.CrownOfSecret:
                    return new
                    {
                        upperRow = ExtraRowMapper.getExtraRow(v3MapperParams, true),
                        bottomRow = ExtraRowMapper.getExtraRow(v3MapperParams, false),
                        wildExpand = WildExpandMapper.getWildExpand(new WildExpandParams(v3MapperParams.GameId, v3MapperParams.Matrix, v3MapperParams.GameConfig, v3MapperParams.Combination.PositionFor2)),
                        bonusData = (combination.GratisGame || combination.AdditionalInformation == 1) ? BonusDataCrownOfSecret.FromByteArray(combination.AdditionalArray) : null,
                    };
                case Games.SantasPresents:
                case Games.EggspandingRush:
                case Games.GoldenExplosion:
                    return new
                    {
                        upperRow = ExtraRowMapper.getExtraRow(v3MapperParams, true),
                        bottomRow = ExtraRowMapper.getExtraRow(v3MapperParams, false),
                        wildExpand = WildExpandMapper.getWildExpand(new WildExpandParams(v3MapperParams.GameId, v3MapperParams.Matrix, v3MapperParams.GameConfig, v3MapperParams.Combination.PositionFor2)),
                        multiplier = GetMultiplier(v3MapperParams)
                    };
                case Games.MysticJungle:
                case Games.BigBuddha:
                case Games.MysteryJokerHot:
                    return new
                    {
                        upperRow = ExtraRowMapper.getExtraRow(v3MapperParams, true),
                        bottomRow = ExtraRowMapper.getExtraRow(v3MapperParams, false),
                        mystery = combination.WinFor2
                    };
                case Games.HeatDouble:
                case Games.Wild27:
                case Games.Wild5:
                case Games.BetAndreasWild:
                    return new
                    {
                        upperRow = ExtraRowMapper.getExtraRow(v3MapperParams, true),
                        bottomRow = ExtraRowMapper.getExtraRow(v3MapperParams, false),
                        doubleWin = combination.WinFor2
                    };
                case Games.JokerTripleDouble:
                case Games.ChilliRespin:
                    return new
                    {
                        upperRow = ExtraRowMapper.getExtraRow(v3MapperParams, true),
                        bottomRow = ExtraRowMapper.getExtraRow(v3MapperParams, false),
                        doubleWin = combination.WinFor2 > 0,
                        fixedSymbols = GetFixedSymbols(v3MapperParams),
                        fixedSymbolsPrevious = GetFixedSymbolsPrev(v3MapperParams)
                    };
                case Games.JokerQueen:
                    return new
                    {
                        upperRow = ExtraRowMapper.getExtraRow(v3MapperParams, true),
                        bottomRow = ExtraRowMapper.getExtraRow(v3MapperParams, false),
                        respinReel = GetReelToBeRespinned(v3MapperParams),
                        multiplier = combination.AdditionalInformation
                    };
                case Games.ElGrandeToro:
                    return new
                    {
                        upperRow = ExtraRowMapper.getExtraRow(v3MapperParams, true),
                        bottomRow = ExtraRowMapper.getExtraRow(v3MapperParams, false),
                        wildPosition = ""
                    };
                case Games.BookOfDread:
                case Games.BookOfScorpionsDouble:
                    return new
                    {
                        upperRow = ExtraRowMapper.getExtraRow(v3MapperParams, true),
                        bottomRow = ExtraRowMapper.getExtraRow(v3MapperParams, false),
                        bonusSymbol1 = combination.AdditionalInformation >> 4,
                        bonusSymbol2 = combination.AdditionalInformation & 0x0F,
                        transformReels1 = GetTransform1(v3MapperParams),
                        transformReels2 = GetTransform2(v3MapperParams)
                    };
                case Games.Spellbook:
                case Games.BookOfSpellsV2:
                case Games.DiceOfSpells:
                case Games.BookOfSpellsDeluxe:
                case Games.LostBook:
                case Games.BookOfSpells2:
                case Games.BookOfSpells:
                case Games.BookOfIbis:
                    WinLineV3[] winLinesExtra = WinLinesMapper.getWinLinesExtraSpellbook(combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
                    return new
                    {
                        upperRow = ExtraRowMapper.getExtraRow(v3MapperParams, true),
                        bottomRow = ExtraRowMapper.getExtraRow(v3MapperParams, false),
                        bonusSymbol = combination.AdditionalInformation == 0 ? null : (int?)combination.AdditionalInformation,
                        transformReels = combination.AdditionalInformation > 0 ? combination.PositionFor2.Select(x => (int)x).ToArray() : new[] { 0, 0, 0, 0, 0 },
                        winsExtra = winLinesExtra,
                        winExtra = winLinesExtra.Sum(we => we.win)
                    };
                case Games.WildLuckyClover:
                    return new
                    {
                        upperRow = ExtraRowMapper.getExtraRow(v3MapperParams, true),
                        bottomRow = ExtraRowMapper.getExtraRow(v3MapperParams, false),
                        bonusSymbol = combination.AdditionalInformation
                    };
                case Games.SuperLucky:
                case Games.RedstoneClover5LockAndCash:
                case Games.MayanCoins:
                case Games.CloverCoin:
                case Games.ShiningRush:
                    return new
                    {
                        upperRow = ExtraRowMapper.getExtraRow(v3MapperParams, true),
                        bottomRow = ExtraRowMapper.getExtraRow(v3MapperParams, false),
                        coins = GetCoins(v3MapperParams),
                        gratisDisplay = combination.GratisGame ? (int)combination.AdditionalArray[15] : 0
                    };
                case Games.GoldenClover:
                    return new
                    {
                        globalJackpotData = GetJackpots(v3MapperParams),
                        scatterWins = GetScatters(v3MapperParams),
                        scatterPosition = GetScatterPositions(v3MapperParams),
                        scatterWin = combination.WinFor2,
                        freeSpinsLeft = Math.Min(combination.AdditionalArray[15], (byte)3),
                        doubleWin = GetDoubleWin(v3MapperParams)
                    };
                case Games.BookOfMayanGold:
                    return new
                    {
                        respinBetsArray = GetRespinBetsArray(v3MapperParams),
                        bonusSymbol = combination.AdditionalInformation > 0 ? combination.AdditionalInformation : -1
                    };
                case Games.SpinCards:
                    return new
                    {
                        possibleWinsPositions = ""
                    };
                case Games.BuffaloFortune:
                case Games.LuckyTwister:
                    return new
                    {
                        firstTwinReel = combination.WinFor2,
                        lastTwinReel = combination.WinFor2 + combination.AdditionalInformation - 1,

                    };
                default:
                    return new
                    {
                        upperRow = ExtraRowMapper.getExtraRow(v3MapperParams, true),
                        bottomRow = ExtraRowMapper.getExtraRow(v3MapperParams, false)
                    };
            }
        }

        private static object GetExtraDataUnicorn(V3MapperParams v3MapperParams)
        {
            switch (v3MapperParams.GameId)
            {
                case Games.UnicornVegasDice:
                    return null;
                case Games.UnicornFruitWildLines:
                case Games.UnicornDiceWildLines:
                    return GetExtraDataFruitWildLines(v3MapperParams);
                case Games.UnicornBigSpinSevens:
                    return new
                    {
                        nearlyMissedSymbols = GetNearlyMissedSymbolsDoubleShift(v3MapperParams)
                    };
                case Games.UnicornStickyHot:
                    return new
                    {
                        nearlyMissedSymbols = GetNearlyMissedSymbolsShift(v3MapperParams),
                        stickyWildPositions = GetStickyWilds(v3MapperParams),
                        mysteryRetrigger = v3MapperParams.Combination.AdditionalArray[15] == 1
                    };
                default:
                    return new
                    {
                        nearlyMissedSymbols = GetNearlyMissedSymbolsShift(v3MapperParams)
                    };
            }
        }

        private static object GetExtraDataFruitWildLines(V3MapperParams v3MapperParams)
        {
            ICombination combination = v3MapperParams.Combination;
            var nearlyMissed = new int[5, 4];
            var replaceMatrix = new int[5, 3];

            for (var i = 0; i < 5; i++)
            {
                nearlyMissed[i, 0] = combination.Matrix[i, 0];
                nearlyMissed[i, 1] = combination.Matrix[i, 1];
                nearlyMissed[i, 2] = combination.Matrix[i, 5];
                nearlyMissed[i, 3] = combination.Matrix[i, 6];

                for (var j = 2; j < 5; j++)
                {
                    replaceMatrix[i, j - 2] = combination.Matrix[i, j];
                }
            }

            for (var i = 0; i < 5; i++) //FIX
            {
                if (nearlyMissed[i, 0] > 9 && nearlyMissed[i, 0] % 10 == 2)
                {
                    nearlyMissed[i, 0]--;
                }
                else if (nearlyMissed[i, 0] > 9 && nearlyMissed[i, 0] % 10 == 3)
                {
                    nearlyMissed[i, 0] -= 3;
                }
                if (nearlyMissed[i, 3] > 9 && nearlyMissed[i, 3] % 10 == 2)
                {
                    nearlyMissed[i, 3]++;
                }
                else if (nearlyMissed[i, 3] > 9 && nearlyMissed[i, 3] % 10 == 1)
                {
                    nearlyMissed[i, 3]--;
                }
            }

            return new
            {
                nearlyMissedSymbols = nearlyMissed,
                wild = combination.WinFor2,
                replaceSymbols = replaceMatrix
            };
        }

        private static object GetNearlyMissedSymbolsShift(V3MapperParams v3MapperParams)
        {
            var nearlyMissed = new int[5, 2];

            for (var i = 0; i < 5; i++)
            {
                nearlyMissed[i, 0] = v3MapperParams.Combination.Matrix[i, 0];
                nearlyMissed[i, 1] = v3MapperParams.Combination.Matrix[i, 4];
            }
            return nearlyMissed;
        }

        private static object GetNearlyMissedSymbolsDoubleShift(V3MapperParams v3MapperParams)
        {
            var nearlyMissed = new int[5, 4];

            for (var i = 0; i < 5; i++)
            {
                nearlyMissed[i, 0] = v3MapperParams.Combination.Matrix[i, 0];
                nearlyMissed[i, 1] = v3MapperParams.Combination.Matrix[i, 1];
                nearlyMissed[i, 2] = v3MapperParams.Combination.Matrix[i, 5];
                nearlyMissed[i, 3] = v3MapperParams.Combination.Matrix[i, 6];
            }
            return nearlyMissed;
        }

        private static object GetStickyWilds(V3MapperParams v3MapperParams)
        {
            var wilds = new List<int[]>();
            for (int i = 0; i < 15; i++)
            {
                if (v3MapperParams.Combination.AdditionalArray[i] > 0)
                {
                    int[] coordinates = new int[2];
                    coordinates[0] = i % 5;
                    coordinates[1] = i / 5;
                    wilds.Add(coordinates);
                }
            }
            return wilds;
        }

        private static object GetJackpots(V3MapperParams v3MapperParams)
        {
            ICombination combination = v3MapperParams.Combination;
            var jackpots = new List<int>();
            if (combination.GratisGamesValues[0] == 1)
            {
                jackpots.Add(BitConverter.ToInt32(combination.GratisGamesValues, 1));
            }
            if (combination.GratisGamesPositions[0] == 1)
            {
                jackpots.Add(BitConverter.ToInt32(combination.GratisGamesPositions, 1));
            }
            return jackpots.ToArray();
        }

        private static int GetReelToBeRespinned(V3MapperParams v3MapperParams)
        {
            int reelToBeRespined = -1;
            if (v3MapperParams.Combination.AdditionalArray != null)
            {
                var sum = v3MapperParams.Combination.AdditionalArray[0] + v3MapperParams.Combination.AdditionalArray[1];
                if (sum > 0 && sum < 4)
                {
                    reelToBeRespined = 3 - sum;
                }
            }
            return reelToBeRespined;
        }

        private static int GetMultiplier(V3MapperParams v3MapperParams)
        {
            int mult = 0;

            if (v3MapperParams.GameId == Games.GoldenExplosion || v3MapperParams.GameId == Games.MegaHot10)
            {
                mult = v3MapperParams.Combination.WinFor2;
            }

            for (var i = 0; i < 5; i++)
            {
                if (v3MapperParams.Combination.PositionFor2[i] < 15)
                {
                    int reel = v3MapperParams.Combination.PositionFor2[i] % 5;
                    int row = v3MapperParams.Combination.PositionFor2[i] / 5;
                    if (v3MapperParams.Matrix[reel, row] > 9)
                    {
                        mult += v3MapperParams.Matrix[reel, row] - 9;
                    }
                }
            }
            return mult;
        }

        private static int[] GetTransform1(V3MapperParams v3MapperParams)
        {
            int[] transform1 = new int[5];
            for (int i = 0; i < 5; i++)
            {
                transform1[i] = v3MapperParams.Combination.PositionFor2[i] == 255 ? 0 : (v3MapperParams.Combination.PositionFor2[i] > 1 ? 1 : 0);
            }
            return transform1;
        }

        private static int[] GetTransform2(V3MapperParams v3MapperParams)
        {
            int[] transform2 = new int[5];
            for (int i = 0; i < 5; i++)
            {
                transform2[i] = v3MapperParams.Combination.PositionFor2[i] == 255 ? 0 : (v3MapperParams.Combination.PositionFor2[i] % 2);
            }
            return transform2;
        }

        private static List<WinSymbolV3> GetFixedSymbols(V3MapperParams v3MapperParams)
        {
            var fixedSymb = new List<WinSymbolV3>();
            for (var i = 0; i < 9; i++)
            {
                if ((v3MapperParams.Combination.AdditionalArray[i] & 0x0F) != 0)
                {
                    fixedSymb.Add(new WinSymbolV3 { reel = i % 3, row = i / 3, id = (v3MapperParams.Combination.AdditionalArray[i] & 0x0F) - 1 });
                }
            }
            return fixedSymb;
        }

        private static List<WinSymbolV3> GetFixedSymbolsPrev(V3MapperParams v3MapperParams)
        {
            var fixedSymbPrev = new List<WinSymbolV3>();
            for (var i = 0; i < 9; i++)
            {
                if ((v3MapperParams.Combination.AdditionalArray[i] >> 4) != 0)
                {
                    fixedSymbPrev.Add(new WinSymbolV3 { reel = i % 3, row = i / 3, id = (v3MapperParams.Combination.AdditionalArray[i] >> 4) - 1 });
                    if (v3MapperParams.Matrix[i % 3, i / 3] == 0)
                    {
                        v3MapperParams.Matrix[i % 3, i / 3] = (int)SoftwareRng.Next(1, 8);
                    }

                }
            }
            return fixedSymbPrev;
        }

        private static int[] GetCoins(V3MapperParams v3MapperParams)
        {
            ICombination combination = v3MapperParams.Combination;
            int[] coinsArray = new int[15];
            if (combination.NumberOfGratisGames > 0 || combination.GratisGame || combination.LinesInformation.Any(x => x.Id == 252))
            {
                for (var i = 0; i < 15; i++)
                {
                    coinsArray[i] = combination.AdditionalArray[i] == 0 ? -1 : MatrixCloverCash.GetWinByIndex(combination.AdditionalArray[i] - 1, combination.AdditionalArray[16]) * combination.WinFor2 / GetCoinsCoefficient(v3MapperParams.GameId);
                }
            }
            else
            {
                var table = MatrixCloverCash.ChooseTable();
                for (var i = 0; i < 15; i++)
                {
                    coinsArray[i] = combination.Matrix[i % 5, i / 5] == 11 ? MatrixCloverCash.GetWinByIndex(MatrixCloverCash.GetRandomIndexByTable(table), table) * combination.WinFor2 / GetCoinsCoefficient(v3MapperParams.GameId) : -1;
                }
            }
            return coinsArray;
        }

        private static int GetCoinsCoefficient(Games gameId)
        {
            switch (gameId)
            {
                case Games.SuperLucky:
                case Games.RedstoneClover5LockAndCash:
                    return 2;
                default:
                    return 1;
            }
        }

        private static int[] GetScatters(V3MapperParams v3MapperParams)
        {
            ICombination combination = v3MapperParams.Combination;
            int[] scatters = null;
            if (combination.WinFor2 > 0)
            {
                scatters = new[] { (int)combination.MultiplyFor2[0], combination.MultiplyFor2[1], combination.MultiplyFor2[2] };
            }
            return scatters;
        }

        private static int[] GetScatterPositions(V3MapperParams v3MapperParams)
        {
            ICombination combination = v3MapperParams.Combination;
            int[] scatterPos = null;
            if (combination.WinFor2 > 0)
            {
                scatterPos = new int[] { combination.PositionFor2[0], combination.PositionFor2[1], combination.PositionFor2[2] };
            }
            return scatterPos;
        }

        private static bool GetDoubleWin(V3MapperParams v3MapperParams)
        {
            ICombination combination = v3MapperParams.Combination;
            bool isCurrentGameGratis = combination.NumberOfGratisGames > 0;
            var addCloverLine = ((!isCurrentGameGratis && combination.GratisGame) || (isCurrentGameGratis && !combination.GratisGame) || isCurrentGameGratis) ? 1 : 0;
            var doubleWin = false;
            if (addCloverLine > 0)
            {
                doubleWin = combination.LinesInformation.Any(x => x.Id == 249);
            }
            return doubleWin;
        }

        private static long[] GetRespinBetsArray(V3MapperParams v3MapperParams)
        {
            long[] respinBets = null;
            if (v3MapperParams.Combination.AdditionalInformation == 0)
            {
                respinBets = new long[5];
                respinBets[4] = 1;
                for (var i = 0; i < 4; i++)
                {
                    respinBets[i] = SoftwareRng.Next(1, 10) * 10; //TODO: ODREDITI PRAVE VREDNOSTI
                }
            }
            return respinBets;
        }

        private static object ExtraDataObjectBuilder(V3MapperParams v3MapperParams)
        {
            var extraDataObject = new ExpandoObject() as IDictionary<string, Object>;
            foreach (string field in v3MapperParams.GameConfig.ExtraFields)
            {
                extraDataObject.Add(field, Type.GetType("V4Converter.ExtraDataMapper").GetMethod($"{field}Get").Invoke(null, new Object[] { v3MapperParams }));
            }

            return extraDataObject;
        }

        //get must be added at the end, otherwise snakecase can't be followed since field names must have lowercase first letters
        public static object wildExpandGet(V3MapperParams v3MapperParams)
        {
            return WildExpandMapper.getWildExpand(new WildExpandParams(v3MapperParams.GameId, v3MapperParams.Matrix, v3MapperParams.GameConfig, v3MapperParams.Combination.PositionFor2));
        }
        public static object upperRowGet(V3MapperParams v3MapperParams)
        {
            return ExtraRowMapper.GetExtraRow(v3MapperParams, true);
        }

        public static object bottomRowGet(V3MapperParams v3MapperParams)
        {
            return ExtraRowMapper.GetExtraRow(v3MapperParams, false);
        }

        public static object recallGet(V3MapperParams v3MapperParams)
        {
            return RecallMapper.GenerateRecall(v3MapperParams);
        }

        public static object bonusSymbolGet(V3MapperParams v3MapperParams)
        {
            switch (v3MapperParams.GameConfig.BonusSymbolStrategy)
            {
                case "Book":
                    return v3MapperParams.Combination.AdditionalInformation == 0 ? null : (int?)v3MapperParams.Combination.AdditionalInformation;
                case "MayanGold":
                    return v3MapperParams.Combination.AdditionalInformation > 0 ? v3MapperParams.Combination.AdditionalInformation : -1;
                default:
                    return v3MapperParams.Combination.AdditionalInformation;
            }
        }

        public static object wildSymbolGet(V3MapperParams v3MapperParams)
        {
            return v3MapperParams.Combination.AdditionalInformation == 0 ? null : (int?)v3MapperParams.Combination.AdditionalInformation;
        }

        public static object mysteryGet(V3MapperParams v3MapperParams)
        {
            return v3MapperParams.Combination.WinFor2;
        }

        public static object doubleWinGet(V3MapperParams v3MapperParams)
        {
            switch (v3MapperParams.GameConfig.DoubleWinStrategy)
            {
                case "GoldenClover":
                    return GetDoubleWin(v3MapperParams);
                case "Joker3x2":
                    return v3MapperParams.Combination.WinFor2 > 0;
                default:
                    return v3MapperParams.Combination.WinFor2;
            }
        }

        public static object fixedSymbolsGet(V3MapperParams v3MapperParams)
        {
            return GetFixedSymbols(v3MapperParams);
        }
        public static object fixedSymbolsPreviousGet(V3MapperParams v3MapperParams)
        {
            return GetFixedSymbolsPrev(v3MapperParams);
        }
        public static object coinsGet(V3MapperParams v3MapperParams)
        {
            return GetCoins(v3MapperParams);
        }
        public static object gratisDisplayGet(V3MapperParams v3MapperParams)
        {
            return v3MapperParams.Combination.GratisGame ? (int)v3MapperParams.Combination.AdditionalArray[15] : 0;
        }
        public static object multiplierGet(V3MapperParams v3MapperParams)
        {
            return GetMultiplier(v3MapperParams);
        }
        public static object transformReelsGet(V3MapperParams v3MapperParams)
        {
            ICombination combination = v3MapperParams.Combination;
            return combination.AdditionalInformation > 0 ? combination.PositionFor2.Select(x => (int)x).ToArray() : new[] { 0, 0, 0, 0, 0 };
        }

        public static object winsExtraGet(V3MapperParams v3MapperParams)
        {
            return WinLinesMapper.getWinLinesExtraSpellbook(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
        }

        public static object winExtraGet(V3MapperParams v3MapperParams)
        {
            return WinLinesMapper.getWinLinesExtraSpellbook(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig).Sum(we => we.win);
        }

        public static object bonusSymbol1Get(V3MapperParams v3MapperParams)
        {
            return v3MapperParams.Combination.AdditionalInformation >> 4;
        }

        public static object bonusSymbol2Get(V3MapperParams v3MapperParams)
        {
            return v3MapperParams.Combination.AdditionalInformation & 0x0F;
        }
        public static object transformReels1Get(V3MapperParams v3MapperParams)
        {
            return GetTransform1(v3MapperParams);
        }

        public static object transformReels2Get(V3MapperParams v3MapperParams)
        {
            return GetTransform2(v3MapperParams);
        }

        public static object multiplicatorGet(V3MapperParams v3MapperParams)
        {
            return v3MapperParams.Combination.WinFor2;
        }

        public static string wildExpandSourceGet(V3MapperParams v3MapperParams)
        {
            return v3MapperParams.GameConfig.WildExpandSource;
        }


        public static object overthrowGet(V3MapperParams v3MapperParams)
        {
            List<WinSymbolV3> wildOverthrow = new List<WinSymbolV3>();
            var line = v3MapperParams.Combination.LinesInformation.SingleOrDefault(l => l.Id == 253);
            if (line != null)
            {
                foreach (var l in line.WinningPosition)
                {
                    wildOverthrow.Add(new WinSymbolV3 { reel = l % 5, row = l / 5, id = 0 });
                }
            }
            return wildOverthrow.ToArray();
        }

        public static object levelGet(V3MapperParams v3MapperParams)
        {
            var levelCount = v3MapperParams.GameConfig.ExtraConfigArray;
            var gamesPlayed = v3MapperParams.Combination.WinFor2;
            var s = 0;
            for (var i = 0; i < levelCount.Length; i++)
            {
                s += levelCount[i];
                if (gamesPlayed < s)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
