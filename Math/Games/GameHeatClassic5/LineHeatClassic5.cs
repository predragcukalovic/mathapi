using MathForGames.GameVegasHot;

namespace GameHeatClassic5
{
    public class LineHeatClassic5 : LineVegasHot
    {
        #region Public properties

        public static readonly int[] WinForLinesHeatClassic = { 300, 200, 100, 75, 75, 35, 35, 35, 35 };

        #endregion

        #region Constructors

        public LineHeatClassic5()
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
                return WinForLinesHeatClassic[Line[0]];
            }
            return 0;
        }

        #endregion
    }
}
