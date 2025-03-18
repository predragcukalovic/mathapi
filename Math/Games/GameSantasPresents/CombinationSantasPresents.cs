using MathCombination.CombinationData;
using System.Collections.Generic;

namespace GameSantasPresents
{
    public class CombinationSantasPresents : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'SantasPresents' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        public void MatrixToCombinationSantasPresents(MatrixSantasPresents matrix, int numberOfLines, int bet, bool gratisGame)
        {
            if (gratisGame)
            {
                matrix.SetWilds();
            }
            FillMatrixArray(matrix);

            CreateEmptyArray(PositionFor2);
            var numScat = matrix.GetNumberOfElement(9);
            GratisGame = numScat >= 3 && !gratisGame;
            NumberOfGratisGames = GratisGame ? MatrixSantasPresents.NumberOfGratisGames[numScat] : 0;
            var nextPosition = 0;
            for (var i = 1; i < 4; i++)
            {
                var elem = -1;
                for (var j = 0; j < 3; j++)
                {
                    if (matrix.GetElement(i, j) >= 10 || matrix.GetElement(i, j) == 0)
                    {
                        PositionFor2[nextPosition++] = (byte)(j * 5 + i);
                        elem = matrix.GetElement(i, j);
                    }
                }
                if (elem >= 10 || elem == 0)
                {
                    matrix.SetElement(i, 0, elem);
                    matrix.SetElement(i, 1, elem);
                    matrix.SetElement(i, 2, elem);
                }
            }

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.CalculateWinLine(i, gratisGame);
                if (win == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i)
                };
                lineInfo.WinningPosition = matrix.GetLinesPositions(i, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (GratisGame)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(9),
                    Id = EXTRA_LINE,
                    Win = 0,
                    WinningElement = 9
                };
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
            PositionFor2 = matrix.FixExpand(LinesInformation, PositionFor2);
        }
    }
}
