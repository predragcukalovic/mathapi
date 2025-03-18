using MathCombination.CombinationData;

namespace MathForNovomatic.GameSizzlingHotDeluxe
{
    public class CombinationSizzlingHotDeluxe : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'Sizzling Hot' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixSizzlingHotDeluxe matrix, int numberOfLines, int bet)
        {
            FillMatrixArray(matrix);

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(
                matrix, numberOfLines, bet,
                1,
                -1,
                null,
                MatrixSizzlingHotDeluxe.GameLines,
                matrix.GetNoLineWin(7, MatrixSizzlingHotDeluxe.WinForScattersNovomaticSizzlingHotDeluxe), 7);
        }
    }
}
