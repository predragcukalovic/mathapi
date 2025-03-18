using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameDiamonds
{
    public class MatrixDiamonds : Matrix
    {
        #region Public methods

        /// <summary>
        /// Računa dobitak linije
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public int CalculateWinOfLine(int lineNumber)
        {
            return GetLine(lineNumber).CalculateLineWin();
        }

        /// <summary>
        /// Uzima liniju iz matrice.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public LineDiamonds GetLine(int lineNumber)
        {
            if (lineNumber < 1 || lineNumber > 10)
            {
                return null;
            }

            var line = new LineDiamonds();
            for (var i = 0; i < 5; i++)
            {
                line.SetElement(i, GetElement(i, GlobalData.GameLineExtra[lineNumber - 1, i]));
            }
            return line;
        }

        #endregion
    }
}
