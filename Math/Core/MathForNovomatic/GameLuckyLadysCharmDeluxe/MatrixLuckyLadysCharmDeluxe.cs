using MathForGames.GamePostman;
using System;
using MathForGames.BasicGameData;

namespace MathForNovomatic.GameLuckyLadysCharmDeluxe
{
    public enum LuckyLadysCharmSymbols
    {
        LuckyLady = 0,
        LadyBug = 10,
        RabbitsFoot = 6,
        HorseShoe = 1,
        Coin = 3,
        Clover = 9,
        A = 8,
        K = 4,
        Q = 11,
        J = 7,
        T = 5,
        Nine = 12,
        Hands = 2
    }

    public class MatrixLuckyLadysCharmDeluxe : MatrixPostman
    {
        #region Public properties

        public const int GRATIS_GAMES = 15;
        public const int GRATIS_MULTIPLICATOR = 3;

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

        public static readonly int[,] WinForLines =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 20, 100, 400},
            {0, 0, 0, 0, 0},
            {0, 0, 15, 75, 250},
            {0, 0, 10, 50, 125},
            {0, 0, 5, 25, 100},
            {0, 2, 25, 125, 750},
            {0, 0, 5, 25, 100},
            {0, 0, 10, 50, 125},
            {0, 0, 15, 75, 250},
            {0, 2, 25, 125, 750},
            {0, 0, 5, 25, 100},
            {0, 2, 5, 25, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };

        public static readonly int[] WinForWilds = { 0, 10, 250, 2500, 9000 };
        public static readonly int[] WinForScatters = { 0, 2, 5, 20, 500 };

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GameLines).CalculateLineWin(WinForLines, WinForWilds, (byte)LuckyLadysCharmSymbols.LuckyLady, 2);
        }

        #endregion
    }
}
