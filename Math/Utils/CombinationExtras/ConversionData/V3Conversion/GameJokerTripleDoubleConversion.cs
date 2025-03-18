using GameJokerTripleDouble;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using RNGUtils.RandomData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameJokerTripleDoubleConversion
    {
        public static Combination3 GetNonWinningCombination(int bet, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 5, 5, 5, 5, 5 }, { 7, 7, 7, 7, 7 }, { 6, 6, 6, 6, 6 } };
            var matrix = new MatrixJokerTripleDouble();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationJokerTripleDouble();
            var tmpArray = new byte[9];
            combination.MatrixToCombination(matrix, bet, false, 0, ref tmpArray);
            return combination;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[3, 3];
            var tmpUpperRow = new int[3];
            var tmpBottomRow = new int[3];
            for (var i = 0; i < 3; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    matrix[i, j - 1] = combination.Matrix[i, j];
                }
                tmpUpperRow[i] = combination.Matrix[i, 0];
                tmpBottomRow[i] = combination.Matrix[i, 4];
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
                while (index < 3)
                {
                    positions.Add(combination.LinesInformation[i].WinningPosition[index++]);
                }
                var m = positions.Count;
                var winSymb = new WinSymbolV3[m];
                for (var j = 0; j < m; j++)
                {
                    winSymb[j] = new WinSymbolV3 { reel = positions[j] % 3, row = positions[j] / 3 };
                    winSymb[j].id = (combination.AdditionalArray[positions[j]] >> 4) == 0 ? matrix[winSymb[j].reel, winSymb[j].row] : (combination.AdditionalArray[positions[j]] >> 4) - 1;
                }
                winLine[i].symbols = winSymb;
            }

            var fixedSymb = new List<WinSymbolV3>();
            var fixedSymbPrev = new List<WinSymbolV3>();
            for (var i = 0; i < 9; i++)
            {
                if ((combination.AdditionalArray[i] & 0x0F) != 0)
                {
                    fixedSymb.Add(new WinSymbolV3 { reel = i % 3, row = i / 3, id = (combination.AdditionalArray[i] & 0x0F) - 1 });
                }
                if ((combination.AdditionalArray[i] >> 4) != 0)
                {
                    fixedSymbPrev.Add(new WinSymbolV3 { reel = i % 3, row = i / 3, id = (combination.AdditionalArray[i] >> 4) - 1 });
                    if (matrix[i % 3, i / 3] == 0)
                    {
                        matrix[i % 3, i / 3] = (int)SoftwareRng.Next(1, 8);
                    }

                }
            }

            var slotData = new SlotDataResV3
            {
                win = combination.TotalWin,
                symbols = matrix,
                extra = new
                {
                    upperRow = tmpUpperRow,
                    bottomRow = tmpBottomRow,
                    fixedSymbols = fixedSymb.ToArray(),
                    doubleWin = combination.WinFor2 > 0,
                    fixedSymbolsPrevious = fixedSymbPrev.ToArray()
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }
    }
}
