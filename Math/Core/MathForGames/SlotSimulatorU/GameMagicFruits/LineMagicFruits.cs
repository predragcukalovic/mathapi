using MathForGames.BasicGameData;
using MathForGames.GameVegasHot;

namespace MathForGames.GameMagicFruits
{
    public class LineMagicFruits : LineVegasHot
    {
        #region Constructors

        public LineMagicFruits()
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
            if (Line[0] > 7 && Line[1] > 7 && Line[2] > 7)
            {
                return LineWinsForGames.WinForLinesMagicFruits[8];
            }
            if ((Line[0] & 7) == (Line[1] & 7) && (Line[1] & 7) == (Line[2] & 7))
            {
                return LineWinsForGames.WinForLinesMagicFruits[(Line[0] & 7)];
            }
            return 0;
        }

        #endregion
    }
}
