using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameMagicTarget
{
    public class MatrixMagicTarget : Matrix
    {
        #region Public properties

        public const int GRATIS_GAMES = 10;

        #endregion

        #region Public methodsx

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineRing).CalculateLineWin(LineWinsForGames.WinForLinesMagicTarget, LineWinsForGames.WinForWildMagicTarget, 0, 1);
        }

        /// <summary>
        /// Da li matrica daje gratis igre?
        /// </summary>
        /// <returns></returns>
        public bool IsMatrixGiveGratisGames()
        {
            if (GetNumberOfElement(2) < 3)
            {
                return false;
            }
            for (var i = 1; i < 4; i++)
            {
                if (IsReelHave(i - 1, 2) && IsReelHave(i, 2) && IsReelHave(i + 1, 2))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
