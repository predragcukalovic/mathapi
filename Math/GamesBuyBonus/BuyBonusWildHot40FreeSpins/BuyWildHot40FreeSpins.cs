using CombinationExtras.ReaderData;
using LibraryBuyBonus;
using MathCombination.CombinationData;
using MathForGames.GameTurboHot40;
using System;

namespace BuyBonusWildHot40FreeSpins
{
    public class BuyWildHot40FreeSpins
    {
        /// <summary>
        /// Daje kombinaciju za igru WildHot40FreeSpins.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="buyBonusType"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>\
        /// <returns></returns>
        public static ICombination GetCombinationWildHot40FreeSpins(string game, int buyBonusType, int numberOfLines,
            int bet, bool gratisGame)
        {
            var reels = MathBuyBonusFilesReader.GetBuyBonusReelsForGame(game);

            if (buyBonusType < 1 || buyBonusType > 3)
            {
                throw new Exception("Buy Bonus Combination" + game + ": Buy bonus type " + buyBonusType +
                                    " not supported!");
            }

            var matrixArray = BonusMatrixLibrary.ReadDirectedMatrixArrayFromReels(2, 2 + buyBonusType, 4, 6,
                new[] { true, true, true, true, true }, 1, reels);

            var matrix = new MatrixTurboHot40();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame);
            return combination;
        }
    }
}
