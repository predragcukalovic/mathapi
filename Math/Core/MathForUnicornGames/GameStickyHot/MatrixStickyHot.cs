using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;
using RNGUtils.RandomData;

namespace MathForUnicornGames.GameStickyHot
{
    public class MatrixStickyHot : Matrix
    {
        #region Constructor

        public MatrixStickyHot()
            : base(5)
        {
        }

        #endregion

        #region Public properties

        public static int[] PlayLines = { 10 };

        public static readonly int[,] WinForLinesStickyHot =
        {
            {0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0 },
            {0, 0, 50, 500, 2000},
            {0, 0, 20, 100, 500},
            {0, 0, 10, 50, 200},
            {0, 0, 10, 50, 200},
            {0, 0, 5, 25, 100},
            {0, 0, 5, 25, 100},
            {0, 0, 5, 25, 100},
            {0, 0, 5, 25, 100}
        };

        public static readonly int[] WinForWildStickyHot = { 0, 0, 50, 500, 2000 };
        public static readonly int[] WinForScatterStickyHot = { 0, 0, 2, 10, 100 };

        public const int GRATIS_GAMES = 11;

        #endregion

        /// <summary>
        /// Računa dobitak linije. Wild symbol za igru je  postavljen kao 1.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineShifted).CalculateLineWin(WinForLinesStickyHot, WinForWildStickyHot, 1, 1);
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
            return GetLine(lineNumber, gameLines).GetWinningElement(wild, lineWin, WinForLinesStickyHot, winForWild);
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
        /// Daje niz pozicija za simbol.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public byte[] GetPositionsArray(int symbol, byte[,] matrix)
        {
            var positions = new byte[5];
            var index = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (matrix[i, j] == symbol)
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
        /// Daje dobitak elemenata koji nisu u liniji.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="noLineWins"></param>
        /// <returns></returns>
        public int GetNoLineWins(int symbol, int[] noLineWins)
        {
            var n = GetNumberOfElement(symbol);
            return n == 0 ? 0 : noLineWins[n - 1];
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza za igru Island Respins.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayStickyHot(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        public static bool ShouldRetriggerBonus(byte alreadyRetriggered)
        {
            if (alreadyRetriggered != 1)
            {
                return SoftwareRng.Next(100) < 16;
            }
            return false;
        }

        #region Struct V3

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 4, 4, 1, 6, 6, 6, 2, 4, 4, 2, 9, 9, 1, 6, 1, 6, 6, 9, 9, 3, 3, 7, 7, 7, 0, 8, 8, 5, 5, 9, 9, 9, 4, 4, 4, 6, 6, 6 };
            fakeReels[1] = new[] { 5, 5, 3, 3, 3, 7, 7, 7, 2, 5, 5, 5, 2, 8, 8, 8, 7, 7, 7, 6, 6, 6, 9, 9, 9, 8, 8, 8, 5, 5, 5, 4, 4, 4 };
            fakeReels[2] = new[] { 4, 4, 1, 6, 6, 6, 2, 4, 4, 4, 2, 9, 9, 9, 6, 6, 6, 9, 1, 9, 3, 3, 3, 5, 5, 5, 6, 6, 6, 8, 8, 8, 0, 9, 9, 9, 4, 4, 4, 7, 7, 7 };
            fakeReels[3] = new[] { 5, 5, 5, 3, 3, 3, 7, 7, 7, 2, 5, 5, 5, 2, 8, 8, 8, 8, 0, 6, 6, 6, 8, 8, 8, 4, 4, 4, 7, 7, 7, 0, 5, 5, 5, 9, 9, 9 };
            fakeReels[4] = new[] { 7, 7, 1, 7, 1, 8, 1, 8, 8, 2, 9, 9, 9, 2, 5, 5, 5, 2, 7, 7, 7, 2, 8, 8, 2, 5, 5, 9, 9, 9, 3, 1, 3, 6, 6, 6, 0, 4, 4, 4, 0, 6, 6, 4, 4 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja u toku respina
        /// </summary>
        /// <returns></returns>
        public static int[][] GetGratisFakeReels()
        {
            var fakeReels = new int[5][];

            fakeReels[0] = new[] { 5, 5, 1, 7, 7, 7, 2, 5, 5, 5, 2, 8, 8, 8, 1, 7, 1, 7, 8, 8, 3, 3, 3, 6, 6, 6, 9, 9, 9, 4, 4, 4, 8, 8, 8, 5 };
            fakeReels[1] = new[] { 4, 4, 4, 3, 3, 3, 6, 6, 6, 2, 4, 4, 4, 2, 9, 9, 9, 6, 6, 6, 6, 7, 7, 7, 8, 8, 8, 9, 9, 9, 4, 4, 4, 5, 5, 5 };
            fakeReels[2] = new[] { 4, 4, 1, 6, 6, 6, 2, 4, 4, 4, 2, 9, 9, 9, 1, 6, 6, 9, 9, 9, 3, 3, 3, 7, 7, 7, 8, 8, 8, 5, 5, 5, 9, 9, 9, 4 };
            fakeReels[3] = new[] { 5, 5, 5, 3, 3, 3, 7, 7, 7, 2, 5, 5, 5, 2, 8, 8, 8, 6, 6, 6, 8, 8, 8, 4, 4, 4, 7, 7, 7, 9, 9, 9 };
            fakeReels[4] = new[] { 7, 7, 1, 8, 1, 8, 8, 8, 2, 9, 9, 9, 2, 5, 5, 5, 2, 7, 7, 7, 2, 8, 8, 8, 2, 5, 5, 5, 9, 9, 9, 3, 1, 3, 6, 6, 6, 4, 4, 4 };
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
                return WinForScatterStickyHot;
            }
            if (id == 1)
            {
                return WinForWildStickyHot;
            }
            var coefficients = new int[5];

            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesStickyHot[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.08,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[10];

            for (var i = 0; i < 10; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    features = new[] { i == 0 ? HelpSymbolFeatureV3.FreeSpin : HelpSymbolFeatureV3.Regular },
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

        #endregion
    }
}
