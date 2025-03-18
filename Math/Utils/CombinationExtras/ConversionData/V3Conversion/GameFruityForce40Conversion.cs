using CombinationExtras.ConversionData.V3Conversion.OtherStructuresV3;
using Papi.GameServer.Math.Contracts.GameSpecificStructures.FruityForce;
using GameFruityForce;
using GameFruityForce.Config;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using System.Collections.Generic;
using System.Reflection;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameFruityForce40Conversion
    {
        public static Combination GetNonWinningCombination(int bet, int numberOfLines)
        {
            var matrixArray = new[,] { { 3, 5, 5, 6, 4, 3 }, { 3, 8, 1, 8, 7, 3 }, { 3, 3, 3, 1, 7, 3 }, { 3, 2, 2, 8, 7, 3 }, { 3, 4, 4, 6, 6, 3 } };
            var matrix = new MatrixFruityForce();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationFruityForce();
            combination.MatrixToCombinationFruityForce(matrix, numberOfLines, bet, 0, 0, true);
            return combination;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[5, 4];
            var tmpUpperRow = new int[5];
            var tmpBottomRow = new int[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j];
                }
                tmpUpperRow[i] = combination.Matrix[i, 5];
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

            var slotData = new SlotDataResV3
                {
                    win = combination.TotalWin,
                    symbols = matrix,
                    extra = new
                    {
                        upperRow = tmpUpperRow,
                        bottomRow = tmpBottomRow,
                        level = MatrixFruityForce.GetLevel(combination.WinFor2)
        },
                    wins = winLine,
                    gratisGame = false
                };

            return slotData;
        }
        public static List<FruityForceFrontConfig> GenerateLevelFields(FruityForceLevelDataRequestParams betCounters)
        {
            var levelFields = new List<FruityForceFrontConfig>();
            foreach (var betCounter in betCounters.LevelDataRequestParams)
            {
                levelFields.Add(GenerateLevelFieldsFront(betCounter));
            }
            return levelFields;
        }

        public static FruityForceFrontConfig GenerateLevelFieldsFront(FruityForceLevelData betCounter)
        {
            var curGamesPlayed = betCounter.GamesPlayed % 230;
            var prevGamesPlayed = (betCounter.GamesPlayed + 229) % 230;
            var prLevel = MatrixFruityForce.GetLevel(prevGamesPlayed);
            var curLevel = MatrixFruityForce.GetLevel(curGamesPlayed);
            return new FruityForceFrontConfig
            {
                bet = betCounter.Bet,
                gamesPlayed = betCounter.GamesPlayed,
                level = curLevel,
                previousLevel = prLevel,
                maxGamesCount = MatrixFruityForce.LevelCount[curLevel],
                previousMaxGamesCount = MatrixFruityForce.LevelCount[prLevel],
                currentGamesCount = MatrixFruityForce.GetGamesPlayedInLevel(curGamesPlayed),
                previousGamesCount = MatrixFruityForce.GetGamesPlayedInLevel(prevGamesPlayed)
            };
        }

        public static FruityForceConfig GetFruityForceConfigObject(ICombination combination, long betPerLine)
        {
            var prLevel = MatrixFruityForce.GetLevel(combination.WinFor2);
            var curLevel = MatrixFruityForce.GetLevel(combination.AdditionalInformation);
            var totalBet = betPerLine * MatrixFruityForce.PlayLines[0];

            return new FruityForceConfig
            {
                GamesPlayed = combination.AdditionalInformation,
                Level = curLevel,
                Bet =totalBet,
                PreviousLevel = prLevel,
                MaxGamesCount = MatrixFruityForce.LevelCount[curLevel],
                PreviousMaxGamesCount = MatrixFruityForce.LevelCount[prLevel],
                CurrentGamesCount = MatrixFruityForce.GetGamesPlayedInLevel(combination.AdditionalInformation),
                PreviousGamesCount = MatrixFruityForce.GetGamesPlayedInLevel(combination.WinFor2)
            };
        }
    }
}
