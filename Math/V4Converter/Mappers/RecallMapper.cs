using Papi.GameServer.Math.Contracts.StructuresV3;
using MathCombination.CombinationData;
using System;
using System.Collections.Generic;
using System.Linq;
using V4Converter.DTOs;

namespace V4Converter.Mappers
{
    public class RecallMapper
    {
        public static object GenerateRecall(V3MapperParams v3MapperParams)
        {
            GameConfig gameConfig = v3MapperParams.GameConfig;
            int numberOfReels = gameConfig.NumberOfReels;
            int numberOfRows = gameConfig.NumberOfRows;
            byte[] positionFor2 = v3MapperParams.Combination.PositionFor2;
            int[,] matrix = v3MapperParams.Matrix;
            switch (gameConfig.RecallStrategy)
            {
                case "Neighboring":
                    return GetRecallNeighboring(positionFor2, numberOfReels, numberOfRows, matrix);
                case "ReelIndex":
                    return GetRecallReelIndex(positionFor2, numberOfReels, numberOfRows, matrix, 0);
                case "Book":
                    return GetRecallReelIndex(positionFor2, numberOfReels, numberOfRows, matrix, v3MapperParams.Combination.AdditionalInformation);
                case "NoWildExpand":
                    return GetRecallNoWildExpand(v3MapperParams.Combination, matrix, numberOfReels, numberOfRows);
                case "Sunburst":
                    return GetRecallSunburst(v3MapperParams.Combination, matrix, numberOfReels, numberOfRows);
                case "ToxicHaze":
                    return GetRecallToxicHaze(v3MapperParams.IsCurrentGameGratis, matrix, numberOfReels, numberOfRows, v3MapperParams.GratisGamesLeft);
                default:
                    return GetRecallDefault(positionFor2, numberOfReels, numberOfRows, matrix);
            }
        }

        public static object MapExpandSymbolsToRecall(int[,] symbols, object extra, GameConfig gameConfig)
        {
            switch (gameConfig.RecallStrategy)
            {
                case "Book":
                    return MapExpandSymbolsToRecallBook(symbols, extra, gameConfig);
                default:
                    return MapExpandSymbolsToRecallDefault(symbols, extra, gameConfig);
            }
        }

        private static object MapExpandSymbolsToRecallBook(int[,] symbols, object extra, GameConfig gameConfig)
        {
            var transformReels = (int[])((dynamic)extra).transformReels;
            if (transformReels.Sum() > 0)
            {
                var bonusSymbol = (int)((dynamic)extra).bonusSymbol;
                var numberOfRows = gameConfig.NumberOfRows;
                var numberOfReels = gameConfig.NumberOfReels;
                int[,] recallMatrix = new int[numberOfReels, numberOfRows];
                for (var i = 0; i < numberOfReels; i++)
                {
                    for (var j = 0; j < numberOfRows; j++)
                    {
                        if (transformReels[i] == 1)
                        {
                            recallMatrix[i, j] = bonusSymbol;
                        }
                        else
                        {
                            recallMatrix[i, j] = symbols[i, j];
                        }
                    }
                }
                return recallMatrix;
            }
            return null;
        }
        private static int[,] GetMatrixCopy(int[,] matrix, int numberOfReels, int numberOfRows)
        {
            int[,] recallMatrix = new int[numberOfReels, numberOfRows];
            for (var i = 0; i < numberOfReels; i++)
            {
                for (var j = 0; j < numberOfRows; j++)
                {
                    recallMatrix[i, j] = matrix[i, j];
                }
            }
            return recallMatrix;
        }
        private static object MapExpandSymbolsToRecallDefault(int[,] symbols, object extra, GameConfig gameConfig)
        {
            var wildExpand = (WildExpandV3[])((dynamic)extra).wildExpand;
            if (wildExpand.Length > 0)
            {
                var numberOfRows = gameConfig.NumberOfRows;
                var numberOfReels = gameConfig.NumberOfReels;
                int[,] recallMatrix = GetMatrixCopy(symbols, numberOfReels, numberOfRows);
                foreach (var expand in wildExpand)
                {
                    foreach (CoordinateV3 coordinate in expand.coordinates)
                    {
                        recallMatrix[coordinate.reel, coordinate.row] = recallMatrix[expand.origin.reel, expand.origin.row];
                    }
                }
                return recallMatrix;
            }
            return null;
        }

        private static object GetRecallDefault(byte[] positionFor2, int numberOfReels, int numberOfRows, int[,] matrix)
        {
            if (HasPosFor2Transformation(positionFor2, matrix.Length))
            {
                int[,] recallMatrix = GetMatrixCopy(matrix, numberOfReels, numberOfRows);
                for (var i = 0; i < numberOfReels; i++)
                {
                    for (var j = 0; j < numberOfRows; j++)
                    {
                        if (positionFor2[i] < matrix.Length && j != positionFor2[i] / numberOfReels)
                        {
                            recallMatrix[positionFor2[i] % numberOfReels, j] = matrix[positionFor2[i] % numberOfReels, positionFor2[i] / numberOfReels];
                        }
                    }
                }
                return recallMatrix;
            }
            return null;
        }

