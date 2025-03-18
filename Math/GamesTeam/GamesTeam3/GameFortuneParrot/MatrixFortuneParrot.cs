using MathForGames.BasicGameData;
using MathForGames.GameGoldenCrown;

namespace GameFortuneParrot
{
    public class MatrixFortuneParrot : MatrixGoldenCrown
    {
        #region Public properties

        public static readonly int[,] WinForLinesFortuneParrot =
        {
            { 0, 0, 0, 0, 0 },
            { 0, 10, 50, 250, 5000 },
            { 0, 0, 40, 120, 700 },
            { 0, 0, 40, 120, 700 },
            { 0, 0, 20, 40, 200 },
            { 0, 0, 10, 30, 150 },
            { 0, 0, 10, 30, 150 },
            { 0, 0, 10, 30, 150 },
            { 0, 0, 10, 30, 150 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
        };

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(WinForLinesFortuneParrot, LineWinsForGames.WinForWildGoldenCrown, 0, 1);
        }

        #endregion
    }
}