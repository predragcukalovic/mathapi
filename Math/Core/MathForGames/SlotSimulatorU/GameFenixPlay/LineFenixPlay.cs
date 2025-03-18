
using MathForGames.BasicGameData;
using MathForGames.GameVegasHot;

namespace MathForGames.GameFenixPlay
{
    public class LineFenixPlay : LineVegasHot
    {
        #region Constructors

        public LineFenixPlay()
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
                return LineWinsForGames.WinForLinesFenixPlay[Line[0]];
            }
            return 0;
        }

        #endregion
    }
}
