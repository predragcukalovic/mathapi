using GameBookOfDouble;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using System.Collections.Generic;
using System.Linq;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameBookOfDoubleConversion
    {
        public static Combination GetNonWinningCombination(int bet, int numberOfLines, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 9, 8, 7 }, { 6, 5, 4 }, { 3, 2, 1 }, { 8, 7, 6 }, { 5, 4, 9 } };
            var matrix = new MatrixBookOfDouble();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, false, 0);
            return combination;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[5, 3];
            var tmpUpperRow = new int[5];
            var tmpBottomRow = new int[5];
            var transform1 = new int[5];
            var transform2 = new int[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j];
                }
                tmpUpperRow[i] = (combination.Matrix[i, 0] + 5) % 10 == combination.Matrix[i, 1] ? (combination.Matrix[i, 0] + 1) % 10 : (combination.Matrix[i, 0] + 5) % 10;
                tmpBottomRow[i] = (combination.Matrix[i, 2] + 5) % 10 == combination.Matrix[i, 1] ? (combination.Matrix[i, 2] + 1) % 10 : (combination.Matrix[i, 2] + 5) % 10;
                transform1[i] = combination.PositionFor2[i] == 255 ? 0 : (combination.PositionFor2[i] > 1 ? 1 : 0);
                transform2[i] = combination.PositionFor2[i] == 255 ? 0 : (combination.PositionFor2[i] % 2);
            }

            var status = 0;
            var winLineList = new List<WinLineV3>();
            var n = combination.LinesInformation.Length;
            WinLineV3 winLine253 = null;
            WinLineV3 winLine252 = null;
            var position253 = new List<int>();
            var position252 = new List<int>();
            for (var i = 0; i < n; i++)
            {
                if (combination.LinesInformation[i].Id == 253)
                {
                    status = 253;
                    winLine253 = new WinLineV3
                    {
                        lineId = 253,
                        win = 0,
                        soundId = combination.AdditionalInformation >> 4
                    };
                    continue;
                }
                if (combination.LinesInformation[i].Id == 252)
                {
                    status = 252;
                    winLine252 = new WinLineV3
                    {
                        lineId = 252,
                        win = 0,
                        soundId = combination.AdditionalInformation & 0x0F
                    };
                    continue;
                }
                switch (status)
                {
                    case 253:
                        winLine253.win += combination.LinesInformation[i].Win;
                        var index = 0;
                        while (index < 5 && combination.LinesInformation[i].WinningPosition[index] != 255)
                        {
                            position253.Add(combination.LinesInformation[i].WinningPosition[index++]);
                        }
                        break;
                    case 252:
                        winLine252.win += combination.LinesInformation[i].Win;
                        var index1 = 0;
                        while (index1 < 5 && combination.LinesInformation[i].WinningPosition[index1] != 255)
                        {
                            position252.Add(combination.LinesInformation[i].WinningPosition[index1++]);
                        }
                        break;
                    default:
                        winLineList.Add(new WinLineV3
                        {
                            lineId = combination.LinesInformation[i].Id,
                            soundId = combination.LinesInformation[i].WinningElement,
                            win = combination.LinesInformation[i].Win
                        });
                        var positions = new List<int>();
                        var index0 = 0;
                        while (index0 < 5 && combination.LinesInformation[i].WinningPosition[index0] != 255)
                        {
                            positions.Add(combination.LinesInformation[i].WinningPosition[index0++]);
                        }
                        var m = positions.Count;
                        var winSymb = new WinSymbolV3[m];
                        for (var j = 0; j < m; j++)
                        {
                            winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = positions[j] / 5 };
                            winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
                        }
                        winLineList.Last().symbols = winSymb;
                        break;
                }
            }
            if (winLine253 != null)
            {
                position253 = position253.Distinct().ToList();
                var m = position253.Count;
                var winSymb = new WinSymbolV3[m];
                for (var j = 0; j < m; j++)
                {
                    winSymb[j] = new WinSymbolV3 { reel = position253[j] % 5, row = position253[j] / 5 };
                    winSymb[j].id = winLine253.soundId;
                }
                winLine253.symbols = winSymb;
                winLineList.Add(winLine253);
            }
            if (winLine252 != null)
            {
                position252 = position252.Distinct().ToList();
                var m = position252.Count;
                var winSymb = new WinSymbolV3[m];
                for (var j = 0; j < m; j++)
                {
                    winSymb[j] = new WinSymbolV3 { reel = position252[j] % 5, row = position252[j] / 5 };
                    winSymb[j].id = winLine252.soundId;
                }
                winLine252.symbols = winSymb;
                winLineList.Add(winLine252);
            }

            var slotData = new SlotDataResV3
            {
                win = combination.TotalWin,
                symbols = matrix,
                extra = new
                {
                    upperRow = tmpUpperRow,
                    bottomRow = tmpBottomRow,
                    bonusSymbol1 = combination.AdditionalInformation >> 4,
                    bonusSymbol2 = combination.AdditionalInformation & 0x0F,
                    transformReels1 = transform1,
                    transformReels2 = transform2
                },
                wins = winLineList.ToArray(),
                gratisGame = combination.GratisGame
            };

            return slotData;
        }
    }
}
