using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameTemplarsQuest
{
    public class MatrixTemplarsQuest : Matrix
    {
        #region Constructor or Singleton implementation

        public MatrixTemplarsQuest()
            : base(5)
        {
        }

        #endregion

        public new void FromMatrixArray(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, (j + 1) % 5]);
                }
            }
        }

        /// <summary>
        /// Uzima liniju iz matrice
        /// </summary>
        /// <param name="lineNumber">broj linije, 1 -- 10</param>
        /// <returns>vraća liniju pod datim brojem</returns>
        public LineTemplarsQuest GetLine(int lineNumber)
        {
            if (lineNumber < 1 || lineNumber > 10)
            {
                return null;
            }

            var line = new LineTemplarsQuest();
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
        public int CalculateLeftWinOfLine(int lineNumber)
        {
            return GetLine(lineNumber).CalculateLeftLineWin(LineWinsForGames.WinForLinesTemplarsQuest, 0, LineWinsForGames.WinForWildsTemplarsQuest, 1);
        }

        /// <summary>
        /// Računa dobitak jedne linije iz matrice zdesna na levo
        /// </summary>
        /// <param name="lineNumber">Broj linije za koji računa dobitak</param>
        /// <returns>Vraća dobitak koji daje tražena linija za uložen 1 kredit</returns>
        public int CalculateRightWinOfLine(int lineNumber)
        {
            return GetLine(lineNumber).CalculateRightLineWin(LineWinsForGames.WinForLinesTemplarsQuest, 0, LineWinsForGames.WinForWildsTemplarsQuest, 1);
        }
    }
}
