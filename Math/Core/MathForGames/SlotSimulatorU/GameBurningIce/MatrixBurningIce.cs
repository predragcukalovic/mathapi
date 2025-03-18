using MathForGames.GameVegasHot;
using RNGUtils.RandomData;

namespace MathForGames.GameBurningIce
{
    public class MatrixBurningIce : MatrixVegasHot
    {
        private int _MysteryWin = 0;

        #region Private methods

        /// <summary>
        /// Daje liniju u zavisnosti od rilova.
        /// </summary>
        /// <param name="reel1"></param>
        /// <param name="reel2"></param>
        /// <param name="reel3"></param>
        /// <returns></returns>
        private LineBurningIce GetLine(int reel1, int reel2, int reel3)
        {
            var line = new LineBurningIce();
            line.SetElement(0, Matrix[0, reel1]);
            line.SetElement(1, Matrix[1, reel2]);
            line.SetElement(2, Matrix[2, reel3]);
            return line;
        }

        #endregion

        #region Public methods

        public LineBurningIce GetLine(int lineNumber)
        {
            lineNumber--;
            var r1 = lineNumber / 9;
            var r2 = (lineNumber / 3) % 3;
            var r3 = lineNumber % 3;
            return GetLine(r1, r2, r3);
        }

        /// <summary>
        /// Daje dobitni element za liniju
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public new int GetWinningElementForLine(int line)
        {
            return Matrix[0, (line - 1) / 9];
        }

        /// <summary>
        /// Daje dobitak za Mystery koji je učitan iz kombinacije (za potrebe simulacije).
        /// </summary>
        /// <returns></returns>
        public int GetMysteryWinSimulation()
        {
            return _MysteryWin;
        }

        /// <summary>
        /// Pretvata niz bajtova u matricu.
        /// </summary>
        /// <param name="array"></param>
        public override void FromByteArray(byte[] array)
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
            _MysteryWin = array[15];
        }

        /// <summary>
        /// Daje dobitak za Mystery.
        /// </summary>
        /// <returns></returns>
        public int GetMysteryWin()
        {
            if (GetNumberOfElement(8) != 3)
            {
                return 0;
            }

            var wins = new[] { 2, 3, 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            return wins[SoftwareRng.Next(wins.Length)];
        }

        /// <summary>
        /// Daje matricu kao niz od 9 bajtova.
        /// </summary>
        /// <returns></returns>
        public byte[] ToMatrixByteArray()
        {
            var array = new byte[9];
            var index = 0;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    array[index++] = (byte)Matrix[i, j];
                }
            }

            return array;
        }

        #endregion
    }
}
