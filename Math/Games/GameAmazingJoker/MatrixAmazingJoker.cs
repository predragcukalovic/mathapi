using MathForGames.BasicGameData;
using MathForGames.GameTurboHot40;

namespace GameAmazingJoker
{
    public class MatrixAmazingJoker : MatrixTurboHot40
    {
        public static readonly int[,] WinForLinesAmazingJoker =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 40, 160, 800},
            {0, 0, 40, 160, 800},
            {0, 0, 40, 120, 480},
            {0, 0, 40, 120, 480},
            {0, 0, 16, 80, 320},
            {0, 0, 16, 80, 320},
            {0, 0, 8, 40, 160},
            {0, 0, 8, 40, 160},
            {0, 0, 8, 40, 160},
            {0, 0, 8, 40, 160},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildAmazingJoker = { 0, 0, 80, 400, 2000 };
        public static readonly int[] NumberOfGratisGames = { 10, 20, 30 };

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineTurbo).CalculateLineWin(WinForLinesAmazingJoker, WinForWildAmazingJoker, 0, 1);
        }
    }
}
