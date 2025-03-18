using MathCombination.CombinationData;
using RNGUtils.RandomData;
using System.Collections.Generic;

namespace GameExtraFlames10
{
    public class CombinationExtraFlames10 : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'ExtraFlames10' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixExtraFlames10 matrix, int numberOfLines, int bet)
        {
            FillMatrixArray(matrix);
            var mult = new[] { 2, 3, 7 };
            var usedField = new[] { false, false, false };
            for (var i = 0; i < 3; i++)
            {
                PositionFor2[i] = (byte)mult[SoftwareRng.Next(3)];//potencijalni množioci za wild, uvek da ih ima tri
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            //CreateLinesInformationsExtraFlames(matrix, numberOfLines, bet, matrix.GetNoLineWin(0, LineWinsForGames.WinForScatterDiamonds));
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var l = matrix.GetLineExtraFlames(i);
                var win1 = l.CalculateLineWin();
                var win2 = l.CalculateLeftWildWin(PositionFor2[MatrixExtraFlames10.GameLineExtraFlames[i - 1, 2]]);
                var win3 = l.CalculateRightWildWin(PositionFor2[MatrixExtraFlames10.GameLineExtraFlames[i - 1, 2]]);
                if (win1 != 0)
                {
                    var lineInfo = new LineInfo
                    {
                        Id = (byte)(i - 1),
                        Win = win1 * bet,
                        WinningElement = (byte)l.GetWinningElement()
                    };
                    lineInfo.WinningPosition = l.GetLinesPositionsExtraFlames(i, lineInfo.WinningElement);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                    usedField[MatrixExtraFlames10.GameLineExtraFlames[i - 1, 2]] = true;
                }
                if (win2 != 0)
                {
                    var lineInfo = new LineInfo
                    {
                        Id = (byte)(i - 1),
                        Win = win2 * bet,
                        WinningElement = (byte)l.GetLeftWinningElement()
                    };
                    lineInfo.WinningPosition = l.GetLinesPositionsExtraFlames(i, lineInfo.WinningElement);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                    usedField[MatrixExtraFlames10.GameLineExtraFlames[i - 1, 2]] = true;
                }
                if (win3 != 0)
                {
                    var lineInfo = new LineInfo
                    {
                        Id = (byte)(i - 1),
                        Win = win3 * bet,
                        WinningElement = (byte)l.GetRightWinningElement()
                    };
                    lineInfo.WinningPosition = l.GetLinesPositionsExtraFlames(i, lineInfo.WinningElement);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                    usedField[MatrixExtraFlames10.GameLineExtraFlames[i - 1, 2]] = true;
                }
            }
            for (var i = 0; i < 3; i++)
            {
                if (!usedField[i] && Matrix[2, i] == 0)
                {
                    var lineInfo = new LineInfo
                    {
                        Id = EXTRA_LINE,
                        Win = MatrixExtraFlames10.WinForWildExtraFlames10 * bet,
                        WinningElement = 0,
                        WinningPosition = new byte[] { (byte)(i * 5 + 2), 255, 255, 255, 255 }
                    };
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        public static Combination GetCombination(int numberOfLines, int bet)
        {
            var matrixArray = MatrixExtraFlames10.GetMatixArray();
            var matrix = new MatrixExtraFlames10();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationExtraFlames10();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }
    }
}
