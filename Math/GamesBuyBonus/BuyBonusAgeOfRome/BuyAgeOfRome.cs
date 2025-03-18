using CombinationExtras.ReaderData;
using GameAgeOfRome;
using LibraryBuyBonus;
using MathCombination.CombinationData;
using System;

namespace BuyBonusAgeOfRome
{
    public class BuyAgeOfRome
    {
        /// <summary>
        /// Daje buy bonus kombinaciju za igru Age Of Rome.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <param name="addArray"></param>
        /// <returns></returns>
        public static ICombination GetCombinationAgeOfRome(string game, int buyBonusType, int numberOfLines, int bet, bool gratisGame, ref byte[] addArray)
        {
            var reels = MathBuyBonusFilesReader.GetBuyBonusReelsForGame(game);

            if (buyBonusType != 1)
            {
                throw new Exception("Buy Bonus Combination" + game + ": Buy bonus type " + buyBonusType + " not supported!");
            }
            var matrixArray = BonusMatrixLibrary.ReadDirectedMatrixArrayFromReels(11, 3, 3, 3, new[] { false, true, true, true, false }, 0, reels);

            if (addArray == null)
            {
                addArray = new byte[16];
            }
            var matrix = new MatrixAgeOfRome();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationAgeOfRome();
            combination.MatrixToCombinationAgeOfRome(matrix, numberOfLines, bet, gratisGame, ref addArray);
            return combination;
        }
    }
}
