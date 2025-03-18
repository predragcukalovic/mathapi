using CombinationUnicornUtils;
using CombinationUtils.UnicornCombinationData;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForUnicornGames.GameEpicMegaCash;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameEpicMegaCashConversion
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
                    winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = positions[j] / 5 - 1 };
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
                    anticipation = GetAnticipation(matrix)
                },
                wins = winLine,
                gratisGame = false
            };

            return slotData;
        }

        public static int GetAnticipation(int[,] matrix)
        {
            var ant = 0;
            var multForFirst = 0;
            for (var i = 0; i < 3; i++)
            {
                if (matrix[1, i] > 9)
                {
                    multForFirst = matrix[1, i];
                }
            }
            if (multForFirst != 0)
            {
                for (var i = 0; i < 3; i++)
                {
                    if (matrix[2, i] > 9)
                    {
                        if ((int)((matrix[2, i] - 9) / 4) == (int)((multForFirst - 9) / 4) || ((matrix[2, i] - 9) % 4 > 0 && (multForFirst - 9) % 4 > 0))
                        {
                            ant = 2;
                        }
                        else
                        {
                            ant = 1;
                        }
                    }
                }
            }
            return ant;
        }

        public static CombinationUnicorn GetNonWinningCombination(int bet, int numberOfLines)
        {
            var matrixArray = new[,] { { 3, 9, 9, 9, 4 }, { 5, 8, 8, 8, 6 }, { 7, 7, 7, 7, 8 }, { 4, 6, 6, 6, 3 }, { 9, 5, 5, 5, 5 } };
            var matrix = new MatrixEpicMegaCash();
            matrix.FromMatrixArrayEpicMegaCash(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }
    }

}
