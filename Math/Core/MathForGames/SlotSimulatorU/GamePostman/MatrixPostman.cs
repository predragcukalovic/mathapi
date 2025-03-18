using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GamePostman
{
    public class MatrixPostman : Matrix
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
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesPostman, LineWinsForGames.WinForWildsPostman, 0, 2);
        }

        #endregion
    }
}
