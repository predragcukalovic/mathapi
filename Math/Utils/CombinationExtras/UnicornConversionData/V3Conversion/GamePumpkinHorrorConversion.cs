using CombinationUnicornUtils;
using CombinationUtils.UnicornCombinationData;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForUnicornGames.GamePumpkinHorror;
using System.Collections.Generic;
using System.Linq;

namespace CombinationExtras.UnicornConversionData.V3Conversion
{
    public class GamePumpkinHorrorConversion
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

            var n = combination.LinesInformation.Count(x => x.Id != 253);
            var winLine = new WinLineV3[n];
            var ind = 0;
            var change = new List<int>();
            foreach (var li in combination.LinesInformation)
            {
                if (li.Id != 253)
                {
                    winLine[ind] = new WinLineV3
                    {
                        lineId = li.Id,
                        soundId = li.WinningElement,
                        win = li.Win
                    };
                    var positions = new List<int>();
                    foreach (var lip in li.WinningPosition)
                    {
                        if (lip != 255)
                        {
                            positions.Add(lip);
                        }
                    }

                    var m = positions.Count;
                    var winSymb = new WinSymbolV3[m];
                    for (var j = 0; j < m; j++)
                    {
                        winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = -1 + positions[j] / 5 };
                        winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
                    }

                    winLine[ind].symbols = winSymb;
                    ind++;
                }
                else
                {
                    foreach (var l in li.WinningPosition)
                    {
                        change.Add(l);
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
                    changedSymbols = change.ToArray()
                },
                wins = winLine,
                gratisGame = false
            };

            return slotData;
        }

        public static CombinationUnicorn GetNonWinningCombination(int bet, int numberOfLines)
        {
            var matrixArray = new[,] { { 7, 7, 7, 7, 7 }, { 6, 6, 6, 6, 6 }, { 5, 5, 5, 5, 5 }, { 4, 4, 4, 4, 4 }, { 3, 3, 3, 3, 3 } };
            var matrix = new MatrixPumpkinHorror();
            matrix.FromMatrixArrayPumpkinHorror(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }
    }
}
