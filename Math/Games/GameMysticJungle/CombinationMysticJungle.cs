using MathCombination.CombinationData;
using System.Collections.Generic;

namespace GameMysticJungle
{
    public class CombinationMysticJungle : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'MysticJungle' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombinationMysticJungle(MatrixMysticJungle matrix, int numberOfLines, int bet, int cheatMystery = -1)
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

            matrix.FixMatrix();

            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var line = matrix.GetLine(i);
                var win = line.CalculateLineWin();
                if (win == 0)
                {
                    continue;
                }
                var winElement = (byte)line.GetWinningElement();
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet,
                    WinningElement = winElement,
                    WinningPosition = line.GetLinesPositionsMysticJungle(i, winElement)
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();

            var mystery = matrix.GetMysterySymbol(TotalWin > 0);
            if (cheatMystery >= 0 && cheatMystery < 9)
            {
                mystery = cheatMystery;
            }
            WinFor2 = mystery;
            Matrix = MatrixMysticJungle.TransformMatrix(Matrix, mystery);
        }
    }
}
