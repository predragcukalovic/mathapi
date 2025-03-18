using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using System;

namespace GameWildClover506
{
    /// <summary>
    /// Class is named 40WildClover6 but it is a 50lines game and renamed to 50 Wild Clover 6 with Games enum WildClover506
    /// </summary>
    public class Matrix40WildClover6
    {
        #region Private fields

        private readonly int[,] _Matrix;

        #endregion

        #region Public fields

        public static readonly int[] GratisNumber = { 10, 20, 30, 40 };
        public static readonly int[] PlayLines = { 50 };
        public static readonly int[,] WinForLines40WildClover6 =
        {
            {0, 0, 0, 0, 0, 0},
            {0, 0, 20, 50, 250, 1000},
            {0, 0, 20, 50, 250, 1000},
            {0, 0, 10, 30, 150, 600},
            {0, 0, 10, 30, 150, 600},
            {0, 0, 5, 20, 100, 400},
            {0, 0, 5, 20, 100, 400},
            {0, 0, 5, 10, 50, 200},
            {0, 0, 5, 10, 50, 200},
            {0, 0, 5, 10, 50, 200},
            {0, 0, 5, 10, 50, 200},
            {0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0 }
        };
        public static readonly int[] WinForWild40WildClover6 = { 0, 0, 25, 100, 750, 2500 };

        #endregion

        #region Constructors

        public Matrix40WildClover6()
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
            return GetLine(lineNumber, GlobalData.GameLineWildClover6Reels).CalculateLineWin(WinForLines40WildClover6, WinForWild40WildClover6, 0, 1);
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
                return WinForWild40WildClover6;
            }
            var coefficients = new int[6];
            for (var i = 0; i < 6; i++)
            {
                coefficients[i] = WinForLines40WildClover6[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)95.263,
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
            var lines = new HelpLineConfigV3[50];
            for (var i = 0; i < 50; i++)
            {
                var pos = new int[6];
                for (var j = 0; j < 6; j++)
                {
                    pos[j] = GlobalData.GameLineWildClover6Reels[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
