using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using MathForGames.GameBurstingHot5;
using System.Linq;

namespace GameVeryHotRespin
{
    public class CombinationVeryHotRespin : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'BurstingHot' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="addInfo"></param>
        public void MatrixToCombination(MatrixBurstingHot5 matrix, int numberOfLines, int bet, byte addInfo)
        {
            for (var i = 0; i < 3; i++)
            {
                if ((addInfo & (1 << i)) != 0)
                {
                    matrix.SetElement(i + 1, 0, 0);
                    matrix.SetElement(i + 1, 1, 0);
                    matrix.SetElement(i + 1, 2, 0);
                }
            }

            CreateEmptyArray(PositionFor2);
            GratisGame = false;
            NumberOfGratisGames = 0;
            FillMatrixArray(matrix);
            LineInfo li9 = null, li10 = null;
            var no9 = matrix.GetNumberOfElement(9);
            if (no9 >= 3)
            {
                li9 = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(9),
                    Id = EXTRA_LINE,
                    Win = LineWinsForGames.WinForScatter1BurstingHot5[no9 - 1] * bet * numberOfLines,
                    WinningElement = 9
                };
            }
            if (matrix.GetNumberOfElement(10) == 3)
            {
                li10 = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(10),
                    Id = EXTRA_LINE,
                    Win = LineWinsForGames.WIN_FOR_SCATTER2_BURSTING_HOT5 * bet * numberOfLines,
                    WinningElement = 10
                };
            }
            AdditionalInformation = addInfo;
            var nextPosition = 0;
            for (var i = 1; i < 4; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (Matrix[i, j] == 0 && (addInfo & (byte)(1 << (i - 1))) == 0)
                    {
                        AdditionalInformation |= (byte)(1 << (i - 1));
                        GratisGame = true;
                        NumberOfGratisGames = 1;
                        if ((AdditionalInformation & (byte)(1 << (i - 1))) != 0)
                        {
                            PositionFor2[nextPosition++] = (byte)(j * 5 + i);
                        }
                    }
                }
            }
            for (var i = 1; i < 4; i++)
            {
                if ((AdditionalInformation & (byte)(1 << (i - 1))) != 0)
                {
                    matrix.SetElement(i, 0, 0);
                    matrix.SetElement(i, 1, 0);
                    matrix.SetElement(i, 2, 0);
                }
            }

            if (AdditionalInformation == addInfo)
            {
                AdditionalInformation = 0;
            }

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, LineWinsForGames.WinForWildBurstingHot5, GlobalData.GameLineExtra);
            var li = LinesInformation.ToList();
            if (li9 == null && li10 == null && TotalWin > 0 && matrix.GetNumberOfElement(0) > 0)
            {
                li.Insert(0, new LineInfo { Id = EXTRA_LINE, Win = 0, WinningElement = 11, WinningPosition = matrix.GetPositionsArray(11) });
                NumberOfWinningLines++;
            }
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
            LinesInformation = li.ToArray();
        }
    }
}
