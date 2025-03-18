using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;
using RNGUtils.RandomData;

namespace MathForUnicornGames.GamePumpkinHorror
{
    public class MatrixPumpkinHorror : Matrix
    {
        #region Reels

        private static readonly int[][] _Reels =
        new[]{new[]{2, 3, 3, 6, 6, 2, 7, 4, 4, 7, 2, 7, 4, 4, 4, 2, 6, 5, 6, 5, 4, 3, 3, 7, 7, 7, 5, 5, 4, 3, 7, 7, 7, 5, 5, 3, 6, 6, 6, 7, 4, 4, 7, 5, 5, 5, 7, 4, 4, 6},
new[]{2, 7, 7, 3, 3, 2, 4, 4, 7, 5, 2, 5, 6, 6, 4, 2, 4, 7, 7, 3, 3, 7, 5, 5, 3, 3, 4, 6, 5, 3, 6, 5, 6, 5, 7, 3, 7, 5, 5, 7, 4, 4, 7, 7, 6, 6, 6, 4, 4, 6},
new[]{2, 5, 4, 5, 4, 2, 3, 3, 4, 7, 2, 5, 4, 4, 4, 2, 6, 5, 5, 3, 3, 7, 4, 7, 4, 4, 6, 6, 6, 3, 4, 4, 7, 7, 7, 3, 6, 7, 6, 6, 4, 7, 4, 7, 5, 5, 7, 7, 7, 6, 6, 6, 2, 3},
new[]{2, 7, 7, 3, 3, 2, 7, 7, 4, 4, 2, 6, 6, 5, 5, 2, 6, 4, 3, 3, 6, 6, 6, 3, 3, 7, 4, 7, 5, 3, 5, 7, 7, 7, 4, 3, 4, 5, 6, 6, 6, 6, 4, 4, 4, 5, 5, 6, 6, 6},
new[]{2, 6, 3, 6, 3, 2, 4, 5, 5, 4, 2, 7, 4, 6, 7, 2, 7, 6, 5, 3, 3, 7, 7, 4, 4, 6, 5, 5, 5, 3, 6, 4, 4, 3, 6, 6, 6, 5, 7, 4, 4, 7, 5, 7, 5, 5, 7, 7, 7, 5}};

