using MathForGames.BasicGameData;
using RNGUtils.RandomData;

namespace GameJokerQueen
{
    public class MatrixJokerQueen
    {
        #region Private fields

        private readonly int[,] _Matrix;

        #endregion

        #region Constructor

        public MatrixJokerQueen()
        {
            _Matrix = new int[3, 5];
        }
        #endregion

        #region Public properties

        public static readonly int[] WinForLinesJokerQueen = { 80, 25, 20, 15, 7, 6, 5, 4, 2 };
        private static readonly int[] _MultipliersJokerQueen = { 10, 5, 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };

        #endregion

        public int CalculateLineWin(int lineNumber)
        {
            return GetLine(lineNumber).CalculateLineWin();
        }

        public int GetWinningElementForLine(int lineNumber)
        {
            return GetLine(lineNumber).GetPotentialWinningElement();
        }

        public byte[] GetPositionArrayForLine(int lineNumber, int element)
        {
            return GetLine(lineNumber).GetLinesPositions(GlobalData.GameLineVegasHot, lineNumber, 0, element);
        }


        private LineJokerQueen GetLine(int lineId)
        {
            var line = new LineJokerQueen();
            line.SetElement(0, _Matrix[0, GlobalData.GameLineVegasHot[lineId - 1, 0] + 1]);
            line.SetElement(1, _Matrix[1, GlobalData.GameLineVegasHot[lineId - 1, 1] + 1]);
            line.SetElement(2, _Matrix[2, GlobalData.GameLineVegasHot[lineId - 1, 2] + 1]);
            return line;
        }

        /// <summary>
        /// Transformiše dvodimenzionalni niz u matricu.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArray(int[,] matrix)
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    _Matrix[i, j] = matrix[i, j];
                }
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

        public int GetNumberOfElement(int element)
        {
            var counter = 0;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (element == _Matrix[i, j])
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }
        #region Multipliers and Respin related methods

        public bool CanRespin(ref byte reel1, ref byte reel2)
        {
            var arrayOfReelSymbol = new int[3];
            var arrayOfPotential = new bool[3];

            for (int i = 0; i < 3; i++)
            {
                var index = 1;
                var element = _Matrix[i, index];
                arrayOfPotential[i] = true;
                while (element == 0 && index < 4)
                {
                    element = _Matrix[i, index++];
                }
                for (int j = index; j < 4; j++)
                {
                    if (_Matrix[i, j] != 0 && element != _Matrix[i, j])
                    {
                        arrayOfPotential[i] = false;
                    }
                }

                arrayOfReelSymbol[i] = element;
            }
            if (arrayOfPotential[0] && arrayOfPotential[1] && (arrayOfReelSymbol[0] == arrayOfReelSymbol[1] || arrayOfReelSymbol[0] == 0 || arrayOfReelSymbol[1] == 0))
            {
                reel1 = 0;
                reel2 = 1;
            }
            if (arrayOfPotential[0] && arrayOfPotential[2] && (arrayOfReelSymbol[0] == arrayOfReelSymbol[2] || arrayOfReelSymbol[0] == 0 || arrayOfReelSymbol[2] == 0))
            {
                if (!((reel1 + reel2 < 4) && arrayOfReelSymbol[1] < arrayOfReelSymbol[2]))
                {
                    reel1 = 0;
                    reel2 = 2;
                }
            }
            if (arrayOfPotential[1] && arrayOfPotential[2] && (arrayOfReelSymbol[1] == arrayOfReelSymbol[2] || (arrayOfReelSymbol[1] == 0 || arrayOfReelSymbol[2] == 0)))
            {
                if ((reel1 + reel2 < 3) && arrayOfReelSymbol[0] > arrayOfReelSymbol[2] && arrayOfReelSymbol[0] > arrayOfReelSymbol[1])
                {
                    reel1 = 1;
                    reel2 = 2;
                }
                reel1 = 1;
                reel2 = 2;
            }

            if (reel1 + reel2 < 4) return true;
            return false;
        }

        public void InformationForAdditionalArray(byte reel1, byte reel2, ref byte[] arr)
        {
            arr[0] = reel1;
            arr[1] = reel2;
            for (int i = 1; i <= 3; i++)
            {
                arr[i + 1] = (byte)_Matrix[reel1, i];
                arr[i + 3 + 1] = (byte)_Matrix[reel2, i];
            }
        }
        public bool CanMultiply()
        {
            var counter = 0;
            var element = -1;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (_Matrix[i, j] == 0)
                    {
                        counter++;
                    }
                    else
                    {
                        if (_Matrix[i, j] == element)
                        {
                            counter++;
                        }
                        else if (element == -1)
                        {
                            element = _Matrix[i, j];
                            counter++;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return counter == 9;
        }

        public static int rngMultiplier()
        {
            var rnd = SoftwareRng.Next(0, 12);
            return _MultipliersJokerQueen[rnd];
        }

        public void SetElement(int i, int j, byte value)
        {
            _Matrix[i, j] = value;
        }

        #endregion

    }
}
