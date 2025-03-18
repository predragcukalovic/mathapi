using MathForGames.BasicGameData;
using MathForGames.GameTurboHot40;

namespace GameCrystalHot40Max
{
    public class MatrixCrystalHot40Max : MatrixTurboHot40
    {
        public static readonly int[,] WinForLinesCrystalHot40Max =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 20, 80, 400},
            {0, 0, 0, 0, 0},
            {0, 0, 20, 40, 200},
            {0, 0, 20, 40, 200},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 40, 400, 1000},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildsCrystalHot40Max = { 0, 0, 0, 0, 0 };
        public static readonly int[] WinForScatterCrystalHot40Max = { 0, 0, 5, 20, 500 };

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineTurbo).CalculateLineWin(WinForLinesCrystalHot40Max, WinForWildsCrystalHot40Max, 0, 1);
        }

        private bool IsReelHaveWild(int reel)
        {
            for (var i = 0; i < 4; i++)
            {
                if (GetElement(reel, i) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetExpanding()
        {
            if (!IsReelHaveWild(1) && !IsReelHaveWild(2) && !IsReelHaveWild(3))
            {
                return;
            }
            var arr = new int[5, 6];
            for (var i = 0; i < 5; i++)
            {
                var wld = IsReelHaveWild(i);
                for (var j = 0; j < 6; j++)
                {
                    arr[i, j] = wld ? 0 : GetElement(i, j + 5);
                }
            }
            FromMatrixArray(arr);
        }
    }
}
