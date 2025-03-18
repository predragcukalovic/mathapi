using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;

namespace GameHotHotStereoWin
{
    public class MatrixHotHotStereoWin : Matrix
    {
        #region Public properties

        public static readonly int[,] WinForLinesHotHotStereoWin =
        {
            {0, 0, 20, 100, 1000},
            {0, 0, 5, 20, 100},
            {0, 0, 5, 20, 100},
            {0, 0, 2, 6, 25},
            {0, 0, 2, 6, 25},
            {0, 0, 2, 6, 25},
            {0, 0, 2, 6, 25}
        };
        public static int[] PlayLines = { 1 };

        public static readonly int[,] GameLineHotHotStereoWin =
        {
            {2, 2, 2, 2, 2},
            {1, 1, 1, 1, 1},
            {3, 3, 3, 3, 3},
            {1, 2, 3, 2, 1},
            {3, 2, 1, 2, 3}
        };

        #endregion

        public MatrixHotHotStereoWin() : base(5)
        {
        }

        /// <summary>
        /// Računa dobitak jedne linije iz matrice sleva na desno
        /// </summary>
        /// <param name="lineNumber">Broj linije za koji računa dobitak</param>
        /// <returns>Vraća dobitak koji daje tražena linija za uložen 1 kredit</returns>
        public virtual int CalculateLeftWinOfLine(int lineNumber)
        {
            return GetLine(lineNumber, GameLineHotHotStereoWin).CalculateLineWin(WinForLinesHotHotStereoWin, null, -1, 1);
        }

        /// <summary>
        /// Računa dobitak jedne linije iz matrice zdesna na levo
        /// </summary>
        /// <param name="lineNumber">Broj linije za koji računa dobitak</param>
        /// <returns>Vraća dobitak koji daje tražena linija za uložen 1 kredit</returns>
        public virtual int CalculateRightWinOfLine(int lineNumber)
        {
            return GetLine(lineNumber, GameLineHotHotStereoWin).CalculateRightLineWin(WinForLinesHotHotStereoWin, null, -1, 1);
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayHotHotStereoWin(int[,] matrix)
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
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 6, 6, 6, 0, 5, 5, 5, 1, 1, 2, 2, 5, 2, 2, 0, 4, 4, 4, 3, 3, 3, 0, 6, 5, 2, 1 };
            fakeReels[1] = new[] { 3, 3, 3, 4, 4, 4, 4, 0, 6, 6, 6, 5, 5, 3, 2, 2, 5, 6, 6, 3, 0, 2, 5, 5, 5, 0 };
            fakeReels[2] = new[] { 5, 5, 5, 1, 6, 6, 4, 3, 3, 3, 2, 4, 4, 4, 1, 0, 4, 4, 3, 6, 6, 6, 0, 5, 5, 2 };
            fakeReels[3] = new[] { 2, 2, 6, 6, 6, 0, 4, 4, 4, 2, 3, 3, 3, 1, 4, 4, 1, 5, 5, 5, 6, 6, 1, 5, 5, 0, 4, 4 };
            fakeReels[4] = new[] { 4, 4, 4, 1, 2, 6, 6, 6, 5, 5, 5, 0, 4, 4, 4, 0, 2, 2, 1, 6, 6, 5, 3, 3, 3 };

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
                coefficients[i] = WinForLinesHotHotStereoWin[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)95.01,
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
                    pos[j] = GameLineHotHotStereoWin[i, j] - 1;
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
