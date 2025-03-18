using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;

namespace MathForGames.GameAlpinist
{
    public class MatrixAlpinist : Matrix
    {
        #region Public properties

        public const int GRATIS_GAMES = 5;

        #endregion

        #region Public methods

        /// <summary>
        /// vraća dobitak matrice koja sadrži pet dvojki u dobitnoj liniji
        /// </summary>
        /// <returns>dobitak</returns>
        public int GetWinOf2Matrix()
        {
            if (Bonus == 250)
            {
                return 600;
            }
            if (Bonus == 20)
            {
                return 40;
            }
            return Bonus;
        }

        /// <summary>
        /// Daje slučajan dobitak za matricu sa sketerima.
        /// </summary>
        /// <returns></returns>
        public int GetRandomWinOf2Matrix()
        {
            if (GetNumberOfElement(2) < 5)
            {
                return 0;
            }
            var wins = new[] { 20, 30, 160, 170, 600 };
            return wins[SoftwareRng.Next(5)];
        }

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLine).CalculateLineWin(LineWinsForGames.WinForLinesAlpinist, LineWinsForGames.WinForWildsAlpinist, 0, 2);
        }

        /// <summary>
        /// Postavlja slučajan bonus za bernardince.
        /// </summary>
        public void SetRandomBernardBonus()
        {
            if (GetNumberOfElement(2) != 5)
            {
                Bonus = 0;
                return;
            }
            var bernard = new[] { 20, 30, 160, 170, 250 };
            Bonus = bernard[SoftwareRng.Next(5)];
        }

        #endregion
    }
}
