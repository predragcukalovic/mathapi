using MathForGames.BasicGameData;
using MathForGames.GameVegasHot;

namespace MathForGames.GameHot777
{
    public class MatrixHot777 : MatrixVegasHot
    {
        #region Constructors

        public MatrixHot777()
        {
            Matrix = new int[3, 3];
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Uzima liniju iz matrice.
        /// </summary>
        /// <param name="numberOfLine"></param>
        /// <returns></returns>
        private LineHot777 GetLine(int numberOfLine)
        {
            var line = new LineHot777();
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
        /// <param name="numberOfLine">Broj linije</param>
        /// <returns></returns>
        public override int CalculateWinOfLine(int numberOfLine)
        {
            var line = GetLine(numberOfLine);
            return line.CalculateLineWin();
        }

        #endregion
    }
}
