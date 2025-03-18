using MathCombination.CombinationData;
using System;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameAfricanTreasureConversion
    {
        /// <summary>
        /// Pretvara kombinaciju u JSON objekat.
        /// </summary>
        /// <param name="combination"></param>
        /// <returns></returns>
        public static object ToJsonObject(ICombination combination)
        {
            var tmpMatrixArray = new byte[15];
            var tmpUpperRow = new byte[5];
            var tmpBottomRow = new byte[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    tmpMatrixArray[j * 5 + i] = combination.Matrix[i, j + 1];
                }
                tmpUpperRow[i] = combination.Matrix[i, 0];
                tmpBottomRow[i] = combination.Matrix[i, 4];
            }

            var obj = new TurboHot
            {
                symbols = Array.ConvertAll(tmpMatrixArray, c => (int)c),
                upperRow = Array.ConvertAll(tmpUpperRow, c => (int)c),
                bottomRow = Array.ConvertAll(tmpBottomRow, c => (int)c),
                totalSum = combination.TotalWin,
                noWinLines = combination.NumberOfWinningLines,
                numberOfFreeSpins = combination.NumberOfGratisGames,
                isGratis = false,
                winStruct = CommonV3Conversion.ToLineInfoJson(combination.LinesInformation)
            };
            return obj;
        }
    }
}
