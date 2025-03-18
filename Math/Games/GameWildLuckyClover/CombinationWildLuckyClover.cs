using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;
using System.Linq;

namespace GameWildLuckyClover
{
    public class CombinationWildLuckyClover : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'WildLuckyClover' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        /// <param name="addInfo"></param>
        public void MatrixToCombination(MatrixWildLuckyClover matrix, int numberOfLines, int bet, bool gratisGame, byte addInfo)
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
            AdditionalInformation = 0;

            var scatNum = matrix.GetNumberOfElement(7);

            if (!gratisGame && scatNum >= 3)
            {
                GratisGame = true;
                NumberOfGratisGames = MatrixWildLuckyClover.FreeSpinsCount[scatNum - 3];
                AdditionalInformation = (byte)(SoftwareRng.Next(3) + 4);
            }
            if (gratisGame)
            {
                AdditionalInformation = addInfo;
                matrix.SetWild(addInfo);
            }

            CreateLinesInformationsTurbo(matrix, numberOfLines, bet, 0, MatrixWildLuckyClover.WinForWildWildLuckyClover, GlobalData.GameLineTurbo);
            if (GratisGame)
            {
                var li = LinesInformation.ToList();
                li.Add(new LineInfo { Id = EXTRA_LINE, Win = 0, WinningElement = 7, WinningPosition = matrix.GetPositionsArray(7) });
                LinesInformation = li.ToArray();
            }
        }
    }
}
