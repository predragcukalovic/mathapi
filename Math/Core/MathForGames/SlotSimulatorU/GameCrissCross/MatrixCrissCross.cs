using RNGUtils.RandomData;
using System.Collections.Generic;

namespace MathForGames.GameCrissCross
{
    public class MatrixCrissCross
    {
        #region Private fields

        private readonly int[,] _Matrix;

        #endregion

        #region Constructors

        public MatrixCrissCross()
        {
            _Matrix = new int[4, 3];
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Daje liniju u zavisnosti od rilova.
        /// </summary>
        /// <param name="reel1"></param>
        /// <param name="reel2"></param>
        /// <param name="reel3"></param>
        /// <param name="reel4"></param>
        /// <returns></returns>
        private LineCrissCross GetLine(int reel1, int reel2, int reel3, int reel4)
        {
            var line = new LineCrissCross();
            line.SetElement(0, _Matrix[0, reel1]);
            line.SetElement(1, _Matrix[1, reel2]);
            line.SetElement(2, _Matrix[2, reel3]);
            line.SetElement(3, _Matrix[3, reel4]);
            return line;
        }

        /// <summary>
        /// Daje broj elemenata u rilu.
        /// </summary>
        /// <param name="reel"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private int GetNumberOfElementsInReel(int reel, int element)
        {
            int number = 0;
            for (int i = 0; i < 3; i++)
            {
                if (_Matrix[reel, i] == element)
                {
                    number++;
                }
            }

            return number;
        }

        /// <summary>
        /// Daje broj elemenata u matrici.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private int GetNumberOfElements(int element)
        {
            int n = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_Matrix[i, j] == element)
                    {
                        n++;
                    }
                }
            }
            return n;
        }

        /// <summary>
        /// Menja wild elemente u određeni simbol.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private MatrixCrissCross ReplaceWilds(int symbol)
        {
            var m = new MatrixCrissCross();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    m.SetElement(i, j, _Matrix[i, j]);
                    if (_Matrix[i, j] == 0)
                    {
                        m.SetElement(i, j, symbol);
                    }
                }
            }

            return m;
        }

        /// <summary>
        /// Postavlja element na poziciju [i,j]
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="element"></param>
        private void SetElement(int i, int j, int element)
        {
            _Matrix[i, j] = element;
        }

        /// <summary>
        /// Daje koliko prvih rila sadrže simbol ili wild
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private int SymbolLength(int symbol)
        {
            int n = 0;
            for (int i = 0; i < 4; i++)
            {
                if (GetNumberOfElementsInReel(i, symbol) > 0 || GetNumberOfElementsInReel(i, 0) > 0)
                {
                    n++;
                }
                else
                {
                    return n;
                }
            }

            return n;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Daje liniju u zavisnosti od broja linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public LineCrissCross GetLine(int lineNumber)
        {
            int r1 = lineNumber / 27;
            int r2 = (lineNumber / 9) % 3;
            int r3 = (lineNumber / 3) % 3;
            int r4 = lineNumber % 3;
            return GetLine(r1, r2, r3, r4);
        }

        /// <summary>
        /// Daje element na poziciji [i,j].
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public int GetElement(int i, int j)
        {
            return _Matrix[i, j];
        }

        /// <summary>
        /// Daje sve dobitne linije.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> GetAllWinningLines()
        {
            var list = new List<int>();
            for (int symbol = 2; symbol <= 10; symbol++)
            {
                int length = SymbolLength(symbol);
                if (length < 3)
                    continue;

                var m = ReplaceWilds(symbol);
                for (int r1 = 0; r1 < 3; r1++)
                {
                    for (int r2 = 0; r2 < 3; r2++)
                    {
                        for (int r3 = 0; r3 < 3; r3++)
                        {
                            if (m.GetElement(0, r1) == symbol && m.GetElement(1, r2) == symbol &&
                                m.GetElement(2, r3) == symbol &&
                                !(GetElement(0, r1) == 0 && GetElement(1, r2) == 0 && GetElement(2, r3) == 0 && GetNumberOfElementsInReel(3, symbol) == 0))
                            {
                                if (length == 3)
                                {
                                    if (GetLine(r1, r2, r3, 0).CalculateLineWin() > 0)
                                        list.Add(r1 * 27 + r2 * 9 + r3 * 3);
                                }
                                if (length == 4)
                                {
                                    for (int r4 = 0; r4 < 3; r4++)
                                    {
                                        if (m.GetElement(3, r4) == symbol)
                                        {
                                            if (GetLine(r1, r2, r3, r4).CalculateLineWin() > 0)
                                                list.Add(r1 * 27 + r2 * 9 + r3 * 3 + r4);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            list.Sort();
            return list;
        }

        /// <summary>
        /// Daje mystery dobitak.
        /// </summary>
        /// <returns></returns>
        public int GetMysteryWin()
        {
            var n = GetNumberOfElements(1);
            if (n < 3)
            {
                return 0;
            }
            var mystery = (int)SoftwareRng.Next(n == 3 ? 11 : 51);
            if (mystery == 0)
            {
                return n == 3 ? 1 : 5;
            }
            return n == 3 ? mystery * 5 : mystery * 10;
        }

        /// <summary>
        /// Daje dobitni element za liniju.
        /// </summary>
        /// <param name="lineNumber">Broj linije</param>
        /// <returns></returns>
        public int GetWinningElementForLine(int lineNumber)
        {
            return GetLine(lineNumber).GetWinningElement();
        }

        /// <summary>
        /// Transformiše dvodimenzionalni niz u matricu.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArray(int[,] matrix)
        {
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        #endregion
    }
}
