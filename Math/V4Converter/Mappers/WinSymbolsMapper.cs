using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using System.Collections.Generic;
using V4Converter.DTOs;

namespace V4Converter
{
    public class WinSymbolsMapper
    {

        public static WinSymbolV3[] getWinSymbols(WinSymbolMapperParams winSymbolMapperParams)
        {
            GameConfig gameConfig = winSymbolMapperParams.GameConfig;
            int numberOfReels = gameConfig.NumberOfReels;
            int[,] matrix = winSymbolMapperParams.Matrix;
            List<int> positions = winSymbolMapperParams.Positions;
            ICombination combination = winSymbolMapperParams.Combination;
            switch (gameConfig.WinSymbolType)
            {
                case "Shift":
                    return GetSymbolsShift(positions, matrix, numberOfReels);
                case "DoubleShift":
                    return GetSymbolsDoubleShift(positions, matrix, numberOfReels);
                case "ZeroSub":
                    return GetSymbolsZeroSub(positions, matrix, numberOfReels);
                case "ZeroSub2":
                    return GetSymbolsZeroSub2(positions, matrix, numberOfReels);
                case "DoubleSub":
                    return GetSymbolsDoubleSub(positions, matrix, numberOfReels);
                case "Joker3x2":
                    return GetSymbolsJoker3x2(positions, matrix, combination, numberOfReels);
                case "MysticJungle":
                    return GetSymbolsMysticJungle(positions, matrix, combination, numberOfReels);
                case "SantasPresents":
                    return GetSymbolsSantasPresents(positions, matrix, numberOfReels);
                default:
                    return GetSymbolsDefault(positions, matrix, numberOfReels);
            }
        }

        private static WinSymbolV3[] GetSymbolsDefault(List<int> positions, int[,] matrix, int numberOfReels)
        {
            var m = positions.Count;
            var winSymb = new WinSymbolV3[m];
            for (var j = 0; j < m; j++)
            {
                winSymb[j] = new WinSymbolV3 { reel = positions[j] % numberOfReels, row = positions[j] / numberOfReels };
                winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
            }

            return winSymb;
        }

        private static WinSymbolV3[] GetSymbolsShift(List<int> positions, int[,] matrix, int numberOfReels)
        {
            var m = positions.Count;
            var winSymb = new WinSymbolV3[m];
            for (var j = 0; j < m; j++)
            {
                winSymb[j] = new WinSymbolV3 { reel = positions[j] % numberOfReels, row = positions[j] / numberOfReels - 1 };
                winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
            }

            return winSymb;
        }

        private static WinSymbolV3[] GetSymbolsDoubleShift(List<int> positions, int[,] matrix, int numberOfReels)
        {
            var m = positions.Count;
            var winSymb = new WinSymbolV3[m];
            for (var j = 0; j < m; j++)
            {
                winSymb[j] = new WinSymbolV3 { reel = positions[j] % numberOfReels, row = positions[j] / numberOfReels - 2 };
                winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
            }

            return winSymb;
        }

        private static WinSymbolV3[] GetSymbolsZeroSub(List<int> positions, int[,] matrix, int numberOfReels)
        {
            var m = positions.Count;
            var winSymb = new WinSymbolV3[m];
            for (var j = 0; j < m; j++)
            {
                winSymb[j] = new WinSymbolV3 { reel = positions[j] % numberOfReels, row = positions[j] / numberOfReels };
                if (matrix[winSymb[j].reel, 0] * matrix[winSymb[j].reel, 1] * matrix[winSymb[j].reel, 2] == 0)
                {
                    winSymb[j].id = 0;
                }
                else
                {
                    winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
                }
            }

            return winSymb;
        }

        private static WinSymbolV3[] GetSymbolsZeroSub2(List<int> positions, int[,] matrix, int numberOfReels)
        {
            var m = positions.Count;
            var winSymb = new WinSymbolV3[m];
            for (var j = 0; j < m; j++)
            {
                winSymb[j] = new WinSymbolV3 { reel = positions[j] % numberOfReels, row = positions[j] / numberOfReels };
                if (matrix[winSymb[j].reel, 0] == 0 || matrix[winSymb[j].reel, 1] == 0 || matrix[winSymb[j].reel, 2] == 0)
                {
                    winSymb[j].id = 0;
                }
                else
                {
                    winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
                }
            }

            return winSymb;
        }

        private static WinSymbolV3[] GetSymbolsDoubleSub(List<int> positions, int[,] matrix, int numberOfReels)
        {
            var m = positions.Count;
            var winSymb = new WinSymbolV3[m];
            for (var j = 0; j < m; j++)
            {
                winSymb[j] = new WinSymbolV3 { reel = positions[j] % numberOfReels, row = positions[j] / numberOfReels };
                if (matrix[winSymb[j].reel, 0] == 0 || matrix[winSymb[j].reel, 1] == 0 || matrix[winSymb[j].reel, 2] == 0)
                {
                    winSymb[j].id = 0;
                }
                else if (matrix[winSymb[j].reel, 0] == 1 || matrix[winSymb[j].reel, 1] == 1 || matrix[winSymb[j].reel, 2] == 1)
                {
                    winSymb[j].id = 1;
                }
                else
                {
                    winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
                }
            }

            return winSymb;
        }

        private static WinSymbolV3[] GetSymbolsJoker3x2(List<int> positions, int[,] matrix, ICombination combination, int numberOfReels)
        {
            var m = positions.Count;
            var winSymb = new WinSymbolV3[m];
            for (var j = 0; j < m; j++)
            {
                winSymb[j] = new WinSymbolV3 { reel = positions[j] % numberOfReels, row = positions[j] / numberOfReels };
                winSymb[j].id = (combination.AdditionalArray[positions[j]] >> 4) == 0 ? matrix[winSymb[j].reel, winSymb[j].row] : (combination.AdditionalArray[positions[j]] >> 4) - 1;
            }

            return winSymb;
        }

        private static WinSymbolV3[] GetSymbolsMysticJungle(List<int> positions, int[,] matrix, ICombination combination, int numberOfReels)
        {
            var m = positions.Count;
            var winSymb = new WinSymbolV3[m];
            for (var j = 0; j < m; j++)
            {
                winSymb[j] = new WinSymbolV3 { reel = positions[j] % numberOfReels, row = positions[j] / numberOfReels };
                winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row] >= 9 ? combination.WinFor2 : matrix[winSymb[j].reel, winSymb[j].row];
            }

            return winSymb;
        }

        private static WinSymbolV3[] GetSymbolsSantasPresents(List<int> positions, int[,] matrix, int numberOfReels)
        {
            var m = positions.Count;
            var winSymb = new WinSymbolV3[m];
            for (var j = 0; j < m; j++)
            {
                winSymb[j] = new WinSymbolV3 { reel = positions[j] % numberOfReels, row = positions[j] / numberOfReels };
                winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
                var expandSymb = -1;
                if (matrix[winSymb[j].reel, 0] == 0 || matrix[winSymb[j].reel, 1] == 0 || matrix[winSymb[j].reel, 2] == 0)
                {
                    expandSymb = 0;
                }
                if (matrix[winSymb[j].reel, 0] >= 10 || matrix[winSymb[j].reel, 1] >= 10 || matrix[winSymb[j].reel, 2] >= 10)
                {
                    expandSymb = System.Math.Max(matrix[winSymb[j].reel, 0], System.Math.Max(matrix[winSymb[j].reel, 1], matrix[winSymb[j].reel, 2]));
                }
                if (expandSymb != -1)
                {
                    winSymb[j].id = expandSymb;
                }
            }
            return winSymb;
        }
    }
}
