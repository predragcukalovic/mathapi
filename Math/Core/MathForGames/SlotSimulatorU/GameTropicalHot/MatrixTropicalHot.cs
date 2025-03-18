using MathForGames.BasicGameData;
using MathForGames.GameHotParty;

namespace MathForGames.GameTropicalHot
{
    public class MatrixTropicalHot : MatrixHotParty
    {

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesTropicalHot, null, -1, 1);
        }

        #endregion
    }
}
