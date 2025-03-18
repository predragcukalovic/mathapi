using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;

namespace MathForGames.GameCloverCash
{
    public class MatrixCloverCash : Matrix
    {
        #region Public properties

        public static readonly int[] FireWins = { 50, 100, 150, 200, 250, 500, 750, 5000 };
        public static readonly int[] IceWins = { 10, 20, 30, 40, 50, 100, 150, 200, 250, 500, 750, 5000 };
        public static readonly double[] IceProbs = { 0.4212299915754, 0.25273799494524, 0.1404099971918, 0.08424599831508, 0.04212299915754, 0.02808199943836, 0.01404099971918, 0.008424599831508, 0.0050547598989048, 0.0025273799494524, 0.0008424599831508, 0.0002808199943836 };
        public static readonly double[] FireProbs = { 0.46242774566474, 0.23121387283237, 0.115606936416185, 0.0578034682080925, 0.0289017341040462, 0.0722543352601156, 0.0289017341040462, 0.00289017341040462 };
        public const double FIRE_TABLE_PROB = 0.17598089;
        public static readonly double[] GratisLockProbs = { 0.11, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.05, 0.00284232 };
        public const int GRAND_JACKPOT = 25000;

        public static int[] PlayLines = { 10 };

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesCloverCash, LineWinsForGames.WinForWildsCloverCash, 0, 1);
        }

        public static int GetWinByIndex(int index, int table)
        {
            if (table == 1)
            {
                return FireWins[index];
            }
            return IceWins[index];
        }

        /// <summary>
        /// 0 = Ice, 1 = Fire
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static int GetRandomIndexByTable(int table)
        {
            return GetIndexByProb(table == 1 ? FireProbs : IceProbs);
        }

        /// <summary>
        /// 0 = Ice, 1 = Fire
        /// </summary>
        /// <returns></returns>
        public static int ChooseTable()
        {
            return SoftwareRng.Next() < FIRE_TABLE_PROB ? 1 : 0;
        }

        private static int GetIndexByProb(double[] probs)
        {
            var sum = 0.0;
            var rnd = SoftwareRng.Next();
            var n = probs.Length;
            for (var i = 0; i < n; i++)
            {
                sum += probs[i];
                if (rnd < sum)
                {
                    return i;
                }
            }
            return 0;
        }

        #endregion

        #region V3Structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 10, 10, 7, 7, 10, 10, 10, 7, 10, 11, 11, 11, 9, 2, 2, 2, 9, 9, 9, 1, 9, 9, 4, 8, 8, 8, 7, 10, 10, 7, 7, 0, 0, 0, 0, 9, 9, 9, 7, 10, 1, 1, 1, 10, 10, 10, 2, 8, 8, 8, 11, 11, 11, 6, 6, 5, 6, 5, 6, 3, 3, 8, 4, 8, 4, 9, 9, 9, 7 };
            fakeReels[1] = new[] { 9, 9, 9, 2, 5, 2, 5, 5, 11, 11, 11, 6, 8, 6, 8, 6, 6, 10, 10, 8, 8, 8, 4, 8, 10, 10, 10, 3, 3, 8, 8, 7, 7, 7, 4, 7, 1, 1, 1, 9, 9, 9, 2, 2, 5, 2, 5, 5, 0, 0, 0, 0, 6, 8, 6, 8, 6, 6, 11, 11, 11, 10, 10, 8, 8, 8, 4, 8, 10, 10, 10 };
            fakeReels[2] = new[] { 4, 7, 4, 4, 2, 2, 9, 9, 9, 11, 11, 11, 5, 5, 5, 10, 5, 10, 10, 10, 3, 0, 0, 0, 3, 1, 1, 1, 9, 2, 9, 9, 9, 8, 6, 8, 8, 8, 6, 6, 7, 7, 7, 4, 7, 4, 1, 9, 0, 0, 0, 5, 5, 5, 10, 5, 10, 10, 10, 3, 10, 10, 11, 11, 11, 3, 2, 9, 2 };
            fakeReels[3] = new[] { 5, 5, 7, 5, 7, 7, 7, 5, 7, 11, 11, 11, 3, 8, 8, 3, 8, 8, 8, 2, 9, 2, 9, 9, 9, 1, 9, 9, 0, 0, 0, 0, 10, 4, 10, 10, 10, 6, 4, 6, 6, 6, 6, 5, 5, 7, 5, 7, 7, 7, 5, 7, 8, 8, 8, 3, 3, 8, 8, 3, 11, 11, 11, 2, 2, 9, 2, 9, 9, 9, 1, 1, 1 };
            fakeReels[4] = new[] { 6, 6, 6, 4, 4, 6, 4, 11, 11, 11, 8, 8, 2, 8, 2, 8, 5, 2, 5, 5, 5, 10, 5, 10, 10, 10, 3, 10, 3, 3, 0, 0, 0, 0, 6, 6, 6, 6, 4, 4, 6, 11, 11, 11, 8, 8, 2, 8, 2, 8, 5, 5, 5, 10, 5, 10, 10, 10, 3, 3, 0, 0, 0, 0, 9, 9, 9, 9, 7, 7, 7, 1, 7, 7 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetGratisFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 11, 12, 12, 12, 11 };
            fakeReels[1] = new[] { 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 11, 12, 12, 12, 12, 12, 11 };
            fakeReels[2] = new[] { 12, 12, 12, 12, 12, 12, 12, 12, 11, 12, 12, 12, 12, 12, 12, 12, 11, 12, 12, 12, 12, 12 };
            fakeReels[3] = new[] { 12, 12, 11, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 11, 12, 12, 12, 12, 12 };
            fakeReels[4] = new[] { 12, 12, 12, 12, 12, 12, 12, 12, 11, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 11 };

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
                return LineWinsForGames.WinForWildsCloverCash;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = LineWinsForGames.WinForLinesCloverCash[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)95.0,
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
                    features = i == 11 ? new[] { HelpSymbolFeatureV3.Bonus } : new[] { HelpSymbolFeatureV3.Regular }
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
