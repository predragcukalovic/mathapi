using MathForGames.GameMagicOfTheRing;
using MathForGames.GameSpellbook;

namespace MathForNovomatic.GameBookOfRaDeluxe
{
    public enum BookOfRaSymbols
    {
        /// <summary>
        /// Book (Scatter)
        /// </summary>
        Book = 0,
        /// <summary>
        /// Ten card
        /// </summary>
        Ten = 9,
        /// <summary>
        /// J card
        /// </summary>
        J = 8,
        /// <summary>
        /// Q card
        /// </summary>
        Q = 7,
        /// <summary>
        /// K card
        /// </summary>
        K = 6,
        /// <summary>
        /// A card
        /// </summary>
        A = 5,
        /// <summary>
        /// Scarab (bubacka)
        /// </summary>
        Scarab = 4,
        /// <summary>
        /// Statue
        /// </summary>
        Statue = 3,
        /// <summary>
        /// Mummies (Faraon)
        /// </summary>
        Mummy = 2,
        /// <summary>
        /// Explorer (Indiana Jones)
        /// </summary>
        Person = 1,
    }

    public class MatrixBookOfRa : MatrixSpellbook
    {
        #region Public properties

        public new const int GRATIS_GAMES = 10;

        public static readonly int[,] GameLines =
        {
            {1,1,1,1,1},
            {0,0,0,0,0},
            {2,2,2,2,2},
            {0,1,2,1,0},
            {2,1,0,1,2},
            {1,2,2,2,1},
            {1,0,0,0,1},
            {2,2,1,0,0},
            {0,0,1,2,2},
            {2,1,1,1,0},
        };

        public static readonly int[,] WinForLinesBookOfRaDeluxe =
        {
            {0, 0, 0, 0, 0},
            {0, 10, 100, 1000, 5000},
            {0, 5, 40, 400, 2000},
            {0, 5, 30, 100, 750},
            {0, 5, 30, 100, 750},
            {0, 0, 5, 40, 150},
            {0, 0, 5, 40, 150},
            {0, 0, 5, 25, 100},
            {0, 0, 5, 25, 100},
            {0, 0, 5, 25, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };

        public static readonly int[] WinForGratisBookOfRa = { 0, 0, 2, 20, 200 };
        public static readonly int[] WinForWildsBookOfRaDeluxe = { 0, 10, 100, 1000, 5000 };

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber).CalculateLineWin(WinForLinesBookOfRaDeluxe, WinForWildsBookOfRaDeluxe, (byte)BookOfRaSymbols.Book, 1);
        }

        /// <summary>
        /// uzima liniju iz matrice
        /// </summary>
        /// <param name="lineNumber">broj linije, 1 -- 15</param>
        /// <returns>vraća liniju pod datim brojem</returns>
        protected LineMagicOfTheRing GetLine(int lineNumber)
        {
            if (lineNumber < 1 || lineNumber > 10)
            {
                return null;
            }

            var line = new LineMagicOfTheRing();
            for (var i = 0; i < 5; i++)
            {
                line.SetElement(i, GetElement(i, GameLines[lineNumber - 1, i]));
            }
            return line;
        }

        /// <summary>
        /// Računa dobitak linije sa bonus simbolom.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="gratisElement"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber, int gratisElement)
        {
            if (gratisElement == 0)
            {
                return CalculateWinLine(lineNumber);
            }
            var line = GetLine(lineNumber);
            for (var i = 0; i < 5; i++)
            {
                if (line.GetElement(i) == gratisElement)
                {
                    line.SetElement(i, 15);
                }
            }
            return line.CalculateLineWin(WinForLinesBookOfRaDeluxe, WinForWildsBookOfRaDeluxe, (byte)BookOfRaSymbols.Book, 1);
        }

        #endregion
    }
}
