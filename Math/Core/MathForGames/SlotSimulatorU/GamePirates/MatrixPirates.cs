using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GamePirates
{
    public class MatrixPirates : Matrix
    {
        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesPirates, LineWinsForGames.WinForWildsPirates, 0, 1);
        }

        #endregion
    }
}
