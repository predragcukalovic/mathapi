using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameForestFruits
{
    public class MatrixForestFruits : Matrix
    {
        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesForestFruits, LineWinsForGames.WinForWildsForestFruits, 2, 1);
        }

        #endregion
    }
}
