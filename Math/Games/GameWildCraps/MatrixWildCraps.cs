using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace GameWildCraps
{
    public class MatrixWildCraps : Matrix
    {
        #region Public properties

        public static readonly int[,] WinForLinesWildCraps =
        {
            {0, 0, 0, 0, 0},
            {0, 5, 50, 150, 2000},
            {0, 0, 30, 50, 500},
            {0, 0, 10, 40, 100},
            {0, 0, 10, 40, 100},
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
        public static readonly int[] WinForWildWildCraps = { 0, 0, 0, 0, 0 };
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
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(WinForLinesWildCraps, WinForWildWildCraps, 0, 1);
        }

        /// <summary>
        /// Postavlja wildove zavisno od broja na kockici.
        /// </summary>
        public void BuildGratisMatrix(int dice)
        {
            if (dice > 6 || dice < 1)
            {
                return;
            }
            if (dice % 2 == 1)
            {
                SetElement(2, 1, 0);
            }
            if (dice == 1)
            {
                return;
            }
            SetElement(3, 0, 0);
            SetElement(1, 2, 0);
            if (dice < 4)
            {
                return;
            }
            SetElement(3, 2, 0);
            SetElement(1, 0, 0);
            if (dice < 6)
            {
                return;
            }
            SetElement(3, 1, 0);
            SetElement(1, 1, 0);
        }

        #endregion
    }
}
