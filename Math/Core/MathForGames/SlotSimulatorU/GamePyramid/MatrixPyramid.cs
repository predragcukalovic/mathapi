using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GamePyramid
{
    public class MatrixPyramid : Matrix
    {
        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesPyramid, LineWinsForGames.WinForWildsPyramid, 0, 1);
        }

        #endregion
    }
}
