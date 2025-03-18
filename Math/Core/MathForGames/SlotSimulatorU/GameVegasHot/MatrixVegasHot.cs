using MathForGames.BasicGameData;

namespace MathForGames.GameVegasHot
{
    public class MatrixVegasHot
    {
        #region Private fields

        protected int[,] Matrix;

        #endregion

        #region Constructors

        public MatrixVegasHot()
        {
            Matrix = new int[3, 3];
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Daje liniju iz matrice.
        /// </summary>
        /// <param name="numberOfLine"></param>
        /// <returns></returns>
        private LineVegasHot GetLine(int numberOfLine)
        {
            var line = new LineVegasHot();
            for (var i = 0; i < 3; i++)
            {
                line.SetElement(i, Matrix[i, GlobalData.GameLineVegasHot[numberOfLine - 1, i]]);
            }
            return line;
        }

        /// <summary>
        /// Seter za element iz matrica na poziciji [i,j]
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="value">Vrednost koja se dodeljuje elementu na [i,j]</param>
        public void SetElement(int i, int j, int value)
        {
            Matrix[i, j] = value;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="numberOfLine"></param>
        /// <returns></returns>
        public virtual int CalculateWinOfLine(int numberOfLine)
        {
            var line = GetLine(numberOfLine);
            return line.CalculateLineWin();
        }

        /// <summary>
        /// Broj simbola u matrici.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public int GetNumberOfElement(int element)
        {
            var number = 0;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (Matrix[i, j] == element)
                    {
                        number++;
                    }
                }
            }
            return number;
        }

        /// <summary>
        /// Da li se duplira dobirak matrice?
        /// </summary>
        /// <returns></returns>
        public bool DoubleWin()
        {
            return Matrix[0, 0] <= 4 && Matrix[0, 0] >= 1 && GetNumberOfElement(Matrix[0, 0]) == 9;
        }

        /// <summary>
        /// Da li ril sadrži element?
        /// </summary>
        /// <param name="numberOfReel"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool IsReelHave(int numberOfReel, int element)
        {
            for (var i = 0; i < 3; i++)
            {
                if (Matrix[numberOfReel, i] == element)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Geter za element iz matrice na poziciji [i,j]
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Vraća element iz matrice na poziciji [i,j]</returns>
        public int GetElement(int i, int j)
        {
            return Matrix[i, j];
        }

        /// <summary>
        /// Daje dobitni element za liniju
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public int GetWinningElementForLine(int line)
        {
            return Matrix[0, GlobalData.GameLineVegasHot[line - 1, 0]];
        }

        /// <summary>
        /// Pretvata niz bajtova u matricu.
        /// </summary>
        /// <param name="array"></param>
        public virtual void FromByteArray(byte[] array)
        {
            if (array.Length != 16)
            {
                return;
            }
            var next = 0;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (next % 2 == 0)
                    {
                        Matrix[i, j] = array[next / 2] >> 4;
                    }
                    else
                    {
                        Matrix[i, j] = array[next / 2] & 0x0F;
                    }
                    next++;
                }
            }
        }

        /// <summary>
        /// Transformiše dvodimenzionalni niz u matricu.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArray(int[,] matrix)
        {
            for (var i = 0; i < 3; i++)
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
