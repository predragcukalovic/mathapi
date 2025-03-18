using CombinationExtras.ReaderData;
using GameBookOfDouble;
using LibraryBuyBonus;
using MathCombination.CombinationData;
using System;

namespace BuyBonusBookOfLuxorDouble
{
    public class BuyBookOfLuxorDouble
    {
        /// <summary>
        /// Daje buy bonus kombinaciju za igru BookOfLuxorDouble.
        /// </summary>
        /// <param name="game">u slucaju kopije ili klona</param>
        /// <param name="buyBonusType"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <param name="gratisSymbols"> </param>
        /// <returns></returns>
        public static ICombination GetCombinationBookOfLuxor(string game, int buyBonusType, int numberOfLines, int bet, bool gratisGame, byte gratisSymbols)
        {
            var reels = MathBuyBonusFilesReader.GetBuyBonusReelsForGame(game);

            if (buyBonusType != 4)
            {
                throw new Exception("Buy Bonus Combination" + game + ": Buy bonus type " + buyBonusType + " not supported!");
            }
            var scatCount = BonusMatrixLibrary.GetRandomDistributionNumber(MathBuyBonusFilesReader.GetBuyBonusProbabilitiesForGame(game)) + 3;
            var matrixArray = BonusMatrixLibrary.ReadDirectedMatrixArrayFromReels(0, scatCount, 3, 3, new[] { true, true, true, true, true }, 0, reels);

            var matrix = new MatrixBookOfDouble();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, gratisSymbols);
            return combination;
        }
    }
}
