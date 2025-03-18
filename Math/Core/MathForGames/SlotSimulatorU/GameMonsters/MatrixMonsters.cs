using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameMonsters
{
    public class MatrixMonsters : Matrix
    {
        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesMonsters, LineWinsForGames.WinForWildsMonsters, 0, 1);
        }

        #endregion
    }
}
