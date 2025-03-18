using CombinationExtras.ReaderData;
using GameBonusEpicCrown;
using LibraryBuyBonus;
using MathCombination.CombinationData;
using System;

namespace BuyBonusBonusEpicCrown
{
    public class BuyBonusEpicCrown
    {
        public static ICombination GetCombinationBonusEpicCrown(string game, int buyBonusType, int numberOfLines,
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


            var matrix = new MatrixBonusEpicCrown();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationBonusEpicCrown();
            combination.MatrixToCombinationBonusEpicCrown(matrix, bet, gratisGame, numberOfLines, 40);
            return combination;
        }

    }
}
