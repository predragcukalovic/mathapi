using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombinationExtras.ReaderData;
using LibraryBuyBonus;
using MathCombination.CombinationData;
using MathForGames.GameBurstingHot5;

namespace BuyWildVeryHot5
{
    public class BuyVeryHot5
    {
        /// <summary>
        /// Daje buy wild kombinaciju za igru VeryHot5.
        /// </summary>
        /// <param name="game">u slucaju kopije ili klona</param>
        /// <param name="buyBonusType"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        public static ICombination GetCombinationVeryHot5(string game, int buyBonusType, int numberOfLines, int bet)
        {
            var reels = MathBuyBonusFilesReader.GetBuyBonusReelsForGame(game);

            if (buyBonusType != 1)
            {
                throw new Exception("Buy Wild Combination" + game + ": Buy Bonus type " + buyBonusType + " not supported!");
            }
            var scatCount = BonusMatrixLibrary.GetRandomDistributionNumber(MathBuyBonusFilesReader.GetBuyBonusProbabilitiesForGame(game)) + 1;
            var matrixArray = BonusMatrixLibrary.ReadDirectedMatrixArrayFromReels(0, scatCount, 3, 3, new[] { false, true, true, true, false }, 0, reels);

            var matrix = new MatrixBurstingHot5();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }
    }
}
