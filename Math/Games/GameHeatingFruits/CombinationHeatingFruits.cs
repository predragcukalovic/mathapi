using MathCombination.CombinationData;
using MathForGames.BasicGameData;

namespace GameHeatingFruits
{
    public class CombinationHeatingFruits : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'HeatingFruits' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="addInfo"></param>
        public void MatrixToCombination(MatrixHeatingFruits matrix, int numberOfLines, int bet, byte addInfo)
        {
            PositionFor2 = new byte[5];
            if (addInfo != 0)
            {
                for (var i = 0; i < 5; i++)
                {
                    if ((addInfo & (1 << i)) != 0)
                    {
                        matrix.SetReelWild(i);
                        PositionFor2[i] = 2;
                    }
                }
            }
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

            CreateLinesInformationsTurbo(matrix, numberOfLines, bet, 0, MatrixHeatingFruits.WinForWildHeatingFruits, GlobalData.GameLineTurbo);

            AdditionalInformation = 0;
            for (var i = 0; i < 5; i++)
            {
                if (matrix.IsReelWildNext(i))
                {
                    AdditionalInformation += (byte)(1 << i);
                    PositionFor2[i] = 1;
                }
            }
        }
    }
}
