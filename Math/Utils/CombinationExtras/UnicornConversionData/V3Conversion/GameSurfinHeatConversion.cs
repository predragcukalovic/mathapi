using CombinationUnicornUtils;
using CombinationUtils.UnicornCombinationData;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForUnicornGames.GameSurfinHeat;
using System.Collections.Generic;

namespace CombinationExtras.UnicornConversionData.V3Conversion
{
    class GameSurfinHeatConversion
    {
        public static SlotDataResV3 ToSlotDataResV3(ICombination combination, bool isCurrentGameGratis)
        {
            var matrix = new int[5, 3];
            var nearlyMissed = new int[5, 2];
            var thisSpinMult = (combination.AdditionalInformation >> 4);
            var nextSpinMult = (combination.AdditionalInformation & 0x0F);

            for (var i = 0; i < 5; i++)
            {
                nearlyMissed[i, 0] = combination.Matrix[i, 0];
                nearlyMissed[i, 1] = combination.Matrix[i, 4];

                for (var j = 1; j < 4; j++)
                {
                    matrix[i, j - 1] = combination.Matrix[i, j];
                    if (isCurrentGameGratis && matrix[i, j - 1] == 0)
                    {
                        matrix[i, j - 1] = 10 + thisSpinMult;
                    }
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
                    winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = -1 + positions[j] / 5 };
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
                    scatterPrizeThisSpin = thisSpinMult > 0 ? MatrixSurfinHeat.PossibleWinsForScatter[thisSpinMult - 1] * combination.WinFor2 : 0,
                    scatterPrizeNextSpin = nextSpinMult > 0 ? MatrixSurfinHeat.PossibleWinsForScatter[nextSpinMult - 1] * combination.WinFor2 : 0
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }

        public static CombinationUnicorn GetNonWinningCombination(int bet, int numberOfLines)
        {
            var matrixArray = new[,] { { 6, 6, 6, 6, 6 }, { 2, 2, 2, 2, 2 }, { 3, 3, 3, 3, 3 }, { 4, 4, 4, 4, 4 }, { 5, 5, 5, 5, 5 } };
            var matrix = new MatrixSurfinHeat();
            matrix.FromMatrixArraySurfinHeat(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet, 0, 0);
            return combination;
        }
    }
}
