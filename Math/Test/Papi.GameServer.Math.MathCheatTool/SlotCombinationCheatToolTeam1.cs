using Papi.GameServer.Math.Contracts.Requests;
using Papi.GameServer.Utils.Enums;
using GameBlowFruits40;
using GameCrownOfSecret;
using GameSpecialFruits;
using GameVeryHot40Extreme;
using MathCombination.CombinationData;
using System.Collections.Generic;
using GameWild5;

namespace Papi.GameServer.Math.MathCheatTool
{
    public static partial class SlotCombinationCheatTool
    {
        #region Private methods

        private static ICombination GetCombinationBlowFruits40(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new MatrixBlowFruits40();
            var matArray2 = new int[5, 6];
            for (var i = 0; i < 5; i++)
            {
                matArray2[i, 0] = 3;
                for (var j = 1; j < 6; j++)
                {
                    matArray2[i, j] = matrixArray[i, j - 1];
                }
            }

            matrix.FromMatrixArray(matArray2);
            var combination = new CombinationBlowFruits40();
            combination.MatrixToCombination(matrix, numberOfLines, bet, false);
            return combination;
        }

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

        private static ICombination GetCombinationCrownOfSecret(ref CheatToolData cheatToolObject, int bet,
            int numberOfLines, bool gratisGame, ref byte[] addArray, byte additionalInformation)
        {
            MatrixCrownOfSecret.InitializeAddArrayIfNeeded(ref addArray);

            int[,] matrixArray;
            if (!gratisGame)
            {
                matrixArray = ReadMatrixArrayFromReelsCheat(ref cheatToolObject,
                    ReadReelsFromSlotFile(Games.CrownOfSecret, false, additionalInformation));
            }
            else
            {
                var bonusData = BonusDataCrownOfSecret.FromByteArray(addArray);
                var mat = new int[5, 3];

                var cheatSymbol = cheatToolObject.NewMatrix[0, 0] == 9 && bonusData.NumberOfActiveReels == 5
                    ? 7 // in case all 5 reels are already active, and we get another bonus symbol as cheat -> ignore it
                    : cheatToolObject.NewMatrix[0, 0];

                for (var i = 0; i < 5; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        mat[i, j] = cheatSymbol;
                    }
                }

                matrixArray = mat;
            }

            var matrix = new MatrixCrownOfSecret();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationCrownOfSecret();
            combination.MatrixToCombinationCrownOfSecret(matrix, bet, gratisGame, numberOfLines, ref addArray);
            return combination;
        }

        private static ICombination GetCombinationWild5(int[,] matrixArray, int bet)
        {
            var matrix = new MatrixWild5();
            var matArray2 = new int[3, 5];
            for (var i = 0; i < 3; i++)
            {
                matArray2[i, 0] = 3;
                for (var j = 1; j < 5; j++)
                {
                    matArray2[i, j] = matrixArray[i, j - 1];
                }
            }
            matrix.FromMatrixArray(matArray2);
            var combination = new CombinationWild5();
            combination.MatrixToCombination(matrix, bet);
            return combination;
        }

        #endregion

        #region Public methods

        public static ICombination GetCombinationTeam1(ref CheatToolData cheatToolObject, Games game, int bet,
            int numberOfLines, int gratisGamesLeft,
            ref byte[] additionalArray, byte additionalInformation = 0, int selectedField = 0,
            object gameDataObj = null)
        {
            switch (game)
            {
                case Games.CrownOfSecret:
                    ValidateLines(game, numberOfLines, 10);
                    return GetCombinationCrownOfSecret(ref cheatToolObject, bet, numberOfLines, gratisGamesLeft > 0,
                        ref additionalArray, additionalInformation);
            }

            List<byte>[] reels;
            switch (game)
            {
                default:
                    reels = ReadReelsFromSlotFile(game, gratisGamesLeft > 0, additionalInformation);
                    break;
            }

            var matrixArray = ReadMatrixArrayFromReelsCheat(ref cheatToolObject, reels);

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