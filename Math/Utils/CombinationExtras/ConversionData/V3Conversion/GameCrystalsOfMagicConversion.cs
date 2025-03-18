using MathCombination.CombinationData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinationExtras.ConversionData.V3Conversion
{
    class GameCrystalsOfMagicConversion
    {
        #region Private methods

        private static object GetWildFeatureObject(ICombination combination)
        {
            if (combination.WinFor2 == 11 || combination.WinFor2 == 12)
            {
                var obj = new
                {
                    featureId = combination.WinFor2 - 10,
                    position = new int[] { combination.PositionFor2[0], combination.PositionFor2[1], combination.PositionFor2[2] }
                };
                return obj;
            }
            if (combination.WinFor2 == 13)
            {
                var positionsList = new List<int>();
                for (var i = combination.PositionFor2[0] - 1; i <= combination.PositionFor2[1] - 1; i++)
                {
                    for (var j = 1; j < 4; j++)
                    {
                        positionsList.Add(i * 5 + j);
                    }
                }
                var obj = new
                {
                    featureId = 3,
                    position = positionsList.ToArray()
                };
                return obj;
            }
            return new
            {
                featureId = 0,
                position = new int[0]
            };
        }

        private static object GetFirstBonusObject(ICombination combination)
        {
            var bonusData = new
            {
                extraBonus = (byte)combination.WinFor2
            };
            return bonusData;
        }

        private static object GetSecondBonusObject(ICombination combination)
        {
            var bonusData = new
            {
                stickyWild = Array.ConvertAll(combination.AdditionalArray, c => (int)c)
            };
            return bonusData;
        }

        private static object GetThirdBonusObject(ICombination combination)
        {
            var bonusData = new
            {
                stepID = combination.AdditionalArray[0],
                selectField = combination.AdditionalArray[4],
                firstField = combination.AdditionalArray[5],
                secondField = combination.AdditionalArray[6],
                multiply = combination.AdditionalArray[2],
                multiplyStepLeft = (byte)Math.Max(combination.AdditionalArray[1] - 1, 0),
                lifesLeft = combination.AdditionalArray[3],
                isMultiplier = combination.AdditionalArray[7] > 0,
                lastStepWin = combination.WinFor2
            };
            return bonusData;
        }

        private static object GetFourthBonusObject(ICombination combination)
        {
            var positions = combination.AdditionalArray.Skip(1).Take(8).ToArray();
            var bonusData = new
            {
                stepID = combination.AdditionalArray[0],
                selectedValue = combination.AdditionalArray[14],
                lifes = combination.AdditionalArray[9],
                openField = Array.ConvertAll(positions, x => (int)x)
            };
            return bonusData;
        }

        #endregion

        public static object ToJsonObject(ICombination combination, int numOfGratisGames, bool isCurrentGameGratis)
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
            object bonusData;
            var bonusLastStep = false;
            switch (combination.AdditionalInformation)
            {
                case 1:
                    bonusData = GetFourthBonusObject(combination);
                    break;
                case 2:
                    bonusData = GetThirdBonusObject(combination);
                    bonusLastStep = combination.WinFor2 > 0;
                    break;
                case 3:
                    bonusData = GetFirstBonusObject(combination);
                    break;
                case 4:
                    bonusData = GetSecondBonusObject(combination);
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
            var obj = new
            {
                symbols = Array.ConvertAll(tmpMatrixArray, c => (int)c),
                upperRow = tmpUpperRow,
                bottomRow = tmpBottomRow,
                totalSum = combination.TotalWin - (bonusLastStep ? combination.WinFor2 : 0),
                noWinLines = combination.NumberOfWinningLines,
                numberOfFreeSpins = numOfGratisGames,
                isGratis = isCurrentGameGratis,
                bonus = combination.GratisGame ? 1 : 0,
                winStruct = CommonV3Conversion.ToLineInfoJson(combination.LinesInformation),
                wildFeature = GetWildFeatureObject(combination),
                bonusObject
            };
            return obj;
        }
    }
}
