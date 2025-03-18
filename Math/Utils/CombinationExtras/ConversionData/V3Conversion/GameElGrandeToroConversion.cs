using GameElGrandeToro;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using System;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameElGrandeToroConversion
    {
        public static Combination GetNonWinningCombination(int bet, int numberOfLines, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 9, 8, 7 }, { 6, 5, 4 }, { 3, 2, 1 }, { 8, 7, 6 }, { 5, 4, 9 } };
            var matrix = new MatrixElGrandeToro();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            var emptyArray = new byte[20];
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGamesLeft > 0, ref emptyArray);
            return combination;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[5, 3];
            var tmpUpperRow = new int[5];
            var tmpBottomRow = new int[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j];
                }
                tmpUpperRow[i] = combination.Matrix[i, 0] == 1 ? 1 : ((combination.Matrix[i, 0] + 5) % 10 == combination.Matrix[i, 1] ? (combination.Matrix[i, 0] + 1) % 10 : (combination.Matrix[i, 0] + 5) % 10);
                tmpBottomRow[i] = combination.Matrix[i, 2] == 1 ? 1 : ((combination.Matrix[i, 2] + 5) % 10 == combination.Matrix[i, 1] ? (combination.Matrix[i, 2] + 1) % 10 : (combination.Matrix[i, 2] + 5) % 10);
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
                    winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = positions[j] / 5 };
                    winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
                }
                winLine[i].symbols = winSymb;
            }

            var wilds = new List<int>();
            for (var i = 0; i < 15; i++)
            {
                if (combination.AdditionalArray[i] != 0)
                {
                    wilds.Add(i);
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
                    wildPosition = wilds.ToArray()
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }

        public static object ToJsonObject(ICombination combination, int numOfGratisGames, bool isCurrentGameGratis)
        {
            var tmpMatrixArray = new byte[15];
            var tmpUpperRow = new int[5];
            var tmpBottomRow = new int[5];
            var tmpStickyWild = new List<int>();
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    tmpMatrixArray[j * 5 + i] = combination.Matrix[i, j];
                }
                tmpUpperRow[i] = combination.Matrix[i, 0] == 1 ? 1 : ((combination.Matrix[i, 0] + 5) % 10 == combination.Matrix[i, 1] ? (combination.Matrix[i, 0] + 1) % 10 : (combination.Matrix[i, 0] + 5) % 10);
                tmpBottomRow[i] = combination.Matrix[i, 2] == 1 ? 1 : ((combination.Matrix[i, 2] + 5) % 10 == combination.Matrix[i, 1] ? (combination.Matrix[i, 2] + 1) % 10 : (combination.Matrix[i, 2] + 5) % 10);
            }
            for (var i = 0; i < 15; i++)
            {
                if (combination.AdditionalArray[i] > 0)
                {
                    tmpStickyWild.Add(i);
                }
            }
            var obj = new
            {
                symbols = Array.ConvertAll(tmpMatrixArray, c => (int)c),
                upperRow = tmpUpperRow,
                bottomRow = tmpBottomRow,
                totalSum = combination.TotalWin,
                noWinLines = combination.NumberOfWinningLines,
                numberOfFreeSpins = numOfGratisGames,
                isGratis = isCurrentGameGratis,
                bonus = combination.GratisGame ? 1 : 0,
                numOfBonus = combination.NumberOfGratisGames,
                winStruct = CommonV3Conversion.ToLineInfoJson(combination.LinesInformation),
                stickyWild = tmpStickyWild.ToArray()
            };
            return obj;
        }
    }
}
