using MathForGames.BasicGameData;

namespace MathForGames.GameCrissCross
{
    public class LineCrissCross
    {
        #region Private fields

        private readonly int[] _Line;

        #endregion

        #region Private methods

        /// <summary>
        /// Geter za određeni element iz niza
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private int GetElement(int element)
        {
            return _Line[element];
        }

        #endregion

        #region Constructors

        public LineCrissCross()
        {
            _Line = new int[4];
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Seter za određeni element u nizu
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public void SetElement(int element, int value)
        {
            _Line[element] = value;
        }

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <returns></returns>
        public int CalculateLineWin()
        {
            var element = GetElement(0);
            var multiply = element == 0 ? 2 : 1;
            if (element == 1)
            {
                return 0;
            }
            var i = 1;
            while (i < 4 && 1 != GetElement(i) && (element == GetElement(i) || GetElement(i) == 0 || element == 0))
            {
                if (GetElement(i) == 0)
                {
                    multiply = 2;
                }
                else
                {
                    if (element == 0)
                    {
                        element = GetElement(i);
                    }
                }
                i++;
            }

            return LineWinsForGames.WinForLinesRollingDices[element, i] * multiply;
        }

        /// <summary>
        /// Daje dobitni element za liniju.
        /// </summary>
        /// <returns></returns>
        public int GetWinningElement()
        {
            var elem = GetElement(0);
            var i = 0;
            while (i < 4 && GetElement(i) == 0)
            {
                elem = GetElement(++i);
            }

            return elem;
        }

        #endregion
    }
}
