using MathForGames.BasicGameData;
using MathForGames.GameVegasHot;

namespace MathForGames.GameMagicFruits
{
    public class MatrixMagicFruits : MatrixVegasHot
    {
        #region Constructors

        public MatrixMagicFruits()
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
        private LineMagicFruits GetLine(int numberOfLine)
        {
            var line = new LineMagicFruits();
            for (var i = 0; i < 3; i++)
            {
                line.SetElement(i, Matrix[i, GlobalData.GameLineVegasHot[numberOfLine - 1, i]]);
            }
            return line;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="numberOfLine"></param>
        /// <returns></returns>
        public override int CalculateWinOfLine(int numberOfLine)
        {
            var line = GetLine(numberOfLine);
            return line.CalculateLineWin();
        }

        #endregion
    }
}
