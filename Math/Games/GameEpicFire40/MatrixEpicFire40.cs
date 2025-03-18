using MathForGames.BasicGameData;
using MathForGames.GameTurboHot40;

namespace GameEpicFire40
{
    public class MatrixEpicFire40 : MatrixTurboHot40
    {
        public static readonly int[,] WinForLinesEpicHot40 =
        {
            {0, 0, 0, 0, 0},
            {0, 4, 60, 200, 1000},
            {0, 0, 0, 0, 0},
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
        public static readonly int[] WinForWildsEpicHot40 = { 0, 0, 0, 0, 0 };
        public static readonly int[] WinForScatterEpicHot40 = { 0, 0, 2, 20, 500 };

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineTurbo).CalculateLineWin(WinForLinesEpicHot40, WinForWildsEpicHot40, 0, 1);
        }
    }
}
