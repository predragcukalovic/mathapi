using MathCombination.CombinationData;
using System.Linq;

namespace GameWild81
{
    public class CombinationWild81 : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'Wild81' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombinationWild81(MatrixWild81 matrix, int bet)
        {
            Matrix = new byte[4, 5];
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }
            CreateEmptyArray(PositionFor2);
            GratisGame = false;
            NumberOfGratisGames = 0;

            LinesInformation = matrix.GetAllWinningLines(bet).ToArray();
            TotalWin = LinesInformation.Sum(x => x.Win);
            var tmp = LinesInformation.Where(x => x.Id == EXTRA_LINE).FirstOrDefault();
            WinFor2 = tmp == null ? 0 : tmp.Win;
        }

        public static Combination GetCombinationWild81(int bet)
        {
            var matrixArray = MatrixWild81.GetMatixArray();
            var matrix = new MatrixWild81();
            matrix.FromMatrixArrayWild81(matrixArray);
            var combination = new CombinationWild81();
            combination.MatrixToCombinationWild81(matrix, bet);
            return combination;
        }

        public static Combination GetCombinationWild81Cheat(int[,] matrixArray, int bet)
        {
            var matArray2 = new int[4, 5];
            for (var i = 0; i < 4; i++)
            {
                matArray2[i, 0] = 3;
                for (var j = 1; j < 5; j++)
                {
                    matArray2[i, j] = matrixArray[i, j - 1];
                }
            }
            var matrix = new MatrixWild81();
            matrix.FromMatrixArrayWild81(matArray2);
            var combination = new CombinationWild81();
            combination.MatrixToCombinationWild81(matrix, bet);
            return combination;
        }
    }
}
