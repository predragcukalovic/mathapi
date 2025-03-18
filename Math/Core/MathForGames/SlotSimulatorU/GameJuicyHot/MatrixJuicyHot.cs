using MathForGames.BasicGameData;
using MathForGames.GameHotParty;

namespace MathForGames.GameJuicyHot
{
    public class MatrixJuicyHot : MatrixHotParty
    {
        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesJuicyHot, null, -1, 1);
        }
    }
}
