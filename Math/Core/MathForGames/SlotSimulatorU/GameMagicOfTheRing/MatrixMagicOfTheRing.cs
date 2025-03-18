using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameMagicOfTheRing
{
    public class MatrixMagicOfTheRing : Matrix
    {
        #region Public properties

        public const int GRATIS_GAMES = 10;

        #endregion

        #region Public methods

        /// <summary>
        /// uzima liniju iz matrice
        /// </summary>
        /// <param name="lineNumber">broj linije, 1 -- 15</param>
        /// <returns>vraća liniju pod datim brojem</returns>
        protected LineMagicOfTheRing GetLine(int lineNumber)
        {
            if (lineNumber < 1 || lineNumber > 10)
            {
                return null;
            }

            var line = new LineMagicOfTheRing();
            for (var i = 0; i < 5; i++)
            {
                line.SetElement(i, GetElement(i, GlobalData.GameLineExtra[lineNumber - 1, i]));
            }
            return line;
        }

        /// <summary>
        /// Računa dobitak linije za bonus simbol.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="gratisElement"></param>
        /// <param name="lineWins"></param>
        /// <returns></returns>
        public int CalculateNonOrderWinOfLine(int lineNumber, int gratisElement, int[,] lineWins)
        {
            return GetLine(lineNumber).CalculateNonOrderWin(gratisElement, lineWins);
        }

        /// <summary>
        /// Proverava da li ima uslova da se element proširi u ceo ril
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool IsCanBeTransformed(int element)
        {
            if (GetNumberOfElement(element) >= 3)
            {
                return true;
            }
            if (GetNumberOfElement(element) >= 2 && (element >= 1 && element <= 4))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Transformiše rilove koje sadrže element
        /// </summary>
        /// <param name="element">Gratis element</param>
        public void Transform(int element)
        {
            if (IsCanBeTransformed(element))
            {
                for (var i = 0; i < 5; i++)
                {
                    if (IsReelHave(i, element))
                    {
                        SetElement(i, 0, element);
                        SetElement(i, 1, element);
                        SetElement(i, 2, element);
                    }
                }
            }
        }

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber).CalculateLineWin(LineWinsForGames.WinForLinesMagicOfTheRing, null, 0, 1);
        }

        /// <summary>
        /// Računa dobitak linije sa gratis elementom.
        /// </summary>
        /// <param name="lineNumber">Broj linije</param>
        /// <param name="gratisElement">Gratis simbol</param>
        /// <returns></returns>
        public virtual int CalculateWinLine(int lineNumber, int gratisElement)
        {
            return 0;
        }

        #endregion
    }
}
