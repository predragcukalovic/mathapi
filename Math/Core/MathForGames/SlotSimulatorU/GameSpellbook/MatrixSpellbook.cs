
using MathForGames.BasicGameData;
using MathForGames.GameMagicOfTheRing;

namespace MathForGames.GameSpellbook
{
    public class MatrixSpellbook : MatrixMagicOfTheRing
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
            return GetLine(lineNumber).CalculateLineWin(LineWinsForGames.WinForLinesSpellbook, LineWinsForGames.WinForWildsSpellbook, 0, 1);
        }

        /// <summary>
        /// Računa dobitak linije sa bonus simbolom.
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
            return line.CalculateLineWin(LineWinsForGames.WinForLinesSpellbook, LineWinsForGames.WinForWildsSpellbook, 0, 1);
        }

        #endregion

    }
}
