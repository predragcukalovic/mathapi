using MathCombination.CombinationData;
using System;

namespace CombinationExtras.ConversionData.V3Conversion
{
    class GameHeatingFruitsConversion
    {
        /// <summary>
        /// Pretvara kombinaciju u JSON objekat.
        /// </summary>
        /// <param name="combination"></param>
        /// <returns></returns>
        public static object ToJsonObject(ICombination combination)
        {
            var tmpMatrixArray = new byte[20];
            var tmpUpperRow = new byte[5];
            var tmpBottomRow = new byte[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    tmpMatrixArray[j * 5 + i] = combination.Matrix[i, j];
                }
                tmpUpperRow[i] = combination.Matrix[i, 5];
                tmpBottomRow[i] = combination.Matrix[i, 4];
            }

            var obj = new TurboHotMax
            {
                symbols = Array.ConvertAll(tmpMatrixArray, c => (int)c),
                upperRow = Array.ConvertAll(tmpUpperRow, c => (int)c),
                bottomRow = Array.ConvertAll(tmpBottomRow, c => (int)c),
                totalSum = combination.TotalWin,
                noWinLines = combination.NumberOfWinningLines,
                numberOfFreeSpins = combination.NumberOfGratisGames,
                isGratis = false,
                winStruct = CommonV3Conversion.ToLineInfoJson(combination.LinesInformation),
                expandWild = Array.ConvertAll(combination.PositionFor2, c => (int)c)
            };
            return obj;
        }
    }
}

