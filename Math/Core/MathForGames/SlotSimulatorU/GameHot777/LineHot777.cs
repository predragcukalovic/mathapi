using MathForGames.BasicGameData;
using MathForGames.GameVegasHot;

namespace MathForGames.GameHot777
{
    public class LineHot777 : LineVegasHot
    {
        #region Constructors

        public LineHot777()
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
                return LineWinsForGames.WinForLinesHot777[1, Line[0]];
            }
            if (Line[0] == Line[1])
            {
                return LineWinsForGames.WinForLinesHot777[0, Line[0]];
            }
            return 0;
        }

        #endregion
    }
}
