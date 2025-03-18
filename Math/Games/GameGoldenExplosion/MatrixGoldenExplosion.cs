using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;

namespace GameGoldenExplosion
{
    public class MatrixGoldenExplosion : Matrix
    {
        #region Public properties GoldenExplosion

        public static readonly int[,] WinForLinesGoldenExplosion =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 10, 50, 150, 1000},
            {0, 0, 30, 100, 500},
            {0, 0, 20, 50, 200},
            {0, 0, 20, 50, 200},
            {0, 0, 20, 50, 200},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 15, 75},
            {0, 0, 10, 15, 75},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildGoldenExplosion = { 0, 0, 0, 0, 0 };
        public static readonly int[] WinForScatterGoldenExplosion = { 0, 0, 2, 10, 100 };

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public int CalculateWinLine(int lineNumber, int multiplier)
        {
            var m = 1;
            var l = GetLine(lineNumber, GlobalData.GameLineExtra);
            if (l.GetElement(2) == 1)
            {
                l.SetElement(2, 0);
                m = multiplier;
            }
            return l.CalculateLineWin(WinForLinesGoldenExplosion, WinForWildGoldenExplosion, 0, m);
        }

        public static int GetRandomMultiplier(ref int multIndex)
        {
            var mult = new[] { 2, 3, 4, 5, 10, 15, 20, 25 };
            var prob = new[] { 0.37, 0.32, 0.075, 0.1, 0.07, 0.015, 0.03, 0.02 };
            var sum = 0.0;
            var rnd = SoftwareRng.Next();
            for (var i = 0; i < prob.Length; i++)
            {
                sum += prob[i];
                if (rnd < sum)
                {
                    multIndex = i;
                    return mult[i];
                }
            }
            return 1;
        }

        #endregion
    }
}
