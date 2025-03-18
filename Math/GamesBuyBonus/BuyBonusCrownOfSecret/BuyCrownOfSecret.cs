using CombinationExtras.ReaderData;
using GameCrownOfSecret;
using LibraryBuyBonus;
using MathCombination.CombinationData;
using System;

namespace BuyBonusCrownOfSecret
{
    public class BuyCrownOfSecret
    {
        /// <summary>
        /// Daje kombinaciju za igru Crown Of Secret.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="buyBonusType"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <returns></returns>
        public static ICombination GetCombinationCrownOfSecret(string game, int buyBonusType, int numberOfLines,
            int bet, bool gratisGame, ref byte[] addArray)
        {
            var reels = MathBuyBonusFilesReader.GetBuyBonusReelsForGame(game);

            // 1 for 3 symbols, 2 for 4, 3 for 5
            if (buyBonusType < 1 || buyBonusType > 3)
            {
                throw new Exception("Buy Bonus Combination" + game + ": Buy bonus type " + buyBonusType +
                                    " not supported!");
            }

            var matrixArray = BonusMatrixLibrary.ReadDirectedMatrixArrayFromReels(9, 2 + buyBonusType, 3, 3,
                new[] { true, true, true, true, true }, 0, reels);


            var matrix = new MatrixCrownOfSecret();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationCrownOfSecret();
            combination.MatrixToCombinationCrownOfSecret(matrix, bet, gratisGame, numberOfLines, ref addArray);
            return combination;
        }
    }
}