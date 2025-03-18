using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Collections.Generic;

namespace Game40FireCash
{
    public class Combination40FireCash : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru '40FireCash' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="numberOfLines"></param>
        public void MatrixToCombination(Matrix40FireCash matrix, int bet, int numberOfLines)
        {
            Matrix = new byte[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.CalculateWinLine(i);
                if (win == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, 1, Matrix40FireCash.WinForWilds40FireCash, win, GlobalData.GameLineTurbo)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, GlobalData.GameLineTurbo).GetLinesPositions(GlobalData.GameLineTurbo, i, 1, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            var addExtraLine = matrix.GetNoLineWin(0, Matrix40FireCash.WinForScatter40FireCash);
            if (addExtraLine > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(0),
                    Id = EXTRA_LINE,
                    Win = addExtraLine * bet * numberOfLines,
                    WinningElement = 0
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Transformiše matricu za igru 'Redstone40FruitFrenzy' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="numberOfLines"></param>
        public void MatrixToCombinationFruitFrenzy(Matrix40FireCash matrix, int bet)
        {
            Matrix = new byte[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= 40; i++)
            {
                var win = matrix.CalculateWinLine(i);
                if (win == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet / 10,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, 1, Matrix40FireCash.WinForWilds40FireCash, win, GlobalData.GameLineTurbo)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, GlobalData.GameLineTurbo).GetLinesPositions(GlobalData.GameLineTurbo, i, 1, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            var addExtraLine = matrix.GetNoLineWin(0, Matrix40FireCash.WinForScatter40FireCash);
            if (addExtraLine > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(0),
                    Id = EXTRA_LINE,
                    Win = addExtraLine * bet * 4,
                    WinningElement = 0
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }
    }
}
