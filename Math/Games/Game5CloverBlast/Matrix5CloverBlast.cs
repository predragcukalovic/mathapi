using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;

namespace Game5CloverBlast
{
    public class Matrix5CloverBlast : Matrix
    {
        #region Public properties

        public static readonly int[,] WinForLines5CloverBlast =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 8, 20, 400},
            {0, 0, 8, 20, 400},
            {0, 0, 4, 10, 40},
            {0, 0, 4, 10, 40},
            {0, 0, 4, 10, 40},
            {0, 0, 2, 4, 20},
            {0, 0, 2, 4, 20},
            {0, 0, 2, 4, 20},
            {0, 0, 2, 4, 20},
            {0, 0, 2, 4, 20}
        };
        public static readonly int[] WinForWild5CloverBlast = { 0, 0, 0, 0, 400 };
        public static int[] PlayLines = { 1 };

        public static readonly int[,] GameLine5CloverBlast =
        {
            {2, 2, 2, 2, 2},
            {1, 1, 1, 1, 1},
            {3, 3, 3, 3, 3},
            {1, 2, 3, 2, 1},
            {3, 2, 1, 2, 3}
        };

        #endregion

        public Matrix5CloverBlast() : base(5)
        {
        }

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GameLine5CloverBlast).CalculateLineWin(WinForLines5CloverBlast, WinForWild5CloverBlast, 0, 1);
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArray5CloverBlast(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][]; //TODO
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
                return WinForWild5CloverBlast;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLines5CloverBlast[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.2,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[11];
            for (var i = 0; i < 11; i++)
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
                    pos[j] = GameLine5CloverBlast[i, j] - 1;
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
