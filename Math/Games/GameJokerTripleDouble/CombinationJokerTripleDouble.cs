using MathCombination.CombinationData;
using System.Collections.Generic;

namespace GameJokerTripleDouble
{
    public class CombinationJokerTripleDouble : Combination3
    {
        /// <summary>
        /// Pretvara matricu u kombinaciju za igru 'JokerTripleDouble'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <param name="addInfo">Ril koji treba da se respinuje</param>
        /// <param name="addArray">Prethodna matrica, za slučaj da se desi respin nekog rila</param>
        public void MatrixToCombination(MatrixJokerTripleDouble matrix, int bet, bool gratisGame, byte addInfo, ref byte[] addArray)
        {
            GratisGame = false;
            Matrix = new byte[3, 5];
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            if (gratisGame)
            {
                for (var i = 0; i < 9; i++)
                {
                    if ((addArray[i] & 0x0F) != 0)
                    {
                        matrix.SetElement(i % 3, i / 3 + 1, (addArray[i] & 0x0F) - 1);
                    }
                }
            }

            AdditionalArray = new byte[9];
            AdditionalInformation = 0;

            TotalWin = 0;
            var dbl = matrix.DoubleWin();
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= 5; i++)
            {
                var winOfLine = matrix.GetLineWin(i, out int winningElement, out bool wild);
                if (winOfLine == 0)
                    continue;
                var win = winOfLine * bet * (dbl ? 2 : 1);
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetWinningPositions(i),
                    Id = (byte)(i - 1),
                    Win = win,
                    WinningElement = (byte)winningElement
                };
                TotalWin += win;
                linesInfo.Add(lineInfo);
                if (wild && ((addInfo & (byte)(1 << i)) == 0))
                {
                    AdditionalArray[lineInfo.WinningPosition[0]] = (byte)(matrix.GetElement(lineInfo.WinningPosition[0] % 3, lineInfo.WinningPosition[0] / 3 + 1) + 1);
                    AdditionalArray[lineInfo.WinningPosition[1]] = (byte)(matrix.GetElement(lineInfo.WinningPosition[1] % 3, lineInfo.WinningPosition[1] / 3 + 1) + 1);
                    AdditionalArray[lineInfo.WinningPosition[2]] = (byte)(matrix.GetElement(lineInfo.WinningPosition[2] % 3, lineInfo.WinningPosition[2] / 3 + 1) + 1);
                    GratisGame = true;
                    AdditionalInformation |= (byte)(1 << i);
                }
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
            NumberOfGratisGames = GratisGame ? 1 : 0;
            WinFor2 = dbl ? 1 : 0;
            for (var i = 0; i < 9; i++)
            {
                AdditionalArray[i] |= (byte)(addArray[i] << 4);
            }
            addArray = AdditionalArray;
        }

        /// <summary>
        /// Daje kombinaciju za igru JokerTripleDouble.
        /// </summary>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <param name="addInfo"></param>
        /// <param name="addArray"></param>
        /// <returns></returns>
        public static ICombination GetCombinationJokerTripleDouble(int bet, bool gratisGame, byte addInfo, ref byte[] addArray)
        {
            if (addArray == null)
            {
                addArray = new byte[9];
            }
            var matrix = new MatrixJokerTripleDouble();
            var matrixArray = MatrixJokerTripleDouble.GetMatixArray(gratisGame);
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationJokerTripleDouble();
            combination.MatrixToCombination(matrix, bet, gratisGame, addInfo, ref addArray);
            return combination;
        }
    }
}
