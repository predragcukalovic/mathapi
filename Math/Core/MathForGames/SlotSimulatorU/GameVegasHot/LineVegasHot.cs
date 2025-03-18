using MathForGames.BasicGameData;

namespace MathForGames.GameVegasHot
{
    public class LineVegasHot
    {
        #region Private fields

        protected int[] Line;

        #endregion

        #region Constructors

        public LineVegasHot()
        {
            Line = new int[3];
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
            Line[element] = value;
        }

        /// <summary>
        /// Računa dobitak linije za ulog 1.
        /// </summary>
        /// <returns></returns>
        public virtual int CalculateLineWin()
        {
            if (Line[0] == Line[1] && Line[1] == Line[2])
            {
                return LineWinsForGames.WinForLinesVegasHot[Line[0]];
            }
            return 0;
        }

        #endregion
    }
}
