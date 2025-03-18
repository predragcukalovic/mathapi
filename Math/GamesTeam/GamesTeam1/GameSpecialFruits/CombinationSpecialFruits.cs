using MathBaseProject.BaseMathData;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;
using System.Collections.Generic;

namespace GameSpecialFruits
{
    public class CombinationSpecialFruits : Combination
    {
        public void MatrixToCombination(MatrixSpecialFruits matrix, int bet, bool isNonWinning = false)
        {
            var numberOfLines = isNonWinning ? 40 : ChoosePlayLine();
            AdditionalInformation = (byte)numberOfLines;

            FillMatrixArray(matrix);

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, MatrixSpecialFruits.WinForWildsSpecialFruits,
                GlobalData.GameLineExtra, matrix.GetNoLineWin(2, MatrixSpecialFruits.WinForScatterSpecialFruits), 2);

        }

        /// <summary>
        /// Kreira niz LinesInformation.
        /// </summary>
        /// <param name="matrix">Matrica</param>
        /// <param name="numberOfLines">Broj linija</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Množilac za vreme gratis igara</param>
        /// <param name="wild">Wild element</param>
        /// <param name="winForWild">Dobitak za wild</param>
        /// <param name="gameLines">Linije na koje se igra</param>
        /// <param name="addExtraLine">Dobitak elemenata van linije</param>
        /// <param name="extraSymbol">Simbol koji daje dobitak van linije</param>
        protected new void CreateLinesInformations(Matrix matrix, int numberOfLines, int bet, int gratisGame,
            int wild, int[] winForWild, int[,] gameLines, int addExtraLine = 0, int extraSymbol = -1)
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
                    Win = win * bet * gratisGame,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, wild, winForWild, win, gameLines)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, gameLines).GetLinesPositions(gameLines, i, wild, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (addExtraLine > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(extraSymbol),
                    Id = EXTRA_LINE,
                    Win = addExtraLine * bet * gratisGame * 20, // Always 20, since number of lines is dynamic
                    WinningElement = (byte)extraSymbol
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        private int ChoosePlayLine()
        {
            var randNumber = SoftwareRng.Next();

            for (int i = 0; i < MatrixSpecialFruits.PayLinesCumulativeProbabilities.Length; i++)
            {
                if (randNumber < MatrixSpecialFruits.PayLinesCumulativeProbabilities[i])
                {
                    return MatrixSpecialFruits.PayLines[i];
                }
            }

            return MatrixSpecialFruits.PayLines[MatrixSpecialFruits.PayLines.Length - 1];

        }

    }
}