using GameWildHot40Blow;
using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;
using System.Collections.Generic;

namespace GamePiratesPapi
{
    public class MatrixPiratesPapi : MatrixWildHot40Blow
    {
        #region Public properties

        public static int[] PlayLines = { 40 };

        public static readonly int[,] WinForLinesPiratesPapi =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 20, 50, 400},
            {0, 0, 20, 45, 300},
            {0, 0, 15, 40, 150},
            {0, 0, 15, 40, 150},
            {0, 0, 5, 20, 50},
            {0, 0, 5, 20, 50},
            {0, 0, 5, 20, 50},
            {0, 0, 5, 20, 50},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildsPiratesPapi = { 0, 0, 50, 150, 750 };

        #endregion

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineTurbo).CalculateLineWin(WinForLinesPiratesPapi, WinForWildsPiratesPapi, 0, 1);
        }

        public void AddWilds()
        {
            var wildProbs = new[] { 100, 100, 100, 100, 100 };
            var wilds = new List<int>();
            for (var i = 0; i < 5; i++)
            {
                if (SoftwareRng.Next(wildProbs[i]) == 0)
                {
                    wilds.Add(i);
                }
            }
            if (wilds.Count == 0)
            {
                return;
            }
            var arr = new int[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    arr[i, j] = GetElement(i, j + 5);
                }
            }
            foreach (var wild in wilds)
            {
                arr[wild, SoftwareRng.Next(4) + 1] = 0;
            }
            FromMatrixArray(arr);
        }

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 2, 2, 6, 6, 3, 3, 2, 2, 4, 8, 8, 5, 5, 7, 7, 4, 4, 1, 1, 5, 5, 7, 7, 2, 6, 8, 8, 0, 3, 3, 5, 4, 4 };
            fakeReels[1] = new[] { 8, 8, 6, 6, 4, 4, 5, 2, 2, 3, 3, 1, 1, 0, 8, 8, 6, 7, 7, 5, 5, 2, 2, 4, 4, 7, 7, 3, 3, 8, 5, 5, 6, 6, 7, 1, 1 };
            fakeReels[2] = new[] { 6, 6, 7, 7, 3, 3, 6, 4, 4, 0, 1, 1, 2, 2, 6, 6, 5, 4, 4, 7, 7, 8, 8, 3, 5, 5, 6, 3, 3, 7, 8, 8, 1, 1, 5, 5, 2 };
            fakeReels[3] = new[] { 7, 7, 2, 2, 6, 6, 4, 5, 5, 7, 7, 4, 4, 2, 2, 1, 1, 8, 8, 6, 3, 3, 2, 1, 1, 6, 6, 5, 0, 8, 8, 3, 3, 4, 4, 8 };
            fakeReels[4] = new[] { 4, 4, 8, 8, 6, 7, 7, 5, 5, 2, 2, 1, 1, 3, 5, 5, 6, 6, 3, 3, 8, 8, 1, 1, 7, 7, 2, 2, 0, 4, 4, 3, 3, 6, 6 };

            return fakeReels;
        }/**/

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            if (id == 0)
            {
                return WinForWildsPiratesPapi;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesPiratesPapi[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.48,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[9];
            for (var i = 0; i < 9; i++)
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
