using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;
using RNGUtils.RandomData;

namespace MathForUnicornGames.GameSurfinHeat
{
    public class MatrixSurfinHeat : Matrix
    {
        #region Constructor

        public MatrixSurfinHeat()
            : base(5)
        {

        }

        #endregion

        #region Public properties

        public static int[] PlayLines = { 10 };

        public static readonly int[,] WinForLinesSurfinHeat =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 10, 40, 200},
            {0, 0, 10, 40, 200},
            {0, 0, 6, 20, 80},
            {0, 0, 6, 20, 80},
            {0, 0, 2, 6, 20},
            {0, 0, 2, 6, 20},
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

        public static readonly int[] PossibleWinsForScatter = { 2, 4, 8, 16, 40, 100, 500 };
        public static readonly double[] ProbsForScatter = { 0.21925, 0.45, 0.225, 0.045, 0.05, 0.01, 0.00075 };

        #endregion

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineShifted).CalculateLineWin(WinForLinesSurfinHeat, null, -1, 1);
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza za igru SurfinHeat.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArraySurfinHeat(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        public static int GetScatterProbIndex()
        {
            var sum = 0.0;
            var rnd = SoftwareRng.Next();
            var n = ProbsForScatter.Length;
            for (var i = 0; i < n; i++)
            {
                sum += ProbsForScatter[i];
                if (rnd < sum)
                {
                    return i + 1;
                }
            }
            return 0;
        }

        #region Struct V3

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 4, 6, 5, 2, 0, 4, 6, 0, 1, 5, 6, 3, 4, 2, 5, 6, 1, 4, 3, 2, 0, 5, 3, 5, 6, 4, 5, 1, 2, 4, 3, 5, 6, 4, 2, 0, 3, 1, 6 };
            fakeReels[1] = new[] { 5, 3, 0, 2, 6, 5, 0, 1, 4, 0, 3, 5, 6, 2, 4, 5, 0, 1, 4, 3, 0, 5, 2, 6, 4, 0, 6, 3, 6, 1, 6, 2, 4, 0, 4, 6, 2, 5, 4, 3, 5, 1, 0 };
            fakeReels[2] = new[] { 4, 6, 5, 2, 0, 6, 1, 4, 6, 3, 5, 0, 2, 5, 0, 1, 3, 4, 2, 0, 5, 3, 5, 0, 4, 5, 1, 6, 2, 4, 3, 4, 6, 0, 5, 2, 3, 6, 4, 1, 6 };
            fakeReels[3] = new[] { 5, 3, 0, 2, 5, 0, 1, 5, 0, 3, 4, 6, 2, 4, 5, 0, 1, 3, 0, 5, 2, 4, 0, 6, 3, 4, 0, 4, 1, 6, 2, 0, 4, 6, 2, 5, 6, 5, 3, 6, 4, 1, 6 };
            fakeReels[4] = new[] { 4, 6, 5, 2, 0, 4, 6, 0, 1, 5, 3, 4, 6, 2, 5, 1, 3, 0, 4, 2, 0, 5, 3, 4, 6, 4, 5, 1, 0, 2, 5, 3, 6, 4, 2, 6, 3, 0, 5, 1, 6 };

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
                coefficients[i] = WinForLinesSurfinHeat[id, i];
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
            var symbols = new HelpSymbolConfigV3<object>[8];

            symbols[0] = new HelpSymbolConfigV3<object>
            {
                id = 0,
                features = new[] { HelpSymbolFeatureV3.FreeSpin },
                extra = new HelpSymbolExtraV3(),
                coefficients = GetSymbolCoefficients(0)
            };

            for (var i = 1; i < 7; i++)
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
            var lines = new HelpLineConfigV3[5];
            for (var i = 0; i < 5; i++)
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
