using MathForGames.BasicGameData;
using MathForGames.GameTurboHot40;

namespace GameHeatingFruits
{
    public class MatrixHeatingFruits : MatrixTurboHot40
    {
        #region Public properties

        public static readonly int[,] WinForLinesHeatingFruits =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 100, 300, 1000},
            {0, 0, 50, 150, 500},
            {0, 0, 40, 100, 300},
            {0, 0, 30, 60, 150},
            {0, 0, 30, 60, 150},
            {0, 0, 20, 40, 120},
            {0, 0, 20, 40, 120},
            {0, 0, 10, 25, 100},
            {0, 0, 10, 25, 100},
            {0, 0, 10, 25, 100},
            {0, 0, 10, 25, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildHeatingFruits = { 0, 0, 100, 300, 1000 };

        #endregion

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineTurbo).CalculateLineWin(WinForLinesHeatingFruits, WinForWildHeatingFruits, 0, 1);
        }

        public void SetReelWild(int reel)
        {
            var arr = new int[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    arr[i, j] = i == reel ? 0 : GetElement(i, j + 5);
                }
            }
            FromMatrixArray(arr);
        }

        public bool IsReelWildNext(int reel)
        {
            var elem = GetElement(reel, 0);
            if (elem == 0)
            {
                return false;
            }
            if (GetElement(reel, 1) == elem && GetElement(reel, 2) == elem && GetElement(reel, 3) == elem)
            {
                return true;
            }
            return false;
        }
    }
}
