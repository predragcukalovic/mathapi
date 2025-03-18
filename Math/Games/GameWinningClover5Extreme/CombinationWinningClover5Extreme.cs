using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Linq;

namespace GameWinningClover5Extreme
{
    public class CombinationWinningClover5Extreme : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'WinningClover5Extreme' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombinationWinningClover5Extreme(MatrixWinningClover5Extreme matrix, int numberOfLines, int bet)
        {
            FillMatrixArray(matrix);

            CreateEmptyArray(PositionFor2);
            GratisGame = false;
            NumberOfGratisGames = 0;
            var nextPosition = 0;
            LineInfo li9 = null, li10 = null;
            var no9 = matrix.GetNumberOfElement(9);
            if (no9 >= 3)
            {
                li9 = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(9),
                    Id = EXTRA_LINE,
                    Win = MatrixWinningClover5Extreme.WinForScatter1WinningClover5Extreme[no9 - 1] * bet * numberOfLines,
                    WinningElement = 9
                };
            }
            if (matrix.GetNumberOfElement(10) == 3)
            {
                li10 = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(10),
                    Id = EXTRA_LINE,
                    Win = MatrixWinningClover5Extreme.WIN_FOR_SCATTER2_WINNING_CLOVER5_EXTREME * bet * numberOfLines,
                    WinningElement = 10
                };
            }
            for (var i = 1; i < 4; i++)
            {
                var haveWild = false;
                for (var j = 0; j < 3; j++)
                {
                    if (matrix.GetElement(i, j) == 0)
                    {
                        PositionFor2[nextPosition++] = (byte)(j * 5 + i);
                        haveWild = true;
                    }
                }
                if (haveWild)
                {
                    matrix.SetElement(i, 0, 0);
                    matrix.SetElement(i, 1, 0);
                    matrix.SetElement(i, 2, 0);
                }
            }

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, MatrixWinningClover5Extreme.WinForWildWinningClover5Extreme, GlobalData.GameLineExtra);
            var li = LinesInformation.ToList();
            if (li9 != null)
            {
                TotalWin += li9.Win;
                li.Add(li9);
                NumberOfWinningLines++;
            }
            if (li10 != null)
            {
                TotalWin += li10.Win;
                li.Add(li10);
                NumberOfWinningLines++;
            }
            PositionFor2 = FixExpandBursting(LinesInformation, PositionFor2, matrix);
            LinesInformation = li.ToArray();
        }

        public static Combination GetNonWinningCombination(int bet, int numberOfLines)
        {
            var matrixArray = new[,] { { 8, 8, 8 }, { 7, 7, 7 }, { 6, 6, 6 }, { 5, 5, 5 }, { 4, 4, 4 } };
            var matrix = new MatrixWinningClover5Extreme();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationWinningClover5Extreme();
            combination.MatrixToCombinationWinningClover5Extreme(matrix, numberOfLines, bet);
            return combination;
        }
    }
}
