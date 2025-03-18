using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;

namespace GameSpecialFruits
{
    public class MatrixSpecialFruits : Matrix
    {
        public static int[] PlayLines = { 20 };

        public enum SymbolsSpecialFruits
        {
            Wild,
            High1,
            Scatter,
            Medium1,
            Medium2,
            Low1,
            Low2,
            Low3,
        }

        public static readonly int[,] WinForLinesSpecialFruits =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 20, 80, 400},
            {0, 0, 0, 0, 0},
            {0, 0, 20, 40, 200},
            {0, 0, 20, 40, 200},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
        };
        public static readonly int[] WinForWildsSpecialFruits = { 0, 0, 40, 400, 1000 };
        public static readonly int[] WinForScatterSpecialFruits = { 0, 0, 5, 20, 500 };

        public static readonly int[] PayLines = { 5, 10, 15, 20, 30, 40 };
        public static readonly double[] PayLinesCumulativeProbabilities = { 0.06, 0.24, 0.48, 0.73, 0.88, 1.00 };


        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(WinForLinesSpecialFruits, WinForWildsSpecialFruits, 0, 1);
        }

        #region V3 structs

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
            var symbols = new HelpSymbolConfigV3<object>[8];
            for (var i = 0; i < 8; i++)
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

        public static int[] GetSymbolCoefficients(int id)
        {
            if (id == (int)SymbolsSpecialFruits.Wild)
            {
                return WinForWildsSpecialFruits;
            }
            if (id == (int)SymbolsSpecialFruits.Scatter)
            {
                return WinForScatterSpecialFruits;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesSpecialFruits[id, i];
            }
            return coefficients;
        }

        private static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            var lines = new HelpLineConfigV3[40];
            for (var i = 0; i < 40; i++)
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

        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 7, 4, 4, 4, 5, 5, 5, 0, 0, 0, 7, 7, 6, 6, 6, 3, 3, 3, 0, 0, 0, 1, 1, 1, 7, 7, 7, 2, 4, 4, 4, 5, 5, 5, 1, 1, 1, 2, 5, 5, 5, 0, 0, 0, 7, 7 };
            fakeReels[1] = new[] { 6, 6, 6, 5, 0, 0, 0, 5, 5, 5, 7, 7, 7, 6, 6, 6, 1, 1, 1, 2, 3, 3, 3, 0, 0, 0, 1, 1, 1, 7, 7, 7, 2, 3, 3, 3, 1, 1, 1, 0, 0, 0, 4, 4, 4, 5, 5 };
            fakeReels[2] = new[] { 3, 3, 3, 6, 6, 6, 4, 4, 4, 0, 0, 0, 3, 3, 3, 5, 5, 5, 0, 0, 0, 4, 4, 4, 1, 1, 1, 7, 7, 7, 2, 1, 1, 1, 3, 3, 3, 2, 1, 1, 1, 0, 0, 0, 5, 5, 5, 2 };
            fakeReels[3] = new[] { 1, 1, 1, 6, 6, 6, 0, 0, 0, 1, 1, 1, 3, 3, 3, 7, 7, 7, 0, 0, 0, 5, 5, 5, 1, 1, 1, 4, 4, 4, 0, 0, 0, 3, 3, 3, 2, 4, 4, 4, 5, 5, 5, 1, 1, 1, 7, 7 };
            fakeReels[4] = new[] { 7, 3, 3, 3, 2, 4, 4, 4, 2, 1, 1, 1, 3, 3, 3, 0, 0, 0, 7, 7, 7, 4, 4, 4, 5, 5, 5, 1, 1, 1, 0, 0, 0, 3, 3, 3, 1, 1, 1, 6, 6, 6, 2, 7, 7 };
            return fakeReels;
        }

        #endregion
    }
}
