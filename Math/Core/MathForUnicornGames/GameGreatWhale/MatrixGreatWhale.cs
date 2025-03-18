using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;


namespace MathForUnicornGames.GameGreatWhale
{
    public class MatrixGreatWhale : Matrix
    {
        #region Private properties

        private readonly int[,] _StickyWildMatrix;

        #endregion

        #region Constructor

        public MatrixGreatWhale()
            : base(5)
        {
            _StickyWildMatrix = new int[5, 5];
        }

        #endregion

        #region Public properties

        public static int[] PlayLines = { 5 };

        public static readonly int[,] WinForLinesGreatWhale =
        {
            {0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0 },
            {0, 5, 50, 500, 1000},
            {0, 5, 25, 150, 750},
            {0, 5, 25, 150, 750},
            {0, 0, 5, 40, 150},
            {0, 0, 5, 40, 150},
            {0, 0, 5, 25, 100},
            {0, 0, 5, 25, 100},
            {0, 0, 5, 25, 100},
            {0, 0, 5, 25, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };

        public static readonly int[] WinForWildGreatWhale = { 0, 10, 100, 750, 2500 };

        public const int GRATIS_GAMES = 10;

        #endregion
        /// <summary>
        /// Računa dobitak linije. Wild symbol za igru je  postavljen kao 0.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineShifted).CalculateLineWin(WinForLinesGreatWhale, WinForWildGreatWhale, 1, 1);
        }
        /// <summary>
        /// Daje dobitni element za liniju.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <param name="wild">Wild element.</param>
        /// <param name="winForWild">Dobitak za wild.</param>
        /// <param name="lineWin">Dobitak linije.</param>
        /// <param name="gameLines">Linije na koje se igra.</param>
        /// <returns></returns>
        public new int GetWinningElementForLine(int lineNumber, int wild, int[] winForWild, int lineWin, int[,] gameLines)
        {
            return GetLine(lineNumber, gameLines).GetWinningElement(wild, lineWin, WinForLinesGreatWhale, winForWild);
        }

        /// <summary>
        /// geter za element iz _StickyWild matrice na poziciji [i,j]
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>vraća element iz matrice na poziciji [i,j]</returns>
        public int GetFromStickyWildMatrix(int i, int j)
        {
            return _StickyWildMatrix[i, j];
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
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza za igru Island Respins.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayGreatWhale(int[,] matrix)
        {
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    if (i < 5)
                    {
                        SetElement(i, j, matrix[i, j]);
                    }
                    else
                    {
                        _StickyWildMatrix[i - 5, j] = matrix[i, j];
                    }
                }
            }
        }

        public void FromMatrixArrayGreatWhaleByte(byte[,] matrix)
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
            if (id == 1)
            {
                return WinForWildGreatWhale;
            }
            var coefficients = new int[5];

            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesGreatWhale[id, i];
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
            var symbols = new HelpSymbolConfigV3<object>[11];

            symbols[0] = new HelpSymbolConfigV3<object>
            {
                id = 0,
                features = new[] { HelpSymbolFeatureV3.FreeSpin },
                extra = new HelpSymbolExtraV3(),
                coefficients = GetSymbolCoefficients(0)
            };

            for (var i = 1; i < 11; i++)
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

        private static HelpLineConfigV3[] GetHelpLineConfigV3()
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
