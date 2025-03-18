using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;

namespace MathForUnicornGames.Game20SuperFlames
{
    public class Matrix20SuperFlames : Matrix
    {
        #region Public properties

        public static readonly int[,] WinForLines20SuperFlames =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 100, 400, 2000},
            {0, 0, 40, 100, 400},
            {0, 0, 10, 40, 100},
            {0, 0, 10, 40, 100},
            {0, 0, 4, 20, 50},
            {0, 0, 4, 20, 50}
        };
        public static readonly int[] WinForScatter20SuperFlames = { 0, 0, 4, 20, 500 };
        public static int[] PlayLines = { 20 };

        #endregion

        #region Constructor

        public Matrix20SuperFlames()
            : base(5)
        {

        }

        #endregion

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza za igru Froot Classic.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArray20SuperFlames(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineShifted).CalculateLineWin(WinForLines20SuperFlames, null, -1, 1);
        }

        /// <summary>
        /// Daje niz pozicija za simbol.
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
                        positions[index++] = (byte)(j * 5 + i);
                    }
                }
            }
            for (; index < 5; index++)
            {
                positions[index] = 255;
            }
            return positions;
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
            return count == 0 ? 0 : WinForScatter20SuperFlames[count - 1];
        }

        #region Struct V3

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];

            fakeReels[0] = new[] { 0, 5, 5, 5, 4, 4, 4, 3, 3, 3, 0, 6, 6, 6, 4, 4, 4, 5, 5, 5, 0, 2, 2, 2, 6, 6, 6, 3, 3, 3, 0, 6, 6, 6, 1, 1, 1, 4, 4, 4, 0, 5, 5, 5, 1, 1, 1, 6, 6, 6, 0, 3, 3, 3, 1, 1, 1, 4, 4, 4, 2, 2, 2, 5, 5, 5, 6, 6, 6, 3, 3, 3, 4, 4, 4, 5, 5, 5, 1, 1, 1, 6, 6, 6, 2, 2, 2 };
            fakeReels[1] = new[] { 0, 5, 5, 5, 4, 4, 4, 3, 3, 3, 0, 6, 6, 6, 4, 4, 4, 5, 5, 5, 0, 2, 2, 2, 6, 6, 6, 3, 3, 3, 0, 6, 6, 6, 1, 1, 1, 4, 4, 4, 0, 5, 5, 5, 1, 1, 1, 6, 6, 6, 0, 3, 3, 3, 1, 1, 1, 4, 4, 4, 2, 2, 2, 5, 5, 5, 6, 6, 6, 3, 3, 3, 4, 4, 4, 5, 5, 5, 1, 1, 1, 6, 6, 6, 2, 2, 2 };
            fakeReels[2] = new[] { 0, 5, 5, 5, 4, 4, 4, 3, 3, 3, 0, 6, 6, 6, 4, 4, 4, 5, 5, 5, 0, 2, 2, 2, 6, 6, 6, 3, 3, 3, 0, 6, 6, 6, 1, 1, 1, 4, 4, 4, 0, 5, 5, 5, 1, 1, 1, 6, 6, 6, 0, 3, 3, 3, 1, 1, 1, 4, 4, 4, 2, 2, 2, 5, 5, 5, 6, 6, 6, 3, 3, 3, 4, 4, 4, 5, 5, 5, 1, 1, 1, 6, 6, 6, 2, 2, 2 };
            fakeReels[3] = new[] { 0, 5, 5, 5, 4, 4, 4, 3, 3, 3, 0, 6, 6, 6, 4, 4, 4, 5, 5, 5, 0, 2, 2, 2, 6, 6, 6, 3, 3, 3, 0, 6, 6, 6, 1, 1, 1, 4, 4, 4, 0, 5, 5, 5, 1, 1, 1, 6, 6, 6, 0, 3, 3, 3, 1, 1, 1, 4, 4, 4, 2, 2, 2, 5, 5, 5, 6, 6, 6, 3, 3, 3, 4, 4, 4, 5, 5, 5, 1, 1, 1, 6, 6, 6, 2, 2, 2 };
            fakeReels[4] = new[] { 0, 5, 5, 5, 4, 4, 4, 3, 3, 3, 0, 6, 6, 6, 4, 4, 4, 5, 5, 5, 0, 2, 2, 2, 6, 6, 6, 3, 3, 3, 0, 6, 6, 6, 1, 1, 1, 4, 4, 4, 0, 5, 5, 5, 1, 1, 1, 6, 6, 6, 0, 3, 3, 3, 1, 1, 1, 4, 4, 4, 2, 2, 2, 5, 5, 5, 6, 6, 6, 3, 3, 3, 4, 4, 4, 5, 5, 5, 1, 1, 1, 6, 6, 6, 2, 2, 2 };
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
                if (id == 0)
                {
                    coefficients[i] = WinForScatter20SuperFlames[i];
                }
                else
                {
                    coefficients[i] = WinForLines20SuperFlames[id, i];
                }
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.0,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[7];

            for (var i = 0; i < 7; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    features = new[] { HelpSymbolFeatureV3.Regular },
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i)
                };
            }
            return symbols;
        }

        public static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            var lines = new HelpLineConfigV3[20];
            for (var i = 0; i < 20; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = UnicornGlobalData.GameLineShifted[i, j] - 1;
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
