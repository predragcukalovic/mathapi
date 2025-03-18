using GameWildClover506;
using MathBaseProject.StructuresV3;
using System;

namespace GameCashBells40
{
    public class MatrixCashBells40
    {
        #region Private fields

        private readonly int[,] _Matrix;

        #endregion

        #region Public fields

        public static readonly int[] GratisNumber = { 10, 20, 30, 40 };
        public static readonly int[] PlayLines = { 40 };

        public static readonly int[,] WinForLines40CashBells =
        {
            {0, 0, 0, 0, 0, 0},
            {0, 0, 16, 48, 240, 1000},
            {0, 0, 16, 48, 240, 1000},
            {0, 0, 12, 32, 160, 600},
            {0, 0, 12, 32, 160, 600},
            {0, 0, 8, 20, 100, 400},
            {0, 0, 8, 20, 100, 400},
            {0, 0, 4, 12, 48, 200},
            {0, 0, 4, 12, 48, 200},
            {0, 0, 4, 12, 48, 200},
            {0, 0, 4, 12, 48, 200},
            {0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0 }
        };
        public static readonly int[] WinForWild40CashBells = { 0, 0, 20, 80, 600, 2400 };

        public static readonly int[,] GameLineCashBells40 =
        {
            { 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2 },
            { 0, 0, 0, 0, 0, 0 },
            { 3, 3, 3, 3, 3, 3 },
            { 1, 2, 3, 3, 2, 1 },
            { 2, 1, 0, 0, 1, 2 },
            { 0, 1, 2, 2, 1, 0 },
            { 3, 2, 1, 1, 2, 3 },
            { 0, 0, 1, 2, 3, 3 },
            { 3, 3, 2, 1, 0, 0 },
            { 0, 0, 0, 1, 2, 3 },
            { 3, 3, 3, 2, 1, 0 },
            { 0, 1, 2, 2, 2, 2 },
            { 3, 2, 1, 1, 1, 1 },
            { 1, 2, 3, 2, 1, 0 },
            { 2, 1, 0, 1, 2, 3 },
            { 2, 3, 2, 1, 0, 1 },
            { 1, 0, 1, 2, 3, 2 },
            { 1, 2, 1, 0, 1, 2 },
            { 2, 1, 2, 3, 2, 1 },
            { 2, 3, 3, 3, 3, 2 },
            { 1, 0, 0, 0, 0, 1 },
            { 1, 2, 2, 2, 2 ,1 },
            { 2, 1, 1, 1, 1, 2 },
            { 0, 1, 1, 1, 1, 0 },
            { 3, 2, 2, 2, 2 ,3 },
            { 0, 1, 0, 1, 0, 1 },
            { 3, 2, 3, 2, 3, 2 },
            { 1, 2, 1, 2, 1, 2 },
            { 2, 1, 2, 1, 2, 1 },
            { 0, 0, 1, 1, 0, 0 },
            { 3, 3, 2, 2, 3, 3 },
            { 2, 2, 3, 3, 2, 2 },
            { 1, 1, 0, 0, 1, 1 },
            { 0, 0, 1, 1, 2, 2 },
            { 3, 3, 2, 2, 1, 1 },
            { 2, 3, 2, 1, 0, 0 },
            { 1, 0, 1, 2, 3, 3 },
            { 0, 0, 1, 2, 3, 2 },
            { 3, 3, 2, 1, 0, 1 }

        };

        #endregion

        #region Constructors

        public MatrixCashBells40()
        {
            _Matrix = new int[6, 6];
        }

        #endregion

        #region Public methods

        /// <summary>
        /// uzima liniju iz matrice
        /// </summary>
        /// <param name="lineNumber">broj linije, 1 -- 50</param>
        /// <param name="lines">Linije </param>
        /// <returns>vraća liniju pod datim brojem</returns>
        public Line40WildClover6 GetLine(int lineNumber, int[,] lines)
        {
            try
            {
                var line = new Line40WildClover6();
                for (var i = 0; i < 6; i++)
                {
                    line.SetElement(i, _Matrix[i, lines[lineNumber - 1, i] + 1]);
                }
                return line;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// geter za element iz matrice na poziciji [i,j]
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>vraća element iz matrice na poziciji [i,j]</returns>
        public int GetElement(int i, int j)
        {
            return _Matrix[i, j];
        }

        /// <summary>
        /// Transformiše dvodimenzionalni niz u matricu.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArray(int[,] matrix)
        {
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    _Matrix[i, j] = matrix[i, j];
                }
            }
        }

