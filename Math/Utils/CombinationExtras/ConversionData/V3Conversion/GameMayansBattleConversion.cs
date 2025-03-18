using MathCombination.CombinationData;
using System;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameMayansBattleConversion
    {
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
                tmpUpperRow[i] = combination.Matrix[i, 0];
                tmpBottomRow[i] = combination.Matrix[i, 2];
            }
            var obj = new
            {
                symbols = Array.ConvertAll(tmpMatrixArray, c => (int)c),
                upperRow = tmpUpperRow,
                bottomRow = tmpBottomRow,
                totalSum = combination.TotalWin,
                noWinLines = combination.NumberOfWinningLines,
                numberOfFreeSpins = numOfGratisGames,
                isGratis = isCurrentGameGratis,
                bonus = combination.GratisGame ? 1 : 0,
                numOfBonus = combination.NumberOfGratisGames,
                winStruct = CommonV3Conversion.ToLineInfoJson(combination.LinesInformation)
            };
            return obj;
        }
    }
}
