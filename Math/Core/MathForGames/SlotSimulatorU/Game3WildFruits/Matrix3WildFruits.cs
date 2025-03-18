using MathForGames.BasicGameData;
using MathForGames.GameVegasHot;
using RNGUtils.RandomData;

namespace MathForGames.Game3WildFruits
{
    public class Matrix3WildFruits : MatrixVegasHot
    {
        #region Private methods

        /// <summary>
        /// Daje liniju u zavisnosti od rilova.
        /// </summary>
        /// <param name="reel1"></param>
        /// <param name="reel2"></param>
        /// <param name="reel3"></param>
        /// <returns></returns>
        private Line3WildFruits GetLine(int reel1, int reel2, int reel3)
        {
            var line = new Line3WildFruits();
            line.SetElement(0, Matrix[0, reel1]);
            line.SetElement(1, Matrix[1, reel2]);
            line.SetElement(2, Matrix[2, reel3]);
            return line;
        }

        #endregion

        #region Public methods

        public Line3WildFruits GetLine(int lineNumber)
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
            if (Matrix[0, (line - 1) / 9] != 0)
            {
                return Matrix[0, (line - 1) / 9];
            }
            if (Matrix[0, ((line - 1) / 3) % 3] != 0)
            {
                return Matrix[1, ((line - 1) / 3) % 3];
            }
            return Matrix[2, (line - 1) % 3];
        }

        /// <summary>
        /// Daje dobitak za Mystery.
        /// </summary>
        /// <returns></returns>
        public int GetMysteryWin()
        {
            if (GetNumberOfElement(9) != 3)
            {
                return 0;
            }

            return LineWinsForGames.WinForMystery3WildFruits[SoftwareRng.Next(LineWinsForGames.WinForMystery3WildFruits.Length)];
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
