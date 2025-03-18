using MathCombination.CombinationData;

namespace MathForNovomatic.GameRoaringForties
{
    public class CombinationRoaringForties : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'EpicFire40' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixRoaringForties matrix, int numberOfLines, int bet)
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

            CreateLinesInformationsTurbo(matrix, numberOfLines, bet, 0, MatrixRoaringForties.WinForWildsRoaringForties, MatrixRoaringForties.GameLines,
                matrix.GetNoLineWin(9, MatrixRoaringForties.WinForScatterRoaringForties), 9);
        }
    }
}
