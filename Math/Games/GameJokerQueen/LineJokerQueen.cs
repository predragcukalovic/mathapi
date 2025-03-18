using MathForGames.GameVegasHot;

namespace GameJokerQueen
{
    class LineJokerQueen : LineVegasHot
    {
        #region Constructor
        public LineJokerQueen()
            : base()
        {
        }
        #endregion


        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <returns></returns>
        public override int CalculateLineWin()
        {
            if (Line[0] == 0)
            {
                if (Line[1] == 0 || Line[2] == 0)
                    return MatrixJokerQueen.WinForLinesJokerQueen[Line[1] + Line[2]];
                if (Line[1] == Line[2])
                    return MatrixJokerQueen.WinForLinesJokerQueen[Line[1]];
            }
            else if (Line[2] == 0)
            {
                if (Line[1] == Line[0])
                    return MatrixJokerQueen.WinForLinesJokerQueen[Line[1]];
                if (Line[1] == 0)
                    return MatrixJokerQueen.WinForLinesJokerQueen[Line[0]];
            }
            else if (Line[0] == Line[2] && (Line[1] == 0 || Line[1] == Line[2]))
                return MatrixJokerQueen.WinForLinesJokerQueen[Line[0]];

            return 0;
        }

        public int GetPotentialWinningElement()
        {
            int i = 0, winningElementId = 0;
            while (winningElementId == 0 && i <= 2)
            {
                winningElementId += Line[i];
                i++;
            }

            return winningElementId;
        }

        public byte[] GetLinesPositions(int[,] lines, int lineNumber, int wild, int element)
        {
            var positionsArray = new byte[3];
            var i = 0;
            while (i < 3 && (Line[i] == wild || Line[i] == element))
            {
                positionsArray[i] = (byte)(lines[lineNumber - 1, i] * 3 + i);
                i++;
            }
            return positionsArray;
        }
        #endregion
    }

}
