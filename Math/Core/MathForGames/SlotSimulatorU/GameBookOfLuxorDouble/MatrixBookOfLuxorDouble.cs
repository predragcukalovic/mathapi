using MathForGames.BasicGameData;
using MathForGames.GameMagicOfTheRing;

namespace MathForGames.GameBookOfLuxorDouble
{
    public class MatrixBookOfLuxorDouble : MatrixMagicOfTheRing
    {
        #region Public properties

        public new const int GRATIS_GAMES = 12;

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber).CalculateLineWin(LineWinsForGames.WinForLinesBookOfDouble, LineWinsForGames.WinForWildsBookOfDouble, 0, 1);
        }

        /// <summary>
        /// Računa dobitak linije sa gratis elementom.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="gratisElement"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber, int gratisElement)
        {
            if (gratisElement == 0)
            {
                return CalculateWinLine(lineNumber);
            }
            var line = GetLine(lineNumber);
            for (var i = 0; i < 5; i++)
            {
                if (line.GetElement(i) == gratisElement)
                {
                    line.SetElement(i, 15);
                }
            }
            return line.CalculateLineWin(LineWinsForGames.WinForLinesBookOfDouble, LineWinsForGames.WinForWildsBookOfDouble, 0, 1);
        }

        /// <summary>
        /// Klon matrice.
        /// </summary>
        /// <returns></returns>
        public MatrixBookOfLuxorDouble Clone()
        {
            var mat = new MatrixBookOfLuxorDouble();
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    mat.SetElement(i, j, GetElement(i, j));
                }
            }
            return mat;
        }

        #endregion
    }
}
