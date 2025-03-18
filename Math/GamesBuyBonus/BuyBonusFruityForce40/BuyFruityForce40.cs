using CombinationExtras.ReaderData;
using GameFruityForce;
using LibraryBuyBonus;
using MathCombination.CombinationData;
using System;

namespace BuyBonusFruityForce40
{
    public class BuyFruityForce40
    {
        private static bool ValidateBonusType(int gamesPlayed, int bonusType)
        {
            if (gamesPlayed >= 0 && gamesPlayed < 120 && bonusType == 120 - gamesPlayed)
            {
                return true;
            }
            if (gamesPlayed >= 120 && gamesPlayed < 180 && bonusType == 180 + 120 - gamesPlayed)
            {
                return true;
            }
            if (gamesPlayed >= 180 && gamesPlayed < 225 && bonusType == 181)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Daje kombinaciju za igru FruityForce40.
        /// </summary>
        /// <param name="bet"></param>
        /// <returns></returns>
        public static ICombination GetCombinationFruityForce(string game, int buyBonusType, int bet, int numberOfLines, int gamesPlayed)
        {
            var reels = MathBuyBonusFilesReader.GetBuyBonusReelsForGame(game);

            if (buyBonusType < 1 || buyBonusType > 181)
            {
                throw new Exception("Buy Bonus Combination" + game + ": Buy bonus type " + buyBonusType + " not supported!");
            }
            if (!ValidateBonusType(gamesPlayed, buyBonusType))
            {
                throw new Exception("Buy Bonus Combination" + game + ": Buy bonus type " + buyBonusType + " not valid!");
            }
            var matrixArray = BonusMatrixLibrary.ReadDirectedMatrixArrayFromReels(-1, 0, 4, 6, new[] { false, false, false, false, false }, 1, reels);

            var matrix = new MatrixFruityForce();
            var level = MatrixFruityForce.GetLevel(gamesPlayed);
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationFruityForce();
            combination.MatrixToCombinationFruityForce(matrix, numberOfLines, bet, level, (byte)gamesPlayed, false, true);
            return combination;
        }
    }
}
