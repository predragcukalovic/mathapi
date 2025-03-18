using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Linq;

namespace GameBonusEpicCrown
{
    public class CombinationBonusEpicCrown : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'BonusEpicCrown' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        public void MatrixToCombinationBonusEpicCrown(MatrixBonusEpicCrown matrix, int bet, bool gratisGame, int numberOfLines, int bonusNumberOfLines)
        {
            FillMatrixArray(matrix);

            CreateEmptyArray(PositionFor2);
            var bonusSymbols = matrix.GetNumberOfElement(9);
            GratisGame = bonusSymbols >= 3 && !gratisGame;
            NumberOfGratisGames = GratisGame ? MatrixBonusEpicCrown.GratisGamesBonusEpicCrown[bonusSymbols - 3] : 0;
            var nextPosition = 0;
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

            CreateLinesInformations(matrix, gratisGame ? bonusNumberOfLines : numberOfLines, bet, 1, 0, MatrixBonusEpicCrown.WinForWildBonusEpicCrown, GlobalData.GameLineExtra);
            LineInfo li9 = null;
            if (bonusSymbols >= 3)
            {
                li9 = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(9),
                    Id = EXTRA_LINE,
                    Win = 0,
                    WinningElement = 9
                };
            }
            if (li9 != null)
            {
                var li = LinesInformation.ToList();
                li.Add(li9);
                NumberOfWinningLines++;
                LinesInformation = li.ToArray();
            }
            PositionFor2 = matrix.FixExpand(LinesInformation, PositionFor2);
        }
    }
}
