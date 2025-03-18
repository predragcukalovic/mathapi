using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;
using System;

namespace MathForGames.GameTurboHot40
{
    public class MatrixTurboHot40
    {
        #region Private fields

        private readonly int[,] _Matrix;

        #endregion

        #region Constructor or Singleton implementation

        /// <summary>
        /// konstruktor za matricu
        /// </summary>
        public MatrixTurboHot40()
        {
            _Matrix = new int[5, 6];
        }

        #endregion

        #region Public methods

        /// <summary>
        /// uzima liniju iz matrice
        /// </summary>
        /// <param name="lineNumber">broj linije, 1 -- 40</param>
        /// <param name="lines">Linije </param>
        /// <returns>vraća liniju pod datim brojem</returns>
        public Line GetLine(int lineNumber, int[,] lines)
        {
            try
            {
                var line = new Line();
                for (var i = 0; i < 5; i++)
                {
                    line.SetElement(i, _Matrix[i, lines[lineNumber - 1, i] + 1]);
                }
                return line;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// geter za element iz matrice na poziciji [i,j]
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>vraća element iz matrice na poziciji [i,j]</returns>
        public int GetElement(int i, int j)
        {
            return _Matrix[i, (j + 1) % 6];
        }

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public virtual int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineTurbo).CalculateLineWin(LineWinsForGames.WinForLinesTurboHot40, LineWinsForGames.WinForWildsTurboHot40, 0, 1);
        }

        /// <summary>
        /// koliko ima nekog elementa u matrici
        /// </summary>
        /// <param name="element">koji element se traži</param>
        /// <returns>broj pojavljivanja tog elementa</returns>
        public int GetNumberOfElement(int element)
        {
            var number = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (_Matrix[i, j + 1] == element)
                    {
                        number++;
                    }
                }
            }
            return number;
        }

        /// <summary>
        /// Daje dobitni element za liniju.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <param name="wild">Wild element.</param>
        /// <param name="winForWild">Dobitak za wild.</param>
        /// <param name="lineWin">Dobitak linije.</param>
        /// <param name="gameLines">Linije na koje se igra.</param>
        /// <returns></returns>
        public int GetWinningElementForLine(int lineNumber, int wild, int[] winForWild, int lineWin, int[,] gameLines)
        {
            return GetLine(lineNumber, gameLines).GetWinningElement(wild, lineWin, winForWild);
        }

        /// <summary>
        /// Daje dobitni element za liniju.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <param name="wild">Wild element.</param>
        /// <param name="winForWild">Dobitak za wild.</param>
        /// <param name="lineWin">Dobitak linije.</param>
        /// <param name="gameLines">Linije na koje se igra.</param>
        /// <returns></returns>
        public int GetRightWinningElementForLine(int lineNumber, int wild, int[] winForWild, int lineWin, int[,] gameLines)
        {
            return GetLine(lineNumber, gameLines).GetRightWinningElement(wild, lineWin, winForWild);
        }

        /// <summary>
        /// Daje dobitak elemenata koji nisu u liniji.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="noLineWins"></param>
        /// <returns></returns>
        public int GetNoLineWin(int symbol, int[] noLineWins)
        {
            var n = GetNumberOfElement(symbol);
            return n == 0 ? 0 : noLineWins[n - 1];
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArray(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    _Matrix[i, j] = matrix[i, j];
                }
            }
        }

        /// <summary>
        /// Daje niz pozicija za simbol.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public byte[] GetPositionsArray(int symbol)
        {
            var positions = new byte[5];
            var index = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (_Matrix[i, j + 1] == symbol)
                    {
                        positions[index++] = (byte)(j * 5 + i);
                    }
                }
            }
            for (; index < 5; index++)
            {
                positions[index] = 255;
            }
            return positions;
        }

        /// <summary>
        /// Učitava matricu iz niza bajtova
        /// </summary>
        /// <param name="array">Niz iz kog učitava</param>
        public void FromByteArray(byte[] array)
        {
            if (array.Length != 16)
            {
                return;
            }
            var next = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (next % 2 == 0)
                    {
                        _Matrix[i, j + 1] = array[next / 2] >> 4;
                    }
                    else
                    {
                        _Matrix[i, j + 1] = array[next / 2] & 0x0F;
                    }
                    next++;
                }
            }
        }

        #endregion
    }

}
