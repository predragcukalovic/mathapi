using MathCombination.CombinationData;
using System;

namespace CombinationExtras.ConversionData.V3Conversion
{
    #region Data structure

    public class TurboHotClover
    {
        public int[] symbols { get; set; }
        public int[] upperRow { get; set; }
        public int[] bottomRow { get; set; }
        public int totalSum { get; set; }
        public byte noWinLines { get; set; }
        public int numberOfFreeSpins { get; set; }
        public bool isGratis { get; set; }
        public int numOfBonus { get; set; }
        public int bonus { get; set; }
        public int wildSymbol { get; set; }
        public LineInfoJson[] winStruct { get; set; }
    }

    #endregion Data structure

    class GameWildLuckyCloverConversion
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
            var tmpMatrixArray = new byte[20];
            var tmpUpperRow = new byte[5];
            var tmpBottomRow = new byte[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    tmpMatrixArray[j * 5 + i] = (byte)((isCurrentGameGratis && combination.Matrix[i, j] == combination.AdditionalInformation) ? combination.Matrix[i, j] + 4 : combination.Matrix[i, j]);
                }
                tmpUpperRow[i] = (byte)((isCurrentGameGratis && combination.Matrix[i, 5] == combination.AdditionalInformation) ? combination.Matrix[i, 5] + 4 : combination.Matrix[i, 5]);
                tmpBottomRow[i] = (byte)((isCurrentGameGratis && combination.Matrix[i, 4] == combination.AdditionalInformation) ? combination.Matrix[i, 4] + 4 : combination.Matrix[i, 4]);
            }

            var obj = new TurboHotClover
            {
                symbols = Array.ConvertAll(tmpMatrixArray, c => (int)c),
                upperRow = Array.ConvertAll(tmpUpperRow, c => (int)c),
                bottomRow = Array.ConvertAll(tmpBottomRow, c => (int)c),
                totalSum = combination.TotalWin,
                noWinLines = combination.NumberOfWinningLines,
                numberOfFreeSpins = numOfGratisGames,
                isGratis = isCurrentGameGratis,
                numOfBonus = combination.NumberOfGratisGames,
                bonus = combination.GratisGame ? 1 : 0,
                wildSymbol = combination.AdditionalInformation,
                winStruct = CommonV3Conversion.ToLineInfoJson(combination.LinesInformation)
            };
            return obj;
        }

        /// <summary>
        /// Pretvara kombinaciju u JSON objekat.
        /// </summary>
        /// <param name="combination"></param>
        /// <param name="numOfGratisGames"></param>
        /// <param name="isCurrentGameGratis"></param>
        /// <returns></returns>
        public static object ToJsonObject2(ICombination combination, int numOfGratisGames, bool isCurrentGameGratis)
        {
            var tmpMatrixArray = new byte[20];
            var tmpUpperRow = new byte[5];
            var tmpBottomRow = new byte[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    tmpMatrixArray[j * 5 + i] = (byte)((isCurrentGameGratis && combination.Matrix[i, j] == combination.AdditionalInformation) ? combination.Matrix[i, j] + 8 : (isCurrentGameGratis && combination.Matrix[i, j] == 0) ? 8 : combination.Matrix[i, j]);
                }
                tmpUpperRow[i] = (byte)((isCurrentGameGratis && combination.Matrix[i, 5] == combination.AdditionalInformation) ? combination.Matrix[i, 5] + 8 : (isCurrentGameGratis && combination.Matrix[i, 5] == 0) ? 8 : combination.Matrix[i, 5]);
                tmpBottomRow[i] = (byte)((isCurrentGameGratis && combination.Matrix[i, 4] == combination.AdditionalInformation) ? combination.Matrix[i, 4] + 8 : (isCurrentGameGratis && combination.Matrix[i, 4] == 0) ? 8 : combination.Matrix[i, 4]);
            }

            var obj = new TurboHotClover
            {
                symbols = Array.ConvertAll(tmpMatrixArray, c => (int)c),
                upperRow = Array.ConvertAll(tmpUpperRow, c => (int)c),
                bottomRow = Array.ConvertAll(tmpBottomRow, c => (int)c),
                totalSum = combination.TotalWin,
                noWinLines = combination.NumberOfWinningLines,
                numberOfFreeSpins = numOfGratisGames,
                isGratis = isCurrentGameGratis,
                numOfBonus = combination.NumberOfGratisGames,
                bonus = combination.GratisGame ? 1 : 0,
                wildSymbol = combination.AdditionalInformation,
                winStruct = CommonV3Conversion.ToLineInfoJson(combination.LinesInformation)
            };
            return obj;
        }
    }
}
