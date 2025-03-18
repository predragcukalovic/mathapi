using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;

namespace GameMayansBattle
{
    public class MatrixMayansBattle : Matrix
    {
        #region Public properties

        public static readonly int[,] WinForLinesMayansBattle =
        {
            {0, 0, 0, 0, 0},
            {0, 5, 10, 20, 50},
            {0, 4, 10, 20, 45},
            {0, 0, 8, 15, 40},
            {0, 0, 8, 15, 30},
            {0, 0, 6, 15, 25},
            {0, 0, 4, 10, 20},
            {0, 0, 4, 10, 20},
            {0, 0, 2, 8, 15},
            {0, 0, 2, 8, 15},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildMayansBattle = { 0, 10, 50, 250, 1000 };
        public static readonly int[] NumberOfGratis = { 7, 15, 30 };
        public const int SCATTER_WIN = 2;

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(WinForLinesMayansBattle, WinForWildMayansBattle, 0, 1);
        }

        /// <summary>
        /// Postavlja rilove u skladu sa verovatnocama.
        /// </summary>
        public void BuildMatrix(bool gratis)
        {
            var probsReel = gratis ? new int[] { 3, 3, 3, 3, 3 } : new int[] { 4, 4, 4, 4, 10 };
            var probsSymbol = gratis ? new int[] { 3, 16, 29, 42, 52, 62, 72, 82, 91, 100 } : new int[] { 1, 7, 13, 22, 31, 44, 58, 72, 86, 100 };
            var rnd = SoftwareRng.Next(100);
            var symbol = -1;
            for (var i = 0; i < probsSymbol.Length; i++)
            {
                if (rnd < probsSymbol[i])
                {
                    symbol = i;
                    break;
                }
            }
            for (var i = 0; i < 5; i++)
            {
                if (SoftwareRng.Next(probsReel[i]) == 0)
                {
                    SetElement(i, 0, symbol);
                    SetElement(i, 1, symbol);
                    SetElement(i, 2, symbol);
                }
            }
        }

        #endregion
    }
}
