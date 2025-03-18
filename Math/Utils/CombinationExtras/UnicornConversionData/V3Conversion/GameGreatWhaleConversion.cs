using CombinationUnicornUtils;
using CombinationUtils.UnicornCombinationData;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForUnicornGames.GameGreatWhale;
using System.Collections.Generic;

namespace CombinationExtras.UnicornConversionData.V3Conversion
{
    public class GameGreatWhaleConversion
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

            var wilds = new List<int[]>();

            for (var i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (j > 0 && j < 4)
                    {
                        if (combination.AdditionalArray[5 * i + j] != 0)
                        {
                            int[] coordinates = new int[2];
                            coordinates[0] = i;
                            coordinates[1] = j - 1;
                            wilds.Add(coordinates);
                        }
                    }
                }
            }


            var slotData = new SlotDataResV3
            {
                win = combination.TotalWin,
                symbols = matrix,
                extra = new
                {
                    nearlyMissedSymbols = nearlyMissed,
                    stickyWildPositions = wilds.ToArray()
                },
                wins = winLine,
                gratisGame = combination.GratisGame

            };

            return slotData;
        }
        public static CombinationUnicorn GetNonWinningCombination(int bet, int numberOfLines, int gratisGamesLeft)
        {
            byte[] helpArray = null;
            var matrixArray = new[,] { { 5, 4, 7, 5, 2 }, { 4, 3, 6, 3, 2 }, { 5, 4, 7, 5, 2 }, { 4, 3, 6, 4, 2 }, { 5, 4, 7, 5, 2 },
                { 99, 99, 99, 99, 99 }, { 99, 99, 99, 99, 99 }, { 99, 99, 99, 99, 99 }, { 99, 99, 99, 99, 99 }, { 99, 99, 99, 99, 99 } };
            var matrix = new MatrixGreatWhale();
            matrix.FromMatrixArrayGreatWhale(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombinationNew(matrix, numberOfLines, bet, gratisGamesLeft > 0, ref helpArray, 0);
            return combination;
        }
    }
}
