using MathBaseProject.BaseMathData;
using System;

namespace GameWildClover506
{
    public class Line40WildClover6
    {
        #region Private fields

        protected int[] Line;

        #endregion

        #region Constructors

        public Line40WildClover6()
        {
            Line = new int[6];
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Računa dobitak linije koja počinje wildovima.
        /// </summary>
        /// <param name="winForWilds">Niz dobitaka za wild.</param>
        /// <param name="wild">Wild element.</param>
        /// <returns></returns>
        private int CalculateLineWildWin(int[] winForWilds, int wild)
        {
            if (winForWilds == null || wild < 0)
            {
                return 0;
            }
            var index = 0;
            while (index < 6 && Line[index] == wild)
            {
                index++;
            }
            return index == 0 ? 0 : winForWilds[index - 1];
        }

        /// <summary>
        /// Daje simbol i broj tog simbola koji daje dobitak.
        /// </summary>
        /// <param name="wild">Wild element</param>
        /// <returns></returns>
        protected SymbolAndPositions GetSymbolAndPositions(int wild)
        {
            var elem = FirstNonWild(wild);
            var index = 0;
            var haveWild = false;
            while (index < 6 && (Line[index] == wild || Line[index] == elem))
            {
                if (Line[index] == wild)
                {
                    haveWild = true;
                }
                index++;
            }
            return new SymbolAndPositions { Symbol = elem, Positions = index - 1, Wild = haveWild };
        }

        /// <summary>
        /// Daje prvi element u liniji koji nije wild.
        /// </summary>
        /// <param name="wild"></param>
        /// <returns></returns>
        private int FirstNonWild(int wild)
        {
            for (var i = 0; i < 6; i++)
            {
                if (Line[i] != wild)
                {
                    return Line[i];
                }
            }
            return wild;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Seter za određeni element u nizu
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public void SetElement(int element, int value)
        {
            Line[element] = value;
        }

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="winForLines">Matrica dobitaka</param>
        /// <param name="winForWilds">Dobici za wild</param>
        /// <param name="wild">Wild (uglavnom 0)</param>
        /// <param name="wildMultiply">Množilac za wild (uglavnom 2 pošto duplira)</param>
        /// <returns></returns>
        public virtual int CalculateLineWin(int[,] winForLines, int[] winForWilds, int wild, int wildMultiply)
        {
            var s = GetSymbolAndPositions(wild);
            return Math.Max(winForLines[s.Symbol, s.Positions] * (s.Wild ? wildMultiply : 1), CalculateLineWildWin(winForWilds, wild));
        }

        /// <summary>
        /// Daje dobitni element linije.
        /// </summary>
        /// <param name="wild">Wild element.</param>
        /// <param name="lineWin">Dobitak linije.</param>
        /// <param name="winForWilds">Dobitak za wildove.</param>
        /// <returns></returns>
        public int GetWinningElement(int wild, int lineWin, int[] winForWilds)
        {
            if (CalculateLineWildWin(winForWilds, wild) == lineWin)
            {
                return 0;
            }
            return GetSymbolAndPositions(wild).Symbol;
        }

        /// <summary>
        /// Daje pozicije dobitnih elemenata.
        /// </summary>
        /// <param name="lines">Linije na koje se igra.</param>
        /// <param name="lineNumber">Broj linije.</param>
        /// <param name="wild">Wild element.</param>
        /// <param name="element">Simbol</param>
        /// <returns></returns>
        public byte[] GetLinesPositions(int[,] lines, int lineNumber, int wild, int element)
        {
            var positionsArray = new byte[6];
            var i = 0;
            while (i < 6 && (Line[i] == wild || Line[i] == element))
            {
                positionsArray[i] = (byte)(lines[lineNumber - 1, i] * 6 + i);
                i++;
            }
            for (; i < 6; i++)
            {
                positionsArray[i] = 255;
            }
            return positionsArray;
        }

        #endregion
    }
}
