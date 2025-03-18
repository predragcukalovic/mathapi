using CombinationExtras.ReaderData;
using GameKingOfThunder;
using LibraryBuyBonus;
using MathCombination.CombinationData;
using System;

namespace BuyBonusKingOfThunder
{
    public class BuyKingOfThunder
    {
        /// <summary>
        /// Daje kombinaciju za igru KingOfThunder.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="buyBonusType"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGames"> </param>
        /// <returns></returns>
        public static ICombination GetCombinationKingOfThunder(string game, int buyBonusType, int numberOfLines, int bet, int gratisGames)
        {
            var reels = MathBuyBonusFilesReader.GetBuyBonusReelsForGame(game);

            if (buyBonusType != 4)
            {
                throw new Exception("Buy Bonus Combination" + game + ": Buy bonus type " + buyBonusType + " not supported!");
            }

            var scatCount = BonusMatrixLibrary.GetRandomDistributionNumber(MathBuyBonusFilesReader.GetBuyBonusProbabilitiesForGame(game)) + 3;
            var matrixArray = BonusMatrixLibrary.ReadDirectedMatrixArrayFromReels(2, scatCount, 3, 3, new[] { true, true, true, true, true }, 0, reels);

            var matrix = new MatrixKingOfThunder();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationKingOfThunder();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGames);
            return combination;
        }
    }
}
