using GameToxicHaze;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameToxicHazeConversion
    {
        public static SlotDataResV3 ToSlotDataResV3(ICombination combination, int gratisGamesLeft, bool isCurrentGameGratis)
        {
            var matrix = new int[5, 5];
            var recallMatrix = new int[5, 5];
            var tmpUpperRow = new int[5];
            var tmpBottomRow = new int[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j + 1];
                    recallMatrix[i, j] = matrix[i, j];
                }
                tmpUpperRow[i] = combination.Matrix[i, 0];
                tmpBottomRow[i] = combination.Matrix[i, 6];
            }
            if (isCurrentGameGratis)
            {
                switch (gratisGamesLeft)
                {
                    case 0:
                        recallMatrix[2, 2] = 14;
                        break;
                    case 1:
                    case 3:
                        recallMatrix[1, 1] = recallMatrix[1, 3] = recallMatrix[3, 1] = recallMatrix[3, 3] = 13;
                        break;
                    case 2:
                        recallMatrix[0, 0] = recallMatrix[0, 4] = recallMatrix[4, 0] = recallMatrix[4, 4] = 13;
                        break;
                    case 4:
                        recallMatrix[2, 2] = 13;
                        break;
                    case 5:
                        recallMatrix[1, 1] = recallMatrix[1, 3] = recallMatrix[3, 1] = recallMatrix[3, 3] = 12;
                        break;
                    case 6:
                        recallMatrix[0, 0] = recallMatrix[0, 4] = recallMatrix[4, 0] = recallMatrix[4, 4] = 12;
                        break;
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
                    winSymb[j].id = recallMatrix[winSymb[j].reel, winSymb[j].row];
                }
                winLine[i].symbols = winSymb;
            }

            var slotData = new SlotDataResV3
            {
                win = combination.TotalWin,
                symbols = matrix,
                extra = new
                {
                    upperRow = tmpUpperRow,
                    bottomRow = tmpBottomRow,
                    recall = recallMatrix
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }

        public static Combination GetNonWinningCombination(int bet, int numberOfLines, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 8, 8, 8, gratisGamesLeft > 0 ? 8 : 11, 7, 7, 7 }, { 10, 10, 10, 10, 1, 1, 1 }, { 5, 5, 5, 6, 6, 6, 6 }, { 9, 9, 9, 4, 4, 4, 4 }, { 2, 2, 2, 3, 3, 0, 0 } };
            var matrix = new MatrixToxicHaze();
            matrix.FromMatrixArrayToxicHaze(matrixArray);
            var combination = new CombinationToxicHaze();
            combination.MatrixToCombinationToxicHaze(matrix, numberOfLines, bet, gratisGamesLeft);
            return combination;
        }
    }
}
