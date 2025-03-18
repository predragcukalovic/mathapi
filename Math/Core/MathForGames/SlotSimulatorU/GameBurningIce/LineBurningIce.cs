using MathForGames.BasicGameData;
using MathForGames.GameVegasHot;

namespace MathForGames.GameBurningIce
{
    public class LineBurningIce : LineVegasHot
    {
        #region Public methodes

        /// <summary>
        /// Računa dobitak linije za ulog 1.
        /// </summary>
        /// <returns></returns>
        public override int CalculateLineWin()
        {
            if (Line[0] == Line[1] && Line[1] == Line[2])
            {
                return LineWinsForGames.WinForLinesBurningIce[Line[0]];
            }
            return 0;
        }

        #endregion
    }
}
