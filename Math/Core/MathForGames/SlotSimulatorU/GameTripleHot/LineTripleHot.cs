using MathForGames.BasicGameData;
using MathForGames.GameVegasHot;

namespace MathForGames.GameTripleHot
{
    public class LineTripleHot : LineVegasHot
    {
        #region Constructors

        public LineTripleHot()
        {
            Line = new int[3];
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <returns></returns>
        public override int CalculateLineWin()
        {
            if (Line[0] == Line[1] && Line[1] == Line[2])
            {
                return LineWinsForGames.WinForLinesTripleHot[Line[0]];
            }
            return 0;
        }

        #endregion
    }
}
