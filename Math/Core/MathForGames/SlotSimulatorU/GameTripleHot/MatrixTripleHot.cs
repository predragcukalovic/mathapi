﻿using MathForGames.BasicGameData;
using MathForGames.GameVegasHot;

namespace MathForGames.GameTripleHot
{
    public class MatrixTripleHot : MatrixVegasHot
    {
        #region Constructors

        public MatrixTripleHot()
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
        private LineTripleHot GetLine(int numberOfLine)
        {
            var line = new LineTripleHot();
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
