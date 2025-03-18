using MathForGames.BasicGameData;
using MathForGames.GameTurboHot40;

namespace GameWildLuckyClover
{
    public class MatrixWildLuckyClover : MatrixTurboHot40
    {
        #region Public properties

        public static readonly int[,] WinForLinesWildLuckyClover =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 20, 80, 400},
            {0, 0, 20, 40, 200},
            {0, 0, 20, 40, 200},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildWildLuckyClover = { 0, 0, 40, 400, 1000 };

        public static readonly int[] FreeSpinsCount = { 7, 10, 15 };

        #endregion

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineTurbo).CalculateLineWin(WinForLinesWildLuckyClover, WinForWildWildLuckyClover, 0, 1);
        }

        public void SetWild(int wild)
        {
            var arr = new int[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    var elem = GetElement(i, j + 5);
                    arr[i, j] = elem == wild ? 0 : elem;
                }
            }
            FromMatrixArray(arr);
        }
    }
}
