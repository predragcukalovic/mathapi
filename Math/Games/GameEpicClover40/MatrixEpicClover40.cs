using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using MathForGames.GameTurboHot40;
using System;
using System.Collections.Generic;

namespace GameEpicClover40
{
    public class MatrixEpicClover40 : MatrixTurboHot40
    {
        #region Public properties

        public static readonly int[,] WinForLinesEpicFire40 =
        {
            {0, 0, 0, 0, 0},
            {0, 10, 50, 200, 3000},
            {0, 0, 40, 100, 500},
            {0, 0, 40, 100, 500},
            {0, 0, 20, 50, 200},
            {0, 0, 10, 30, 100},
            {0, 0, 10, 30, 100},
            {0, 0, 10, 30, 100},
            {0, 0, 10, 30, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildEpicFire40 = { 0, 0, 0, 0, 0 };
        public static readonly int[] WinForScatter1EpicFire40 = { 0, 0, 3, 20, 100 };
        public const int WIN_FOR_SCATTER2_EPIC_FIRE40 = 20;

        #endregion

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineTurbo).CalculateLineWin(WinForLinesEpicFire40, WinForWildEpicFire40, 0, 1);
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

        /// <summary>
        /// Ako su samo dobici sačinjeni od dva elementa i ne mora da se širi četvrti ril; nakon širenja vajldova se poziva.
        /// </summary>
        /// <param name="lineInfo"></param>
        /// <param name="position2"></param>
        /// <returns></returns>
        public byte[] FixExpand(IEnumerable<LineInfo> lineInfo, byte[] position2)
        {
            var shouldBeFixed = new[] { true, true, true };
            foreach (var info in lineInfo)
            {
                for (var i = 0; i < 5; i++)
                {
                    var el = info.WinningPosition[i];
                    if (el < 20 && GetElement(el % 5, el / 5) == 0)
                    {
                        shouldBeFixed[el % 5 - 1] = false;
                    }
                }
            }
            for (var i = 0; i < 5; i++)
            {
                if (position2[i] < 20)
                {
                    if (shouldBeFixed[position2[i] % 5 - 1])
                    {
                        position2[i] = 255;
                    }
                }
            }
            Array.Sort(position2);
            return position2;
        }
    }
}
