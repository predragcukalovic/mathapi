using System;
using CombinationExtras.ReaderData;
using GameEpicClover40;
using LibraryBuyBonus;
using MathCombination.CombinationData;

namespace BuyWildEpicClover40
{
    public class BuyEpicClover40
    {
        /// <summary>
        /// Daje buy wild kombinaciju za igru EpicClover40.
        /// </summary>
        /// <param name="game">u slucaju kopije ili klona</param>
        /// <param name="buyBonusType"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        public static ICombination GetCombinationEpicClover40(string game, int buyBonusType, int numberOfLines, int bet)
        {
            var reels = MathBuyBonusFilesReader.GetBuyBonusReelsForGame(game);

            if (buyBonusType != 1)
            {
                throw new Exception("Buy Wild Combination" + game + ": Buy Bonus type " + buyBonusType + " not supported!");
            }
            var scatCount = BonusMatrixLibrary.GetRandomDistributionNumber(MathBuyBonusFilesReader.GetBuyBonusProbabilitiesForGame(game)) + 1;
            var matrixArray = BonusMatrixLibrary.ReadDirectedMatrixArrayFromReelsWithPredefinedPositions(0, scatCount,
                4, 6, new[] { false, true, true, true, false }, 0, new[] { 0, 4, 8, 12 }, reels);

            var matrix = new MatrixEpicClover40();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationEpicClover40();
            combination.MatrixToCombinationEpicClover40(matrix, numberOfLines, bet);
            return combination;
        }
    }
}
