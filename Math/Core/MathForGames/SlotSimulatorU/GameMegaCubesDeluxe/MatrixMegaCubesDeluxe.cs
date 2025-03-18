using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameMegaCubesDeluxe
{
    public class MatrixMegaCubesDeluxe : Matrix
    {
        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesMegaCubesDeluxe, null, -1, 1);
        }
        #endregion
    }
}
