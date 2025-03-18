using System;

namespace MathBaseProject.BaseMathData
{
    public class Matrix
    {
        #region Constructor or Singleton implementation

        /// <summary>
        /// konstruktor za matricu
        /// </summary>
        protected Matrix(int rows = 3)
        {
            _Matrix = new int[5, rows];
            Bonus = 1;
            GratisGame = false;
        }

        #endregion

        #region Private fields

        private readonly int[,] _Matrix;
        protected int Bonus;
        protected bool GratisGame;

        #endregion

        #region Public methods

        /// <summary>
        /// uzima liniju iz matrice
        /// </summary>
        /// <param name="lineNumber">broj linije, 1 -- 15</param>
        /// <param name="lines">Linije </param>
        /// <returns>vraća liniju pod datim brojem</returns>
        public Line GetLine(int lineNumber, int[,] lines)
        {
            try
            {
                var line = new Line();
                for (var i = 0; i < 5; i++)
                {
                    line.SetElement(i, _Matrix[i, lines[lineNumber - 1, i]]);
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
            return _Matrix[i, j];
        }

        /// <summary>
        /// seter za element iz matrica na poziciji [i,j]
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="value">vrednos koja se dodeljuje elementu na [i,j]</param>
        public void SetElement(int i, int j, int value)
        {
            _Matrix[i, j] = value;
        }

        /// <summary>
        /// koliko ima nekog elementa u matrici
        /// </summary>
        /// <param name="element">koji element se traži</param>
        /// <returns>broj pojavljivanja tog elementa</returns>
        public int GetNumberOfElement(int element)
        {
            int number = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_Matrix[i, j] == element)
                    {
                        number++;
                    }
                }
            }
            return number;
        }

        /// <summary>
        /// da li određeni reel ima određeni element
        /// </summary>
        /// <param name="reel">koji reel po redu</param>
        /// <param name="element">koji element se traži</param>
        /// <returns>vraća true ako reel ima taj element, inače false</returns>
        public bool IsReelHave(int reel, int element)
        {
            for (int i = 0; i < 3; i++)
            {
                if (_Matrix[reel, i] == element)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Proverava da li matrica daje gratis igre
        /// </summary>
        /// <returns>Da li matrica daje gratis igre</returns>
        public bool IsGiveGratisGames()
        {
            return GratisGame;
        }

        /// <summary>
        /// Seter za bonus i za gratis igre
        /// </summary>
        /// <param name="bonus">Vrednost bonusa</param>
        /// <param name="gratis">Da li daje gratis igre</param>
        public void SetBonusAndGratis(int bonus, bool gratis)
        {
            Bonus = bonus;
            GratisGame = gratis;
        }

        /// <summary>
        /// Učitava matricu iz niza bajtova
        /// </summary>
        /// <param name="array">Niz iz kog učitava</param>
        public virtual void FromByteArray(byte[] array)
        {
            if (array.Length != 16)
            {
                return;
            }
            int next = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (next % 2 == 0)
                    {
                        _Matrix[i, j] = array[next / 2] >> 4;
                    }
                    else
                    {
                        _Matrix[i, j] = array[next / 2] & 0x0F;
                    }
                    next++;
                }
            }
            GratisGame = (array[14] == 1);
            Bonus = array[15];
        }

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public virtual int CalculateWinLine(int lineNumber)
        {
            return 0;
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
                for (var j = 0; j < 3; j++)
                {
                    if (_Matrix[i, j] == symbol)
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
        /// Daje dobitak za sketere.
        /// </summary>
        /// <returns></returns>
        public int GetScatterWin()
        {
            return Bonus;
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
                for (var j = 0; j < 3; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        #endregion
    }
}
