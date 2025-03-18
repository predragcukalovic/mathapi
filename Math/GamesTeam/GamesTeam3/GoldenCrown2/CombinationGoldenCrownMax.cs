using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Linq;

namespace GoldenCrownMax
{
    public class CombinationGoldenCrownMax : Combination
    {
        public void MatrixToCombination(MatrixGoldenCrownMax matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 6];
            var index = 0;
            CreateEmptyArray(PositionFor2);
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if (j < 4 && Matrix[i, j] == 0)
                    {
                        PositionFor2[index++] = (byte)(j * 5 + i);
                    }
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;
            LineInfo li9 = null, li10 = null;
            var no9 = matrix.GetNumberOfElement(9);
            if (no9 >= 3)
            {
                li9 = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(9),
                    Id = EXTRA_LINE,
                    Win = MatrixGoldenCrownMax.WinForScatter1GoldenCrownMax[no9 - 1] * bet * numberOfLines,
                    WinningElement = 9
                };
            }
            if (matrix.GetNumberOfElement(10) == 3)
            {
                li10 = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(10),
                    Id = EXTRA_LINE,
                    Win = MatrixGoldenCrownMax.WIN_FOR_SCATTER2_GOLDEN_CROWN_MAX * bet * numberOfLines,
                    WinningElement = 10
                };
            }
            matrix.SetExpanding();

            CreateLinesInformationsTurbo(matrix, numberOfLines, bet, 0, MatrixGoldenCrownMax.WinForWildGoldenCrownMax, GlobalData.GameLineTurbo);
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
