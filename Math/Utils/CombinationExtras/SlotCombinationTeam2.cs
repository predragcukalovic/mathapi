using Papi.GameServer.Utils.Enums;
using Game20FireCash;
using GameAgeOfRome;
using GameAquaFlame;
using GameFruityForce;
using MathCombination.CombinationData;
using MathCombination.ReelsData;
using System;

namespace CombinationExtras
{
    public static partial class SlotCombination
    {
        #region Private methods

        /// <summary>
        /// Daje kombinaciju za igru AgeOfRome.
        /// </summary>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationAgeOfRome(int bet, bool gratisGame, ref byte[] addArray)
        {
            if (addArray == null)
            {
                addArray = new byte[16];
            }
            var matrixArray = MatrixAgeOfRome.GetMatixArray(gratisGame, addArray[15]);
            var matrix = new MatrixAgeOfRome();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationAgeOfRome();
            combination.MatrixToCombinationAgeOfRome(matrix, 20, bet, gratisGame, ref addArray);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru FruityForce40.
        /// </summary>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationFruityForce(int bet, int numberOfLines, int gamesPlayed)
        {
            var matrix = new MatrixFruityForce();
            var level = MatrixFruityForce.GetLevel(gamesPlayed);
            var matrixArray = MatrixFruityForce.GetMatixArray(level);
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationFruityForce();
            combination.MatrixToCombinationFruityForce(matrix, numberOfLines, bet, level, (byte)gamesPlayed);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru AquaFlame.
        /// </summary>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="aquaFlame">0 za aqua, 1 za flame</param>
        /// <returns></returns>
        private static ICombination GetCombinationAquaFlame(int bet, int numberOfLines, int aquaFlame)
        {
            var matrixArray = MatrixAquaFlame.GetMatixArray(aquaFlame);
            var matrix = new MatrixAquaFlame();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationAquaFlame();
            combination.MatrixToCombinationAquaFlame(matrix, numberOfLines, bet, aquaFlame);
            return combination;
        }

        #endregion

        #region Public methods

        public static ICombination GetCombinationTeam2(Games game, int bet, int numberOfLines, int gratisGamesLeft, ref byte[] additionalArray, byte additionalInformation = 0, int selectedField = 0, object gameDataObj = null)
        {
            switch (game)
            {
                case Games.RedstoneVolcanoHot:
                    return Combination20FireCash.GetCombination20FireCash(numberOfLines, bet);
                case Games.AgeOfRome:
                    ValidateLines(game, numberOfLines, 1);
                    return GetCombinationAgeOfRome(bet, gratisGamesLeft > 0, ref additionalArray);
                case Games.FruityForce40:
                    var gamesPlayed = Convert.ToInt32(gameDataObj);
                    ValidateLines(game, numberOfLines, 40);
                    ValidateGamesPlayed(game, gamesPlayed, 0, 230);
                    return GetCombinationFruityForce(bet, numberOfLines, gamesPlayed);
                case Games.AquaFlame:
                    return GetCombinationAquaFlame(bet, numberOfLines, selectedField);
            }

            var reels = ReadReelsFromSlotFile(game, gratisGamesLeft > 0, additionalInformation);
            var matrixArray = ReelsReader.ReadMatrixArrayFromReels(reels);

            switch (game)
            {
                case Games.RedstoneDiceDouble:
                    ValidateLines(game, numberOfLines, 5);
                    return GetCombinationVeryHot5Extreme(matrixArray, numberOfLines, bet);
                case Games.BonusCrown:
                    ValidateLines(game, numberOfLines, 10);
                    return GetCombinationBonusEpicCrown(matrixArray, bet, gratisGamesLeft > 0, 10, 20);
            }
            return null;
        }

        #endregion
    }
}
