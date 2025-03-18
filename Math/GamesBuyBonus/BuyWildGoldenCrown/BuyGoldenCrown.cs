using System;
using CombinationExtras.ReaderData;
using LibraryBuyBonus;
using MathCombination.CombinationData;
using MathForGames.GameGoldenCrown;

namespace BuyWildGoldenCrown
{
    public class BuyGoldenCrown
    {
        /// <summary>
        /// Daje buy wild kombinaciju za igru GoldenCrown.
        /// </summary>
        /// <param name="game">u slucaju kopije ili klona</param>
        /// <param name="buyBonusType"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        public static ICombination GetCombinationGoldenCrown(string game, int buyBonusType, int numberOfLines, int bet)
        {
            var reels = MathBuyBonusFilesReader.GetBuyBonusReelsForGame(game);

            if (buyBonusType != 1)
            {
                throw new Exception("Buy Wild Combination" + game + ": Buy Bonus type " + buyBonusType + " not supported!");
            }
            var scatCount = BonusMatrixLibrary.GetRandomDistributionNumber(MathBuyBonusFilesReader.GetBuyBonusProbabilitiesForGame(game)) + 1;
            var matrixArray = BonusMatrixLibrary.ReadDirectedMatrixArrayFromReels(0, scatCount, 3, 3, new[] { false, true, true, true, false}, 0, reels);

            var matrix = new MatrixGoldenCrown();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }
    }
}
