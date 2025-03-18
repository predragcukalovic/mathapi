using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;

namespace GameBlazingHeat
{
    public class MatrixBlazingHeat : Matrix
    {
        #region Public properties

        public static readonly int[,] WinForLinesBlazingHeat =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 20, 200, 1000},
            {0, 0, 10, 40, 100},
            {0, 0, 10, 40, 100},
            {0, 0, 4, 10, 40},
            {0, 0, 4, 10, 40},
            {0, 0, 4, 10, 40},
            {0, 1, 4, 10, 40},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForScatterBlazingHeat = { 0, 0, 2, 10, 50 };
        public static int[] PlayLines = { 1 };

        public static readonly int[,] GameLineBlazingHeat =
        {
            {2, 2, 2, 2, 2},
            {1, 1, 1, 1, 1},
            {3, 3, 3, 3, 3},
            {1, 2, 3, 2, 1},
            {3, 2, 1, 2, 3}
        };

        #endregion

        public MatrixBlazingHeat() : base(5)
        {
        }

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GameLineBlazingHeat).CalculateLineWin(WinForLinesBlazingHeat, null, -1, 1);
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayBlazingHeat(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        public byte[] GetLinePositions(int lineNumber, int element)
        {
            var line = GetLine(lineNumber, GameLineBlazingHeat);
            var positionsArray = line.GetLinesPositions(GameLineBlazingHeat, lineNumber, -1, element);
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
            return count == 0 ? 0 : WinForScatterBlazingHeat[count - 1];
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
            fakeReels[0] = new[] { 6, 6, 6, 0, 5, 5, 5, 1, 1, 7, 7, 7, 2, 2, 5, 2, 2, 0, 4, 4, 4, 3, 3, 0, 6, 5, 2, 1 };
            fakeReels[1] = new[] { 3, 3, 4, 4, 4, 4, 0, 7, 7, 7, 6, 6, 6, 5, 5, 7, 7, 3, 2, 2, 5, 6, 6, 3, 0, 2, 5, 5, 5, 0 };
            fakeReels[2] = new[] { 5, 5, 5, 1, 6, 6, 4, 3, 3, 2, 4, 4, 4, 1, 7, 7, 7, 7, 0, 4, 4, 3, 6, 6, 6, 0, 5, 5, 7, 7, 2 };
            fakeReels[3] = new[] { 2, 2, 7, 7, 7, 6, 6, 6, 0, 4, 4, 4, 2, 3, 3, 1, 4, 4, 7, 1, 5, 5, 5, 6, 6, 1, 5, 5, 0, 4, 4 };
            fakeReels[4] = new[] { 4, 4, 4, 1, 7, 7, 2, 6, 6, 6, 5, 5, 5, 0, 4, 4, 4, 0, 2, 2, 7, 7, 7, 1, 6, 6, 5, 3, 3 };

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
                return WinForScatterBlazingHeat;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesBlazingHeat[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.15,
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
            var lines = new HelpLineConfigV3[5];
            for (var i = 0; i < 5; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = GameLineBlazingHeat[i, j] - 1;
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
