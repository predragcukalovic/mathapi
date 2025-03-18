using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;

namespace GameWildCraps
{
    public class CombinationWildCraps : Combination
    {
        public void MatrixToCombination(MatrixWildCraps matrix, int numberOfLines, int bet, bool gratisGame, byte addInfo)
        {
            WinFor2 = 0;
            if (gratisGame)
            {
                matrix.BuildGratisMatrix(addInfo);
                AdditionalInformation = addInfo;
                WinFor2 = addInfo;
            }
            FillMatrixArray(matrix);


            CreateEmptyArray(PositionFor2);
            NumberOfGratisGames = 0;
            GratisGame = matrix.GetNumberOfElement(0) == 3 && !gratisGame;
            var scatWin = 0;
            if (GratisGame)
            {
                var dice1 = (int)(SoftwareRng.Next(6) + 1);
                var dice2 = (int)(SoftwareRng.Next(6) + 1);
                var dice3 = (int)(SoftwareRng.Next(6) + 1);
                NumberOfGratisGames = dice2 + dice3;
                AdditionalInformation = (byte)dice1;
                scatWin = MatrixWildCraps.SCATTER_WIN;
                WinFor2 = dice1;
                WinFor2 |= dice2 << 8;
                WinFor2 |= dice3 << 16;
            }

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, MatrixWildCraps.WinForWildWildCraps, GlobalData.GameLineExtra, scatWin, 0);
        }
    }
}
