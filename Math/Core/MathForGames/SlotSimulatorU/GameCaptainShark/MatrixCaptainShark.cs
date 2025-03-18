using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameCaptainShark
{
    public class MatrixCaptainShark : Matrix
    {
        #region Public properties

        public const int GRATIS_MULTIPLICATOR = 3;

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineRing).CalculateLineWin(LineWinsForGames.WinForLinesCaptainShark, LineWinsForGames.WinForWildsCaptainShark, 0, 2);
        }

        #endregion
    }
}
