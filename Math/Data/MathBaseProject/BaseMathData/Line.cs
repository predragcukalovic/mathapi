using System;

namespace MathBaseProject.BaseMathData
{

    public struct SymbolAndPositions
    {
        public int Symbol;
        public int Positions;
        public bool Wild;
    }

    public class Line
    {
        #region Constructor or Singleton implementation

        /// <summary>
        /// konstruktor klase
        /// </summary>
        public Line()
        {
            _Line = new int[5];
        }

        #endregion

        #region Private fields

        private readonly int[] _Line;

        #endregion

        #region Private methods

        /// <summary>
        /// Računa dobitak linije koja počinje wildovima.
        /// </summary>
        /// <param name="winForWilds">Niz dobitaka za wild.</param>
        /// <param name="wild">Wild element.</param>
        /// <returns></returns>
        protected int CalculateLineWildWin(int[] winForWilds, int wild)
        {
            if (winForWilds == null || wild < 0)
            {
                return 0;
            }
            var index = 0;
            while (index < 5 && _Line[index] == wild)
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
            while (index < 5 && (_Line[index] == wild || _Line[index] == elem))
            {
                if (_Line[index] == wild)
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
            for (int i = 0; i < 5; i++)
            {
                if (_Line[i] != wild)
                {
                    return _Line[i];
                }
            }
            return wild;
        }

        /// <summary>
        /// Окреће линију.
        /// </summary>
        protected void InvertLine()
        {
            var tmp = _Line[0];
            _Line[0] = _Line[4];
            _Line[4] = tmp;
            tmp = _Line[1];
            _Line[1] = _Line[3];
            _Line[3] = tmp;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// seter za određeni element u nizu
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public void SetElement(int element, int value)
        {
            _Line[element] = value;
        }

        /// <summary>
        /// geter za određeni element iz niza
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public int GetElement(int element)
        {
            return _Line[element];
        }

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="winForLines">Matrica dobitaka</param>
        /// <param name="winForWilds">Dobici za wild</param>
        /// <param name="wild">Wild (uglavnom 0)</param>
        /// <param name="wildMultiply">Množilac za wild (uglavnom 2 pošto duplira)</param>
        /// <returns></returns>
        public int CalculateLineWin(int[,] winForLines, int[] winForWilds, int wild, int wildMultiply)
        {
            var s = GetSymbolAndPositions(wild);
            return Math.Max(winForLines[s.Symbol, s.Positions] * (s.Wild ? wildMultiply : 1), CalculateLineWildWin(winForWilds, wild));
        }

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="winForLines">Matrica dobitaka</param>
        /// <param name="winForWilds">Dobici za wild</param>
        /// <param name="wild">Wild (uglavnom 0)</param>
        /// <param name="wildMultiply">Množilac za wild (uglavnom 2 pošto duplira)</param>
        /// <returns></returns>
        public int CalculateRightLineWin(int[,] winForLines, int[] winForWilds, int wild, int wildMultiply)
        {
            InvertLine();
            var s = GetSymbolAndPositions(wild);
            var win = Math.Max(winForLines[s.Symbol, s.Positions] * (s.Wild ? wildMultiply : 1), CalculateLineWildWin(winForWilds, wild));
            InvertLine();
            if (s.Positions == 4)
            {
                return 0;
            }
            return win;
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
        /// Daje dobitni element linije.
        /// </summary>
        /// <param name="wild">Wild element.</param>
        /// <param name="lineWin">Dobitak linije.</param>
        /// <param name="winForLines">Dobici za linije.</param>
        /// <param name="winForWilds">Dobitak za wildove.</param>
        /// <returns></returns>
        public int GetWinningElement(int wild, int lineWin, int[,] winForLines, int[] winForWilds)
        {
            SymbolAndPositions sym = GetSymbolAndPositions(wild);
            if (sym.Wild)
            {
                if (CalculateLineWildWin(winForWilds, wild) > winForLines[sym.Symbol, sym.Positions])
                {
                    return wild;
                }
            }
            return sym.Symbol;
        }

        /// <summary>
        /// Daje dobitni element linije.
        /// </summary>
        /// <param name="wild">Wild element.</param>
        /// <param name="lineWin">Dobitak linije.</param>
        /// <param name="winForWilds">Dobitak za wildove.</param>
        /// <returns></returns>
        public int GetRightWinningElement(int wild, int lineWin, int[] winForWilds)
        {
            InvertLine();
            var elem = GetWinningElement(wild, lineWin, winForWilds);
            InvertLine();
            return elem;
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
            var positionsArray = new byte[5];
            var i = 0;
            while (i < 5 && (_Line[i] == wild || _Line[i] == element))
            {
                positionsArray[i] = (byte)(lines[lineNumber - 1, i] * 5 + i);
                i++;
            }
            for (; i < 5; i++)
            {
                positionsArray[i] = 255;
            }
            return positionsArray;
        }

        /// <summary>
        /// Daje pozicije dobitnih elemenata za desnu liniju (za igru Fruits).
        /// </summary>
        /// <param name="lines">Linije na koje se igra.</param>
        /// <param name="lineNumber">Broj linije.</param>
        /// <param name="element">Simbol</param>
        /// <param name="wild"></param>
        /// <returns></returns>
        public byte[] GetLinesPositionsRight(int[,] lines, int lineNumber, int element, int wild = 0)
        {
            var positionsArray = new byte[5];
            var i = 4;
            while (i >= 0 && (GetElement(i) == wild || GetElement(i) == element))
            {
                positionsArray[4 - i] = (byte)(lines[lineNumber - 1, i] * 5 + i);
                i--;
            }
            for (; i >= 0; i--)
            {
                positionsArray[4 - i] = 255;
            }
            return positionsArray;
        }

        /// <summary>
        /// Daje pozicije dobitnih elemenata razbacanih po liniji (za igru Ring i Spellbook).
        /// </summary>
        /// <param name="lines">Linije na koje se igra.</param>
        /// <param name="lineNumber">Broj linije.</param>
        /// <param name="element">Simbol</param>
        /// <returns></returns>
        public byte[] GetLinesPositionsNonOrder(int[,] lines, int lineNumber, int element)
        {
            var positionsArray = new byte[5];
            var i = 0;
            for (var j = 0; j < 5; j++)
            {
                if (GetElement(j) == element)
                {
                    positionsArray[i] = (byte)(lines[lineNumber - 1, j] * 5 + j);
                    i++;
                }
            }
            for (; i < 5; i++)
            {
                positionsArray[i] = 255;
            }
            return positionsArray;
        }

        #endregion
    }
}
