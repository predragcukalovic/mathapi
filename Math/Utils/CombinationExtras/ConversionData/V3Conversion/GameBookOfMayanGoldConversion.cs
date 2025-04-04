﻿using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.GameBookOfMayanGold;
using RNGUtils.RandomData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameBookOfMayanGoldConversion
    {
        public static Combination GetNonWinningCombination(int bet, int numberOfLines, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 8, 1, 5 }, { 7, 9, 2 }, { 3, 5, 6 }, { 7, 4, 8 }, { 6, 0, 9 } };
            var matrix = new MatrixBookOfMayanGold();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGamesLeft > 0, 0);
            return combination;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[5, 3];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j];
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
                    winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = positions[j] / 5 };
                    winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
                }
                winLine[i].symbols = winSymb;
            }

            long[] respinBets = null;
            if (combination.AdditionalInformation == 0)
            {
                respinBets = new long[5];
                respinBets[4] = 1;
                for (var i = 0; i < 4; i++)
                {
                    respinBets[i] = SoftwareRng.Next(1, 10) * 10; //TODO: ODREDITI PRAVE VREDNOSTI
                }
            }

            var slotData = new SlotDataResV3
            {
                win = combination.TotalWin,
                symbols = matrix,
                extra = new MatrixBookOfMayanGold.BookOfMayanGoldRespinConfig
                {
                    bonusSymbol = combination.AdditionalInformation > 0 ? combination.AdditionalInformation : -1,
                    respinBetsArray = respinBets
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }
    }
}
