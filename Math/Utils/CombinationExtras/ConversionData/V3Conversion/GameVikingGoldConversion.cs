using MathCombination.CombinationData;
using RNGUtils.RandomData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinationExtras.ConversionData.V3Conversion
{
    class GameVikingGoldConversion
    {
        /// <summary>
        /// Daje podatke o igri kao objekat.
        /// </summary>
        /// <param name="combination"></param>
        /// <param name="isGratis"></param>
        /// <returns></returns>
        private static object GetGameData(ICombination combination, bool isGratis)
        {
            var tmpMatrixArray = new byte[15];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    tmpMatrixArray[j * 5 + i] = combination.Matrix[i, j];
                }
            }
            var fieldsToRemove = new List<int>();
            for (var i = 0; i < 15; i++)
            {
                if (combination.AdditionalArray[15 + i + (isGratis ? 38 : 0)] > 0)
                {
                    fieldsToRemove.Add(i);
                }
            }
            var obj = new
            {
                noWinLines = combination.NumberOfWinningLines,
                symbols = Array.ConvertAll(tmpMatrixArray, x => (int)x),
                totalSum = combination.TotalWin,
                winStruct = CommonV3Conversion.ToLineInfoJson(combination.LinesInformation),
                cascadeIndices = fieldsToRemove.ToArray()
            };
            return obj;
        }

        public static object ToJsonObject(ICombination combination, int numOfGratisGames, bool isCurrentGameGratis)
        {
            var gameData = new List<object> { GetGameData(combination, isCurrentGameGratis) };
            gameData.AddRange(combination.CascadeList.Select(t => GetGameData(t, isCurrentGameGratis)));

            var obj = new
            {
                numberOfFreeSpins = numOfGratisGames,
                isGratis = isCurrentGameGratis ? 1 : 0,
                bonus = combination.GratisGame ? 1 : 0,
                gamesData = gameData.ToArray()
            };
            return obj;
        }

        #region GamePowerOfTheGreat

        /*/// <summary>
        /// Daje podatke o igri kao objekat.
        /// </summary>
        /// <param name="combination"></param>
        /// <param name="isGratis"></param>
        /// <returns></returns>
        private static object GetGameDataPowerOfTheGreat(ICombination combination, bool isGratis)
        {
            var tmpMatrixArray = new byte[15];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    tmpMatrixArray[j * 5 + i] = combination.Matrix[i, j];
                }
            }
            var fieldsToRemove = new List<int>();
            for (var i = 0; i < 15; i++)
            {
                if (combination.AdditionalArray[15 + i + (isGratis ? 38 : 0)] > 0)
                {
                    fieldsToRemove.Add(i);
                }
            }
            var obj = new
            {
                noWinLines = combination.NumberOfWinningLines,
                symbols = Array.ConvertAll(tmpMatrixArray, x => (int)x),
                totalSum = combination.TotalWin,
                winStruct = CommonV3Conversion.ToLineInfoJson(combination.LinesInformation),
                cascadeIndices = fieldsToRemove.ToArray()
            };
            return obj;
        }*/

        public static object ToJsonObjectPowerOfTheGreat(ICombination combination, int numOfGratisGames, bool isCurrentGameGratis)
        {
            var bottomRowTmp = new int[5];
            var topRowTmp = new int[5];
            var lastMatrix = (combination.CascadeList.Count > 0) ? combination.CascadeList.Last().Matrix : combination.Matrix;
            for (var i = 0; i < 5; i++)
            {
                do
                {
                    bottomRowTmp[i] = (int)SoftwareRng.Next(2, 9);
                } while (combination.Matrix[i, 2] == bottomRowTmp[i] || combination.Matrix[i, 1] == bottomRowTmp[i]);
                if (i < 3 && combination.Matrix[i, 1] != 1 && combination.Matrix[i, 2] != 1 && SoftwareRng.Next(10) == 0)
                {
                    bottomRowTmp[i] = 1;
                }
                if (i > 0 && i < 4 && combination.Matrix[i, 1] != 0 && combination.Matrix[i, 2] != 0 && SoftwareRng.Next(10) == 0)
                {
                    bottomRowTmp[i] = 0;
                }
                topRowTmp[i] = (int)SoftwareRng.Next(2, 9);
                if (i < 3 && lastMatrix[i, 0] != 1 && lastMatrix[i, 1] != 1 && SoftwareRng.Next(10) == 0)
                {
                    topRowTmp[i] = 1;
                }
                if (i > 0 && i < 4 && lastMatrix[i, 0] != 0 && SoftwareRng.Next(10) == 0)
                {
                    topRowTmp[i] = 0;
                }
            }
            var gameData = new List<object> { GetGameData(combination, isCurrentGameGratis) };
            gameData.AddRange(combination.CascadeList.Select(t => GetGameData(t, isCurrentGameGratis)));

            var obj = new
            {
                numberOfFreeSpins = numOfGratisGames,
                isGratis = isCurrentGameGratis ? 1 : 0,
                bonus = combination.GratisGame ? 1 : 0,
                bottomRow = bottomRowTmp,
                topRow = topRowTmp,
                gamesData = gameData.ToArray()
            };
            return obj;
        }

        #endregion
    }
}
