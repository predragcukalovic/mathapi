using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using System;
using System.Collections.Generic;

namespace GameWildHeartBeat
{
    public class MatrixWildHeartBeat : Matrix
    {
        #region Public properties

        public static readonly int[,] WinForLinesWildHeartBeat =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 20, 80, 600},
            {0, 0, 8, 20, 100},
            {0, 0, 8, 20, 100},
            {0, 0, 6, 10, 40},
            {0, 0, 2, 4, 20},
            {0, 0, 2, 4, 20},
            {0, 0, 2, 4, 20},
            {0, 0, 2, 4, 20},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildWildHeartBeat = { 0, 0, 0, 0, 400 };
        public static readonly int[] WinForScatter1WildHeartBeat = { 0, 0, 3, 20, 100 };
        public const int WIN_FOR_SCATTER2_WILD_HEART_BEAT = 20;
        public static int[] PlayLines = { 1 };

        public static readonly int[,] GameLineWildHeartBeat =
        {
            {2, 2, 2, 2, 2},
            {1, 1, 1, 1, 1},
            {3, 3, 3, 3, 3},
            {1, 2, 3, 2, 1},
            {3, 2, 1, 2, 3}
        };

        #endregion

        public MatrixWildHeartBeat() : base(5)
        {
        }

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GameLineWildHeartBeat).CalculateLineWin(WinForLinesWildHeartBeat, WinForWildWildHeartBeat, 0, 1);
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayWildHeartBeat(int[,] matrix)
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
        /// Ako su samo dobici sačinjeni od tri elementa i ne mora da se širi peti ril; nakon širenja vajldova se poziva.
        /// </summary>
        /// <param name="lineInfo"></param>
        /// <param name="position2"></param>
        /// <returns></returns>
        public byte[] FixExpand(IEnumerable<LineInfo> lineInfo, byte[] position2)
        {
            var shouldBeFixed = new[] { true, true, true, true, true };
            foreach (var info in lineInfo)
            {
                for (var i = 0; i < 5; i++)
                {
                    var el = info.WinningPosition[i];
                    if (el < 20 && GetElement(el % 5, el / 5) == 0)
                    {
                        shouldBeFixed[el % 5] = false;
                    }
                }
            }
            for (var i = 0; i < 5; i++)
            {
                if (position2[i] < 20)
                {
                    if (shouldBeFixed[position2[i] % 5])
                    {
                        position2[i] = 255;
                    }
                }
            }
            Array.Sort(position2);
            return position2;
        }

        /// <summary>
        /// Daje dobitak za sketere.
        /// </summary>
        /// <returns></returns>
        public int GetScatterCount(int scatter)
        {
            var count = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (GetElement(i, j) == scatter)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Daje niz pozicija za sketere.
        /// </summary>
        /// <returns></returns>
        public byte[] GetScatterPositionsArray(int scatter)
        {
            var positions = new byte[5];
            var index = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (GetElement(i, j) == scatter)
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
            fakeReels[0] = new[] { 9, 8, 8, 8, 4, 10, 5, 5, 5, 8, 8, 8, 3, 2, 10, 4, 6, 6, 6, 1, 0, 7, 7, 7, 4, 9, 4, 5, 5, 5, 9, 6, 6, 6, 7, 7, 7, 10, 8, 8, 8, 4, 0, 5, 5, 5, 8, 8, 8, 3, 2, 4, 0, 6, 6, 6, 1, 4, 7, 7, 7, 9, 4, 6, 6, 6, 10, 7, 7, 7 };
            fakeReels[1] = new[] { 9, 8, 8, 8, 4, 5, 5, 5, 8, 8, 8, 3, 2, 4, 6, 6, 6, 1, 0, 7, 7, 7, 4, 9, 4, 5, 5, 5, 9, 6, 6, 6, 7, 7, 7, 8, 8, 8, 4, 0, 5, 5, 5, 8, 8, 8, 3, 2, 4, 0, 6, 6, 6, 1, 4, 7, 7, 7, 9, 4, 6, 6, 6, 7, 7, 7 };
            fakeReels[2] = new[] { 9, 8, 8, 8, 4, 10, 5, 5, 5, 8, 8, 8, 3, 2, 10, 4, 6, 6, 6, 1, 0, 7, 7, 7, 4, 9, 4, 5, 5, 5, 9, 6, 6, 6, 7, 7, 7, 10, 8, 8, 8, 4, 0, 5, 5, 5, 8, 8, 8, 3, 2, 4, 0, 6, 6, 6, 1, 4, 7, 7, 7, 9, 4, 6, 6, 6, 10, 7, 7, 7 };
            fakeReels[3] = new[] { 9, 8, 8, 8, 4, 5, 5, 5, 8, 8, 8, 3, 2, 4, 6, 6, 6, 1, 0, 7, 7, 7, 4, 9, 4, 5, 5, 5, 9, 6, 6, 6, 7, 7, 7, 8, 8, 8, 4, 0, 5, 5, 5, 8, 8, 8, 3, 2, 4, 0, 6, 6, 6, 1, 4, 7, 7, 7, 9, 4, 6, 6, 6, 7, 7, 7 };
            fakeReels[4] = new[] { 9, 8, 8, 8, 4, 10, 5, 5, 5, 8, 8, 8, 3, 2, 10, 4, 6, 6, 6, 1, 0, 7, 7, 7, 4, 9, 4, 5, 5, 5, 9, 6, 6, 6, 7, 7, 7, 10, 8, 8, 8, 4, 0, 5, 5, 5, 8, 8, 8, 3, 2, 4, 0, 6, 6, 6, 1, 4, 7, 7, 7, 9, 4, 6, 6, 6, 10, 7, 7, 7 };

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
                return WinForWildWildHeartBeat;
            }
            if (id == 9)
            {
                return WinForScatter1WildHeartBeat;
            }
            if (id == 10)
            {
                return new[] { 0, 0, WIN_FOR_SCATTER2_WILD_HEART_BEAT, 0, 0 };
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesWildHeartBeat[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.36,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[11];
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
                    pos[j] = GameLineWildHeartBeat[i, j] - 1;
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
