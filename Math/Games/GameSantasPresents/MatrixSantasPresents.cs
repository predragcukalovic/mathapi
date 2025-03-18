using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;
using System;
using System.Collections.Generic;

namespace GameSantasPresents
{
    public class MatrixSantasPresents : Matrix
    {
        #region Public properties

        public static readonly int[,] WinForLinesSantasPresents =
        {
            {0, 0, 0, 0, 0},
            {0, 5, 50, 100, 1000},
            {0, 0, 25, 50, 200},
            {0, 0, 25, 50, 200},
            {0, 0, 15, 30, 100},
            {0, 0, 10, 20, 50},
            {0, 0, 10, 20, 50},
            {0, 0, 10, 20, 50},
            {0, 0, 10, 20, 50},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildSantasPresents = { 0, 0, 0, 0, 0 };
        public static readonly int[] NumberOfGratisGames = { 0, 0, 0, 8, 12, 20 };
        public static int[] PlayLines = { 10 };

        #endregion

        public int CalculateWinLine(int lineNumber, bool gratis)
        {
            var l = GetLine(lineNumber, GlobalData.GameLineExtra);
            if (!gratis)
            {
                return l.CalculateLineWin(WinForLinesSantasPresents, WinForWildSantasPresents, 0, 1);
            }
            var mult = 0;
            var elem = l.GetElement(0);
            var cnt = 1;
            for (var i = 1; i < 5; i++)
            {
                var ee = l.GetElement(i);
                if (ee == elem || ee >= 10)
                {
                    mult += Math.Max(0, ee - 9);
                    cnt++;
                }
                else
                {
                    break;
                }
            }
            return WinForLinesSantasPresents[elem, cnt - 1] * Math.Max(1, mult);
        }

        /// <summary>
        /// Postavlja expand wildove sa verovatnocama 40%, 30%, 30% za x1, x2, x3 za gratis igru
        /// </summary>
        public void SetWilds()
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (GetElement(i, j) == 0)
                    {
                        var rand = SoftwareRng.Next(10);
                        if (rand < 4)
                        {
                            SetElement(i, j, 10);
                        }
                        else if (rand > 6)
                        {
                            SetElement(i, j, 12);
                        }
                        else
                        {
                            SetElement(i, j, 11);
                        }
                    }
                }
            }
        }

        public int GetWinningElementForLine(int line)
        {
            var l = GetLine(line, GlobalData.GameLineExtra);
            return l.GetElement(0);
        }

        public byte[] GetLinesPositions(int lineNumber, int winElem)
        {
            var positionsArray = new byte[5];
            var i = 0;
            var l = GetLine(lineNumber, GlobalData.GameLineExtra);
            while (i < 5 && (l.GetElement(i) == 0 || l.GetElement(i) > 9 || l.GetElement(i) == winElem))
            {
                positionsArray[i] = (byte)(GlobalData.GameLineExtra[lineNumber - 1, i] * 5 + i);
                i++;
            }
            for (; i < 5; i++)
            {
                positionsArray[i] = 255;
            }
            return positionsArray;
        }

        /// <summary>
        /// Ako su samo dobici sačinjeni od dva elementa i ne mora da se širi četvrti ril; nakon širenja vajldova se poziva.
        /// </summary>
        /// <param name="lineInfo"></param>
        /// <param name="position2"></param>
        /// <returns></returns>
        public byte[] FixExpand(IEnumerable<LineInfo> lineInfo, byte[] position2)
        {
            var shouldBeFixed = new[] { true, true, true };
            foreach (var info in lineInfo)
            {
                for (var i = 0; i < 5; i++)
                {
                    var el = info.WinningPosition[i];
                    if (el < 15 && (GetElement(el % 5, el / 5) == 0 || GetElement(el % 5, el / 5) >= 10))
                    {
                        shouldBeFixed[el % 5 - 1] = false;
                    }
                }
            }
            for (var i = 0; i < 5; i++)
            {
                if (position2[i] < 15)
                {
                    if (shouldBeFixed[position2[i] % 5 - 1])
                    {
                        position2[i] = 255;
                    }
                }
            }
            Array.Sort(position2);
            return position2;
        }

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 7, 7, 7, 9, 1, 5, 5, 5, 9, 4, 4, 4, 1, 6, 6, 6, 1, 8, 8, 3, 2, 2, 1, 4, 4, 5, 5, 2, 2, 7, 7, 6, 6, 5, 7, 4, 4, 8, 8, 8, 6, 6, 8, 8, 8, 9, 2, 2, 2, 6, 6, 3, 3, 7, 7, 7, 3, 3, 5, 5, 5, 6, 1, 9, 7, 7, 6, 2, 8, 8, 8, 7, 2, 2, 1 };
            fakeReels[1] = new[] { 2, 2, 1, 0, 2, 1, 6, 6, 6, 7, 7, 8, 8, 9, 2, 2, 5, 5, 5, 0, 4, 4, 5, 5, 9, 3, 8, 1, 7, 7, 6, 6, 1, 5, 5, 7, 3, 8, 8, 8, 1, 3, 3, 4, 4, 4, 9, 5, 5, 5, 7, 1, 6, 6, 6, 5, 4, 4, 0, 8, 8, 2, 7, 7, 2, 5, 5, 9, 3, 3, 6, 6, 6, 2, 4, 7, 0, 1, 2, 2, 9 };
            fakeReels[2] = new[] { 9, 2, 4, 4, 5, 5, 5, 0, 6, 6, 9, 6, 6, 6, 5, 5, 5, 7, 7, 7, 2, 6, 6, 5, 5, 1, 0, 4, 4, 6, 8, 8, 1, 2, 2, 9, 5, 5, 0, 8, 7, 7, 1, 5, 9, 3, 3, 2, 0, 4, 4, 4, 3, 3, 7, 6, 6, 1, 8, 8, 8, 2, 3, 6, 7, 7, 0, 8, 8, 9, 6, 3, 2, 2, 9, 7, 1, 4, 7, 7, 0, 5, 5, 5, 8, 8, 8 };
            fakeReels[3] = new[] { 4, 4, 7, 7, 7, 2, 6, 6, 9, 5, 5, 4, 0, 5, 4, 1, 9, 2, 1, 3, 9, 7, 8, 8, 8, 2, 5, 3, 9, 7, 7, 7, 2, 0, 5, 5, 6, 6, 6, 4, 3, 0, 4, 3, 5, 5, 5, 9, 8, 8, 3, 0, 1, 4, 4, 5, 5, 2, 2, 9, 7, 7, 6, 6, 5, 1, 7, 4, 4, 2, 2, 8, 8, 8, 6, 6, 8, 8, 8, 6, 6, 3, 3, 7, 7, 7, 3, 3, 5, 5, 5, 6, 1 };
            fakeReels[4] = new[] { 9, 1, 4, 4, 4, 9, 6, 6, 6, 5, 5, 5, 7, 7, 7, 2, 6, 6, 5, 5, 4, 4, 6, 8, 8, 2, 2, 9, 5, 5, 8, 7, 7, 1, 5, 2, 2, 9, 3, 3, 2, 4, 4, 4, 1, 3, 3, 7, 6, 6, 9, 8, 8, 8, 2, 3, 6, 7, 7, 9, 8, 8, 6, 3, 2, 2, 7, 1 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja u bonusu
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReelsGratis()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 1, 4, 4, 4, 6, 6, 6, 5, 5, 5, 7, 7, 7, 2, 6, 6, 5, 5, 4, 4, 6, 8, 8, 2, 2, 5, 5, 8, 7, 7, 1, 5, 2, 2, 3, 3, 2, 4, 4, 4, 1, 3, 3, 7, 6, 6, 8, 8, 8, 2, 3, 6, 7, 7, 8, 8, 6, 3, 2, 2, 7, 1 };
            fakeReels[1] = new[] { 2, 4, 4, 5, 5, 5, 10, 6, 6, 6, 11, 1, 5, 5, 5, 12, 7, 7, 7, 2, 6, 6, 5, 5, 1, 10, 4, 4, 6, 8, 8, 12, 2, 2, 5, 5, 11, 8, 7, 7, 1, 5, 3, 3, 2, 10, 4, 4, 4, 3, 3, 7, 6, 6, 1, 8, 8, 8, 2, 3, 6, 7, 7, 11, 8, 8, 6, 3, 2, 2, 7, 1, 4, 7, 7, 10, 5, 5, 5, 12, 8, 8, 8 };
            fakeReels[2] = new[] { 4, 4, 7, 7, 7, 2, 6, 6, 5, 5, 4, 12, 5, 4, 1, 2, 10, 3, 7, 8, 8, 8, 2, 5, 3, 7, 7, 7, 2, 10, 5, 5, 6, 6, 6, 4, 3, 11, 4, 3, 5, 5, 5, 8, 8, 3, 12, 1, 4, 4, 5, 5, 2, 2, 7, 7, 6, 6, 5, 1, 7, 4, 4, 10, 2, 2, 8, 8, 8, 12, 6, 6, 8, 8, 8, 6, 6, 11, 3, 3, 7, 7, 7, 3, 3, 10, 5, 5, 5, 6, 1 };
            fakeReels[3] = new[] { 2, 2, 1, 10, 2, 1, 11, 6, 6, 6, 7, 7, 12, 8, 8, 12, 2, 2, 5, 5, 5, 11, 4, 4, 5, 5, 3, 8, 1, 7, 7, 6, 6, 1, 5, 5, 7, 3, 8, 8, 8, 1, 3, 3, 4, 4, 4, 5, 5, 5, 7, 1, 6, 6, 6, 5, 4, 4, 12, 8, 8, 2, 7, 7, 12, 5, 5, 3, 3, 11, 6, 6, 6, 2, 4, 7, 10, 1, 2, 2, 11 };
            fakeReels[4] = new[] { 7, 7, 7, 1, 5, 5, 5, 4, 4, 4, 1, 6, 6, 6, 1, 8, 8, 3, 2, 2, 1, 4, 4, 5, 5, 2, 2, 7, 7, 6, 6, 5, 7, 4, 4, 8, 8, 8, 6, 6, 8, 8, 8, 2, 2, 2, 6, 6, 3, 3, 7, 7, 7, 3, 3, 5, 5, 5, 6, 1, 7, 7, 6, 2, 8, 8, 8, 7, 2, 2, 1 };

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
                coefficients[i] = WinForLinesSantasPresents[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.5,
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
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i),
                    features = new[] { HelpSymbolFeatureV3.Regular }
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
                    pos[j] = GlobalData.GameLineExtra[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