        private static object GetRecallReelIndex(byte[] positionFor2, int numberOfReels, int numberOfRows, int[,] matrix, int symbol)
        {
            if (HasPosFor2TransformationReelIndex(positionFor2))
            {
                int[,] recallMatrix = new int[numberOfReels, numberOfRows];
                for (var i = 0; i < numberOfReels; i++)
                {
                    for (int j = 0; j < numberOfRows; j++)
                    {
                        if (positionFor2[i] == 1)
                        {
                            recallMatrix[i, j] = symbol;
                        }
                        else
                        {
                            recallMatrix[i, j] = matrix[i, j];
                        }
                    }
                }
                return recallMatrix;
            }
            return null;
        }
        private static object GetRecallNeighboring(byte[] positionFor2, int numberOfReels, int numberOfRows, int[,] matrix)
        {
            if (HasPosFor2Transformation(positionFor2, matrix.Length))
            {
                int[,] recallMatrix = GetMatrixCopy(matrix, numberOfReels, numberOfRows);
                for (var i = 0; i < positionFor2.Length; i++)
                {
                    if (positionFor2[i] < matrix.Length)
                    {
                        var reel = positionFor2[i] % numberOfReels;
                        var row = positionFor2[i] / numberOfReels;
                        for (var k = Math.Max(reel - 1, 0); k < Math.Min(reel + 2, numberOfReels); k++)
                        {
                            for (var j = Math.Max(row - 1, 0); j < Math.Min(row + 2, numberOfRows); j++)
                            {
                                if (k != reel || j != row)
                                {
                                    recallMatrix[k, j] = matrix[reel, row];
                                }
                            }
                        }
                    }
                }
                return recallMatrix;
            }
            return null;
        }

        private static object GetRecallNoWildExpand(ICombination combination, int[,] matrix, int numberOfReels, int numberOfRows)
        {
            int[,] recallMatrix = GetMatrixCopy(matrix, numberOfReels, numberOfRows);
            if (combination.TotalWin > 0)
            {
                for (var i = 1; i < 4; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        if (matrix[i, j] == 0 || matrix[i, j] == 1)
                        {
                            var symb = matrix[i, j];
                            recallMatrix[i, 0] = symb;
                            recallMatrix[i, 1] = symb;
                            recallMatrix[i, 2] = symb;
                            break;
                        }
                    }
                }
                return recallMatrix;
            }
            return null;
        }
        private static bool HasPosFor2Transformation(byte[] positionFor2, int matrixLength)
        {
            return positionFor2.Where(x => x < matrixLength).Count() > 0;
        }

        private static bool HasPosFor2TransformationReelIndex(byte[] positionFor2)
        {
            return positionFor2.Where(x => x > 0).Count() > 0;
        }

        private static object GetRecallSunburst(ICombination combination, int[,] matrix, int numberOfReels, int numberOfRows)
        {
            int[,] recallMatrix = GetMatrixCopy(matrix, numberOfReels, numberOfRows);
            List<WinSymbolV3> wildOverthrow = new List<WinSymbolV3>();
            var line = combination.LinesInformation.SingleOrDefault(l => l.Id == 253);
            if (line != null)
            {
                foreach (var wp in line.WinningPosition)
                {
                    var wpReel = wp % 5;
                    var wpRow = wp / 5;
                    recallMatrix[wpReel, wpRow] = recallMatrix[wpReel, wpRow] > 10 ? recallMatrix[wpReel, wpRow] + 1 : 11;
                }
                return recallMatrix;
            }
            return null;
        }

        private static object GetRecallToxicHaze(bool isCurrentGameGratis, int[,] matrix, int numberOfReels, int numberOfRows, int gratisGamesLeft)
        {
            int[,] recallMatrix = GetMatrixCopy(matrix, numberOfReels, numberOfRows);
            if (isCurrentGameGratis)
            {
                switch (gratisGamesLeft)
                {
                    case 0:
                        recallMatrix[2, 2] = 14;
                        break;
                    case 1:
                    case 3:
                        recallMatrix[1, 1] = recallMatrix[1, 3] = recallMatrix[3, 1] = recallMatrix[3, 3] = 13;
                        break;
                    case 2:
                        recallMatrix[0, 0] = recallMatrix[0, 4] = recallMatrix[4, 0] = recallMatrix[4, 4] = 13;
                        break;
                    case 4:
                        recallMatrix[2, 2] = 13;
                        break;
                    case 5:
                        recallMatrix[1, 1] = recallMatrix[1, 3] = recallMatrix[3, 1] = recallMatrix[3, 3] = 12;
                        break;
                    case 6:
                        recallMatrix[0, 0] = recallMatrix[0, 4] = recallMatrix[4, 0] = recallMatrix[4, 4] = 12;
                        break;
                }
                return recallMatrix;
            }
            return null;
        }
    }
}
