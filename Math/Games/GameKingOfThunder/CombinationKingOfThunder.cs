using MathCombination.CombinationData;
using MathForGames.BasicGameData;

namespace GameKingOfThunder
{
    public class CombinationKingOfThunder : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'KingOfThunder' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGamesLeft"></param>
        public void MatrixToCombination(MatrixKingOfThunder matrix, int numberOfLines, int bet, int gratisGamesLeft)
        {
            var gratisMult = 1;
            if (gratisGamesLeft == 1 || gratisGamesLeft == 2)
            {
                matrix.SetElement(2, 0, 0);
                matrix.SetElement(2, 1, 0);
                matrix.SetElement(2, 2, 0);
                gratisMult = 2;
            }
            if (gratisGamesLeft == 3 || gratisGamesLeft == 4)
            {
                matrix.SetElement(1, 0, 0);
                matrix.SetElement(1, 1, 0);
                matrix.SetElement(1, 2, 0);
                matrix.SetElement(3, 0, 0);
                matrix.SetElement(3, 1, 0);
                matrix.SetElement(3, 2, 0);
            }
            if (gratisGamesLeft == 5 || gratisGamesLeft == 6)
            {
                matrix.SetElement(0, 0, 0);
                matrix.SetElement(0, 1, 0);
                matrix.SetElement(0, 2, 0);
                matrix.SetElement(4, 0, 0);
                matrix.SetElement(4, 1, 0);
                matrix.SetElement(4, 2, 0);
            }
            FillMatrixArray(matrix);

            CreateEmptyArray(PositionFor2);
            GratisGame = matrix.GetNumberOfElement(2) >= 3 && gratisGamesLeft == 0;
            NumberOfGratisGames = GratisGame ? MatrixKingOfThunder.GRATIS_GAMES : 0;
            CreateLinesInformations(matrix, numberOfLines, bet, gratisMult, 0, MatrixKingOfThunder.WinForWildKingOfThunder, GlobalData.GameLineExtra,
                matrix.GetNoLineWin(2, MatrixKingOfThunder.WinForGratisKingOfThunder), 2);
        }
    }
}
