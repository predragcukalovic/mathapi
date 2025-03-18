using Papi.GameServer.Math.Contracts.Requests;
using Papi.GameServer.Utils.Enums;
using GameAgeOfRome;
using GameAquaFlame;
using GameFruityForce;
using MathCombination.CombinationData;
using System;
using System.Collections.Generic;

namespace Papi.GameServer.Math.MathCheatTool
{
    public static partial class SlotCombinationCheatTool
    {
        #region Private methods

        /// <summary>
        /// Daje kombinaciju za igru AgeOfRome.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationAgeOfRome(int[,] matrixArray, int bet, bool gratisGame, ref byte[] addArray)
        {
            if (addArray == null)
            {
                addArray = new byte[16];
            }
            var matrix = new MatrixAgeOfRome();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationAgeOfRome();
            combination.MatrixToCombinationAgeOfRome(matrix, 20, bet, gratisGame, ref addArray);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru AquaFlame.
        /// </summary>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="aquaFlame">0 za aqua, 1 za flame</param>
        /// <returns></returns>
        private static ICombination GetCombinationAquaFlame(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new MatrixAquaFlame();
            var aquaFlame = matrixArray[0, 0] > 7 ? 1 : 0;
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationAquaFlame();
            combination.MatrixToCombinationAquaFlame(matrix, numberOfLines, bet, aquaFlame);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru FruityForce40.
        /// </summary>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationFruityForce(int[,] matrixArray, int bet, int numberOfLines, int gamesPlayed)
        {
            matrixArray = FixMatrixArray(matrixArray);
            var matrix = new MatrixFruityForce();
            var level = MatrixFruityForce.GetLevel(gamesPlayed);
            for (var i = 0; i < matrixArray.GetLength(0); i++)
            {
                for (var j = 0; j < matrixArray.GetLength(1); j++)
                {
                    if (matrixArray[i, j] > 8)
                    {
                        matrixArray[i, j] = 0;
                    }
                }
            }
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationFruityForce();
            combination.MatrixToCombinationFruityForce(matrix, numberOfLines, bet, level, (byte)gamesPlayed);
            return combination;
        }

        #endregion

        #region Public methods

        public static ICombination GetCombinationTeam2(ref CheatToolData cheatToolObject, Games game, int bet, int numberOfLines, int gratisGamesLeft,
        ref byte[] additionalArray, byte additionalInformation = 0, int selectedField = 0, object gameDataObj = null)
        {
            List<byte>[] reels;
            reels = ReadReelsFromSlotFile(game, gratisGamesLeft > 0, additionalInformation);
            var matrixArray = ReadMatrixArrayFromReelsCheat(ref cheatToolObject, reels);

            switch (game)
            {
                case Games.RedstoneDiceDouble:
                    ValidateLines(game, numberOfLines, 5);
                    return GetCombinationVeryHot5Extreme(matrixArray, numberOfLines, bet);
                case Games.AgeOfRome:
                    ValidateLines(game, numberOfLines, 1);
                    return GetCombinationAgeOfRome(matrixArray, bet, gratisGamesLeft > 0, ref additionalArray);
                case Games.BonusCrown:
                    ValidateLines(game, numberOfLines, 10);
                    return GetCombinationBonusEpicCrown(matrixArray, bet, gratisGamesLeft > 0, 10, 20);
                case Games.AquaFlame:
                    return GetCombinationAquaFlame(matrixArray, bet, numberOfLines);
                case Games.FruityForce40:
                    var gamesPlayed = Convert.ToInt32(gameDataObj);
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationFruityForce(matrixArray, bet, numberOfLines, gamesPlayed);

            }
            return null;
        }

        #endregion
    }
}
