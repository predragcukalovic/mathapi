using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;
using RNGUtils.RandomData;

namespace MathForUnicornGames.GameMiniMegaCash
{
    public class MatrixMiniMegaCash : Matrix
    {
        #region Constructor

        public MatrixMiniMegaCash()
            : base(5)
        {

        }

        #endregion

        #region Public properties

        public static int[] PlayLines = { 10 };

        public static readonly int[,] WinForLinesMiniMegaCash =
        {
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 20, 100, 500, 2500 },
            { 0, 0, 20, 50, 200 },
            { 0, 0, 20, 50, 200 },
            { 0, 0, 10, 25, 100 },
            { 0, 0, 10, 25, 100 },
            { 0, 0, 5, 15, 50 },
            { 0, 0, 5, 15, 50 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 }
        };

        #endregion

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineShifted).CalculateLineWin(WinForLinesMiniMegaCash, null, -1, 1);
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayMiniMegaCash(int[,] matrix)
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
        /// Za koliko se uveća ID simbola sketera od početne vrednosti.
        /// </summary>
        /// <returns></returns>
        private static int GetIdOffset()
        {
            var p = SoftwareRng.Next();
            if (p < 0.59909)
            {
                return 0;
            }
            if (p < 0.78459)
            {
                return 1;
            }
            if (p < 0.97018)
            {
                return 2;
            }
            return 3;
        }

        public void SetScatters()
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    var elem = GetElement(i, j);
                    if (elem < 3)
                    {
                        SetElement(i, j, 10 + 4 * elem + GetIdOffset());
                    }
                }
            }
        }

        public int GetScatterMultiplier()
        {
            var mult = new[] { 1, 2, 3, 5 };
            var megaCount = 0;
            var majorCount = 0;
            var minorCount = 0;
            var megaMult = 1;
            var majorMult = 1;
            var minorMult = 1;
            for (var i = 1; i < 4; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    var elem = GetElement(i, j);
                    if (elem >= 10 && elem <= 13)
                    {
                        megaCount++;
                        megaMult *= mult[elem - 10];
                    }
                    if (elem >= 14 && elem <= 17)
                    {
                        majorCount++;
                        majorMult *= mult[elem - 14];
                    }
                    if (elem >= 18 && elem <= 21)
                    {
                        minorCount++;
                        minorMult *= mult[elem - 18];
                    }
                }
            }
            if (megaCount == 3)
            {
                return megaMult * 40;
            }
            if (majorCount == 3)
            {
                return majorMult * 10;
            }
            if (minorCount == 3)
            {
                return minorMult * 5;
            }
            if (megaCount + majorCount + minorCount == 3)
            {
                return minorMult * majorMult * megaMult * 2;
            }
            return 0;
        }

        public byte[] GetScatterPositionArray()
        {
            var position = new byte[] { 255, 255, 255, 255, 255 };
            var index = 0;
            for (var i = 1; i < 4; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (GetElement(i, j) > 9)
                    {
                        position[index++] = (byte)(j * 5 + i);
                    }
                }
            }
            return position;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 8, 8, 3, 9, 4, 5, 6, 7, 7, 9, 3, 9, 4, 5, 6, 6, 7, 8, 3, 9, 9, 4, 5, 6, 7, 8, 3, 9, 4, 5, 6, 7, 8, 3, 9, 4, 5, 6, 7, 7, 8, 3, 9, 9, 4, 5, 6, 6, 7, 5, 3, 9, 4, 5, 6, 7, 4, 5 };
            fakeReels[1] = new[] { 8, 8, 3, 9, 10, 4, 5, 6, 7, 7, 9, 3, 9, 4, 11, 5, 6, 6, 7, 9, 3, 9, 9, 4, 5, 6, 7, 9, 3, 9, 4, 5, 6, 7, 9, 3, 9, 4, 5, 6, 7, 7, 8, 3, 9, 9, 4, 5, 6, 6, 7, 5, 3, 9, 4, 5, 17, 6, 7, 4, 5 };
            fakeReels[2] = new[] { 18, 8, 8, 3, 9, 4, 5, 6, 7, 7, 9, 3, 9, 4, 5, 6, 6, 7, 9, 3, 9, 9, 4, 5, 6, 7, 9, 3, 9, 4, 5, 6, 7, 9, 3, 9, 4, 5, 14, 6, 7, 7, 8, 3, 9, 9, 4, 5, 6, 6, 7, 15, 5, 3, 9, 4, 5, 6, 7, 4, 5 };
            fakeReels[3] = new[] { 7, 7, 16, 3, 8, 4, 12, 5, 6, 7, 7, 9, 3, 9, 4, 5, 6, 6, 7, 7, 9, 3, 9, 9, 4, 5, 6, 7, 8, 13, 3, 8, 4, 5, 6, 7, 9, 3, 9, 4, 5, 6, 7, 7, 3, 9, 9, 4, 5, 6, 6, 7, 5, 3, 8, 4, 5, 6, 19, 7, 4, 5 };
            fakeReels[4] = new[] { 7, 7, 3, 8, 4, 5, 6, 7, 7, 9, 3, 9, 4, 5, 6, 6, 7, 7, 9, 3, 9, 9, 4, 5, 6, 7, 8, 3, 8, 4, 5, 6, 7, 9, 3, 9, 4, 5, 6, 7, 7, 3, 9, 9, 4, 5, 6, 6, 7, 5, 3, 8, 4, 5, 6, 7, 4, 5 };
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
                coefficients[i] = WinForLinesMiniMegaCash[id, i];
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

        /// <summary>
        /// Returns symbols configuration.
        /// </summary>
        /// <returns></returns>
        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[10];

            for (var i = 0; i < 10; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    features = new[] { i > 2 ? HelpSymbolFeatureV3.Regular : HelpSymbolFeatureV3.Scatter },
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i)
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
                    pos[j] = UnicornGlobalData.GameLineShifted[i, j] - 1;
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }
    }
}
