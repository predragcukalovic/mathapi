using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using MathForGames.GameTurboHot40;
using System;
using System.Collections.Generic;

namespace GameFearOfDark
{
    public class MatrixFearOfDark : MatrixTurboHot40
    {
        #region Public properties

        public static readonly int[,] WinForLinesFearOfDark =
        {
            {0, 0, 0, 0, 0},
            {0, 1, 5, 20, 300},
            {0, 0, 4, 10, 50},
            {0, 0, 4, 10, 50},
            {0, 0, 2, 5, 20},
            {0, 0, 1, 3, 10},
            {0, 0, 1, 3, 10},
            {0, 0, 1, 3, 10},
            {0, 0, 1, 3, 10},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildFearOfDark = { 0, 0, 0, 0, 0 };
        public static readonly int[] WinForScatter1FearOfDark = { 0, 0, 3, 20, 100 };
        public const int WIN_FOR_SCATTER2_FEAR_OF_DARK = 20;
        public static int[] PlayLines = { 4 };

        #endregion

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineTurbo).CalculateLineWin(WinForLinesFearOfDark, WinForWildFearOfDark, 0, 1);
        }

        private bool IsReelHaveWild(int reel)
        {
            for (var i = 0; i < 4; i++)
            {
                if (GetElement(reel, i) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetExpanding()
        {
            if (!IsReelHaveWild(1) && !IsReelHaveWild(2) && !IsReelHaveWild(3))
            {
                return;
            }
            var arr = new int[5, 6];
            for (var i = 0; i < 5; i++)
            {
                var wld = IsReelHaveWild(i);
                for (var j = 0; j < 6; j++)
                {
                    arr[i, j] = wld ? 0 : GetElement(i, j + 5);
                }
            }
            FromMatrixArray(arr);
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
                    if (el < 20 && GetElement(el % 5, el / 5) == 0)
                    {
                        shouldBeFixed[el % 5 - 1] = false;
                    }
                }
            }
            for (var i = 0; i < 5; i++)
            {
                if (position2[i] < 20)
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
            fakeReels[0] = new[] { 3, 3, 3, 3, 6, 6, 6, 6, 5, 5, 5, 5, 9, 7, 7, 7, 7, 10, 2, 2, 2, 2, 8, 8, 8, 8, 1, 1, 1, 1, 6, 6, 6, 6, 4, 4, 4, 4, 7, 7, 7, 7, 9, 6, 6, 6, 6, 2, 2, 2, 2, 5, 5, 5, 5, 3, 3, 3, 3, 8, 8, 8, 8, 9, 7, 7, 7, 7, 6, 6, 6, 6, 4, 4, 4, 4, 6, 6, 6, 6, 9, 5, 5, 5, 5, 4, 4, 4, 4, 9, 7, 7, 7, 7, 10, 5, 5, 5, 5, 9, 6, 6, 6, 6, 8, 8, 8, 8, 7, 7, 7, 7, 2, 2, 2, 2 };
            fakeReels[1] = new[] { 6, 6, 6, 6, 0, 8, 8, 8, 8, 3, 3, 3, 3, 9, 7, 7, 7, 7, 1, 1, 1, 1, 7, 7, 7, 7, 2, 2, 2, 2, 5, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3, 8, 8, 8, 8, 6, 6, 6, 6, 0, 2, 2, 2, 2, 8, 8, 8, 8, 1, 1, 1, 1, 7, 7, 7, 7, 9, 4, 4, 4, 4, 3, 3, 3, 3, 6, 6, 6, 6, 9, 8, 8, 8, 8, 5, 5, 5, 5, 2, 2, 2, 2, 4, 4, 4, 4, 1, 1, 1, 1, 5, 5, 5, 5, 9, 2, 2, 2, 2 };
            fakeReels[2] = new[] { 8, 8, 8, 8, 0, 6, 6, 6, 6, 4, 4, 4, 4, 1, 1, 1, 1, 10, 5, 5, 5, 5, 9, 3, 3, 3, 3, 5, 5, 5, 5, 2, 2, 2, 2, 8, 8, 8, 8, 4, 4, 4, 4, 6, 6, 6, 6, 9, 8, 8, 8, 8, 0, 1, 1, 1, 1, 7, 7, 7, 7, 2, 2, 2, 2, 9, 5, 5, 5, 5, 3, 3, 3, 3, 8, 8, 8, 8, 4, 4, 4, 4, 6, 6, 6, 6, 1, 1, 1, 1, 5, 5, 5, 5, 10, 8, 8, 8, 8, 3, 3, 3, 3, 7, 7, 7, 7, 5, 5, 5, 5, 8, 8, 8, 8 };
            fakeReels[3] = new[] { 8, 8, 8, 8, 0, 6, 6, 6, 6, 1, 1, 1, 1, 7, 7, 7, 7, 3, 3, 3, 3, 4, 4, 4, 4, 8, 8, 8, 8, 9, 2, 2, 2, 2, 8, 8, 8, 8, 3, 3, 3, 3, 4, 4, 4, 4, 0, 5, 5, 5, 5, 9, 6, 6, 6, 6, 3, 3, 3, 3, 8, 8, 8, 8, 7, 7, 7, 7, 5, 5, 5, 5, 4, 4, 4, 4, 2, 2, 2, 2, 8, 8, 8, 8, 6, 6, 6, 6, 4, 4, 4, 4, 3, 3, 3, 3, 5, 5, 5, 5, 1, 1, 1, 1, 7, 7, 7, 7, 9, 8, 8, 8, 8, 7, 7, 7, 7 };
            fakeReels[4] = new[] { 7, 7, 7, 7, 9, 8, 8, 8, 8, 1, 1, 1, 1, 6, 6, 6, 6, 9, 5, 5, 5, 5, 4, 4, 4, 4, 10, 3, 3, 3, 3, 7, 7, 7, 7, 5, 5, 5, 5, 3, 3, 3, 3, 2, 2, 2, 2, 7, 7, 7, 7, 9, 6, 6, 6, 6, 8, 8, 8, 8, 5, 5, 5, 5, 7, 7, 7, 7, 1, 1, 1, 1, 8, 8, 8, 8, 4, 4, 4, 4, 7, 7, 7, 7, 2, 2, 2, 2, 5, 5, 5, 5, 10, 8, 8, 8, 8, 9, 6, 6, 6, 6, 3, 3, 3, 3, 8, 8, 8, 8, 5, 5, 5, 5 };

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
                return WinForWildFearOfDark;
            }
            if (id == 9)
            {
                return WinForScatter1FearOfDark;
            }
            if (id == 10)
            {
                return new int[] { 0, 0, WIN_FOR_SCATTER2_FEAR_OF_DARK, 0, 0 };
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesFearOfDark[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.24,
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
            var lines = new HelpLineConfigV3[40];
            for (var i = 0; i < 40; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = GlobalData.GameLineTurbo[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
