using CombinationExtras.ReaderData;
using GameElGrandeToro;
using LibraryBuyBonus;
using MathCombination.CombinationData;
using System;

namespace BuyBonusElGrandeToro
{
    public class BuyElGrandeToro
    {
        /// <summary>
        /// Daje buy bonus kombinaciju za igru El Grande Toro.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <param name="addArray"></param>
        /// <returns></returns>
        public static ICombination GetCombinationElGrandeToro(string game, int buyBonusType, int numberOfLines, int bet, bool gratisGame, ref byte[] addArray)
        {
            var reels = MathBuyBonusFilesReader.GetBuyBonusReelsForGame(game);

            if (buyBonusType != 1)
            {
                throw new Exception("Buy Bonus Combination" + game + ": Buy bonus type " + buyBonusType + " not supported!");
            }
            var matrixArray = BonusMatrixLibrary.ReadDirectedMatrixArrayFromReels(10, 3, 3, 3, new[] { true, false, true, false, true }, 0, reels);

            if (addArray == null)
            {
                addArray = new byte[15];
            }
            var matrix = new MatrixElGrandeToro();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, ref addArray);
            return combination;
        }
    }
}
