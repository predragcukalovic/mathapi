using MathForGames.BasicGameData;
using MathForGames.GameVegasHot;

namespace GameTripleFieldsOfLuck
{
    public class MatrixTripleFieldsOfLuck : MatrixVegasHot
    {
        #region Public fields

        public static readonly int[] WinForTripleFieldsOfLuck = { 200, 200, 100, 30, 30, 15, 15, 15, 15 };

        #endregion

        #region Public methods

        /// <summary>
        /// Daje liniju iz matrice.
        /// </summary>
        /// <param name="numberOfLine"></param>
        /// <returns></returns>
        public LineTripleFieldsOfLuck GetLine(int numberOfLine)
        {
            var line = new LineTripleFieldsOfLuck();
            for (var i = 0; i < 3; i++)
            {
                line.SetElement(i, Matrix[i, GlobalData.GameLineVegasHot[numberOfLine - 1, i]]);
            }
            return line;
        }

        /// <summary>
        /// Daje dobitni element za liniju
        /// </summary>
        /// <param name="numberOfLine"></param>
        /// <returns></returns>
        public int GetWinningElementForLineTripleFieldsOfLuck(int numberOfLine)
        {
            return GetLine(numberOfLine).FirstNonWild();
        }

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="numberOfLine"></param>
        /// <returns></returns>
        public override int CalculateWinOfLine(int numberOfLine)
        {
            return GetLine(numberOfLine).CalculateLineWin();
        }

        #endregion
    }
}
