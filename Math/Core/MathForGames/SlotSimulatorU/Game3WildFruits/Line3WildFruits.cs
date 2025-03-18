using MathForGames.BasicGameData;
using MathForGames.GameVegasHot;

namespace MathForGames.Game3WildFruits
{
    public class Line3WildFruits : LineVegasHot
    {
        /// <summary>
        /// Računa dobitak linije za ulog 1.
        /// </summary>
        /// <returns></returns>
        public override int CalculateLineWin()
        {
            var winElem = Line[0];
            if (Line[1] != 0)
            {
                if (winElem == 0)
                {
                    winElem = Line[1];
                }
                else if (winElem != Line[1])
                {
                    return 0;
                }
            }
            if (Line[2] != 0)
            {
                if (winElem == 0)
                {
                    winElem = Line[2];
                }
                else if (winElem != Line[2])
                {
                    return 0;
                }
            }
            return LineWinsForGames.WinForLines3WildFruits[winElem];
        }
    }
}
