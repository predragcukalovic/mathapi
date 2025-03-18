using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;
using System;

namespace GameWildSunburst
{
    public class LineWildSunburst : Line
    {
        /// <summary>
        /// Daje prvi element u liniji koji nije wild.
        /// </summary>
        /// <param name="wild"></param>
        /// <returns></returns>
        private int FirstNonWild()
        {
            for (var i = 0; i < 5; i++)
            {
                var elem = GetElement(i);
                if (elem != 0)
                {
                    return elem;
                }
            }
            return -1;
        }

        private int GetWildPositions()
        {
            var index = 0;
            while (index < 5 && GetElement(index) == 0)
            {
                index++;
            }
            return index;
        }

        private int GetElementPositions(int elem)
        {
            var index = 0;
            while (index < 5 && (GetElement(index) == elem || GetElement(index) == 0))
            {
                index++;
            }
            return index;
        }

        public int CalculateLineWin(int[,] wins, int[] wildWins, bool gratisGame, int lineNumber, out int winningElement, out byte[] winningPosition)
        {
            var mult = new[] { 1, 1, 1, 1, 1 };
            if (gratisGame)
            {
                SetElement(2, 0);
                var tmpMult = 1;
                for (var i = 0; i < 5; i++)
                {
                    var elem = GetElement(i);
                    mult[i] = tmpMult * (elem > 10 ? (elem - 10) : 1);
                    tmpMult = mult[i];
                }
                for (var i = 0; i < 5; i++)
                {
                    if (i != 2)
                    {
                        var elem = GetElement(i);
                        if (elem > 10)
                        {
                            SetElement(i, 0);
                        }
                    }
                }
            }
            winningElement = FirstNonWild();
            if (winningElement == -1)
            {
                winningElement = 0;
                winningPosition = GetLinesPositions(GlobalData.GameLineExtra, lineNumber, 0, 0);
                return wildWins[4] * mult[4];
            }
            var posElem = GetElementPositions(winningElement);
            var posWild = GetWildPositions();
            var win = wins[winningElement, Math.Max(0, posElem - 1)] * mult[Math.Max(0, posElem - 1)];
            var winWild = wildWins[Math.Max(0, posWild - 1)] * mult[Math.Max(0, posWild - 1)];
            if (winWild > win)
            {
                winningElement = 0;
                winningPosition = GetLinesPositions(GlobalData.GameLineExtra, lineNumber, 0, 0);
                return winWild;
            }
            winningPosition = GetLinesPositions(GlobalData.GameLineExtra, lineNumber, 0, winningElement);
            return win;
        }
    }
}
