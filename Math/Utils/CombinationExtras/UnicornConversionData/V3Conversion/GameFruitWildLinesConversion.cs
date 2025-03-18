using CombinationUnicornUtils;
using CombinationUtils.UnicornCombinationData;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForUnicornGames.GameFruitWildLines;
using System.Collections.Generic;

namespace CombinationExtras.UnicornConversionData.V3Conversion
{
    public class GameFruitWildLinesConversion
    {
        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[5, 3];
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
                    matrix[i, j - 2] = combination.Matrix[i, j];
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
                    winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = -2 + positions[j] / 5 };
                    if (replaceMatrix[winSymb[j].reel, winSymb[j].row] < 10)
                    {
                        replaceMatrix[winSymb[j].reel, winSymb[j].row] = combination.LinesInformation[i].WinningElement;
                    }
                    winSymb[j].id = replaceMatrix[winSymb[j].reel, winSymb[j].row];
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
                    wild = combination.WinFor2,
                    replaceSymbols = replaceMatrix
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }

        public static CombinationUnicorn GetNonWinningCombination(int bet, int numberOfLines)
        {
            var matrixArray = new[,] { { 2, 2, 2, 2, 2, 2, 2 }, { 6, 6, 6, 6, 6, 6, 6 }, { 5, 5, 5, 5, 5, 5, 5 }, { 4, 4, 4, 4, 4, 4, 4 }, { 3, 3, 3, 3, 3, 3, 3 } };
            var matrix = new MatrixFruitWildLines();
            matrix.FromMatrixArrayFruitWildLines(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet, 1);
            return combination;
        }
    }
}
