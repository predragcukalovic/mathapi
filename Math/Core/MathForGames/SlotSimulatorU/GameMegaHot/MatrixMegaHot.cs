using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameMegaHot
{
    public class MatrixMegaHot : Matrix
    {
        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesMegaHot, null, -1, 1);
        }

        /// <summary>
        /// Daje sa koliko se množi dobitak (1, 3, 4 ili 5).
        /// </summary>
        /// <returns></returns>
        public int GetMultiplicator()
        {
            var firstElement = GetElement(0, 0);
            if (firstElement >= 1 && firstElement <= 4)
            {
                var next = 0;
                while (next < 15 && GetElement(next / 3, next % 3) == firstElement)
                {
                    next++;
                }
                if (next >= 9)
                {
                    return next / 3;
                }
            }

            return 1;
        }

        #endregion
    }
}
