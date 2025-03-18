using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;
using System.Collections.Generic;

namespace GameMysticJungle
{
    public class MatrixMysticJungle : Matrix
    {
        public MatrixMysticJungle() : base(5)
        {
        }

        #region Public properties

        public static int[] PlayLines = { 10 };
        public static readonly int[,] WinForLinesMysticJungle =
        {
            {0, 0, 100, 250, 500},
            {0, 0, 50, 200, 400},
            {0, 0, 50, 100, 300},
            {0, 0, 30, 80, 250},
            {0, 0, 30, 80, 250},
            {0, 0, 20, 50, 150},
            {0, 0, 20, 50, 150},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };

        #endregion

        /// <summary>
        /// uzima liniju iz matrice
        /// </summary>
        /// <param name="lineNumber">broj linije, 1 -- 10</param>
        /// <param name="lines">Linije </param>
        /// <returns>vraća liniju pod datim brojem</returns>
        public LineMysticJungle GetLine(int lineNumber)
        {
            var line = new LineMysticJungle();
            for (var i = 0; i < 5; i++)
            {
                line.SetElement(i, GetElement(i, GlobalData.GameLineExtra[lineNumber - 1, i] + 1));
            }
            return line;
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayMysticJungle(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        public int GetMysterySymbol(bool winning)
        {
            var counts = new int[9];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    counts[GetElement(i, j)]++;
                }
            }
            var possible = new List<int>();
            for (var i = 0; i < 9; i++)
            {
                if (counts[i] > 5)
                {
                    possible.Add(i);
                }
            }
            if (possible.Count == 0)
            {
                return -1;
            }
            if (!winning && SoftwareRng.Next(5) != 0)
            {
                return -1;
            }
            if (winning && SoftwareRng.Next(4) == 0)
            {
                return -1;
            }
            return possible[(int)SoftwareRng.Next(possible.Count)];
        }

        public static byte[,] TransformMatrix(byte[,] matrix, int symbol)
        {
            var newMatrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    newMatrix[i, j] = (byte)((symbol != -1 && matrix[i, j] == symbol + 9) ? symbol + 9 : matrix[i, j] % 9);
                }
            }
            return newMatrix;
        }

        public void FixMatrix()
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, GetElement(i, j) % 9);
                }
            }
        }

        #region Struct V3

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 8, 8, 8, 8, 7, 7, 7, 7, 1, 1, 1, 1, 4, 4, 4, 4, 18, 18, 18, 18, 18, 3, 3, 3, 3, 2, 2, 2, 2, 6, 6, 6, 6, 0, 0, 0, 0, 18, 18, 18, 18, 18, 5, 5, 5, 5 };
            fakeReels[1] = new[] { 18, 18, 18, 18, 7, 7, 7, 7, 5, 5, 5, 5, 0, 0, 0, 0, 4, 4, 4, 4, 6, 6, 6, 6, 2, 2, 2, 2, 1, 1, 1, 1, 18, 18, 18, 18, 18, 18, 8, 8, 8, 8, 0, 0, 0, 5, 5, 5, 2, 2, 2, 2, 4, 4, 4, 4, 6, 6, 6, 1, 1, 1, 1 };
            fakeReels[2] = new[] { 6, 6, 6, 6, 0, 0, 0, 0, 7, 7, 7, 7, 18, 18, 18, 18, 18, 5, 5, 5, 5, 3, 3, 3, 3, 1, 1, 1, 1, 6, 6, 6, 7, 7, 7, 7, 18, 18, 18, 18, 2, 2, 2, 2, 5, 5, 5, 5, 18, 18, 18 };
            fakeReels[3] = new[] { 4, 4, 4, 4, 18, 18, 18, 18, 18, 3, 3, 3, 3, 2, 2, 2, 2, 8, 8, 8, 8, 7, 7, 7, 7, 1, 1, 1, 1, 6, 6, 6, 6, 0, 0, 0, 0, 18, 18, 18, 18, 18, 5, 5, 5, 5 };
            fakeReels[4] = new[] { 18, 18, 18, 18, 18, 2, 2, 2, 2, 4, 4, 4, 4, 5, 5, 5, 5, 0, 0, 0, 0, 4, 4, 4, 4, 6, 6, 6, 6, 2, 2, 2, 2, 6, 6, 6, 7, 7, 7, 7, 1, 1, 1, 1, 18, 18, 18, 18, 18, 18, 8, 8, 8, 8, 0, 0, 0, 5, 5, 5, 1, 1, 1, 1 };
            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesMysticJungle[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.49,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[10];
            for (var i = 0; i < 10; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i == 9 ? 18 : i,
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i),
                    features = new[] { HelpSymbolFeatureV3.Regular }
                };
            }

            return symbols;
        }

        private static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            var lines = new HelpLineConfigV3[10];
            for (var i = 0; i < 10; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = GlobalData.GameLineExtra[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
