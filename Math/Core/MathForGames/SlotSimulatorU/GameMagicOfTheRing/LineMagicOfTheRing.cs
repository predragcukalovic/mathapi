using MathBaseProject.BaseMathData;

namespace MathForGames.GameMagicOfTheRing
{
    public class LineMagicOfTheRing : Line
    {
        #region Private methods

        /// <summary>
        /// Broj simbola u liniji.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected int NumberOfElements(int element)
        {
            var n = 0;
            for (var i = 0; i < 5; i++)
            {
                if (GetElement(i) == element)
                {
                    n++;
                }
            }
            return n;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak kad elementi nisu jedan do drugog u liniji.
        /// </summary>
        /// <param name="element">Element za koji se računa dobitak</param>
        /// <param name="winsForLines">Dobici za liniju</param>
        /// <returns>Dobitak koji daje linija.</returns>
        public int CalculateNonOrderWin(int element, int[,] winsForLines)
        {
            var numberOfElement = NumberOfElements(element);
            if (numberOfElement == 0)
            {
                return 0;
            }
            return winsForLines[element, numberOfElement - 1];
        }

        #endregion
    }
}
