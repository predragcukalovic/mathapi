using System.Collections.Generic;
using MathBaseProject.BaseMathData;
using MathCombination.CombinationData;
using MathForUnicornGames.BasicUnicornData;
using MathForUnicornGames.GameHavanaDice;
using MathForUnicornGames.GameReelDice;
using MathForUnicornGames.GameTwentyFruits;
using MathForUnicornGames.GameVegasDice;
using MathForUnicornGames.GameWildParadice;
using MathForUnicornGames.GameIslandRespins;
using MathForUnicornGames.GameGreatWhale;
using Papi.GameServer.Utils.Enums;

namespace CombinationUtils.UnicornCombinationData
{
    public class CombinationUnicorn : ICombination
    {
        #region Public properties

        public byte[,] Matrix { get; protected set; }
        public bool GratisGame { get; protected set; }
        public int NumberOfGratisGames { get; protected set; }
        public int WinFor2 { get; protected set; }
        public byte[] MultiplyFor2 { get; protected set; }
        public byte[] MultiplyFor2Alpinist { get; protected set; }
        public byte[] PositionFor2 { get; protected set; }
        public byte NumberOfWinningLines { get; protected set; }
        public LineInfo[] LinesInformation { get; protected set; }
        public byte[] GratisGamesValues { get; protected set; }
        public byte[] GratisGamesPositions { get; protected set; }
        public byte AdditionalInformation { get; protected set; }
        public int TotalWin { get; protected set; }
        public byte[] AdditionalArray { get; protected set; }
        public List<ICombination> CascadeList { get; protected set; }

        #endregion

        #region Constructor implementation

        /// <summary>
        /// Konstruktor za kombinaciju
        /// </summary>
        public CombinationUnicorn()
        {
            Matrix = new byte[5, 3];
            PositionFor2 = new byte[5];
            MultiplyFor2 = new byte[4];
            MultiplyFor2Alpinist = new byte[5];
            GratisGamesValues = new byte[5];
            GratisGamesPositions = new byte[5];
            CascadeList = null;
        }

        #endregion

        #region Private properties

        protected const byte EXTRA_LINE = 254;

        #endregion

        #region Private methods

        /// <summary>
        /// Kreira niz LinesInformation.
        /// </summary>
        /// <param name="matrix">Matrica</param>
        /// <param name="numberOfLines">Broj linija</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Množilac za vreme gratis igara</param>
        /// <param name="wild">Wild element</param>
        /// <param name="winForWild">Dobitak za wild</param>
        /// <param name="gameLines">Linije na koje se igra</param>
        /// <param name="addExtraLine">Dobitak elemenata van linije</param>
        /// <param name="extraSymbol">Simbol koji daje dobitak van linije</param>
        protected void CreateLinesInformations(Matrix matrix, int numberOfLines, int bet, int gratisGame,
            int wild, int[] winForWild, int[,] gameLines, int addExtraLine = 0, int extraSymbol = -1)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.CalculateWinLine(i);
                if (win == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet * gratisGame,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, wild, winForWild, win, gameLines)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, gameLines).GetLinesPositions(gameLines, i, wild, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (addExtraLine > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(extraSymbol),
                    Id = EXTRA_LINE,
                    Win = addExtraLine * bet * gratisGame * numberOfLines,
                    WinningElement = (byte)extraSymbol
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Kreira niz LinesInformation.
        /// </summary>
        /// <param name="matrix">Matrica</param>
        /// <param name="numberOfLines">Broj linija</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Množilac za vreme gratis igara</param>
        /// <param name="gameLines">Linije na koje se igra</param>
        /// <param name="specialTopSymbol">Specijalni simbol koji moze da zameni regularni simbol kao wild i daje gratis igre</param>
        /// <param name="regularTopSymbol">Regularni simbol koji moze biti zamenjen specijalnim simbolom</param>
        protected void CreateLinesInformationIslandRespin(MatrixIslandRespins matrix, int numberOfLines, int bet, int gratisGame, int[,] gameLines, int specialTopSymbol, int regularTopSymbol)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.CalculateWinLine(i);
                if (win == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet * gratisGame,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, gameLines, win, specialTopSymbol, regularTopSymbol)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, gameLines)
                    .GetLinesPositions(gameLines, i, (lineInfo.WinningElement == regularTopSymbol) ? specialTopSymbol : -1, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }

            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Kreira niz LinesInformation.
        /// </summary>
        /// <param name="matrix">Matrica</param>
        /// <param name="numberOfLines">Broj linija</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Množilac za vreme gratis igara</param>
        /// <param name="wild">Wild element</param>
        /// <param name="winForWild">Dobitak za wild</param>
        /// <param name="gameLines">Linije na koje se igra</param>
        /// <param name="addExtraLine">Dobitak elemenata van linije</param>
        /// <param name="extraSymbol">Simbol koji daje dobitak van linije</param>
        protected void CreateLinesInformationGreatWhale(MatrixGreatWhale matrix, int numberOfLines, int bet, int gratisGame,
            int wild, int[] winForWild, int[,] gameLines, int addExtraLine = 0, int extraSymbol = -1)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.CalculateWinLine(i);
                if (win == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet * gratisGame,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, wild, winForWild, win, gameLines)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, gameLines).GetLinesPositions(gameLines, i, wild, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (addExtraLine > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(extraSymbol),
                    Id = EXTRA_LINE,
                    Win = addExtraLine * bet * gratisGame * numberOfLines,
                    WinningElement = (byte)extraSymbol
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Popunjava matricu Matrix vrednostima iz matrice.
        /// </summary>
        /// <param name="matrix"></param>
        protected void FillMatrixArray(Matrix matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Transformiše matricu za igru 'VegasDice' i njene klonove('FruitIsland ') u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixVegasDice matrix, int numberOfLines, int bet)
        {
            FillMatrixArray(matrix);

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, UnicornGlobalData.GameLine);
        }

        /// <summary>
        /// Transformiše matricu za igru 'ReelDice' i njene klonove ('RainbowSevens')u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixReelDice matrix, int numberOfLines, int bet)
        {
            FillMatrixArray(matrix);

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, UnicornGlobalData.GameLine);
        }