        private static readonly int[][][] _ReelsSpecial =
{new[]{new[]{-1, -1, -1, -1, 0}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, 0}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, 0}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, 0}, new[]{-1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, 0}},
new[]{new[]{-1, -1, -1, -1, 10}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, 10}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, 10}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, 10}, new[]{-1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, 10}},
new[]{new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}},
new[]{new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}},
new[]{new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}},
new[]{new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}},
new[]{new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}},
new[]{new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}},
new[]{new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}},
new[]{new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}},
new[]{new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}},
new[]{new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}},
new[]{new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}},
new[]{new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}},
new[]{new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}},
new[]{new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}},
new[]{new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}},
new[]{new[]{-1, -1, -1, -1, -1, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}},
new[]{new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}, new[]{-1, -1, -1, 8, 9, -1}}};

        #endregion

        #region Constructor

        public MatrixPumpkinHorror()
            : base(5)
        {

        }

        #endregion

        public static readonly int[,] WinForPumpkinHorror =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 50, 100, 250},
            {0, 0, 20, 40, 100},
            {0, 0, 10, 20, 50},
            {0, 0, 10, 20, 50},
            {0, 0, 5, 10, 25},
            {0, 0, 2, 5, 10},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static int[] WinForWildPumpkinHorror = { 0, 0, 50, 100, 250 };
        public static int[] WinForScatterPumpkinHorror = { 0, 10, 20, 100, 10000 };
        private static readonly double[] _ReelProbs = { 0.0033512, 0.0033512, 0.0033512, 0.0033512, 0.0033512, 0.0366488, 0.0366488, 0.0366488, 0.0366488, 0.0366488, 0.21619674, 0.09368525, 0.09368525, 0.09368525, 0.09368525, 0.09368525, 0.00720656, 0.00720656, 0.00720656, 0.00720656, 0.00720656, 0.00720656, 0.00720656, 0.00720656, 0.00720656, 0.00720656, 0.00360328, 0.00360328, 0.00360328, 0.00360328, 0.00360328, 0.00360328, 0.00360328, 0.00360328, 0.00360328, 0.00360328, 0.00144131, 0.00144131, 0.00144131, 0.00144131, 0.00144131, 0.00007206 };
        public static int[] PlayLines = { 10 };

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineShifted).CalculateLineWin(WinForPumpkinHorror, WinForWildPumpkinHorror, 1, 1);
        }

        public static int[,] GetMatixArray()
        {
            var reelsSet = -1;
            var sum = 0.0;
            var rnd = SoftwareRng.Next();
            var n = _ReelProbs.Length;
            for (var i = 0; i < n; i++)
            {
                sum += _ReelProbs[i];
                if (rnd < sum)
                {
                    reelsSet = i;
                    break;
                }
            }
            var mat = new int[5, 5];
            for (var i = 0; i < 5; i++)
            {
                var l = _Reels[i].Length;
                var p = SoftwareRng.Next(l);
                var lspec = _ReelsSpecial[reelsSet][i].Length;
                var pspec = SoftwareRng.Next(lspec);
                for (var j = 0; j < 5; j++)
                {
                    mat[i, j] = _Reels[i][(p + j) % l];
                    var spec = _ReelsSpecial[reelsSet][i][(pspec + j) % lspec];
                    if (spec != -1)
                    {
                        mat[i, j] = spec;
                    }
                }
            }
            return mat;
        }

        /// <summary>
		/// Konstruiše matricu na osnovu dvodimenzionalnog niza za igru PumpkinHorror.
		/// </summary>
		/// <param name="matrix"></param>
		public void FromMatrixArrayPumpkinHorror(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, j]);
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

            fakeReels[0] = new[] { 2, 3, 3, 10, 11, 6, 6, 2, 7, 4, 4, 7, 2, 7, 0, 4, 4, 4, 2, 6, 5, 6, 5, 4, 3, 3, 10, 11, 7, 7, 7, 5, 5, 4, 3, 7, 7, 7, 20, 5, 5, 3, 6, 6, 6, 7, 4, 4, 20, 7, 5, 5, 5, 7, 4, 4, 6 };
            fakeReels[1] = new[] { 2, 3, 3, 10, 11, 6, 6, 2, 7, 4, 4, 7, 2, 7, 0, 4, 4, 4, 2, 6, 5, 6, 5, 4, 3, 3, 10, 11, 7, 7, 7, 5, 5, 4, 3, 7, 7, 7, 20, 5, 5, 3, 6, 6, 6, 7, 4, 4, 20, 7, 5, 5, 5, 7, 4, 4, 6 };
            fakeReels[2] = new[] { 2, 3, 3, 10, 11, 6, 6, 2, 7, 4, 4, 7, 2, 7, 0, 4, 4, 4, 2, 6, 5, 6, 5, 4, 3, 3, 10, 11, 7, 7, 7, 5, 5, 4, 3, 7, 7, 7, 20, 5, 5, 3, 6, 6, 6, 7, 4, 4, 20, 7, 5, 5, 5, 7, 4, 4, 6 };
            fakeReels[3] = new[] { 2, 3, 3, 10, 11, 6, 6, 2, 7, 4, 4, 7, 2, 7, 0, 4, 4, 4, 2, 6, 5, 6, 5, 4, 3, 3, 10, 11, 7, 7, 7, 5, 5, 4, 3, 7, 7, 7, 20, 5, 5, 3, 6, 6, 6, 7, 4, 4, 20, 7, 5, 5, 5, 7, 4, 4, 6 };
            fakeReels[4] = new[] { 2, 3, 3, 10, 11, 6, 6, 2, 7, 4, 4, 7, 2, 7, 0, 4, 4, 4, 2, 6, 5, 6, 5, 4, 3, 3, 10, 11, 7, 7, 7, 5, 5, 4, 3, 7, 7, 7, 20, 5, 5, 3, 6, 6, 6, 7, 4, 4, 20, 7, 5, 5, 5, 7, 4, 4, 6 };
            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            if (id == 10)
            {
                return WinForScatterPumpkinHorror;
            }
            if (id == 1)
            {
                return WinForWildPumpkinHorror;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForPumpkinHorror[id, i];
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

            for (var i = 0; i < 8; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i == 7 ? 10 : i + 1,
                    features = new[] { HelpSymbolFeatureV3.Regular },
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i)
                };
            }
            return symbols;
        }

        public static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            var lines = new HelpLineConfigV3[10];
            for (var i = 0; i < 10; i++)
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
