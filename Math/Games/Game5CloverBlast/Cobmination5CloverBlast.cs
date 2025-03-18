
using MathCombination.CombinationData;

namespace Game5CloverBlast
{
    public class Combination5CloverBlast : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru '5CloverBlast' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="numberOfLines"></param>
        public void MatrixToCombination5CloverBlast(Matrix5CloverBlast matrix, int bet)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            Matrix = new byte[5, 5];
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            var next = 0;
            PositionFor2 = new byte[5];
            CreateEmptyArray(PositionFor2);
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if (j > 0 && j < 4 && Matrix[i, j] == 0)
                    {
                        PositionFor2[next++] = (byte)(5 * j + i);
                    }
                }
            }

            for (var i = 0; i < 5; i++)
            {
                if (PositionFor2[i] != 255)
                {
                    var reel = PositionFor2[i] % 5;
                    matrix.SetElement(reel, 1, 0);
                    matrix.SetElement(reel, 2, 0);
                    matrix.SetElement(reel, 3, 0);
                }
            }

            CreateLinesInformations(matrix, 5, bet, 1, 0, Matrix5CloverBlast.WinForWild5CloverBlast, Matrix5CloverBlast.GameLine5CloverBlast);
        }
    }
}
