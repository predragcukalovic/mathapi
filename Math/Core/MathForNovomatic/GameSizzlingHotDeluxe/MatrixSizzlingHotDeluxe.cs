using MathForGames.GameHotParty;

namespace MathForNovomatic.GameSizzlingHotDeluxe
{
    public class MatrixSizzlingHotDeluxe : MatrixHotParty
    {
        #region Public properties Sizzling Hot Deluxe

        public static readonly int[,] GameLines =
        {
            {1, 1, 1, 1, 1 },
            {0, 0, 0, 0, 0 },
            {2, 2, 2, 2, 2 },
            {0, 1, 2, 1, 0 },
            {2, 1, 0, 1, 2 }
        };

        public static readonly int[,] WinForLinesNovomaticSizzlingHotDeluxe =
        {
            {0, 0, 100, 1000, 5000},
            {0, 0, 50, 200, 500},
            {0, 0, 50, 200, 500},
            {0, 0, 20, 50, 200},
            {0, 0, 20, 50, 200},
            {0, 0, 20, 50, 200},
            {0, 5, 20, 50, 200},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };

        public static readonly int[] WinForScattersNovomaticSizzlingHotDeluxe = { 0, 0, 2, 10, 50 };

        public MatrixSizzlingHotDeluxe() : base()
        {
            Bonus = 0;
        }

        #endregion

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GameLines)
                .CalculateLineWin(WinForLinesNovomaticSizzlingHotDeluxe,
                null, -1, 1);
        }
    }
}
