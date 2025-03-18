using CombinationUnicornUtils;
using CombinationUtils.UnicornCombinationData;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForUnicornGames.GameIslandRespins;
using System.Collections.Generic;

namespace CombinationExtras.UnicornConversionData.V3Conversion
{
    public class GameIslandRespinsConversion
    {
        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[5, 3];
            var nearlyMissed = new int[5, 2];

            for (var i = 0; i < 5; i++)
            {
                nearlyMissed[i, 0] = combination.Matrix[i, 0];
                nearlyMissed[i, 1] = combination.Matrix[i, 4];

                for (var j = 1; j < 4; j++)
                {
                    matrix[i, j - 1] = combination.Matrix[i, j];
                }
            }

            var n = combination.LinesInformation.Length;
            var winLine = new WinLineV3[n];
            for (var i = 0; i < n; i++)
            {
                winLine[i] = new WinLineV3
                {
                    lineId = combination.LinesInformation[i].Id,
                    soundId = combination.LinesInformation[i].WinningElement,
                    win = combination.LinesInformation[i].Win
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
                    winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = -1 + positions[j] / 5 };
                    winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
                }

                winLine[i].symbols = winSymb;
            }

            var slotData = new SlotDataResV3
            {
                win = combination.TotalWin,
                symbols = matrix,
                extra = new
                {
                    nearlyMissedSymbols = nearlyMissed,
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }

        public static SlotDataResV3 ToSlotDataResV3_2(ICombination combination, bool isCurrentGameGratis)
        {
            var matrix = new int[5, 3];
            //var nearlyMissed = new int[5, 2];
            var ant = new int[3];
            var antB = new int[5];

            for (var i = 0; i < 5; i++)
            {
                //nearlyMissed[i, 0] = combination.Matrix[i, 0];
                //nearlyMissed[i, 1] = combination.Matrix[i, 4];

                for (var j = 1; j < 4; j++)
                {
                    matrix[i, j - 1] = combination.Matrix[i, j];
                    if (matrix[i, j - 1] == 0)
                    {
                        antB[i]++;
                    }
                }
            }
            var ss = antB[0] + antB[1];
            if (ss == 2)
            {
                ant[0] = 1;
            }
            ss += antB[2];
            if (ss == 2)
            {
                antB[1] = 1;
            }
            ss += antB[3];
            if (ss == 2)
            {
                antB[2] = 1;
            }

            var n = combination.LinesInformation.Length;
            var winLine = new WinLineV3[n];
            for (var i = 0; i < n; i++)
            {
                winLine[i] = new WinLineV3
                {
                    lineId = combination.LinesInformation[i].Id,
                    soundId = combination.LinesInformation[i].WinningElement,
                    win = combination.LinesInformation[i].Win
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
                    winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = -1 + positions[j] / 5 };
                    winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
                }

                winLine[i].symbols = winSymb;
            }

            var slotData = new SlotDataResV3
            {
                win = combination.TotalWin,
                symbols = matrix,
                extra = new
                {
                    //nearlyMissedSymbols = nearlyMissed,
                    anticipation = ant,
                    nextGratisNumber = combination.AdditionalInformation
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }

        public static CombinationUnicorn GetNonWinningCombination(int bet, int numberOfLines, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 5, 4, 7, 5, 2 }, { 4, 3, 6, 3, 2 }, { 5, 4, 7, 5, 2 }, { 4, 3, 6, 4, 2 }, { 5, 4, 7, 5, 2 } };
            var matrix = new MatrixIslandRespins();
            matrix.FromMatrixArrayIslandRespins(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombinationNew(matrix, numberOfLines, bet, gratisGamesLeft > 0);
            return combination;
        }

        public static CombinationUnicorn GetNonWinningCombination2(int bet, int numberOfLines, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 5, 4, 7, 5, 2 }, { 4, 3, 6, 3, 2 }, { 5, 4, 7, 5, 2 }, { 4, 3, 6, 4, 2 }, { 5, 4, 7, 5, 2 } };
            var matrix = new MatrixIslandRespins2();
            matrix.FromMatrixArrayIslandRespins2(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGamesLeft > 0, 0);
            return combination;
        }
    }
}
