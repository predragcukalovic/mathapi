using Papi.GameServer.Utils.Enums;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using System;
using System.Collections.Generic;
using System.Linq;
using V4Converter.DTOs;

namespace V4Converter
{
    public class WinLinesMapper
    {

        public static WinLineV3[] getWinLines(V3MapperParams v3MapperParams)
        {
            if (Enum.GetName(typeof(Games), v3MapperParams.GameId).StartsWith("Unicorn"))
            {
                return GetWinLinesUnicorn(v3MapperParams);
            }
            else
            {
                return GetWinLinesRegular(v3MapperParams);
            }
        }


        public static WinLineV3[] getWinLinesExtraSpellbook(ICombination combination, int numberOfReels, int[,] matrix, long bet, GameConfig gameConfig)
        {
            var n = combination.LinesInformation.Length;
            var winLines = new WinLineV3[n];
            var winLineList = new List<WinLineV3>();
            var extraLines = false;
            for (var i = 0; i < n; i++)
            {
                winLines[i] = new WinLineV3
                {
                    lineId = combination.LinesInformation[i].Id,
                    soundId = combination.LinesInformation[i].WinningElement,
                    win = combination.LinesInformation[i].Win * bet
                };
                var positions = new List<int>();
                var index = 0;
                while (index < numberOfReels && combination.LinesInformation[i].WinningPosition[index] < matrix.Length)
                {
                    positions.Add(combination.LinesInformation[i].WinningPosition[index++]);
                }
                winLines[i].symbols = WinSymbolsMapper.getWinSymbols(new WinSymbolMapperParams(positions, matrix, combination, gameConfig));

                if (winLines[i].win > 0 && extraLines)
                {
                    winLineList.Add(winLines[i]);
                }

                if (winLines[i].lineId == 254)
                {
                    extraLines = true;
                }
            }
            return winLineList.ToArray();
        }

        private static WinLineV3[] GetWinLinesSpellbook(ICombination combination, int numberOfReels, int[,] matrix, long bet, GameConfig gameConfig)
        {
            var n = combination.LinesInformation.Length;
            var winLine = new WinLineV3[n];
            var winLineList = new List<WinLineV3>();
            var extraLines = false;
            for (var i = 0; i < n; i++)
            {
                winLine[i] = new WinLineV3
                {
                    lineId = combination.LinesInformation[i].Id,
                    soundId = combination.LinesInformation[i].WinningElement,
                    win = combination.LinesInformation[i].Win * bet
                };
                var positions = new List<int>();
                var index = 0;
                while (index < numberOfReels && combination.LinesInformation[i].WinningPosition[index] < matrix.Length)
                {
                    positions.Add(combination.LinesInformation[i].WinningPosition[index++]);
                }
                winLine[i].symbols = WinSymbolsMapper.getWinSymbols(new WinSymbolMapperParams(positions, matrix, combination, gameConfig));
                if (winLine[i].win > 0 && !extraLines)
                {
                    winLineList.Add(winLine[i]);
                }
                if (winLine[i].lineId == 254)
                {
                    extraLines = true;
                }
            }
            return winLineList.ToArray();
        }

