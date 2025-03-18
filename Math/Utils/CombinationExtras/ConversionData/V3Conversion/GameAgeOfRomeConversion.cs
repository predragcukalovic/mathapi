using GameAgeOfRome;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameAgeOfRomeConversion
    {
        public static Combination GetNonWinningCombination(int bet, int numberOfLines, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 6, 4, 3 }, { 11, 9, 1 }, { 8, 0, 10 }, { 3, 5, 11 }, { 2, 1, 7 } };
            var matrix = new MatrixAgeOfRome();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationAgeOfRome();
            var tmpArray = new byte[16];
            combination.MatrixToCombinationAgeOfRome(matrix, numberOfLines, bet, gratisGamesLeft > 0, ref tmpArray);
            return combination;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination, bool isCurrentGameGratis)
        {
            var matrix = new int[5, 3];
            var recallMatrix = new int[5, 3];
            var tmpUpperRow = new int[5];
            var tmpBottomRow = new int[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j];
                    recallMatrix[i, j] = matrix[i, j];
                }
                tmpUpperRow[i] = (matrix[i, 0] + matrix[i, 1] + 4) % 11;
                tmpBottomRow[i] = (matrix[i, 2] + matrix[i, 1] + 5) % 11;
            }
            var winLine = new WinLineV3[0];
            var stickySymbols = new List<WinSymbolV3>();
            for (var i = 0; i < 15; i++)
            {
                if (combination.AdditionalArray[i] != 0)
                {
                    recallMatrix[i % 5, i / 5] = combination.AdditionalArray[i] - 1;
                    stickySymbols.Add(new WinSymbolV3 { reel = i % 5, row = i / 5, id = combination.AdditionalArray[i] - 1 });
                }
            }
            if (combination.WinFor2 > 0)
            {
                var n = combination.LinesInformation.Length;
                winLine = new WinLineV3[n];
                for (var i = 0; i < n; i++)
                {
                    winLine[i] = new WinLineV3
                    {
                        lineId = combination.LinesInformation[i].Id,
                        soundId = combination.LinesInformation[i].WinningElement,
                        win = combination.LinesInformation[i].Win
                    };
                    var positions = new List<int>();
                    foreach (var pos in combination.LinesInformation[i].WinningPosition)
                    {
                        positions.Add(pos);
                    }
                    var m = positions.Count;
                    var winSymb = new WinSymbolV3[m];
                    for (var j = 0; j < m; j++)
                    {
                        winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = positions[j] / 5 };
                        winSymb[j].id = recallMatrix[winSymb[j].reel, winSymb[j].row];
                        winSymb[j].value = winSymb[j].id == 0 ? combination.PositionFor2[winLine[i].lineId] : 0;
                    }
                    winLine[i].symbols = winSymb;
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
                    recall = recallMatrix,
                    sticky = stickySymbols.ToArray(),
                    lastRespin = combination.WinFor2
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }
    }
}
