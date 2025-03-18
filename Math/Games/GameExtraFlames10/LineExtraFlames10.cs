using MathBaseProject.BaseMathData;

namespace GameExtraFlames10
{
    public class LineExtraFlames10 : Line
    {
        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <returns></returns>
        public int CalculateLineWin()
        {
            var element = GetElement(2);
            if (element == 0)
            {
                return 0;
            }
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
                return MatrixExtraFlames10.WinForLinesExtraFlames10[element, end - start];
            }
            return 0;
        }

        /// <summary>
        /// Računa dobitak linije koja ima wild u sredini, srednji ili sleva na desno.
        /// </summary>
        /// <returns></returns>
        public int CalculateLeftWildWin(int mult)
        {
            if (GetElement(2) != 0)
            {
                return 0;
            }
            int start = 1, end = 2;
            var element = GetElement(1);
            if (GetElement(0) == element)
            {
                start = 0;
            }
            if (GetElement(3) == element)
            {
                end = 3;
                if (GetElement(4) == element)
                {
                    end = 4;
                }
            }
            if (end - start >= 2)
            {
                return MatrixExtraFlames10.WinForLinesExtraFlames10[element, end - start] * mult;
            }
            return 0;
        }

        /// <summary>
        /// Računa dobitak linije koja ima wild u sredini, zdesna na levo, samo za situaciju YZ0XX.
        /// </summary>
        /// <returns></returns>
        public int CalculateRightWildWin(int mult)
        {
            if (GetElement(2) != 0 || GetElement(1) == GetElement(3))
            {
                return 0;
            }
            if (GetElement(3) == GetElement(4))
            {
                return MatrixExtraFlames10.WinForLinesExtraFlames10[GetElement(4), 2] * mult;
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
        /// Daje simbol koji daje dobitak ako ima wild sleva
        /// </summary>
        /// <returns></returns>
        public int GetLeftWinningElement()
        {
            return GetElement(1);
        }

        /// <summary>
        /// Daje simbol koji daje dobitak ako ima wild zdesna
        /// </summary>
        /// <returns></returns>
        public int GetRightWinningElement()
        {
            return GetElement(3);
        }

        /// <summary>
        /// Daje pozicije dobitnih elemenata (za igru ExtraFlames).
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <param name="element">Simbol</param>
        /// <returns></returns>
        public byte[] GetLinesPositionsExtraFlames(int lineNumber, int element)
        {
            var positionsArray = new byte[5];
            var index = 0;
            var startElement = 2;
            while (startElement > 0 && GetElement(startElement - 1) == element)
            {
                startElement--;
            }
            while (startElement < 5 && (GetElement(startElement) == element || GetElement(startElement) == 0))
            {
                positionsArray[index++] = (byte)(MatrixExtraFlames10.GameLineExtraFlames[lineNumber - 1, startElement] * 5 + startElement);
                startElement++;
            }
            for (; index < 5; index++)
            {
                positionsArray[index] = 255;
            }
            return positionsArray;
        }
    }
}
