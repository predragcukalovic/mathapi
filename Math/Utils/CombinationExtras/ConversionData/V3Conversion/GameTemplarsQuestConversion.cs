using MathCombination.CombinationData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinationExtras.ConversionData.V3Conversion
{
    class GameTemplarsQuestConversion
    {
        /// <summary>
        /// Pretvara kombinaciju u JSON objekat.
        /// </summary>
        /// <param name="combination"></param>
        /// <param name="numOfGratisGames"></param>
        /// <param name="isCurrentGameGratis"></param>
        /// <returns></returns>
        public static object ToJsonObject(ICombination combination, int numOfGratisGames, bool isCurrentGameGratis)
        {
            object bonusData;
            var knightLastStep = false;
            switch (combination.AdditionalInformation)
            {
                case 1:
                    bonusData = GetCoinBonusObject(combination);
                    break;
                case 2:
                    bonusData = GetKnightBonusObject(combination);
                    knightLastStep = combination.WinFor2 > 0;
                    break;
                default:
                    bonusData = null;
                    break;
            }
            var bonusObject = new
            {
                bonusType = combination.AdditionalInformation,
                bonusData
            };
            var gameData = new List<object> { GetGameData(combination, knightLastStep) };
            gameData.AddRange(combination.CascadeList.Select(t => GetGameData(t, knightLastStep)));
            var obj = new
            {
                numberOfFreeSpins = numOfGratisGames,
                isGratis = isCurrentGameGratis ? 1 : 0,
                bonus = combination.GratisGame ? 1 : 0,
                gamesData = gameData.ToArray(),
                bonusObject
            };
            return obj;
        }

        #region Private Methods

        /// <summary>
        /// Daje podatke o igri kao objekat.
        /// </summary>
        /// <param name="combination"></param>
        /// <param name="knightLastStep"></param>
        /// <returns></returns>
        private static object GetGameData(ICombination combination, bool knightLastStep)
        {
            var tmpMatrixArray = new byte[15];
            var tmpUpperRow = new int[5];
            var tmpBottomRow = new int[5];
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
                noWinLines = combination.NumberOfWinningLines,
                symbols = Array.ConvertAll(tmpMatrixArray, x => (int)x),
                upperRow = tmpUpperRow,
                bottomRow = tmpBottomRow,
                totalSum = combination.TotalWin - (knightLastStep ? combination.WinFor2 : 0),
                winStruct = CommonV3Conversion.ToLineInfoJson(combination.LinesInformation)
            };
            return obj;
        }

        private static object GetKnightBonusObject(ICombination combination)
        {
            object stepData;
            if (combination.AdditionalArray[1] % 3 == 1)
            {
                stepData = new
                {
                    win = combination.AdditionalArray[2]
                };
            }
            else if (combination.AdditionalArray[1] % 3 == 2)
            {
                stepData = new
                {
                    selectedField = combination.AdditionalArray[2],
                    otherFields = new int[] { combination.AdditionalArray[6], combination.AdditionalArray[7], combination.AdditionalArray[8], combination.AdditionalArray[9] }
                };
            }
            else
            {
                stepData = new
                {
                    battleWon = combination.TotalWin > 0
                };
            }
            var bonusData = new
            {
                gameType = (combination.AdditionalArray[1] - 1) % 3 + 1,
                lifesLeft = combination.AdditionalArray[3],
                opponentLifesLeft = combination.AdditionalArray[4],
                lastStepWin = combination.WinFor2,
                stepData
            };
            return bonusData;
        }

        private static object GetCoinBonusObject(ICombination combination)
        {
            var positions = new int[15];
            for (var i = 0; i < 15; i++)
            {
                positions[i] = combination.AdditionalArray[i + 1];
                if (positions[i] == 2)
                {
                    positions[i] = combination.TotalWin > 0 ? 1 : -1;
                }
            }
            var bonusData = new
            {
                fields = positions
            };
            return bonusData;
        }

        #endregion
    }
}
