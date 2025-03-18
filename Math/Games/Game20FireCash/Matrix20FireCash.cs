using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using RNGUtils.RandomData;

namespace Game20FireCash
{
    public class Matrix20FireCash : Matrix
    {
        public Matrix20FireCash() : base(5)
        {
        }

        private static readonly int[][] _Reels =
        {
            new[] { 2, 2, 2, 0, 6, 6, 6, 7, 7, 7, 5, 5, 5, 1, 1, 1, 6, 6, 6, 4, 4, 4, 3, 3, 3, 5, 5, 5, 2, 2, 2, 7, 7, 7, 4, 4, 4, 5, 5, 5, 6, 6, 6, 0, 3, 3, 3, 5, 5, 5, 4, 4, 4, 7, 7, 7, 5, 5, 5, 6, 6, 6, 2, 2, 2, 6, 6, 6, 5, 5, 5 },
            new[] { 6, 6, 6, 0, 4, 4, 4, 7, 7, 7, 2, 2, 2, 3, 3, 3, 5, 5, 5, 6, 6, 6, 7, 7, 7, 3, 3, 3, 1, 1, 1, 5, 5, 5, 6, 6, 6, 0, 7, 7, 7, 2, 2, 2, 6, 6, 6, 4, 4, 4, 5, 5, 5, 7, 7, 7, 3, 3, 3, 6, 6, 6, 5, 5, 5, 3, 3, 3, 4, 4, 4, 6, 6, 6, 3, 3, 3 },
            new[] { 5, 5, 5, 0, 4, 4, 4, 7, 7, 7, 6, 6, 6, 2, 2, 2, 5, 5, 5, 4, 4, 4, 1, 1, 1, 3, 3, 3, 7, 7, 7, 3, 3, 3, 6, 6, 6, 7, 7, 7, 5, 5, 5, 2, 2, 2, 4, 4, 4, 7, 7, 7, 3, 3, 3, 5, 5, 5, 7, 7, 7, 4, 4, 4, 5, 5, 5, 2, 2, 2, 6, 6, 6, 0, 2, 2, 2, 6, 6, 6, 4, 4, 4 },
            new[] { 5, 5, 5, 0, 7, 7, 7, 3, 3, 3, 5, 5, 5, 2, 2, 2, 4, 4, 4, 7, 7, 7, 2, 2, 2, 3, 3, 3, 6, 6, 6, 1, 1, 1, 5, 5, 5, 3, 3, 3, 4, 4, 4, 6, 6, 6, 0, 5, 5, 5, 7, 7, 7, 3, 3, 3, 7, 7, 7, 6, 6, 6, 5, 5, 5, 2, 2, 2, 6, 6, 6, 4, 4, 4, 6, 6, 6 },
            new[] { 4, 4, 4, 0, 7, 7, 7, 3, 3, 3, 5, 5, 5, 2, 2, 2, 6, 6, 6, 4, 4, 4, 5, 5, 5, 1, 1, 1, 6, 6, 6, 2, 2, 2, 7, 7, 7, 0, 6, 6, 6, 3, 3, 3, 5, 5, 5, 7, 7, 7, 4, 4, 4, 6, 6, 6, 5, 5, 5, 2, 2, 2, 5, 5, 5, 3, 3, 3, 6, 6, 6, 7, 7, 7 }
        };

        #region Public properties

        public static int[] PlayLines = { 20 };
        public static int[] PlayLinesFrenzy = { 2 };

        public static readonly int[,] GameLine20FireCash =
        {
            {2, 2, 2, 2, 2},
            {1, 1, 1, 1, 1},
            {3, 3, 3, 3, 3},
            {1, 2, 3, 2, 1},
            {3, 2, 1, 2, 3},
            {1, 1, 2, 3, 3},
            {3, 3, 2, 1, 1},
            {2, 3, 3, 3, 2},
            {2, 1, 1, 1, 2},
            {1, 2, 2, 2, 1},
            {3, 2, 2, 2, 3},
            {2, 3, 2, 1, 1},
            {2, 1, 2, 3, 3},
            {1, 2, 1, 2, 1},
            {3, 2, 3, 2, 3},
            {2, 2, 3, 2, 2},
            {2, 2, 1, 2, 2},
            {1, 3, 1, 3, 1},
            {3, 1, 3, 1, 3},
            {2, 1, 3, 1, 2}
        };

        public static readonly int[,] WinForLines20FireCash =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 20, 80, 400},
            {0, 0, 20, 40, 200},
            {0, 0, 20, 40, 200},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWild20FireCash = { 0, 0, 40, 400, 1000 };
        public static readonly int[] WinForScatter20FireCash = { 0, 0, 5, 20, 500 };

