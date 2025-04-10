﻿using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using MathForGames.GameTurboHot40;

namespace GameVintageFruits40
{
    public class MatrixVintageFruits40 : MatrixTurboHot40
    {
        public static readonly int[,] WinForLinesVintageFruits40 =
        {
            {0, 0, 0, 0, 0},
            {0, 4, 60, 200, 1000},
            {0, 0, 40, 100, 300},
            {0, 0, 20, 80, 200},
            {0, 0, 20, 80, 200},
            {0, 0, 8, 40, 100},
            {0, 0, 8, 40, 100},
            {0, 0, 8, 40, 100},
            {0, 0, 8, 40, 100},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildsVintageFruits40 = { 0, 0, 0, 0, 0 };
        public static readonly int[] WinForScatterVintageFruits40 = { 0, 0, 2, 20, 500 };
        public static int[] PlayLines = { 40 };

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineTurbo).CalculateLineWin(WinForLinesVintageFruits40, WinForWildsVintageFruits40, 0, 1);
        }

        #region Struct V3

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 1, 1, 1, 1, 1, 6, 6, 5, 5, 8, 8, 4, 4, 4, 4, 9, 7, 7, 3, 3, 3, 3, 2, 2, 9, 5, 5, 5, 5, 6, 6, 7, 7, 7, 7, 9, 8, 8, 8, 8, 7, 7, 7, 2, 2, 2, 2, 9, 8, 8, 4, 4, 4, 4, 9, 5, 5, 5, 3, 3, 6, 6, 6, 6, 8, 8, 8, 8, 9, 7, 7, 7, 7, 4, 4, 3, 3, 3, 3, 6, 6 };
            fakeReels[1] = new[] { 1, 1, 1, 1, 8, 8, 8, 8, 9, 7, 7, 7, 6, 6, 6, 6, 0, 0, 0, 0, 3, 3, 3, 3, 9, 8, 8, 8, 8, 1, 1, 5, 5, 4, 4, 4, 4, 0, 0, 0, 0, 8, 8, 8, 8, 9, 5, 5, 5, 5, 2, 2, 2, 2, 6, 6, 6, 6, 5, 5, 2, 2, 6, 6, 6, 6, 9, 8, 8, 8, 8 };
            fakeReels[2] = new[] { 1, 1, 1, 1, 7, 7, 7, 7, 9, 5, 5, 5, 5, 2, 2, 2, 2, 9, 3, 3, 3, 3, 0, 0, 0, 0, 0, 5, 5, 5, 6, 6, 6, 4, 4, 4, 4, 9, 7, 7, 7, 7, 0, 0, 0, 0, 2, 2, 2, 7, 7, 7, 1, 1, 1, 5, 5, 7, 7, 7, 7 };
            fakeReels[3] = new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 4, 4, 4, 4, 6, 6, 6, 6, 6, 0, 0, 0, 0, 7, 7, 7, 7, 7, 1, 1, 1, 1, 3, 3, 3, 3, 2, 2, 2, 0, 0, 0, 0, 0, 8, 8, 8, 8, 8, 9, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 4, 4, 4, 4, 9, 5, 5, 5, 5, 5, 2, 2, 2, 1 };
            fakeReels[4] = new[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 4, 4, 4, 4, 6, 6, 6, 6, 8, 8, 8, 8, 9, 3, 3, 3, 3, 6, 6, 5, 5, 5, 5, 1, 1, 1, 1, 7, 7, 7, 7, 9, 8, 8, 2, 2, 2, 2, 5, 5, 5, 5, 1, 1, 1, 1, 1, 7, 7, 2, 2, 2, 2, 2, 3, 3, 3, 3, 5, 5, 9, 4, 4, 4, 4, 4 };

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
                return WinForWildsVintageFruits40;
            }
            if (id == 9)
            {
                return WinForScatterVintageFruits40;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesVintageFruits40[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.38,
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
