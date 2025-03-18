using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameHotStars
{
    public class MatrixHotStars : Matrix
    {
        #region Public methods

        /// <summary>
        /// Uzima liniju iz matrice
        /// </summary>
        /// <param name="lineNumber">broj linije, 1 -- 10</param>
        /// <returns>vraća liniju pod datim brojem</returns>
        public LineHotStars GetLine(int lineNumber)
        {
            if (lineNumber < 1 || lineNumber > 10)
            {
                return null;
            }

            var line = new LineHotStars();
            for (var i = 0; i < 5; i++)
            {
                line.SetElement(i, GetElement(i, GlobalData.GameLineExtra[lineNumber - 1, i]));
            }
            return line;
        }

        /// <summary>
        /// Računa dobitak jedne linije iz matrice sleva na desno
        /// </summary>
        /// <param name="lineNumber">Broj linije za koji računa dobitak</param>
        /// <returns>Vraća dobitak koji daje tražena linija za uložen 1 kredit</returns>
        public virtual int CalculateLeftWinOfLine(int lineNumber)
        {
            return GetLine(lineNumber).CalculateLeftLineWin(LineWinsForGames.WinForLinesHotStars, 0);
        }

        /// <summary>
        /// Računa dobitak jedne linije iz matrice zdesna na levo
        /// </summary>
        /// <param name="lineNumber">Broj linije za koji računa dobitak</param>
        /// <returns>Vraća dobitak koji daje tražena linija za uložen 1 kredit</returns>
        public virtual int CalculateRightWinOfLine(int lineNumber)
        {
            return GetLine(lineNumber).CalculateRightLineWin(LineWinsForGames.WinForLinesHotStars, 0);
        }

        #endregion
    }
}
