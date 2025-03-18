using MathForGames.BasicGameData;
using MathForGames.GameHotStars;
using RNGUtils.RandomData;

namespace GameCrownsAndStars
{
    public class MatrixCrownsAndStars : MatrixHotStars
    {
        #region Reels
        private static readonly int[][][] _Reels =
        {
                new[]
                {
                    new[]{ 5,5,5,5,4,4,4,9,6,6,6,2,2,8,8,8,8,3,7,7,7,1,6,6,6,7,7,7,5,5,5,5,4,4,4,9,6,6,6,8,8,8,8,3,7,7,7,8,8,8,8,1,5,5,5,5,6,6,6,7,7,7 },
                    new[]{ 5,5,5,5,2,7,7,7,7,7,8,8,8,8,8,2,2,1,3,3,3,6,6,6,6,6,1,4,4,0,4,4,4,4 },
                    new[]{ 2,2,3,3,3,1,5,5,5,5,2,2,1,4,4,4,4,2,7,7,7,3,3,8,8,8,1,6,6,0,6,6,6,6 },
                    new[]{ 8,8,8,8,3,3,1,7,7,7,7,7,4,4,4,4,6,6,6,6,2,2,2,3,3,5,5,0,5,5,5,5 },
                    new[]{ 5,5,5,5,3,6,6,6,6,1,8,8,8,8,8,2,2,4,4,4,4,1,3,3,7,7,7,7,7,2 }
                },
                new[]
                {
                    new[]{ 5,5,5,8,8,8,4,4,4,6,6,6,6,2,8,8,8,3,3,7,7,7,7,8,8,8,1,5,5,5,7,7,7,7,5,5,5,4,4,4,6,6,6,6,2,7,7,7,7,8,8,8,1,5,5,5,6,6,6,6 },
                    new[]{ 4,4,4,4,2,5,5,5,5,8,8,8,8,8,2,2,1,3,3,3,6,6,6,6,6,1,7,7,0,7,7,7,7,7 },
                    new[]{ 2,2,3,3,3,1,6,6,6,6,2,2,1,4,4,4,4,2,7,7,7,3,3,8,8,8,1,5,5,0,5,5,5,5 },
                    new[]{ 5,5,5,5,3,3,3,1,7,7,7,7,7,3,2,4,4,4,4,6,6,6,6,2,2,8,8,0,8,8,8,8 },
                    new[]{ 5,5,5,5,6,6,6,6,1,8,8,8,8,8,9,2,2,4,4,4,4,1,3,3,3,9,7,7,7,7,7,2 }
                },
                new[]
                {
                    new[]{ 5,5,5,4,4,4,6,6,6,2,8,8,8,3,7,7,7,8,8,8,1,5,5,5,6,6,6,7,7,7 },
                    new[]{ 5,5,5,5,2,2,2,8,8,8,8,8,1,6,6,6,6,6,4,4,4,7,7,7,7,7,1,4,4,4,3,3,3 },
                    new[]{ 5,5,5,5,2,2,2,3,3,1,6,6,6,6,6,3,3,3,4,4,4,4,1,7,7,7,8,8,8,1,2,2,2 },
                    new[]{ 5,5,5,5,5,8,8,8,8,3,3,3,3,7,7,7,7,7,4,4,4,4,1,6,6,6,6,2,2,2,2 },
                    new[]{ 5,5,5,5,3,3,3,6,6,6,6,1,8,8,8,8,8,2,2,2,4,4,4,4,1,7,7,7,7,7 }
                }
        };
        #endregion

        #region Private properties
        private static readonly double _FirstReelProbs = 0.65;
        private static readonly double[] _ScatterProbs = { 0.78143, 0.98143, 0.99943, 0.99998, 1 };
        #endregion

        #region Public properties
        public static readonly int[] WinForWildCrownsAndStars = { 0, 0, 0, 0, 0 };
        public static readonly int[] WinForScatterCrownsAndStars = { 0, 0, 100, 1000, 10000 };
        public static readonly int[,] WinForLinesCrownsAndStars =
        {
            {0, 0, 0, 0, 0},
            {0, 10, 50, 200, 5000},
            {0, 0, 40, 120, 700},
            {0, 0, 40, 120, 700},
            {0, 0, 20, 40, 200},
            {0, 0, 10, 30, 150},
            {0, 0, 10, 30, 150},
            {0, 0, 10, 30, 150},
            {0, 0, 10, 30, 150},
            {0, 0, 0, 0, 0},
        };

        #endregion

        #region Public methods
        public static (int[,], int) GetMatrixArray()
        {
            var reelSetUsed = SoftwareRng.Next() < _FirstReelProbs ? 0 : 2;
            if (reelSetUsed == 0 && SoftwareRng.Next() >= _FirstReelProbs)
                reelSetUsed = 1;

            var mat = new int[5, 3];

            for (var i = 0; i < 5; i++)
            {
                var l = _Reels[reelSetUsed][i].Length;
                var p = SoftwareRng.Next(l);
                for (var j = 0; j < 3; j++)
                {
                    mat[i, j] = _Reels[reelSetUsed][i][(p + j) % l];
                }
            }

            var megapotsToAdd = 0;
            if (reelSetUsed == 2)
            {
                var megapotCountRnd = SoftwareRng.Next();
                for (int i = 0; i < 4; i++)
                {
                    if (megapotCountRnd >= _ScatterProbs[i] && megapotCountRnd < _ScatterProbs[i + 1])
                        megapotsToAdd = i + 1;
                }
            }

            return (mat, megapotsToAdd);
        }

        public new LineHotStars GetLine(int lineNumber, int[,] lines)
        {
            if (lineNumber < 1 || lineNumber > 10)
            {
                return null;
            }

            var line = new LineHotStars();
            for (var i = 0; i < 5; i++)
            {
                line.SetElement(i, GetElement(i, GlobalData.GameLineExtra[lineNumber - 1, i]));
            }
            return line;
        }

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(WinForLinesCrownsAndStars, WinForWildCrownsAndStars, 0, 1);
        }

        #endregion
    }
}