using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace GameSevenClassicHot
{
    public class MatrixSevenClassicHot : Matrix
    {
        #region Public properties

        public const int GRATIS_GAMES = 10;
        public static readonly int[,] WinForLinesSevenClassicHot =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 20, 100, 200},
            {0, 0, 20, 100, 200},
            {0, 0, 20, 100, 200},
            {0, 0, 5, 20, 100},
            {0, 0, 5, 20, 100},
            {0, 0, 1, 10, 50},
            {0, 0, 1, 10, 50},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildSevenClassicHot = { 0, 0, 40, 400, 1000 };
        public static readonly int[] WinForScatterSevenClassicHot = { 0, 0, 10, 50, 2000 };

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(WinForLinesSevenClassicHot, WinForWildSevenClassicHot, 0, 1);
        }

        #endregion
    }
}
