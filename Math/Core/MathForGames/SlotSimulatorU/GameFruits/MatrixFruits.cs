using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameFruits
{
    public class MatrixFruits : Matrix
    {
        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesFruits, LineWinsForGames.WinForWildsFruits, 2, 1);
        }

        #endregion
    }
}
