using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameWizard
{
    public class MatrixWizard : Matrix
    {
        #region Public properties

        public const int GRATIS_GAMES = 15;
        public const int GRATIS_MULTIPLICATOR = 3;

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesWizard, LineWinsForGames.WinForWildsWizard, 0, 2);
        }

        #endregion
    }
}
