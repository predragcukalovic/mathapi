using CombinationExtras.ReaderData;
using LibraryBuyBonus;
using MathCombination.CombinationData;
using NewGameBonusBells;
using System;

namespace BuyBonusBonusBells
{
    public class BuyBonusBells
    {
        /// <summary>
        /// Daje kombinaciju za igru Bonus Bells.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="buyBonusType"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGames"> </param>
        /// <returns></returns>
        public static ICombination GetCombinationBonusBells(string game, int buyBonusType, int numberOfLines, int bet, bool gratisGame)
        {
            var reels = MathBuyBonusFilesReader.GetBuyBonusReelsForGame(game);

            if (buyBonusType != 4)
            {
                throw new Exception("Buy Bonus Combination" + game + ": Buy bonus type " + buyBonusType + " not supported!");
            }
            var scatCount = BonusMatrixLibrary.GetRandomDistributionNumber(MathBuyBonusFilesReader.GetBuyBonusProbabilitiesForGame(game)) + 3;
            var matrixArray = BonusMatrixLibrary.ReadDirectedMatrixArrayFromReels(9, scatCount, 3, 3, new[] { true, true, true, true, true }, 0, reels);


            var matrix = new MatrixNewBonusBells();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationNewBonusBells();
            combination.MatrixToCombinationNew(matrix, numberOfLines, bet, gratisGame);
            return combination;
        }
    }
}