        #endregion

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GameLine20FireCash).CalculateLineWin(WinForLines20FireCash, WinForWild20FireCash, 1, 1);
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArray20FireCash(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        public static int[,] GetMatixArray()
        {
            var mat = new int[5, 5];
            for (var i = 0; i < 5; i++)
            {
                var l = _Reels[i].Length;
                var p = SoftwareRng.Next(l);
                for (var j = 0; j < 5; j++)
                {
                    mat[i, j] = _Reels[i][(p + j) % l];
                }
            }
            return mat;
        }

        public byte[] GetLinePositions(int lineNumber, int element)
        {
            var line = GetLine(lineNumber, GameLine20FireCash);
            var positionsArray = line.GetLinesPositions(GameLine20FireCash, lineNumber, 1, element);
            for (var i = 0; i < 5; i++)
            {
                positionsArray[i] = (byte)(positionsArray[i] == 255 ? 255 : positionsArray[i] - 5);
            }
            return positionsArray;
        }

        /// <summary>
        /// Daje dobitak za sketere.
        /// </summary>
        /// <returns></returns>
        public new int GetScatterWin()
        {
            var count = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (GetElement(i, j) == 0)
                    {
                        count++;
                    }
                }
            }
            return count == 0 ? 0 : WinForScatter20FireCash[count - 1];
        }

        /// <summary>
        /// Daje niz pozicija za sketere.
        /// </summary>
        /// <returns></returns>
        public byte[] GetScatterPositionsArray()
        {
            var positions = new byte[5];
            var index = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (GetElement(i, j) == 0)
                    {
                        positions[index++] = (byte)((j - 1) * 5 + i);
                    }
                }
            }
            for (; index < 5; index++)
            {
                positions[index] = 255;
            }
            return positions;
        }

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 2, 2, 2, 0, 6, 6, 6, 7, 7, 7, 5, 5, 5, 1, 1, 1, 6, 6, 6, 4, 4, 4, 3, 3, 3, 5, 5, 5, 2, 2, 2, 7, 7, 7, 4, 4, 4, 5, 5, 5, 6, 6, 6, 0, 3, 3, 3, 5, 5, 5, 4, 4, 4, 7, 7, 7 };
            fakeReels[1] = new[] { 6, 6, 6, 0, 4, 4, 4, 7, 7, 7, 2, 2, 2, 3, 3, 3, 5, 5, 5, 6, 6, 6, 7, 7, 7, 3, 3, 3, 1, 1, 1, 5, 5, 5, 6, 6, 6, 0, 7, 7, 7, 2, 2, 2, 6, 6, 6, 4, 4, 4, 5, 5, 5, 7, 7, 7, 3, 3, 3 };
            fakeReels[2] = new[] { 5, 5, 5, 0, 4, 4, 4, 7, 7, 7, 6, 6, 6, 2, 2, 2, 5, 5, 5, 4, 4, 4, 1, 1, 1, 3, 3, 3, 7, 7, 7, 3, 3, 3, 0, 6, 6, 6, 7, 7, 7, 5, 5, 5, 2, 2, 2, 4, 4, 4, 0, 7, 7, 7, 3, 3, 3 };
            fakeReels[3] = new[] { 5, 5, 5, 0, 7, 7, 7, 3, 3, 3, 5, 5, 5, 2, 2, 2, 4, 4, 4, 7, 7, 7, 2, 2, 2, 3, 3, 3, 6, 6, 6, 1, 1, 1, 5, 5, 5, 3, 3, 3, 4, 4, 4, 6, 6, 6, 0, 5, 5, 5, 7, 7, 7, 3, 3, 3, 7, 7, 7, 6, 6, 6 };
            fakeReels[4] = new[] { 4, 4, 4, 0, 7, 7, 7, 3, 3, 3, 5, 5, 5, 2, 2, 2, 6, 6, 6, 4, 4, 4, 5, 5, 5, 1, 1, 1, 6, 6, 6, 2, 2, 2, 7, 7, 7, 0, 6, 6, 6, 3, 3, 3, 5, 5, 5, 7, 7, 7, 4, 4, 4, 0, 6, 6, 6, 5, 5, 5 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            if (id == 0)
            {
                return WinForScatter20FireCash;
            }
            if (id == 1)
            {
                return WinForWild20FireCash;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLines20FireCash[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.47,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[8];
            for (var i = 0; i < 8; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i),
                    features = new[] { HelpSymbolFeatureV3.Regular }
                };
            }

            return symbols;
        }

        private static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            var lines = new HelpLineConfigV3[20];
            for (var i = 0; i < 20; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = GameLine20FireCash[i, j] - 1;
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
