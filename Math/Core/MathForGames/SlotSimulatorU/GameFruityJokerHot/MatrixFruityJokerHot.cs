using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameFruityJokerHot
{
    public class MatrixFruityJokerHot : Matrix
    {
        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesFruityJokerHot, LineWinsForGames.WinForWildsFruityJokerHot, 0, 1);
        }

        #endregion
    }
}
