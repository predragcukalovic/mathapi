using GameEpicClover40;
using MathForGames.BasicGameData;

namespace GoldenCrownMax
{
    public class MatrixGoldenCrownMax : MatrixEpicClover40
    {
        public static readonly int[,] WinForLinesGoldenCrownMax =
        {
            { 0, 0, 0, 0, 0 },
            { 0, 10, 50, 250, 5000 },
            { 0, 0, 40, 120, 700 },
            { 0, 0, 40, 120, 700 },
            { 0, 0, 20, 40, 200 },
            { 0, 0, 10, 30, 150 },
            { 0, 0, 10, 30, 150 },
            { 0, 0, 10, 30, 150 },
            { 0, 0, 10, 30, 150 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
        };

        public static readonly int[] WinForWildGoldenCrownMax = { 0, 0, 0, 0, 0 };
        public static readonly int[] WinForScatter1GoldenCrownMax = { 0, 0, 5, 20, 100 };
        public const int WIN_FOR_SCATTER2_GOLDEN_CROWN_MAX = 20;

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineTurbo)
                .CalculateLineWin(WinForLinesGoldenCrownMax, WinForWildGoldenCrownMax, 0, 1);
        }
    }
}
