using MathForGames.BasicGameData;
using MathForGames.GameHotParty;

namespace MathForGames.GameNeonHot5
{
    public class MatrixNeonHot5 : MatrixHotParty
    {
        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesNeonHot, null, -1, 1);
        }

        #endregion
    }
}
