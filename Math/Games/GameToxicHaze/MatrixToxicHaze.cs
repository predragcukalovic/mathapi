using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using System;

namespace GameToxicHaze
{
    public class MatrixToxicHaze : Matrix
    {
        public MatrixToxicHaze() : base(7)
        {
        }

        #region Public properties

        public static readonly int[,] WinForLinesToxicHaze =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 40, 80, 400},
            {0, 0, 20, 50, 200},
            {0, 0, 20, 50, 200},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 5, 15, 60},
            {0, 0, 5, 15, 60},
            {0, 0, 5, 10, 40},
            {0, 0, 5, 10, 40},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildToxicHaze = { 0, 0, 50, 150, 500 };
        public static int[] PlayLines = { 40 };
        public const int GRATIS_GAMES = 7;

        public static readonly int[,] GameLineToxicHaze =
        {
            {3, 3, 3, 3, 3},
            {2, 2, 2, 2, 2},
            {4, 4, 4, 4, 4},
            {1, 1, 1, 1, 1},
            {5, 5, 5, 5, 5},
            {1, 2, 1, 2, 1},
            {2, 3, 2, 3, 2},
            {3, 4, 3, 4, 3},
            {4, 5, 4, 5, 4},
            {2, 1, 2, 1, 2},
            {3, 2, 3, 2, 3},
            {4, 3, 4, 3, 4},
            {5, 4, 5, 4, 5},
            {1, 2, 3, 2, 1},
            {2, 3, 4, 3, 2},
            {3, 4, 5, 4, 3},
            {3, 2, 1, 2, 3},
            {4, 3, 2, 3, 4},
            {5, 4, 3, 4, 5},
            {1, 2, 3, 4, 5},
            {5, 4, 3, 2, 1},
            {1, 2, 2, 2, 1},
            {2, 3, 3, 3, 2},
            {3, 4, 4, 4, 3},
            {4, 5, 5, 5, 4},
            {2, 1, 1, 1, 2},
            {3, 2, 2, 2, 3},
            {4, 3, 3, 3, 4},
            {5, 4, 4, 4, 5},
            {1, 1, 2, 1, 1},
            {2, 2, 3, 2, 2},
            {3, 3, 4, 3, 3},
            {4, 4, 5, 4, 4},
            {2, 2, 1, 2, 2},
            {3, 3, 2, 3, 3},
            {4, 4, 3, 4, 4},
            {5, 5, 4, 5, 5},
            {4, 3, 3, 3, 2},
            {2, 3, 3, 3, 4},
            {3, 4, 3, 2, 3}
        };

        #endregion

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public int CalculateWinLine(int lineNumber, bool gratisGame, out int winningElement, out byte[] winningPosition)
        {
            var l = GetLine(lineNumber, GameLineToxicHaze);
            var mult = 1;
            if (gratisGame)
            {
                for (var i = 0; i < 5; i++)
                {
                    var elem = l.GetElement(i);
                    if (elem > 11)
                    {
                        mult = Math.Max(mult, 1 << ((elem - 12) * 2));
                        l.SetElement(i, 0);
                    }
                }
                var tmpWildWin = new[] { 0, 0, WinForWildToxicHaze[2] * mult, WinForWildToxicHaze[3] * mult, WinForWildToxicHaze[4] * mult };
                var win = l.CalculateLineWin(WinForLinesToxicHaze, tmpWildWin, 0, mult);
                winningElement = l.GetWinningElement(0, win, tmpWildWin);
                winningPosition = l.GetLinesPositions(GameLineToxicHaze, lineNumber, 0, winningElement);
                return win;
            }
            var winR = l.CalculateLineWin(WinForLinesToxicHaze, WinForWildToxicHaze, 0, mult);
            winningElement = l.GetWinningElement(0, winR, WinForWildToxicHaze);
            winningPosition = l.GetLinesPositions(GameLineToxicHaze, lineNumber, 0, winningElement);
            return winR;
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayToxicHaze(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 7; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        /// <summary>
        /// Proverava da li igra daje gratis.
        /// </summary>
        public bool IsGiveGratisGame()
        {
            var s = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 6; j++)
                {
                    if (GetElement(i, j) == 11)
                    {
                        s++;
                    }
                }
            }
            return s >= 3;
        }

        /// <summary>
        /// Daje niz pozicija za bonus simbol.
        /// </summary>
        /// <returns></returns>
        public byte[] GetScatterPositionsArray()
        {
            var positions = new byte[5];
            var index = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 6; j++)
                {
                    if (GetElement(i, j) == 11)
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

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 7, 7, 7, 7, 7, 3, 3, 3, 11, 10, 10, 10, 9, 9, 9, 9, 9, 9, 8, 8, 8, 8, 8, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 10, 10, 10, 6, 6, 6, 6, 6, 6, 11, 9, 9, 9, 5, 5, 5, 5, 5, 4, 4, 4, 6, 6, 6, 7, 7, 7, 8, 8, 8, 1, 1, 1, 1, 1, 4, 4, 4, 4, 4, 2, 2, 2, 5, 5, 5, 5, 0, 0, 0, 0, 0, 1, 1, 1, 1, 11, 8, 8, 8, 9, 9, 9, 0, 0, 0, 0, 0, 4, 4, 4, 11, 5, 5, 5, 3, 3, 3 };
            fakeReels[1] = new[] { 9, 9, 9, 9, 9, 9, 10, 10, 10, 10, 10, 2, 2, 2, 2, 2, 9, 9, 9, 0, 0, 0, 0, 0, 3, 3, 3, 1, 1, 1, 1, 1, 10, 10, 10, 9, 9, 9, 8, 8, 8, 8, 8, 5, 5, 5, 5, 5, 4, 4, 4, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 4, 4, 4, 8, 8, 8, 8, 8, 1, 1, 1, 1, 1, 7, 7, 7, 0, 0, 0, 0, 0, 5, 5, 5, 6, 6, 6, 6, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 10, 10, 10, 10, 10 };
            fakeReels[2] = new[] { 8, 8, 8, 8, 8, 2, 2, 2, 2, 7, 7, 7, 7, 7, 11, 8, 8, 8, 3, 3, 3, 11, 10, 10, 10, 9, 9, 9, 9, 9, 9, 4, 4, 4, 4, 4, 2, 2, 2, 5, 5, 5, 5, 0, 0, 0, 0, 0, 1, 1, 1, 1, 8, 8, 8, 8, 8, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 10, 10, 10, 6, 6, 6, 6, 6, 6, 11, 9, 9, 9, 5, 5, 5, 5, 5, 4, 4, 4, 6, 6, 6, 7, 7, 7, 8, 8, 8, 1, 1, 1, 1, 1, 9, 9, 9, 0, 0, 0, 0, 0, 4, 4, 4, 11, 5, 5, 5, 3, 3, 3 };
            fakeReels[3] = new[] { 4, 4, 4, 4, 2, 2, 2, 8, 8, 8, 8, 10, 10, 10, 10, 10, 2, 2, 2, 2, 2, 9, 9, 9, 0, 0, 0, 0, 0, 3, 3, 3, 1, 1, 1, 1, 1, 10, 10, 10, 9, 9, 9, 8, 8, 8, 8, 8, 5, 5, 5, 5, 5, 4, 4, 4, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 4, 4, 4, 8, 8, 8, 8, 8, 1, 1, 1, 1, 1, 7, 7, 7, 0, 0, 0, 0, 0, 5, 5, 5, 6, 6, 6, 6, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 10, 10, 10, 10, 10 };
            fakeReels[4] = new[] { 3, 3, 4, 4, 4, 4, 4, 9, 9, 9, 9, 0, 0, 0, 0, 0, 9, 9, 9, 11, 3, 3, 3, 3, 1, 1, 1, 1, 1, 8, 8, 8, 8, 2, 2, 2, 2, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 10, 10, 10, 6, 6, 6, 6, 6, 6, 11, 9, 9, 9, 7, 7, 7, 7, 7, 11, 8, 8, 8, 3, 3, 3, 11, 10, 10, 10, 9, 9, 9, 9, 9, 9, 4, 4, 4, 4, 4, 2, 2, 2, 5, 5, 5, 5, 0, 0, 0, 0, 0, 1, 1, 1, 1, 8, 8, 8, 8, 8, 5, 5, 5, 5, 5, 4, 4, 4, 6, 6, 6, 7, 7, 7, 8, 8, 8, 1, 1, 1, 1, 1, 9, 9, 9, 0, 0, 0, 0, 0, 4, 4, 4, 11, 5, 5, 5, 3, 3, 3 };
            return fakeReels;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja u bonusus
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReelsGratis()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 7, 7, 7, 7, 7, 3, 3, 3, 10, 10, 10, 9, 9, 9, 9, 9, 9, 8, 8, 8, 8, 8, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 10, 10, 10, 6, 6, 6, 6, 6, 6, 9, 9, 9, 5, 5, 5, 5, 5, 4, 4, 4, 6, 6, 6, 7, 7, 7, 8, 8, 8, 1, 1, 1, 1, 1, 4, 4, 4, 4, 4, 2, 2, 2, 5, 5, 5, 5, 0, 0, 0, 0, 0, 1, 1, 1, 1, 8, 8, 8, 9, 9, 9, 0, 0, 0, 0, 0, 4, 4, 4, 5, 5, 5, 3, 3, 3 };
            fakeReels[1] = new[] { 9, 9, 9, 9, 9, 9, 10, 10, 10, 10, 10, 2, 2, 2, 2, 2, 9, 9, 9, 0, 0, 0, 0, 0, 3, 3, 3, 1, 1, 1, 1, 1, 10, 10, 10, 9, 9, 9, 8, 8, 8, 8, 8, 5, 5, 5, 5, 5, 4, 4, 4, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 4, 4, 4, 8, 8, 8, 8, 8, 1, 1, 1, 1, 1, 7, 7, 7, 0, 0, 0, 0, 0, 5, 5, 5, 6, 6, 6, 6, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 10, 10, 10, 10, 10 };
            fakeReels[2] = new[] { 8, 8, 8, 8, 8, 2, 2, 2, 2, 7, 7, 7, 7, 7, 8, 8, 8, 3, 3, 3, 10, 10, 10, 9, 9, 9, 9, 9, 9, 4, 4, 4, 4, 4, 2, 2, 2, 5, 5, 5, 5, 0, 0, 0, 0, 0, 1, 1, 1, 1, 8, 8, 8, 8, 8, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 10, 10, 10, 6, 6, 6, 6, 6, 6, 9, 9, 9, 5, 5, 5, 5, 5, 4, 4, 4, 6, 6, 6, 7, 7, 7, 8, 8, 8, 1, 1, 1, 1, 1, 9, 9, 9, 0, 0, 0, 0, 0, 4, 4, 4, 5, 5, 5, 3, 3, 3 };
            fakeReels[3] = new[] { 4, 4, 4, 4, 2, 2, 2, 8, 8, 8, 8, 10, 10, 10, 10, 10, 2, 2, 2, 2, 2, 9, 9, 9, 0, 0, 0, 0, 0, 3, 3, 3, 1, 1, 1, 1, 1, 10, 10, 10, 9, 9, 9, 8, 8, 8, 8, 8, 5, 5, 5, 5, 5, 4, 4, 4, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 4, 4, 4, 8, 8, 8, 8, 8, 1, 1, 1, 1, 1, 7, 7, 7, 0, 0, 0, 0, 0, 5, 5, 5, 6, 6, 6, 6, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 10, 10, 10, 10, 10 };
            fakeReels[4] = new[] { 3, 3, 4, 4, 4, 4, 4, 9, 9, 9, 9, 0, 0, 0, 0, 0, 9, 9, 9, 3, 3, 3, 3, 1, 1, 1, 1, 1, 8, 8, 8, 8, 2, 2, 2, 2, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 10, 10, 10, 6, 6, 6, 6, 6, 6, 9, 9, 9, 7, 7, 7, 7, 7, 8, 8, 8, 3, 3, 3, 10, 10, 10, 9, 9, 9, 9, 9, 9, 4, 4, 4, 4, 4, 2, 2, 2, 5, 5, 5, 5, 0, 0, 0, 0, 0, 1, 1, 1, 1, 8, 8, 8, 8, 8, 5, 5, 5, 5, 5, 4, 4, 4, 6, 6, 6, 7, 7, 7, 8, 8, 8, 1, 1, 1, 1, 1, 9, 9, 9, 0, 0, 0, 0, 0, 4, 4, 4, 5, 5, 5, 3, 3, 3 };
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
                return WinForWildToxicHaze;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesToxicHaze[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.82,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[12];
            for (var i = 0; i < 12; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i),
                    features = new[] { i == 11 ? HelpSymbolFeatureV3.FreeSpin : HelpSymbolFeatureV3.Regular }
                };
            }

            return symbols;
        }

        private static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            var lines = new HelpLineConfigV3[40];
            for (var i = 0; i < 40; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = GameLineToxicHaze[i, j] - 1;
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
