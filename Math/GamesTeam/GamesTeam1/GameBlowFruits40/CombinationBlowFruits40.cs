using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Collections.Generic;

namespace GameBlowFruits40
{
    public class CombinationBlowFruits40 : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'BlowFruits40' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixBlowFruits40 matrix, int numberOfLines, int bet, bool shouldAddWilds = true)
        {
            Matrix = new byte[5, 6];
            if (shouldAddWilds)
            {
                matrix.AddWilds();
            }

            CreateEmptyArray(PositionFor2);
            var index = 0;
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

            matrix.SetExpanding();

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformationsBlowFruits40(matrix, numberOfLines, bet, 0, MatrixBlowFruits40.WinForWildsBlowFruits40, GlobalData.GameLineTurbo);
        }

        /// <summary>
        /// Kreira niz LinesInformation.
        /// </summary>
        /// <param name="matrix">Matrica</param>
        /// <param name="numberOfLines">Broj linija</param>
        /// <param name="bet">Ulog</param>
        /// <param name="wild">Wild element</param>
        /// <param name="winForWild">Dobitak za wild</param>
        /// <param name="gameLines">Linije na koje se igra</param>
        /// <param name="addExtraLine">Dobitak elemenata van linije</param>
        /// <param name="extraSymbol">Simbol koji daje dobitak van linije</param>
        private void CreateLinesInformationsBlowFruits40(MatrixBlowFruits40 matrix, int numberOfLines, int bet,
            int wild, int[] winForWild, int[,] gameLines)
        {
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
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, wild, winForWild, win, gameLines)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, gameLines).GetLinesPositions(gameLines, i, wild, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

    }
}
