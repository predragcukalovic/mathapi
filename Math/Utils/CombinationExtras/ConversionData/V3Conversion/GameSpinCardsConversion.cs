using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.GamePokerSlot;
using System;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameSpinCardsConversion
    {
        public static Combination GetNonWinningCombination(int bet, int numberOfLines, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 0, 12, 3, 11, 30 }, { 17, 10, 46, 15, 2 }, { 22, 37, 28, 50, 40 }, { 43, 5, 33, 19, 25 }, { 6, 1, 44, 47, 38 } };
            var matrix = new MatrixPokerSlot();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGamesLeft > 0, false);
            return combination;
        }

        /// <summary>
        /// Pretvara kombinaciju u JSON objekat.
        /// </summary>
        /// <param name="combination"></param>
        /// <returns></returns>
        public static object ToJsonObject(ICombination combination)
        {
            var tmpMatrixArray = new byte[15];
            var tmpUpperRow = new byte[5];
            var tmpBottomRow = new byte[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    tmpMatrixArray[j * 5 + i] = combination.Matrix[i, j];
                }
                tmpUpperRow[i] = combination.Matrix[i, 4];
                tmpBottomRow[i] = combination.Matrix[i, 3];
            }
            var obj = new
            {
                symbols = Array.ConvertAll(tmpMatrixArray, c => (int)c),
                upperRow = Array.ConvertAll(tmpUpperRow, c => (int)c),
                bottomRow = Array.ConvertAll(tmpBottomRow, c => (int)c),
                totalSum = combination.TotalWin,
                noWinLines = combination.NumberOfWinningLines,
                numberOfFreeSpins = combination.NumberOfGratisGames,
                isGratis = combination.GratisGame ? 1 : 0,
                winStruct = CommonV3Conversion.ToLineInfoJson(combination.LinesInformation)
            };
            return obj;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[5, 3];
            var matrixPokerSlot = new MatrixPokerSlot();
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    matrix[i, j] = combination.WinFor2 == 1 && combination.Matrix[i, j] == 52
                        ? 53
                        : combination.Matrix[i, j];
                    matrixPokerSlot.SetElement(i, j, combination.Matrix[i, j]);
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
                for (var j = 0; j < 5; j++)
                {
                    if (combination.LinesInformation[i].WinningPosition[j] != 255)
                    {
                        positions.Add(combination.LinesInformation[i].WinningPosition[j]);
                    }
                }
                var m = positions.Count;
                var winSymb = new WinSymbolV3[m];
                for (var j = 0; j < m; j++)
                {
                    winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = positions[j] / 5 };
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
                    possibleWinPositions = matrixPokerSlot.GetPotentionalWinObject(combination.AdditionalInformation, combination.WinFor2 == 1)
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }
    }
}
