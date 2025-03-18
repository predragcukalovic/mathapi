using MathBaseProject.BaseMathData;
using MathForUnicornGames.BasicUnicornData;
using System;

namespace MathForUnicornGames.GameIslandRespins
{
    public class LineIslandRespins2 : Line
    {
        public int CalculateLineWin(int[,] wins, int[] winTableSeven, int[] winTableGoldenSeven)
        {
            return Math.Max(CalculateLineWin(wins, null, -1, 1), CalculateSevenWin(winTableSeven, winTableGoldenSeven));
        }

        public int CalculateSevenWin(int[] winTableSeven, int[] winTableGoldenSeven)
        {
            var count = 0;
            for (var i = 0; i < 5; i++)
            {
                if (GetElement(i) > 1)
                {
                    break;
                }
                count++;
            }
            if (count == 0)
            {
                return 0;
            }
            return Math.Max(winTableSeven[count - 1], CalculateGoldSevenWin(winTableGoldenSeven));
        }

        private int CalculateGoldSevenWin(int[] winTable)
        {
            var count = 0;
            for (var i = 0; i < 5; i++)
            {
                if (GetElement(i) != 0)
                {
                    break;
                }
                count++;
            }
            if (count == 0)
            {
                return 0;
            }
            return winTable[count - 1];
        }

        public int GetWinningElement(int win, int[] winGoldenSeven)
        {
            if (GetElement(0) > 1)
            {
                return GetElement(0);
            }
            return win == CalculateGoldSevenWin(winGoldenSeven) ? 0 : 1;
        }

        public byte[] GetPositions(int line, int element)
        {
            return GetLinesPositions(UnicornGlobalData.GameLineShifted, line, element == 1 ? 0 : -1, element);
        }
    }
}
