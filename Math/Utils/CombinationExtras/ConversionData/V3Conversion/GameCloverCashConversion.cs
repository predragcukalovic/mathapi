using GameCloverHit;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.GameCloverCash;
using System.Collections.Generic;
using System.Linq;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameCloverCashConversion
    {
        public static Combination GetNonWinningCombination(int bet, int numberOfLines, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 8, 8, 8 }, { 7, 7, 7 }, { 6, 6, 6 }, { 10, 10, 10 }, { 9, 9, 9 } };
            if (gratisGamesLeft > 0)
            {
                matrixArray = new[,] { { 12, 12, 12 }, { 12, 12, 12 }, { 12, 12, 12 }, { 12, 12, 12 }, { 12, 12, 12 } };
            }
            var matrix = new MatrixCloverCash();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationCloverHit();
            var tmpArray = new byte[17];
            combination.MatrixToCombinationCloverHit(matrix, numberOfLines, bet, gratisGamesLeft > 0, ref tmpArray);
            return combination;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination, bool isCurrentGameGratis)
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
                tmpUpperRow[i] = combination.Matrix[i, 0];
                tmpBottomRow[i] = combination.Matrix[i, 2];
            }
            var n = combination.LinesInformation.Length;
            var winLine = new WinLineV3[n];
            for (var i = 0; i < n; i++)
            {
                winLine[i] = new WinLineV3
                {
                    lineId = combination.LinesInformation[i].Id == 252 ? 253 : combination.LinesInformation[i].Id,
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
            var coinsArray = new int[15];
            if (isCurrentGameGratis || combination.GratisGame || combination.LinesInformation.Any(x => x.Id == 252))
            {
                for (var i = 0; i < 15; i++)
                {
                    coinsArray[i] = combination.AdditionalArray[i] == 0 ? -1 : MatrixCloverCash.GetWinByIndex(combination.AdditionalArray[i] - 1, combination.AdditionalArray[16]) * combination.WinFor2;
                }
            }
            else
            {
                var table = MatrixCloverCash.ChooseTable();
                for (var i = 0; i < 15; i++)
                {
                    coinsArray[i] = combination.Matrix[i % 5, i / 5] == 11 ? MatrixCloverCash.GetWinByIndex(MatrixCloverCash.GetRandomIndexByTable(table), table) * combination.WinFor2 : -1;
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
                    coins = coinsArray,
                    gratisDisplay = combination.GratisGame ? (int)combination.AdditionalArray[15] : 0
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }

        public static SlotDataResV3 ToSlotDataResV3SuperLucky(ICombination combination, bool isCurrentGameGratis)
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
                tmpUpperRow[i] = combination.Matrix[i, 0];
                tmpBottomRow[i] = combination.Matrix[i, 2];
            }
            var n = combination.LinesInformation.Length;
            var winLine = new WinLineV3[n];
            for (var i = 0; i < n; i++)
            {
                winLine[i] = new WinLineV3
                {
                    lineId = combination.LinesInformation[i].Id == 252 ? 253 : combination.LinesInformation[i].Id,
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
            var coinsArray = new int[15];
            if (isCurrentGameGratis || combination.GratisGame || combination.LinesInformation.Any(x => x.Id == 252))
            {
                for (var i = 0; i < 15; i++)
                {
                    coinsArray[i] = combination.AdditionalArray[i] == 0 ? -1 : MatrixCloverCash.GetWinByIndex(combination.AdditionalArray[i] - 1, combination.AdditionalArray[16]) * combination.WinFor2 / 2;
                }
            }
            else
            {
                var table = MatrixCloverCash.ChooseTable();
                for (var i = 0; i < 15; i++)
                {
                    coinsArray[i] = combination.Matrix[i % 5, i / 5] == 11 ? MatrixCloverCash.GetWinByIndex(MatrixCloverCash.GetRandomIndexByTable(table), table) * combination.WinFor2 / 2 : -1;
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
                    coins = coinsArray,
                    gratisDisplay = combination.GratisGame ? (int)combination.AdditionalArray[15] : 0
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }
    }
}