        public int GetNumberOfElement(int element)
        {
            var counter = 0;
            for (var i = 0; i < 6; i++)
            {
                for (var j = 1; j < 5; j++)
                {
                    if (element == _Matrix[i, j])
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
        public byte[] GetPositionsArray(int symbol)
        {
            var positions = new byte[6];
            var index = 0;
            for (var i = 0; i < 6; i++)
            {
                for (var j = 1; j < 5; j++)
                {
                    if (_Matrix[i, j] == symbol)
                    {
                        positions[index++] = (byte)((j - 1) * 6 + i);
                    }
                }
            }
            for (; index < 6; index++)
            {
                positions[index] = 255;
            }
            return positions;
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
        public int GetWinningElementForLine(int lineNumber, int wild, int[] winForWild, int lineWin, int[,] gameLines)
        {
            return GetLine(lineNumber, gameLines).GetWinningElement(wild, lineWin, winForWild);
        }

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GameLineCashBells40).CalculateLineWin(WinForLines40CashBells, WinForWild40CashBells, 0, 1);
        }

        #endregion

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[6][];
            fakeReels[0] = new[] { 9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 8, 8, 8, 8, 2, 2, 2, 2, 7, 7, 7, 1, 1, 1, 1, 1, 10, 10, 10, 10, 4, 4, 4, 4, 9, 9, 9, 9, 3, 3, 3, 3, 7, 7, 7, 7, 7, 11, 5, 5, 5, 5, 8, 8, 8, 8, 6, 6, 6, 6, 10, 10, 10, 10, 11, 4, 4, 6, 6, 6, 3, 3, 5, 5, 5 };
            fakeReels[1] = new[] { 9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 8, 8, 8, 8, 2, 2, 2, 2, 7, 7, 7, 1, 1, 1, 1, 1, 10, 10, 10, 10, 4, 4, 4, 4, 9, 9, 9, 9, 3, 3, 3, 3, 7, 7, 7, 7, 7, 11, 5, 5, 5, 5, 8, 8, 8, 8, 6, 6, 6, 6, 10, 10, 10, 10, 11, 4, 4, 6, 6, 6, 3, 3, 5, 5, 5 };
            fakeReels[2] = new[] { 9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 8, 8, 8, 8, 2, 2, 2, 2, 7, 7, 7, 1, 1, 1, 1, 1, 10, 10, 10, 10, 4, 4, 4, 4, 9, 9, 9, 9, 3, 3, 3, 3, 7, 7, 7, 7, 7, 5, 5, 5, 5, 8, 8, 8, 8, 6, 6, 6, 6, 10, 10, 10, 10, 11, 4, 4, 6, 6, 6, 3, 3, 5, 5, 5 };
            fakeReels[3] = new[] { 9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 0, 8, 8, 8, 8, 2, 2, 2, 2, 7, 7, 7, 1, 1, 1, 1, 1, 10, 10, 10, 10, 4, 4, 4, 4, 9, 9, 9, 9, 3, 3, 3, 3, 7, 7, 7, 7, 7, 11, 5, 5, 5, 5, 8, 8, 8, 8, 6, 6, 6, 6, 10, 10, 10, 10, 11, 4, 4, 6, 6, 6, 3, 3, 5, 5, 5 };
            fakeReels[4] = new[] { 9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 8, 8, 8, 2, 2, 2, 2, 7, 7, 7, 1, 1, 1, 1, 1, 10, 10, 10, 10, 4, 4, 4, 4, 9, 9, 9, 9, 3, 3, 3, 3, 7, 7, 7, 7, 7, 11, 5, 5, 5, 5, 8, 8, 8, 8, 6, 6, 6, 6, 10, 10, 10, 10, 11, 4, 4, 6, 6, 6, 3, 3, 5, 5, 5 };
            fakeReels[5] = new[] { 9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 8, 8, 8, 2, 2, 2, 2, 7, 7, 7, 1, 1, 1, 1, 1, 10, 10, 10, 10, 4, 4, 4, 4, 9, 9, 9, 9, 3, 3, 3, 3, 7, 7, 7, 7, 7, 11, 5, 5, 5, 5, 8, 8, 8, 8, 6, 6, 6, 6, 10, 10, 10, 10, 11, 4, 4, 6, 6, 6, 3, 3, 5, 5, 5 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja u bonusu
        /// </summary>
        /// <returns></returns>
        public static int[][] GetGratisFakeReels()
        {
            var fakeReels = new int[6][];
            fakeReels[0] = new[] { 3, 3, 3, 3, 0, 1, 1, 1, 1, 1, 4, 4, 4, 4, 0, 2, 2, 2, 2, 11, 6, 6, 6, 6, 0, 5, 5, 5, 5, 0, 7, 7, 7, 7, 0, 8, 8, 8, 8, 0, 0, 0, 0, 0, 10, 10, 10, 10, 11, 9, 9, 9, 9, 11 };
            fakeReels[1] = new[] { 3, 3, 3, 3, 0, 1, 1, 1, 1, 1, 4, 4, 4, 4, 0, 2, 2, 2, 2, 11, 6, 6, 6, 6, 0, 5, 5, 5, 5, 0, 7, 7, 7, 7, 0, 8, 8, 8, 8, 0, 0, 0, 0, 0, 10, 10, 10, 10, 11, 9, 9, 9, 9, 11 };
            fakeReels[2] = new[] { 3, 3, 3, 3, 0, 1, 1, 1, 1, 1, 4, 4, 4, 4, 0, 2, 2, 2, 2, 11, 6, 6, 6, 6, 0, 5, 5, 5, 5, 0, 7, 7, 7, 7, 0, 8, 8, 8, 8, 0, 0, 0, 0, 0, 10, 10, 10, 10, 11, 9, 9, 9, 9, 11 };
            fakeReels[3] = new[] { 3, 3, 3, 3, 0, 1, 1, 1, 1, 1, 4, 4, 4, 4, 0, 2, 2, 2, 2, 11, 6, 6, 6, 6, 0, 5, 5, 5, 5, 0, 7, 7, 7, 7, 0, 8, 8, 8, 8, 0, 0, 0, 0, 0, 10, 10, 10, 10, 11, 9, 9, 9, 9, 11 };
            fakeReels[4] = new[] { 3, 3, 3, 3, 0, 1, 1, 1, 1, 1, 4, 4, 4, 4, 0, 2, 2, 2, 2, 11, 6, 6, 6, 6, 0, 5, 5, 5, 5, 0, 7, 7, 7, 7, 0, 8, 8, 8, 8, 0, 0, 0, 0, 0, 10, 10, 10, 10, 11, 9, 9, 9, 9, 11 };
            fakeReels[5] = new[] { 3, 3, 3, 3, 0, 1, 1, 1, 1, 1, 4, 4, 4, 4, 0, 2, 2, 2, 2, 11, 6, 6, 6, 6, 0, 5, 5, 5, 5, 0, 7, 7, 7, 7, 0, 8, 8, 8, 8, 0, 0, 0, 0, 0, 10, 10, 10, 10, 11, 9, 9, 9, 9, 11 };

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
                return WinForWild40CashBells;
            }
            var coefficients = new int[6];
            for (var i = 0; i < 6; i++)
            {
                coefficients[i] = WinForLines40CashBells[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)95.4,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[12];
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
            symbols[11] = new HelpSymbolConfigV3<object>
            {
                id = 11,
                extra = new HelpSymbolExtraV3(),
                coefficients = GetSymbolCoefficients(11),
                features = new[] { HelpSymbolFeatureV3.FreeSpin }
            };

            return symbols;
        }

        private static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            var lines = new HelpLineConfigV3[40];
            for (var i = 0; i < 40; i++)
            {
                var pos = new int[6];
                for (var j = 0; j < 6; j++)
                {
                    pos[j] = GameLineCashBells40[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
