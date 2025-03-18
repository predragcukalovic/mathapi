using Papi.GameServer.Utils.Enums;
using GameBlowFruits40;
using GameCrownOfSecret;
using GameSpecialFruits;
using GameVeryHot40Extreme;
using GameWild5;
using MathCombination.CombinationData;
using MathCombination.ReelsData;

namespace CombinationExtras
{
    public static partial class SlotCombination
    {
        #region Private methods

        /// <summary>
        /// Daje kombinaciju za igru BlowFruits40.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationBlowFruits40(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new MatrixBlowFruits40();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationBlowFruits40();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru VeryHot40Extreme.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationVeryHot40Extreme(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new MatrixVeryHot40Extreme();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationVeryHot40Extreme();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        private static ICombination GetCombinationSpecialFruits(int[,] matrixArray, int bet)
        {
            var matrix = new MatrixSpecialFruits();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationSpecialFruits();
            combination.MatrixToCombination(matrix, bet);
            return combination;
        }

        private static ICombination GetCombinationCrownOfSecret(int bet, int numberOfLines, bool gratisGame, ref byte[] addArray, byte additionalInformation)
        {
            MatrixCrownOfSecret.InitializeAddArrayIfNeeded(ref addArray);

            int[,] matrixArray = !gratisGame
                ? ReelsReader.ReadMatrixArrayFromReels(ReadReelsFromSlotFile(Games.CrownOfSecret, false, additionalInformation))
                : MatrixCrownOfSecret.GetBonusMatrixArray(ref addArray);

            var matrix = new MatrixCrownOfSecret();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationCrownOfSecret();
            combination.MatrixToCombinationCrownOfSecret(matrix, bet, gratisGame, numberOfLines, ref addArray);
            return combination;
        }

        private static ICombination GetCombinationWild5(int[,] matrixArray, int bet)
        {
            var matrix = new MatrixWild5();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationWild5();
            combination.MatrixToCombination(matrix, bet);
            return combination;
        }

        #endregion

        #region Public methods

        public static ICombination GetCombinationTeam1(Games game, int bet, int numberOfLines, int gratisGamesLeft, ref byte[] additionalArray, byte additionalInformation = 0, int selectedField = 0, object gameDataObj = null)
        {
            switch (game)
            {
                case Games.CrownOfSecret:
                    ValidateLines(game, numberOfLines, 10);
                    return GetCombinationCrownOfSecret(bet, numberOfLines, gratisGamesLeft > 0, ref additionalArray, additionalInformation);
            }

            var reels = ReadReelsFromSlotFile(game, gratisGamesLeft > 0, additionalInformation);
            var matrixArray = ReelsReader.ReadMatrixArrayFromReels(reels);

            switch (game)
            {
                case Games.BlowFruits40:
                case Games.CoinSplash:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationBlowFruits40(matrixArray, bet, numberOfLines);
                case Games.VeryHot40Extreme:
                    ValidateLines(game, numberOfLines, 4);
                    return GetCombinationVeryHot40Extreme(matrixArray, bet, numberOfLines);
                case Games.SpecialFruits:
                    ValidateLines(game, numberOfLines, 20);
                    return GetCombinationSpecialFruits(matrixArray, bet);
                case Games.Wild5:
                    return GetCombinationWild5(matrixArray, bet);
            }
            return null;
        }

        #endregion
    }
}
