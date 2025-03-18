using MathForGames.GameVegasHot;

namespace GameTripleFieldsOfLuck
{
    public class LineTripleFieldsOfLuck : LineVegasHot
    {
        #region Public methods

        /// <summary>
        /// Daje prvi element u liniji koji nije wild.
        /// </summary>
        /// <returns></returns>
        public int FirstNonWild()
        {
            for (var i = 0; i < 3; i++)
            {
                if (Line[i] != 0)
                {
                    return Line[i];
                }
            }
            return 0;
        }

        /// <summary>
        /// Računa dobitak linije za ulog 1.
        /// </summary>
        /// <returns></returns>
        public override int CalculateLineWin()
        {
            var elem = FirstNonWild();
            var index = 0;
            while (index < 3 && (Line[index] == 0 || Line[index] == elem))
            {
                index++;
            }
            return index == 3 ? MatrixTripleFieldsOfLuck.WinForTripleFieldsOfLuck[elem] : 0;
        }

        #endregion
    }
}
