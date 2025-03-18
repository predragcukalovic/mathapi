using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using MathForGames.GameGoldenClover;
using RNGUtils.RandomData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameGoldenCloverConversion
    {
        /// <summary>
        /// Daje niz pozicija za bonus simbole, promenljiva dužina niza.
        /// </summary>
        /// <returns></returns>
        private static WinSymbolV3[] GetCloverPositionsArray(ICombination combination, byte lineId)
        {
            var listPos = new List<byte>();
            var listWin = new List<int>();
            foreach (var lineInfo in combination.LinesInformation)
            {
                if (lineInfo.Id == lineId)
                {
                    listPos.Add(lineInfo.WinningPosition[0]);
                    listWin.Add(lineInfo.Win);
                }
            }

            var m = listPos.Count;
            var winSymb = new WinSymbolV3[m];
            for (var j = 0; j < m; j++)
            {
                winSymb[j] = new WinSymbolV3 { reel = listPos[j] % 5, row = listPos[j] / 5 };
                winSymb[j].id = combination.Matrix[winSymb[j].reel, winSymb[j].row];
                winSymb[j].value = listWin[j];
            }

            return winSymb;
        }

        public static object ToJsonObject(ICombination combination)
        {
            var tmpMatrixArray = new byte[15];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    tmpMatrixArray[j * 5 + i] = combination.Matrix[i, j];
                }
            }
            var obj = new
            {
                symbols = tmpMatrixArray,
                totalSum = combination.TotalWin,
                noWinLines = combination.NumberOfWinningLines,
                numberOfFreeSpins = combination.NumberOfGratisGames,
                isGratis = combination.GratisGame,
                scatterWin = combination.WinFor2,
                scatterPotentialWin = combination.MultiplyFor2,
                scatterPosition = combination.PositionFor2,
                winStruct = combination.LinesInformation.ToArray()
            };
            return obj;
        }

        public static CombinationGame GetNonWinningCombination(int bet, int numberOfLines, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 8, 8, 5 }, { 7, 2, 2 }, { 3, 3, 4 }, { 7, 8, 8 }, { 1, 6, 6 } };
            if (gratisGamesLeft > 0)
            {
                matrixArray = new[,] { { 11, 11, 11 }, { 11, 11, 11 }, { 11, 11, 11 }, { 11, 11, 11 }, { 11, 11, 11 } };
            }
            var matrix = new MatrixGoldenClover();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationGame();
            var tmpArray = new byte[20];
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGamesLeft > 0, ref tmpArray, null);
            return combination;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination, int numOfGratisGames, bool isCurrentGameGratis)
        {
            var matrix = new int[5, 3];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j];
                }
            }
            var n = combination.LinesInformation.Length;
            var addCloverLine = ((!isCurrentGameGratis && combination.GratisGame) || (isCurrentGameGratis && !combination.GratisGame) || isCurrentGameGratis) ? 1 : 0;
            var newClovers = combination.LinesInformation.Any(x => x.Id == 253) ? 1 : 0;
            var doubleWin = false;
            //var winLine = new WinLineV3[n + addCloverLine]; //TODO: provera
            var winLine = new WinLineV3[n + 1 + newClovers];
            for (var i = 0; i < n; i++)
            {
                winLine[i] = new WinLineV3
                {
                    lineId = combination.LinesInformation[i].Id,
                    soundId = combination.LinesInformation[i].WinningElement,
                    win = (addCloverLine > 0 && combination.LinesInformation[i].Id == 253) ? 0 : combination.LinesInformation[i].Win
                };
                var positions = new List<int>();
                var index = 0;
                while (index < 5 && combination.LinesInformation[i].WinningPosition[index] != 255)
                {
                    positions.Add(combination.LinesInformation[i].WinningPosition[index++]);
                }
                var m = positions.Count;
                var winSymb = new WinSymbolV3[m];
                for (var j = 0; j < m; j++)
                {
                    winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = positions[j] / 5 };
                    winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
                    if (matrix[winSymb[j].reel, 0] == 0 || matrix[winSymb[j].reel, 1] == 0 || matrix[winSymb[j].reel, 2] == 0)
                    {
                        winSymb[j].id = 0;
                    }
                    if (winLine[i].lineId == 253)
                    {
                        winSymb[j].value = combination.LinesInformation[i].Win;
                    }
                }
                winLine[i].symbols = winSymb;
            }
            if (addCloverLine > 0)
            {
                doubleWin = combination.LinesInformation.Any(x => x.Id == 249);
            }
            if (newClovers > 0)
            {
                winLine[n + 1] = new WinLineV3
                {
                    lineId = 251,
                    soundId = 11,
                    win = 0,
                    symbols = GetCloverPositionsArray(combination, 253)
                };
            }
            //if (addCloverLine > 0 || isCurrentGameGratis) //TODO: Provera
            //{
            winLine[n] = new WinLineV3
            {
                lineId = 252,
                soundId = 11,
                win = 0,
                symbols = GetCloverPositionsArray(combination, 250)
            };
            RemoveLinesWithId(ref winLine, 250, 248, 249, 246);
            //}
            //if (!isCurrentGameGratis && combination.GratisGame)
            //{
            RemoveLinesWithId(ref winLine, 253);
            //}

            var jackpots = new List<int>();
            if (combination.GratisGamesValues[0] == 1)
            {
                jackpots.Add(BitConverter.ToInt32(combination.GratisGamesValues, 1));
            }
            if (combination.GratisGamesPositions[0] == 1)
            {
                jackpots.Add(BitConverter.ToInt32(combination.GratisGamesPositions, 1));
            }

            int[] scatters = null;
            int[] scatterPos = null;
            if (combination.WinFor2 > 0)
            {
                scatters = new[] { (int)combination.MultiplyFor2[0], combination.MultiplyFor2[1], combination.MultiplyFor2[2] };
                scatterPos = new int[] { combination.PositionFor2[0], combination.PositionFor2[1], combination.PositionFor2[2] };
            }

            if (isCurrentGameGratis)
            {
                FixMatrix(ref matrix);
            }
            var slotData = new SlotDataResV3
            {
                win = combination.TotalWin,
                symbols = matrix,
                extra = new MatrixGoldenClover.ExtraGoldenCloverData
                {
                    globalJackpotData = jackpots.ToArray(), //TODO: rešiti tip jackpota koji se pao, rešiti za više jackpotova
                    scatterWins = scatters,
                    scatterPosition = scatterPos,
                    scatterWin = combination.WinFor2,
                    freeSpinsLeft = Math.Min(combination.AdditionalArray[15], (byte)3),
                    doubleWin = doubleWin
                    //scatterId = 10 //todo: beze hardkodiranja
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };
            /*if (!isCurrentGameGratis && combination.GratisGame)
            {
                slotData.win -= BitConverter.ToInt32(combination.AdditionalArray, 20);
            }
            if (isCurrentGameGratis && !combination.GratisGame)
            {
                slotData.win += BitConverter.ToInt32(combination.AdditionalArray, 20);
            }*/

            return slotData;
        }

        private static void FixMatrix(ref int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (matrix[i, j] == 11)
                    {
                        matrix[i, j] = (int)SoftwareRng.Next(21, 29);
                    }
                }
            }
        }

        private static void RemoveLinesWithId(ref WinLineV3[] lines, params byte[] lineId)
        {
            var linesList = lines.ToList();
            foreach (var id in lineId)
            {
                linesList.RemoveAll(x => x.lineId == id);
            }
            lines = linesList.ToArray();
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.0,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[17];
            for (var i = 0; i < 17; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    extra = new HelpSymbolExtraV3(),
                    coefficients = MatrixGoldenClover.GetSymbolCoefficients(i)
                };
                if (i == 9 || i == 10)
                {
                    symbols[i].features = new[] { HelpSymbolFeatureV3.Scatter };
                }
                else if (i > 10)
                {
                    symbols[i].features = new[] { HelpSymbolFeatureV3.Bonus };
                }
                else
                {
                    symbols[i].features = new[] { HelpSymbolFeatureV3.Regular };
                }
            }
            return symbols;
        }

        private static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            var lines = new HelpLineConfigV3[50];
            for (var i = 0; i < 50; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = GlobalData.GameLineExtra[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }
    }
}
