using MathCombination.CombinationData;
using System.Collections.Generic;

namespace GameToxicHaze
{
    public class CombinationToxicHaze : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'ToxicHaze' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGamesLeft"></param>
        public void MatrixToCombinationToxicHaze(MatrixToxicHaze matrix, int numberOfLines, int bet, int gratisGamesLeft)
        {
            Matrix = new byte[5, 7];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 7; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }
            switch (gratisGamesLeft)
            {
                case 1:
                    matrix.SetElement(2, 3, 14);
                    break;
                case 2:
                case 4:
                    matrix.SetElement(1, 2, 13);
                    matrix.SetElement(1, 4, 13);
                    matrix.SetElement(3, 2, 13);
                    matrix.SetElement(3, 4, 13);
                    break;
                case 3:
                    matrix.SetElement(0, 1, 13);
                    matrix.SetElement(0, 5, 13);
                    matrix.SetElement(4, 1, 13);
                    matrix.SetElement(4, 5, 13);
                    break;
                case 5:
                    matrix.SetElement(2, 3, 13);
                    break;
                case 6:
                    matrix.SetElement(1, 2, 12);
                    matrix.SetElement(1, 4, 12);
                    matrix.SetElement(3, 2, 12);
                    matrix.SetElement(3, 4, 12);
                    break;
                case 7:
                    matrix.SetElement(0, 1, 12);
                    matrix.SetElement(0, 5, 12);
                    matrix.SetElement(4, 1, 12);
                    matrix.SetElement(4, 5, 12);
                    break;
            }

            GratisGame = gratisGamesLeft == 0 && matrix.IsGiveGratisGame();
            NumberOfGratisGames = GratisGame ? MatrixToxicHaze.GRATIS_GAMES : 0;

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.CalculateWinLine(i, gratisGamesLeft > 0, out int winElem, out byte[] winPos);
                if (win == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet,
                    WinningElement = (byte)winElem
                };
                lineInfo.WinningPosition = winPos;
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (GratisGame)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetScatterPositionsArray(),
                    Id = EXTRA_LINE,
                    Win = 0,
                    WinningElement = 11
                };
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }
    }
}
