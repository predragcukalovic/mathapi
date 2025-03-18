using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;
using System.Collections.Generic;

namespace MathForGames.GameCrystalsOfMagic
{
    public class MatrixCrystalsOfMagic : Matrix
    {
        #region Public fields

        public const int MAX_WIN = 5000;

        #endregion

        #region Constructor or Singleton implementation

        public MatrixCrystalsOfMagic()
            : base(5)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            var line = GetLine(lineNumber, GlobalData.GameLineExtra);
            if (line.GetElement(4) == 14)
            {
                line.SetElement(4, 0);
            }
            return line.CalculateLineWin(LineWinsForGames.WinForLinesCrystalsOfMagic, LineWinsForGames.WinForWildsCrystalsOfMagic, 0, 1);
        }

        public new void FromMatrixArray(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, (j + 1) % 5]);
                }
            }
        }

        /// <summary>
        /// Daje objekat za A bonus.
        /// </summary>
        /// <param name="addArray"></param>
        /// <returns></returns>
        public static object GetFirstBonusData(byte[] addArray)
        {
            var open = new List<int>();
            var possible = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            for (var i = 1; i <= 8; i++)
            {
                if (addArray[i] != 0)
                {
                    possible.Remove(addArray[i]);
                    open.Add(i);
                }
            }
            var obj = new
            {
                openFields = open.ToArray(),
                possibleWins = possible.ToArray(),
                theEnd = addArray[9] == 1
            };

            return obj;
        }

        #endregion
    }
}
