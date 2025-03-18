using CombinationExtras.ReaderData;
using GameWildLuckyClover;
using GameWildLuckyClover2;
using LibraryBuyBonus;
using MathCombination.CombinationData;
using System;

namespace BuyBonusWildLuckyClover
{
    public class BuyWildLuckyClover
    {
        /// <summary>
        /// Daje kombinaciju za igru WildLuckyClover.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="buyBonusType"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGames"> </param>
        /// <returns></returns>
        public static ICombination GetCombinationWildLuckyClover(string game, int buyBonusType, int numberOfLines, int bet, bool gratisGame, byte addInfo)
        {
            var reels = MathBuyBonusFilesReader.GetBuyBonusReelsForGame(game);

            if (buyBonusType < 1 || buyBonusType > 3)
            {
                throw new Exception("Buy Bonus Combination" + game + ": Buy bonus type " + buyBonusType + " not supported!");
            }
            var matrixArray = BonusMatrixLibrary.ReadDirectedMatrixArrayFromReels(7, 2 + buyBonusType, 4, 6, new[] { true, true, true, true, true }, 1, reels);

            var matrix = new MatrixWildLuckyClover();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationWildLuckyClover();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, addInfo);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru WildLuckyClover2.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="buyBonusType"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGames"> </param>
        /// <returns></returns>
        public static ICombination GetCombinationWildLuckyClover2(string game, int buyBonusType, int numberOfLines, int bet, bool gratisGame, byte addInfo)
        {
            var reels = MathBuyBonusFilesReader.GetBuyBonusReelsForGame(game);

            if (buyBonusType < 1 || buyBonusType > 3)
            {
                throw new Exception("Buy Bonus Combination" + game + ": Buy bonus type " + buyBonusType + " not supported!");
            }
            var matrixArray = BonusMatrixLibrary.ReadDirectedMatrixArrayFromReels(7, 2 + buyBonusType, 4, 6, new[] { true, true, true, true, true }, 1, reels);

            var matrix = new MatrixWildLuckyClover2();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationWildLuckyClover2();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, addInfo);
            return combination;
        }
    }
}
