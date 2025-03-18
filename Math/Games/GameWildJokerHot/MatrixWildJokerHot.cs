using MathBaseProject.BaseMathData;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System;
using System.Collections.Generic;

namespace GameWildJokerHot
{
    public class MatrixWildJokerHot : Matrix
    {
        #region Public properties WildJokerHot

        public static readonly int[,] WinForLinesWildJokerHot =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 10, 50, 250, 5000},
            {0, 0, 40, 100, 600},
            {0, 0, 40, 100, 600},
            {0, 0, 20, 40, 160},
            {0, 0, 10, 30, 120},
            {0, 0, 10, 30, 120},
            {0, 0, 10, 30, 100},
            {0, 0, 10, 30, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildWildJokerHot = { 0, 0, 0, 0, 0 };
        public static readonly int[] WinForScatter1WildJokerHot = { 0, 0, 5, 20, 100 };
        public const int WIN_FOR_SCATTER2_WILD_JOKER_HOT = 20;

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            var m = 1;
            var l = GetLine(lineNumber, GlobalData.GameLineExtra);
            if (l.GetElement(2) == 1)
            {
                l.SetElement(2, 0);
                m = 2;
            }
            return l.CalculateLineWin(WinForLinesWildJokerHot, WinForWildWildJokerHot, 0, m);
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
                    if (el < 15 && GetElement(el % 5, el / 5) <= 1)
                    {
                        shouldBeFixed[el % 5 - 1] = false;
                    }
                }
            }
            for (var i = 0; i < 5; i++)
            {
                if (position2[i] < 15)
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

        #endregion
    }
}
