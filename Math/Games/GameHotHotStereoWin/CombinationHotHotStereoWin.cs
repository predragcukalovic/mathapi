using MathCombination.CombinationData;
using System.Collections.Generic;

namespace GameHotHotStereoWin
{
    public class CombinationHotHotStereoWin : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'HotHotStereoWin' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="numberOfLines"></param>
        public void MatrixToCombinationHotHotStereoWin(MatrixHotHotStereoWin matrix, int bet)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= 5; i++)
            {
                var leftWin = matrix.CalculateLeftWinOfLine(i);
                var leftElement = (byte)matrix.GetLine(i, MatrixHotHotStereoWin.GameLineHotHotStereoWin).GetElement(0);
                if (leftWin != 0)
                {
                    var lineInfo = new LineInfo
                    {
                        Id = (byte)(i - 1),
                        Win = leftWin * bet,
                        WinningElement = leftElement
                    };
                    lineInfo.WinningPosition = matrix.GetLine(i, MatrixHotHotStereoWin.GameLineHotHotStereoWin).GetLinesPositions(MatrixHotHotStereoWin.GameLineHotHotStereoWin, i, -1, lineInfo.WinningElement);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }
                var rightWin = matrix.CalculateRightWinOfLine(i);
                var rightElement = (byte)matrix.GetLine(i, MatrixHotHotStereoWin.GameLineHotHotStereoWin).GetElement(4);
                if (rightWin != 0)
                {
                    var lineInfo = new LineInfo
                    {
                        Id = (byte)(i - 1),
                        Win = rightWin * bet,
                        WinningElement = rightElement
                    };
                    if (!(leftWin == rightWin && leftElement == rightElement))
                    {
                        lineInfo.WinningPosition =
                            matrix.GetLine(i, MatrixHotHotStereoWin.GameLineHotHotStereoWin)
                                .GetLinesPositionsRight(MatrixHotHotStereoWin.GameLineHotHotStereoWin, i, lineInfo.WinningElement, -1);
                        TotalWin += lineInfo.Win;
                        linesInfo.Add(lineInfo);
                    }
                }
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }
    }
}
