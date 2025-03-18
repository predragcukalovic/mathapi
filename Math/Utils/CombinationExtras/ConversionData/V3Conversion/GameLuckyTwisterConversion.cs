using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.GameLuckyTwister;
using System;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameLuckyTwisterConversion
    {
        public static Combination GetNonWinningCombination(int bet)
        {
            var matrixArray = new[,] { { 0, 1, 2, 3, 4, 5, 6 }, { 7, 8, 0, 1, 2, 3, 4 }, { 5, 6, 7, 8, 0, 1, 2 }, { 3, 4, 5, 6, 7, 8, 0 }, { 1, 2, 3, 4, 5, 6, 7 }, { 8, 0, 1, 2, 3, 4, 5 } };
            var matrix = new MatrixLuckyTwister();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, bet);
            return combination;
        }

        /// <summary>
        /// Pretvara kombinaciju u JSON objekat.
        /// </summary>
        /// <param name="combination"></param>
        /// <returns></returns>
        public static object ToJsonObject(ICombination combination)
        {
            var tmpMatrixArray = new byte[30];
            var tmpUpperRow = new byte[6];
            var tmpBottomRow = new byte[6];
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    tmpMatrixArray[j * 6 + i] = combination.Matrix[i, j + 1];
                }
                tmpUpperRow[i] = combination.Matrix[i, 0];
                tmpBottomRow[i] = combination.Matrix[i, 6];
            }
            var obj = new
            {
                symbols = Array.ConvertAll(tmpMatrixArray, c => (int)c),
                upperRow = Array.ConvertAll(tmpUpperRow, c => (int)c),
                bottomRow = Array.ConvertAll(tmpBottomRow, c => (int)c),
                totalSum = combination.TotalWin,
                noWinLines = combination.NumberOfWinningLines,
                firstTwinReel = combination.WinFor2,
                lastTwinReel = combination.WinFor2 + combination.AdditionalInformation - 1,
                winStruct = CommonV3Conversion.ToLineInfoJson(combination.LinesInformation)
            };
            return obj;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[6, 5];
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j + 1];
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
                while (index < 30 && combination.LinesInformation[i].WinningPosition[index] != 255)
                {
                    positions.Add(combination.LinesInformation[i].WinningPosition[index++]);
                }
                var m = positions.Count;
                var winSymb = new WinSymbolV3[m];
                for (var j = 0; j < m; j++)
                {
                    winSymb[j] = new WinSymbolV3 { reel = positions[j] % 6, row = positions[j] / 6 };
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
                    firstTwinReel = combination.WinFor2,
                    lastTwinReel = combination.WinFor2 + combination.AdditionalInformation - 1,
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }
    }
}
