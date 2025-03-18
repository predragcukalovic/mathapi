using MathCombination.CombinationData;

namespace MathForNovomatic.GameLuckyLadysCharmDeluxe
{
    public class CombinationLuckyLadysCharmDeluxe : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'NovomaticLuckyLadysCharmDeluxe' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Da li je gratis igra</param>
        public void MatrixToCombination(MatrixLuckyLadysCharmDeluxe matrix, int numberOfLines, int bet, bool gratisGame)
        {
            var gratisMultiplicator = gratisGame ? MatrixLuckyLadysCharmDeluxe.GRATIS_MULTIPLICATOR : 1;
            FillMatrixArray(matrix);

            GratisGame = matrix.GetNumberOfElement((byte)LuckyLadysCharmSymbols.Hands) >= 3;
            NumberOfGratisGames = GratisGame ? MatrixLuckyLadysCharmDeluxe.GRATIS_GAMES : 0;
            CreateEmptyArray(PositionFor2);
            CreateEmptyArray(MultiplyFor2);

            CreateLinesInformations(matrix, numberOfLines, bet, gratisMultiplicator, (byte)LuckyLadysCharmSymbols.LuckyLady,
                                    MatrixLuckyLadysCharmDeluxe.WinForWilds, MatrixLuckyLadysCharmDeluxe.GameLines,
                                    matrix.GetNoLineWin(
                                        (byte)LuckyLadysCharmSymbols.Hands,
                                        MatrixLuckyLadysCharmDeluxe.WinForScatters),
                                    (byte)LuckyLadysCharmSymbols.Hands);
        }
    }
}
