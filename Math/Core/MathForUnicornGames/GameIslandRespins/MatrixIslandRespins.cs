using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;


namespace MathForUnicornGames.GameIslandRespins
{
    public class MatrixIslandRespins : Matrix
    {
        #region Constructor
        public MatrixIslandRespins()
            : base(5)
        {

        }
        #endregion

        #region Public properties

        public static int[] PlayLines = { 5 };

        public static readonly int[,] WinForLinesIslandRespins =
        {
            {0, 20, 500, 5000, 25000},
            {0, 10, 100, 1000, 5000},
            {0, 0, 50, 200, 1000},
            {0, 0, 50, 200, 1000},
            {0, 0, 20, 100, 250},
            {0, 0, 20, 100, 250},
            {0, 0, 20, 100, 200},
            {0, 0, 20, 100, 200},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };

        public static readonly int[] WinForSpecialTopSymbol = { 0, 20, 500, 5000, 25000 };
        #endregion

        #region Public methods
        /// <summary>
        /// uzima liniju iz matrice
        /// </summary>
        /// <param name="lineNumber">broj linije, 1 -- 15</param>
        /// <param name="lines">Linije </param>
        /// <returns>vraća liniju pod datim brojem</returns>
        public new LineIslandRespins GetLine(int lineNumber, int[,] lines)
        {
            try
            {
                var line = new LineIslandRespins();
                for (var i = 0; i < 5; i++)
                {
                    line.SetElement(i, GetElement(i, lines[lineNumber - 1, i]));
                }
                return line;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineShifted).CalculateLineWinWithSemiLines(WinForLinesIslandRespins, WinForSpecialTopSymbol, 0, 1);
        }
        /// <summary>
        /// Vraca dobitni simbol za liniju lineNumber 
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="gameLines"></param>
        /// <param name="lineWin"></param>
        /// <param name="specialTopSymbol"></param>
        /// <param name="regularTopSymbol"></param>
        /// <returns></returns>
        public int GetWinningElementForLine(int lineNumber, int[,] gameLines, int lineWin, int specialTopSymbol, int regularTopSymbol)
        {
            return GetLine(lineNumber, gameLines).GetWinningElement(specialTopSymbol, regularTopSymbol, lineWin, WinForSpecialTopSymbol);
        }
        /// <summary>
        /// Broj pojavljivanja elementa u matrici 5x3
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public new int GetNumberOfElement(int element)
        {
            var counter = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (element == GetElement(i, j))
                        counter++;
                }
            }
            return counter;
        }
        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza za igru Island Respins.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayIslandRespins(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        #endregion

        #region Struct V3


        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 4, 6, 5, 2, 7, 4, 6, 7, 1, 5, 6, 3, 4, 2, 5, 6, 1, 4, 3, 7, 2, 7, 5, 0, 7, 3, 5, 6, 4, 5, 1, 2, 4, 3, 5, 6, 4, 2, 7, 3, 7, 1, 6 };
            fakeReels[1] = new[] { 5, 3, 7, 2, 6, 5, 7, 1, 4, 7, 3, 5, 6, 2, 4, 5, 7, 1, 4, 3, 7, 5, 2, 6, 4, 0, 6, 3, 6, 1, 6, 2, 4, 7, 4, 6, 2, 5, 4, 3, 5, 1, 7 };
            fakeReels[2] = new[] { 4, 6, 5, 2, 7, 6, 1, 4, 6, 3, 5, 7, 2, 5, 7, 1, 3, 4, 2, 7, 5, 0, 7, 3, 5, 7, 4, 5, 1, 6, 2, 4, 3, 4, 6, 7, 5, 2, 3, 6, 4, 1, 6 };
            fakeReels[3] = new[] { 5, 3, 7, 2, 5, 7, 1, 5, 7, 3, 4, 6, 2, 4, 5, 7, 1, 3, 7, 5, 2, 4, 0, 6, 3, 4, 7, 4, 1, 6, 2, 7, 4, 6, 2, 5, 6, 5, 3, 6, 4, 1, 6 };
            fakeReels[4] = new[] { 4, 6, 5, 2, 7, 4, 6, 7, 1, 5, 3, 4, 6, 2, 5, 1, 3, 7, 4, 2, 7, 5, 0, 7, 3, 4, 6, 4, 5, 1, 7, 2, 5, 3, 6, 4, 2, 6, 3, 7, 5, 1, 6 };

            return fakeReels;
        }
        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja u toku respina
        /// </summary>
        /// <returns></returns>
        public static int[][] GetGratisFakeReels()
        {
            var fakeReels = new int[5][];

            fakeReels[0] = new[] { 6, 3, 2, 5, 4, 0, 7, 6, 2, 7, 5, 4, 6, 0, 3, 2, 4, 5, 3, 7, 0, 5, 2, 5, 3, 7, 4, 0, 6, 6, 4, 2, 0, 5, 7, 3, 6, 7, 4, 3, 5, 6, 2, 0, 7, 4 };
            fakeReels[1] = new[] { 6, 3, 2, 5, 4, 0, 7, 6, 2, 7, 5, 4, 6, 0, 3, 2, 4, 5, 3, 7, 0, 5, 2, 5, 3, 7, 4, 0, 6, 6, 4, 2, 0, 5, 7, 3, 6, 7, 4, 3, 5, 6, 2, 0, 7, 4 };
            fakeReels[2] = new[] { 6, 3, 2, 5, 4, 0, 7, 6, 2, 7, 5, 4, 6, 0, 3, 2, 4, 5, 3, 7, 0, 5, 2, 5, 3, 7, 4, 0, 6, 6, 4, 2, 0, 5, 7, 3, 6, 7, 4, 3, 5, 6, 2, 0, 7, 4 };
            fakeReels[3] = new[] { 6, 3, 2, 5, 4, 0, 7, 6, 2, 7, 5, 4, 6, 0, 3, 2, 4, 5, 3, 7, 0, 5, 2, 5, 3, 7, 4, 0, 6, 6, 4, 2, 0, 5, 7, 3, 6, 7, 4, 3, 5, 6, 2, 0, 7, 4 };
            fakeReels[4] = new[] { 6, 3, 2, 5, 4, 0, 7, 6, 2, 7, 5, 4, 6, 0, 3, 2, 4, 5, 3, 7, 0, 5, 2, 5, 3, 7, 4, 0, 6, 6, 4, 2, 0, 5, 7, 3, 6, 7, 4, 3, 5, 6, 2, 0, 7, 4 };
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
                coefficients[i] = WinForLinesIslandRespins[id, i];
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

            for (var i = 1; i < 8; i++)
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

        #endregion Struct v3
    }
}