        /// <summary>
        /// Transformiše matricu za igru 'WildParadice' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixWildParadice matrix, int numberOfLines, int bet)
        {
            for (var i = 0; i < 5; i++)
            {
                var index = -1;
                for (var j = 0; j < 3; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if (index >= 0)
                    {
                        matrix.SetElement(i, j, 0);
                    }
                    else if (Matrix[i, j] == 0)
                    {
                        index = j;
                    }
                }
                if (index > 0)
                {
                    for (int j = 0; j < index; j++)
                    {
                        matrix.SetElement(i, j, 0);
                    }
                }
            }
            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, null, UnicornGlobalData.GameLine);
        }

        /// <summary>
        /// Transformiše matricu za igru 'HavanaDice' u kombinaciju
        /// Simbole ciji je id jednak 12 ili 13 menja simbolom 0, nakon upisivanja u polje Matrix
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixHavanaDice matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if ((Matrix[i, j] | 1) == 13)
                    {
                        matrix.SetElement(i, j, 0);
                    }
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, MatrixHavanaDice.WinForWildHavanaDice, UnicornGlobalData.GameLineShifted);
        }

        /// <summary>
        /// Transformiše matricu za igru 'TwentyFruits' i njene klonove('TwentyDice ') u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixTwentyFruits matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, UnicornGlobalData.GameLineTwenties);
        }

        /// <summary>
        /// Transformiše matricu za igru 'GreatWhale' i njene klonove u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        /// <param name="previousAdditionalArray"></param>
        /// <param name="fslFileId"></param>
        public void MatrixToCombination(MatrixGreatWhale matrix, int numberOfLines, int bet, bool gratisGame, ref byte[] previousAdditionalArray, byte fslFileId)
        {
            Matrix = new byte[5, 5];
            AdditionalArray = new byte[50];
            AdditionalInformation = gratisGame ? fslFileId : (byte)0;

            var counter = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if (gratisGame && (previousAdditionalArray[counter] == 1 || previousAdditionalArray[counter + 25] == 1))
                    {
                        Matrix[i, j] = 1;
                    }
                    AdditionalArray[counter++] = (gratisGame && Matrix[i, j] == 1) ? (byte)1 : (byte)0;
                }
            }
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    AdditionalArray[counter++] = (gratisGame && matrix.GetFromStickyWildMatrix(i, j) == 1) ? (byte)1 : (byte)0;
                }
            }

            GratisGame = matrix.GetNumberOfElement(0) >= 3;
            WinFor2 = 0;
            NumberOfGratisGames = GratisGame ? MatrixGreatWhale.GRATIS_GAMES : 0;

            matrix.FromMatrixArrayGreatWhaleByte(Matrix);

            CreateLinesInformationGreatWhale(matrix, numberOfLines, bet, 1, 1, MatrixGreatWhale.WinForWildGreatWhale, UnicornGlobalData.GameLineShifted, 0, 0);
            previousAdditionalArray = AdditionalArray;
        }

        /// <summary>
        /// Transformiše matricu za igru 'IslandRespin' i njene klonove('DiceRespin ') u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"> Da li je trenutna igra gratis igra</param>
        public void MatrixToCombination(MatrixIslandRespins matrix, int numberOfLines, int bet, bool gratisGame)
        {
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }
            var specialTopSymbolsNumber = matrix.GetNumberOfElement(0);
            GratisGame = gratisGame ? specialTopSymbolsNumber >= 1 : specialTopSymbolsNumber >= 2;
            NumberOfGratisGames = GratisGame ? 1 : 0;

            CreateLinesInformationIslandRespin(matrix, numberOfLines, bet, 1, UnicornGlobalData.GameLineShifted,
                0, 1);
        }

        public object ToGameData(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis,
            ICombination combination, bool json)
        {
            /*switch (game)
            {
                case Games.UnicornVegasDice:
                case Games.UnicornFruitIsland:
                    return GameVegasDiceConversion.ToSlotDataResV3(combination);
                case Games.UnicornReelDice:
                case Games.UnicornRainbowSevens:
                    return GameReelDiceConversion.ToSlotDataResV3(combination);
                case Games.UnicornWildParadise:
                case Games.UnicornTheCrownFruit:
                    return GameWildParadiceConversion.ToSlotDataResV3(combination);
                case Games.UnicornHavanaDice:
                case Games.UnicornCasinoFruits:
                    return GameHavanaDiceConversion.ToSlotDataResV3(combination);
                case Games.UnicornTwentyFruits:
                case Games.UnicornTwentyDice:
                    return GameTwentyFruitsConversion.ToSlotDataResV3(combination);
                case Games.UnicornIslandRespins:
                case Games.UnicornDiceRespins:
                    return GameIslandRespinsConversion.ToSlotDataResV3(combination);
                case Games.UnicornGreatWhale:
                    return GameGreatWhaleConversion.ToSlotDataResV3(combination);
                default:
                    return null;
            }*/
            return null;
        }

        public bool IsBonus(Games game, bool gratisGame, byte addInfo, byte[] addArray)
        {
            return false;
        }

        public byte[] ToByteArray(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis,
            ICombination combination)
        {
            return null;
        }

        public object ToJson(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis, ICombination combination)
        {
            return null;
        }

        #endregion

    }
}