        private static WinLineV3[] GetWinLinesUnicorn(V3MapperParams v3MapperParams)
        {
            switch (v3MapperParams.GameId)
            {
                case Games.UnicornVegasDice:
                    return GetWinLinesDefault(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
                case Games.UnicornFruitWildLines:
                case Games.UnicornDiceWildLines:
                case Games.UnicornBigSpinSevens:
                    return GetWinLinesDoubleShift(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
                default:
                    return GetWinLinesShift(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
            }
        }

        private static WinLineV3[] GetWinLinesRegular2(V3MapperParams v3MapperParams)
        {
            switch (v3MapperParams.GameId)
            {
                case Games.BookOfScorpionsDouble:
                case Games.BookOfLuxorDouble:
                    return GetWinLinesBookOfDouble(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
                case Games.BookOfSpells:
                case Games.Spellbook:
                case Games.BookOfSpells2:
                case Games.BookOfSpellsDeluxe:
                case Games.BookOfSpellsV2:
                case Games.LostBook:
                case Games.BookOfBruno:
                    return GetWinLinesSpellbook(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
                default:
                    return GetWinLinesDefault(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
            }
        }

        private static WinLineV3[] GetWinLinesRegular(V3MapperParams v3MapperParams)
        {
            switch (v3MapperParams.GameConfig.WinLineType)
            {
                case "BookDouble":
                    return GetWinLinesBookOfDouble(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
                case "Book":
                    return GetWinLinesSpellbook(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
                case "DecrementSymbol":
                    return GetWinLinesDecrementSymbol(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
                case "OmitLines":
                    return GetWinLinesOmit(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
                case "Shift":
                    return GetWinLinesShift(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
                default:
                    return GetWinLinesDefault(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
            }
        }

        private static WinLineV3[] GetWinLinesUnicorn2(V3MapperParams v3MapperParams)
        {
            switch (v3MapperParams.GameConfig.WinLineType)
            {
                case "Shift":
                    return GetWinLinesShift(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
                case "DoubleShift":
                    return GetWinLinesDoubleShift(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
                default:
                    return GetWinLinesDefault(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.Matrix, v3MapperParams.Bet, v3MapperParams.GameConfig);
            }
        }

        private static WinLineV3[] GetWinLinesShift(ICombination combination, int numberOfReels, int[,] matrix, long bet, GameConfig gameConfig)
        {
            var n = combination.LinesInformation.Length;
            var winLines = new WinLineV3[n];
            for (var i = 0; i < n; i++)
            {
                winLines[i] = new WinLineV3
                {
                    lineId = combination.LinesInformation[i].Id,
                    soundId = combination.LinesInformation[i].WinningElement,
                    win = combination.LinesInformation[i].Win * bet
                };
                var positions = new List<int>();
                var index = 0;
                while (index < numberOfReels && combination.LinesInformation[i].WinningPosition[index] < matrix.Length + numberOfReels)
                {
                    positions.Add(combination.LinesInformation[i].WinningPosition[index++]);
                }

                winLines[i].symbols = WinSymbolsMapper.getWinSymbols(new WinSymbolMapperParams(positions, matrix, combination, gameConfig));
            }
            return winLines;
        }

        private static WinLineV3[] GetWinLinesDoubleShift(ICombination combination, int numberOfReels, int[,] matrix, long bet, GameConfig gameConfig)
        {
            var n = combination.LinesInformation.Length;
            var winLines = new WinLineV3[n];
            for (var i = 0; i < n; i++)
            {
                winLines[i] = new WinLineV3
                {
                    lineId = combination.LinesInformation[i].Id,
                    soundId = combination.LinesInformation[i].WinningElement,
                    win = combination.LinesInformation[i].Win * bet
                };
                var positions = new List<int>();
                var index = 0;
                while (index < numberOfReels && combination.LinesInformation[i].WinningPosition[index] < matrix.Length + numberOfReels * 2)
                {
                    positions.Add(combination.LinesInformation[i].WinningPosition[index++]);
                }

                winLines[i].symbols = WinSymbolsMapper.getWinSymbols(new WinSymbolMapperParams(positions, matrix, combination, gameConfig));
            }
            return winLines;
        }

        private static WinLineV3[] GetWinLinesDefault(ICombination combination, int numberOfReels, int[,] matrix, long bet, GameConfig gameConfig)
        {
            var n = combination.LinesInformation.Length;
            var winLines = new List<WinLineV3>();
            for (var i = 0; i < n; i++)
            {
                var lineInformation = combination.LinesInformation[i];
                if (lineInformation.Win == 0 && lineInformation.Id == 254 && gameConfig.OmitExtraLineNoWin)
                {
                    continue;
                }
                WinLineV3 winLine = new WinLineV3
                {
                    lineId = lineInformation.Id,
                    soundId = lineInformation.WinningElement,
                    win = lineInformation.Win * bet
                };
                var positions = new List<int>();
                var index = 0;
                while (index < numberOfReels && lineInformation.WinningPosition[index] < matrix.Length)
                {
                    positions.Add(lineInformation.WinningPosition[index++]);
                }
                winLine.symbols = WinSymbolsMapper.getWinSymbols(new WinSymbolMapperParams(positions, matrix, combination, gameConfig));
                winLines.Add(winLine);
            }
            return winLines.ToArray();
        }

        private static WinLineV3[] GetWinLinesDecrementSymbol(ICombination combination, int numberOfReels, int[,] matrix, long bet, GameConfig gameConfig)
        {
            var n = combination.LinesInformation.Length;
            var winLines = new List<WinLineV3>();
            for (var i = 0; i < n; i++)
            {
                var lineInformation = combination.LinesInformation[i];
                if (lineInformation.Win == 0 && lineInformation.Id == 254 && gameConfig.OmitExtraLineNoWin)
                {
                    continue;
                }
                WinLineV3 winLine = new WinLineV3
                {
                    lineId = lineInformation.Id,
                    soundId = lineInformation.WinningElement - 1,
                    win = lineInformation.Win * bet
                };
                var positions = new List<int>();
                var index = 0;
                while (index < numberOfReels && lineInformation.WinningPosition[index] < matrix.Length)
                {
                    positions.Add(lineInformation.WinningPosition[index++]);
                }
                winLine.symbols = WinSymbolsMapper.getWinSymbols(new WinSymbolMapperParams(positions, matrix, combination, gameConfig));
                winLines.Add(winLine);
            }
            return winLines.ToArray();
        }

        private static WinLineV3[] GetWinLinesBookOfDouble(ICombination combination, int numberOfReels, int[,] matrix, long bet, GameConfig gameConfig)
        {
            var status = 0;
            var winLineList = new List<WinLineV3>();
            var n = combination.LinesInformation.Length;
            WinLineV3 winLine253 = null;
            WinLineV3 winLine252 = null;
            var position253 = new List<int>();
            var position252 = new List<int>();
            for (var i = 0; i < n; i++)
            {
                if (combination.LinesInformation[i].Id == 253)
                {
                    status = 253;
                    winLine253 = new WinLineV3
                    {
                        lineId = 253,
                        win = 0,
                        soundId = combination.AdditionalInformation >> 4
                    };
                    continue;
                }
                if (combination.LinesInformation[i].Id == 252)
                {
                    status = 252;
                    winLine252 = new WinLineV3
                    {
                        lineId = 252,
                        win = 0,
                        soundId = combination.AdditionalInformation & 0x0F
                    };
                    continue;
                }
                switch (status)
                {
                    case 253:
                        winLine253.win += combination.LinesInformation[i].Win * bet;
                        var index = 0;
                        while (index < numberOfReels && combination.LinesInformation[i].WinningPosition[index] != 255)
                        {
                            position253.Add(combination.LinesInformation[i].WinningPosition[index++]);
                        }
                        break;
                    case 252:
                        winLine252.win += combination.LinesInformation[i].Win * bet;
                        var index1 = 0;
                        while (index1 < numberOfReels && combination.LinesInformation[i].WinningPosition[index1] != 255)
                        {
                            position252.Add(combination.LinesInformation[i].WinningPosition[index1++]);
                        }
                        break;
                    default:
                        winLineList.Add(new WinLineV3
                        {
                            lineId = combination.LinesInformation[i].Id,
                            soundId = combination.LinesInformation[i].WinningElement,
                            win = combination.LinesInformation[i].Win * bet
                        });
                        var positions = new List<int>();
                        var index0 = 0;
                        while (index0 < numberOfReels && combination.LinesInformation[i].WinningPosition[index0] != 255)
                        {
                            positions.Add(combination.LinesInformation[i].WinningPosition[index0++]);
                        }
                        winLineList.Last().symbols = WinSymbolsMapper.getWinSymbols(new WinSymbolMapperParams(positions, matrix, combination, gameConfig)); ;
                        break;
                }
            }
            if (winLine253 != null)
            {
                position253 = position253.Distinct().ToList();
                var m = position253.Count;
                var winSymb = new WinSymbolV3[m];
                for (var j = 0; j < m; j++)
                {
                    winSymb[j] = new WinSymbolV3 { reel = position253[j] % numberOfReels, row = position253[j] / numberOfReels };
                    winSymb[j].id = winLine253.soundId;
                }
                winLine253.symbols = winSymb;
                winLineList.Add(winLine253);
            }
            if (winLine252 != null)
            {
                position252 = position252.Distinct().ToList();
                var m = position252.Count;
                var winSymb = new WinSymbolV3[m];
                for (var j = 0; j < m; j++)
                {
                    winSymb[j] = new WinSymbolV3 { reel = position252[j] % numberOfReels, row = position252[j] / numberOfReels };
                    winSymb[j].id = winLine252.soundId;
                }
                winLine252.symbols = winSymb;
                winLineList.Add(winLine252);
            }

            return winLineList.ToArray();
        }

        private static WinLineV3[] GetWinLinesOmit(ICombination combination, int numberOfReels, int[,] matrix, long bet, GameConfig gameConfig)
        {
            var n = combination.LinesInformation.Length;
            var winLines = new List<WinLineV3>();
            for (var i = 0; i < n; i++)
            {
                var lineInformation = combination.LinesInformation[i];
                if (gameConfig.LinesToOmit.Contains(lineInformation.Id))
                {
                    continue;
                }
                WinLineV3 winLine = new WinLineV3
                {
                    lineId = lineInformation.Id,
                    soundId = lineInformation.WinningElement,
                    win = lineInformation.Win * bet
                };
                var positions = new List<int>();
                var index = 0;
                while (index < numberOfReels && lineInformation.WinningPosition[index] < matrix.Length)
                {
                    positions.Add(lineInformation.WinningPosition[index++]);
                }
                winLine.symbols = WinSymbolsMapper.getWinSymbols(new WinSymbolMapperParams(positions, matrix, combination, gameConfig));
                winLines.Add(winLine);
            }
            return winLines.ToArray();
        }
    }
}
