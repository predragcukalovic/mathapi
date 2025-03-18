using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameDiamonds
{
    public class LineDiamonds : Line
    {
        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <returns></returns>
        public int CalculateLineWin()
        {
            var element = GetElement(2);
            int start = 2, end = 2;
            if (GetElement(1) == element)
            {
                start = 1;
                if (GetElement(0) == element)
                {
                    start = 0;
                }
            }
            if (GetElement(3) == element)
            {
                end = 3;
                if (GetElement(4) == element)
                {
                    end = 4;
                }
            }
            if (end - start >= 2 && element != 0)
            {
                return LineWinsForGames.WinForLinesDiamonds[element, end - start];
            }
            return 0;
        }

        /// <summary>
        /// Daje simbol koji daje dobitak
        /// </summary>
        /// <returns></returns>
        public int GetWinningElement()
        {
            return GetElement(2);
        }

        /// <summary>
        /// Daje pozicije dobitnih elemenata (za igru Diamonds).
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <param name="element">Simbol</param>
        /// <returns></returns>
        public byte[] GetLinesPositionsDiamonds(int lineNumber, int element)
        {
            var positionsArray = new byte[5];
            var index = 0;
            var startElement = 2;
            while (startElement > 0 && GetElement(startElement - 1) == element)
            {
                startElement--;
            }
            while (startElement < 5 && GetElement(startElement) == element)
            {
                positionsArray[index++] = (byte)(GlobalData.GameLineExtra[lineNumber - 1, startElement] * 5 + startElement);
                startElement++;
            }
            for (; index < 5; index++)
            {
                positionsArray[index] = 255;
            }
            return positionsArray;
        }

        #endregion
    }
}
