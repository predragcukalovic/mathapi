using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameJungle
{
    public class MatrixJungle : Matrix
    {
        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesJungle, LineWinsForGames.WinForWildsJungle, 0, 1);
        }

        #endregion
    }
}
