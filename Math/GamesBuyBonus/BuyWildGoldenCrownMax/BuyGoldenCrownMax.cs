using System;
using CombinationExtras.ReaderData;
using GoldenCrownMax;
using LibraryBuyBonus;
using MathCombination.CombinationData;

namespace BuyWildGoldenCrownMax
{
    public class BuyGoldenCrownMax
    {
        /// <summary>
        /// Daje buy wild kombinaciju za igru GoldenCrownMax.
        /// </summary>
        /// <param name="game">u slucaju kopije ili klona</param>
        /// <param name="buyBonusType"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        public static ICombination GetCombinationGoldenCrownMax(string game, int buyBonusType, int numberOfLines, int bet)
        {
            var reels = MathBuyBonusFilesReader.GetBuyBonusReelsForGame(game);

            if (buyBonusType != 1)
            {
                throw new Exception("Buy Wild Combination" + game + ": Buy Bonus type " + buyBonusType +
                                    " not supported!");
            }

            var scatCount = BonusMatrixLibrary.GetRandomDistributionNumber(MathBuyBonusFilesReader.GetBuyBonusProbabilitiesForGame(game)) + 1;
            var matrixArray = BonusMatrixLibrary.ReadDirectedMatrixArrayFromReelsWithPredefinedPositions(0, scatCount,
                4, 6, new[] { false, true, true, true, false }, 0, new[] { 0, 4, 8, 12 }, reels);

            var matrix = new MatrixGoldenCrownMax();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationGoldenCrownMax();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }
    }
}