using MathCombination.CombinationData;
using System.Linq;

namespace GameWildHeartBeat
{
    public class CombinationWildHeartBeat : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'WildHeartBeat' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="numberOfLines"></param>
        public void MatrixToCombinationWildHeartBeat(MatrixWildHeartBeat matrix, int bet)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            Matrix = new byte[5, 5];
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            LineInfo li9 = null, li10 = null;
            var no9 = matrix.GetScatterCount(9);
            if (no9 >= 3)
            {
                li9 = new LineInfo
                {
                    WinningPosition = matrix.GetScatterPositionsArray(9),
                    Id = EXTRA_LINE,
                    Win = MatrixWildHeartBeat.WinForScatter1WildHeartBeat[no9 - 1] * bet,
                    WinningElement = 9
                };
            }
            if (matrix.GetScatterCount(10) == 3)
            {
                li10 = new LineInfo
                {
                    WinningPosition = matrix.GetScatterPositionsArray(10),
                    Id = EXTRA_LINE,
                    Win = MatrixWildHeartBeat.WIN_FOR_SCATTER2_WILD_HEART_BEAT * bet,
                    WinningElement = 10
                };
            }

            var next = 0;
            PositionFor2 = new byte[5];
            CreateEmptyArray(PositionFor2);
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if (j > 0 && j < 4 && Matrix[i, j] == 0)
                    {
                        PositionFor2[next++] = (byte)(5 * j + i);
                    }
                }
            }

            for (var i = 0; i < 5; i++)
            {
                if (PositionFor2[i] != 255)
                {
                    var reel = PositionFor2[i] % 5;
                    matrix.SetElement(reel, 1, 0);
                    matrix.SetElement(reel, 2, 0);
                    matrix.SetElement(reel, 3, 0);
                }
            }

            CreateLinesInformations(matrix, 5, bet, 1, 0, MatrixWildHeartBeat.WinForWildWildHeartBeat, MatrixWildHeartBeat.GameLineWildHeartBeat);
            var li = LinesInformation.ToList();
            if (li9 != null)
            {
                TotalWin += li9.Win;
                li.Insert(0, li9);
                NumberOfWinningLines++;
            }
            if (li10 != null)
            {
                TotalWin += li10.Win;
                li.Insert(0, li10);
                NumberOfWinningLines++;
            }
            PositionFor2 = matrix.FixExpand(LinesInformation, PositionFor2);
            LinesInformation = li.ToArray();
        }
    }
}
