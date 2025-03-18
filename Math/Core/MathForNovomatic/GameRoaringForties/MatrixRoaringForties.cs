using GameEpicFire40;

namespace MathForNovomatic.GameRoaringForties
{
    public class MatrixRoaringForties : MatrixEpicFire40
    {
        public static readonly int[,] GameLines =
        {
            { 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2 },
            { 3, 3, 3, 3, 3 },

            { 0, 1, 2, 1, 0 },
            { 1, 2, 3, 2, 1 },
            { 2, 1, 0, 1, 2 },
            { 3, 2, 1, 2, 3 },

            { 0, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 0 },
            { 2, 2, 2, 2, 3 },
            { 3, 3, 3, 3, 2 },

            { 0, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 2 },
            { 2, 2, 2, 2, 1 },
            { 3, 2, 2, 2, 2 },

            { 1, 0, 0, 0, 0 },
            { 1, 2, 2, 2, 2 },
            { 2, 1, 1, 1, 1 },
            { 2, 3, 3, 3, 3 },

            { 0, 0, 0, 1, 2 },
            { 1, 1, 1, 2, 3 },
            { 2, 2, 2, 1, 0 },
            { 3, 3, 3, 2, 1 },

            { 0, 1, 2, 2, 2 },
            { 1, 2, 3, 3, 3 },
            { 2, 1, 0, 0, 0 },
            { 3, 2, 1, 1, 1 },

            { 0, 0, 1, 0, 0 },
            { 1, 1, 0, 1, 1 },
            { 2, 2, 3, 2, 2 },
            { 3, 3, 2, 3, 3 },

            { 0, 1, 1, 1, 0 },
            { 1, 1, 2, 1, 1 },
            { 2, 2, 1, 2, 2 },
            { 3, 2, 2, 2, 3 },

            { 1, 0, 0, 0, 1 },
            { 1, 2, 2, 2, 1 },
            { 2, 1, 1, 1, 2 },
            { 2, 3, 3, 3, 2 },
        };

        public static readonly int[,] WinForLinesRoaringForties =
        {
            {0, 0, 0, 0, 0},
            {0, 4, 60, 200, 1000},
            {0, 0, 40, 100, 300},
            {0, 0, 20, 80, 200},
            {0, 0, 20, 80, 200},
            {0, 0, 8, 40, 100},
            {0, 0, 8, 40, 100},
            {0, 0, 8, 40, 100},
            {0, 0, 8, 40, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };

        public static readonly int[] WinForWildsRoaringForties = { 0, 0, 0, 0, 0 };
        public static readonly int[] WinForScatterRoaringForties = { 0, 0, 2, 20, 500 };

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GameLines).CalculateLineWin(WinForLinesRoaringForties, WinForWildsRoaringForties, 0, 1);
        }
    }
}
