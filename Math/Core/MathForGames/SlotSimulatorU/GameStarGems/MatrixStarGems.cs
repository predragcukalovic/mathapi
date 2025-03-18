using MathForGames.BasicGameData;
using MathForGames.GameHotStars;

namespace MathForGames.GameStarGems
{
    public class MatrixStarGems : MatrixHotStars
    {
        #region Public methods

        /// <summary>
        /// Računa dobitak jedne linije iz matrice sleva na desno
        /// </summary>
        /// <param name="lineNumber">Broj linije za koji računa dobitak</param>
        /// <returns>Vraća dobitak koji daje tražena linija za uložen 1 kredit</returns>
        public override int CalculateLeftWinOfLine(int lineNumber)
        {
            return GetLine(lineNumber).CalculateLeftLineWin(LineWinsForGames.WinForLinesStarGems, 0);
        }

        /// <summary>
        /// Računa dobitak jedne linije iz matrice zdesna na levo
        /// </summary>
        /// <param name="lineNumber">Broj linije za koji računa dobitak</param>
        /// <returns>Vraća dobitak koji daje tražena linija za uložen 1 kredit</returns>
        public override int CalculateRightWinOfLine(int lineNumber)
        {
            return GetLine(lineNumber).CalculateRightLineWin(LineWinsForGames.WinForLinesStarGems, 0);
        }

        #endregion
    }
}
