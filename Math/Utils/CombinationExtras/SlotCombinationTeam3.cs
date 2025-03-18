using Papi.GameServer.Utils.Enums;
using GameCloversAndStars;
using GameCrownsAndStars;
using GameFortuneParrot;
using GameTopHot5;
using GameTurboStars20;
using GameTurboStars40;
using GoldenCrownMax;
using MathCombination.CombinationData;
using MathCombination.ReelsData;

namespace CombinationExtras
{
    public static partial class SlotCombination
    {
        #region Private methods

        #endregion

        #region Public methods

        /// <summary>
        /// Daje kombinaciju za igru CloversAndStars.
        /// </summary>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationCloversAndStars(int bet, int numberOfLines)
        {
            var matrixArray = MatrixCloversAndStars.GetMatixArray();
            var matrix = new MatrixCloversAndStars();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationCloversAndStars();
            combination.MatrixToCombinationCloversAndStars(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru FortuneParrot.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationFortuneParrot(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixFortuneParrot();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru TurboStars40.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="stars"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationTurboStars40(int[,] matrixArray, int numberOfLines, int bet, byte stars)
        {
            var matrix = new MatrixTurboStars40();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationTurboStars40();
            combination.MatrixToCombination(matrix, numberOfLines, bet, stars);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru TurboStars20.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="stars"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationTurboStars20(int[,] matrixArray, int numberOfLines, int bet, byte stars)
        {
            var matrix = new MatrixTurboStars20();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationTurboStars20();
            combination.MatrixToCombination(matrix, numberOfLines, bet, stars);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru GoldenCrown.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationGoldenCrownMax(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixGoldenCrownMax();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationGoldenCrownMax();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru CrownsAndStars.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="stars"></param>
        /// <param name="addArray"></param>
        /// <returns></returns>
        private static ICombination GetCombinationCrownsAndStars(int numberOfLines, int bet, ref byte[] addArray)
        {
            if (addArray == null)
            {
                addArray = new byte[5];
            }
            var (matrixArray, megapotsToAdd) = MatrixCrownsAndStars.GetMatrixArray();
            var matrix = new MatrixCrownsAndStars();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationCrownsAndStars();
            combination.MatrixToCombination(matrix, numberOfLines, bet, megapotsToAdd, ref addArray);
            return combination;
        }
        /// <summary>
        /// Daje kombinaciju za igru TopHot5.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationTopHot5(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixTopHot5();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationTopHot5();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }


        public static ICombination GetCombinationTeam3(Games game, int bet, int numberOfLines, int gratisGamesLeft, ref byte[] additionalArray, byte additionalInformation = 0, int selectedField = 0, object gameDataObj = null)
        {
            switch (game)
            {
                case Games.CloversAndStars:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationCloversAndStars(bet, numberOfLines);
                case Games.CrownsAndStars:
                    ValidateLines(game, numberOfLines, 10);
                    return GetCombinationCrownsAndStars(numberOfLines, bet, ref additionalArray);
            }

            var reels = ReadReelsFromSlotFile(game, gratisGamesLeft > 0, additionalInformation);
            var matrixArray = ReelsReader.ReadMatrixArrayFromReels(reels);

            switch (game)
            {
                case Games.FortuneParrot:
                    ValidateLines(game, numberOfLines, 10);
                    return GetCombinationFortuneParrot(matrixArray, numberOfLines, bet);
                case Games.TurboStars40:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationTurboStars40(matrixArray, numberOfLines, bet, additionalInformation);
                case Games.TurboStars20:
                    ValidateLines(game, numberOfLines, 20);
                    return GetCombinationTurboStars20(matrixArray, numberOfLines, bet, additionalInformation);
                case Games.GoldenCrownMax:
                case Games.BrilliantHeart:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationGoldenCrownMax(matrixArray, numberOfLines, bet);
                case Games.TopHot5:
                    ValidateLines(game, numberOfLines, 5);
                    return GetCombinationTopHot5(matrixArray, numberOfLines, bet);
            }

            return null;
        }

        #endregion
    }
}
