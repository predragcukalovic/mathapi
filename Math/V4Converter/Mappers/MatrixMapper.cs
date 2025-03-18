using Papi.GameServer.Utils.Enums;
using MathCombination.CombinationData;
using System;
using V4Converter.DTOs;

namespace V4Converter
{
    public class MatrixMapper
    {
        public static int[,] GetMatrix(GameConfig gameConfig, ICombination combination, bool isCurrentGameGratis)
        {
            int numberOfReels = gameConfig.NumberOfReels;
            int numberOfRows = gameConfig.NumberOfRows;
            switch (gameConfig.MatrixType)
            {
                case "WLC":
                    return WLCMatrix(combination, numberOfReels, numberOfRows, isCurrentGameGratis);
                case "WLC2":
                    return WLCMatrix2(combination, numberOfReels, numberOfRows, isCurrentGameGratis);
                case "MysticJungle":
                    return MysticJungleMatrix(combination, numberOfReels, numberOfRows);
                case "Shift":
                    return GetMatrixShift(combination, numberOfReels, numberOfRows);
                case "DoubleShift":
                    return GetMatrixDoubleShift(combination, numberOfReels, numberOfRows);
                case "DecrementSymbol":
                    return GetMatrixDecrementSymbol(combination, numberOfReels, numberOfRows);
                default:
                    return GetMatrixDefault(combination, numberOfReels, numberOfRows);
            }
        }

        public static int[,] ModifyMatrixIfNeeded(GameConfig gameConfig, int[,] matrix)
        {
            switch (gameConfig.MatrixModifier)
            {
                case "LollasWorld":
                    return ModifyMatrixLollasWorld(matrix, gameConfig);
                default:
                    return matrix;
            }
        }

        private static int[,] ModifyMatrixLollasWorld(int[,] matrix, GameConfig gameConfig)
        {

            int[,] modifiedMatrix = new int[gameConfig.NumberOfReels, gameConfig.NumberOfRows];
            for (var i = 0; i < gameConfig.NumberOfReels; i++)
            {
                for (var j = 0; j < gameConfig.NumberOfRows; j++)
                {
                    modifiedMatrix[i, j] = matrix[i, j];
                }
            }
            for (var i = 1; i < 4; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (modifiedMatrix[i, j + 1] == 0 && modifiedMatrix[i, j] > 0 && modifiedMatrix[i, j] < 10)
                    {
                        modifiedMatrix[i, j + 1] = 10;
                    }
                    if (modifiedMatrix[i, j] == 0 && modifiedMatrix[i, j + 1] > 0 && modifiedMatrix[i, j + 1] < 10)
                    {
                        modifiedMatrix[i, j] = 11;
                    }
                }
            }
            return modifiedMatrix;
        }

        private static int[,] GetMatrixDecrementSymbol(ICombination combination, int numberOfReels, int numberOfRows)
        {
            int[,] matrix = new int[numberOfReels, numberOfRows];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < numberOfRows; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j] - 1;
                }
            }
            return matrix;
        }

        private static int[,] WLCMatrix(ICombination combination, int numberOfReels, int numberOfRows, bool isCurrentGameGratis)
        {
            if (isCurrentGameGratis)
            {
                int[,] matrix = new int[numberOfReels, numberOfRows];
                for (var i = 0; i < 5; i++)
                {
                    for (var j = 0; j < 4; j++)
                    {
                        matrix[i, j] = (combination.Matrix[i, j] == combination.AdditionalInformation) ? combination.Matrix[i, j] + 4 : combination.Matrix[i, j];
                    }
                }
                return matrix;
            }
            else
            {
                return GetMatrixDefault(combination, numberOfReels, numberOfRows);
            }
        }

        private static int[,] WLCMatrix2(ICombination combination, int numberOfReels, int numberOfRows, bool isCurrentGameGratis)
        {
            if (isCurrentGameGratis)
            {
                int[,] matrix = new int[numberOfReels, numberOfRows];
                for (var i = 0; i < 5; i++)
                {
                    for (var j = 0; j < 4; j++)
                    {
                        matrix[i, j] = (combination.Matrix[i, j] == combination.AdditionalInformation) ? combination.Matrix[i, j] + 8 : (combination.Matrix[i, j] == 0) ? 8 : combination.Matrix[i, j];
                    }
                }
                return matrix;
            }
            else
            {
                return GetMatrixDefault(combination, numberOfReels, numberOfRows);
            }
        }

        private static int[,] MysticJungleMatrix(ICombination combination, int numberOfReels, int numberOfRows)
        {
            int[,] matrix = new int[numberOfReels, numberOfRows];
            for (var i = 0; i < numberOfReels; i++)
            {
                for (var j = 0; j < numberOfRows; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j + 1] + (combination.Matrix[i, j] == 9 ? combination.WinFor2 : 0);
                }
            }
            return matrix;
        }
        private static int[,] GetMatrixDefault(ICombination combination, int numberOfReels, int numberOfRows)
        {
            int[,] matrix = new int[numberOfReels, numberOfRows];
            for (var i = 0; i < numberOfReels; i++)
            {
                for (var j = 0; j < numberOfRows; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j];
                }
            }
            return matrix;
        }

        private static int[,] GetMatrixUnicorn(string gameName, ICombination combination, int numberOfReels, int numberOfRows, bool isCurrentGameGratis)
        {
            switch ((Games)Enum.Parse(typeof(Games), gameName))
            {
                case Games.UnicornVegasDice:
                    return GetMatrixDefault(combination, numberOfReels, numberOfRows);
                case Games.UnicornFruitWildLines:
                case Games.UnicornDiceWildLines:
                case Games.UnicornBigSpinSevens:
                    return GetMatrixDoubleShift(combination, numberOfReels, numberOfRows);
                default:
                    return GetMatrixShift(combination, numberOfReels, numberOfRows);
            }
        }

        private static int[,] GetMatrixShift(ICombination combination, int numberOfReels, int numberOfRows)
        {
            int[,] matrix = new int[numberOfReels, numberOfRows];
            for (var i = 0; i < numberOfReels; i++)
            {
                for (var j = 0; j < numberOfRows; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j + 1];
                }
            }
            return matrix;
        }

        private static int[,] GetMatrixDoubleShift(ICombination combination, int numberOfReels, int numberOfRows)
        {
            int[,] matrix = new int[numberOfReels, numberOfRows];
            for (var i = 0; i < numberOfReels; i++)
            {
                for (var j = 0; j < numberOfRows; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j + 2];
                }
            }
            return matrix;
        }
    }
}
