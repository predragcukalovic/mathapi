using CombinationUtils.UnicornCombinationData;
using MathCombination.CombinationData;
using MathForUnicornGames.BasicUnicornData;
using MathForUnicornGames.Game10JingleFruits;
using MathForUnicornGames.Game20HotStrike;
using MathForUnicornGames.Game20MegaFlames;
using MathForUnicornGames.Game20SuperFlames;
using MathForUnicornGames.Game40FruitReels;
using MathForUnicornGames.Game40MegaFlames;
using MathForUnicornGames.Game5HotStrike;
using MathForUnicornGames.GameBigSpinSevens;
using MathForUnicornGames.GameBikiniFruits;
using MathForUnicornGames.GameBuffaloSevens;
using MathForUnicornGames.GameChristmasPresents;
using MathForUnicornGames.GameCoyoteSevens;
using MathForUnicornGames.GameEpicMegaCash;
using MathForUnicornGames.GameFastFruits;
using MathForUnicornGames.GameFireStars;
using MathForUnicornGames.GameFrootClassic;
using MathForUnicornGames.GameFruitWildLines;
using MathForUnicornGames.GameGoldLine;
using MathForUnicornGames.GameGreatWhale;
using MathForUnicornGames.GameHitLine;
using MathForUnicornGames.GameIslandRespins;
using MathForUnicornGames.GameMiniMegaCash;
using MathForUnicornGames.GameMoneyStandardWild;
using MathForUnicornGames.GamePumpkinHorror;
using MathForUnicornGames.GameSimplySevens;
using MathForUnicornGames.GameStickyHot;
using MathForUnicornGames.GameSurfinHeat;
using MathForUnicornGames.GameTopPrizeWilds;
using MathForUnicornGames.GameWinterFruits;
using RNGUtils.RandomData;
using System.Collections.Generic;
using System.Linq;
using MathForUnicornGames.GameBigSpinSevens;
using MathForUnicornGames.GameBuffaloSevens;
using MathForUnicornGames.GameFrootClassic;
using MathForUnicornGames.GameStickyHot;
using MathForUnicornGames.GamePumpkinHorror;
using MathForUnicornGames.GameHitLine;
using MathForUnicornGames.Game20SuperFlames;
using MathForUnicornGames.GameEpicMegaCash;
using MathForUnicornGames.GameTopPrizeWilds;
using MathForUnicornGames.GameBigHitSevens;

namespace CombinationUnicornUtils
{
    public class CombinationUnicornNew : CombinationUnicorn
    {
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
        protected void CreateLinesInformation20MegaFlames(Matrix20MegaFlames matrix, int numberOfLines, int bet, int gratisGame,
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
        /// <param name="wild">Wild element</param>
        /// <param name="winForWild">Dobitak za wild</param>
        /// <param name="gameLines">Linije na koje se igra</param>
        /// <param name="addExtraLine">Dobitak elemenata van linije</param>
        /// <param name="extraSymbol">Simbol koji daje dobitak van linije</param>
        protected void CreateLinesInformationIslandRespins2(MatrixIslandRespins2 matrix, int numberOfLines, int bet,
            int wild, int[] winForWild, int[,] gameLines, bool addExtraLine, int extraSymbol = -1)
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
                    Win = win * bet,
                    WinningElement = (byte)matrix.GetWinningElement(i, win)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, gameLines).GetPositions(i, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (addExtraLine)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetScatterPositionsArray(extraSymbol),
                    Id = EXTRA_LINE,
                    Win = 0,
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
        /// <param name="wild">Wild element</param>
        /// <param name="winForWild">Dobitak za wild</param>
        /// <param name="gameLines">Linije na koje se igra</param>
        /// <param name="addExtraLine">Dobitak elemenata van linije</param>
        /// <param name="extraSymbol">Simbol koji daje dobitak van linije</param>
        protected void CreateLinesInformations40MegaFlames(Matrix40MegaFlames matrix, int numberOfLines, int bet,
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
                    Win = win * bet,
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
                    Win = addExtraLine * bet * numberOfLines,
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
        /// <param name="wild">Wild element</param>
        /// <param name="winForWild">Dobitak za wild</param>
        /// <param name="gameLines">Linije na koje se igra</param>
        /// <param name="addExtraLine">Dobitak elemenata van linije</param>
        /// <param name="extraSymbol">Simbol koji daje dobitak van linije</param>
        protected void CreateLinesInformationsWinterFruits(MatrixWinterFruits matrix, int numberOfLines, int bet,
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
                    Win = win * bet,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, win)
                };
                lineInfo.WinningPosition = matrix.GetPositions(i, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (addExtraLine > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(extraSymbol),
                    Id = EXTRA_LINE,
                    Win = addExtraLine * bet * numberOfLines,
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
        /// <param name="wild">Wild element</param>
        /// <param name="winForWild">Dobitak za wild</param>
        /// <param name="gameLines">Linije na koje se igra</param>
        /// <param name="extraSymbol">Simbol koji daje dobitak van linije</param>
        protected void CreateLinesInformation20HotStrike(Matrix20HotStrike matrix, int numberOfLines, int bet, int extraSymbol = -1)
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
                    Win = win * bet,
                    WinningElement = (byte)matrix.GetWinningElement(i, win)
                };
                lineInfo.WinningPosition = matrix.GetLinesPositions(i, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            var scaterWin = matrix.GetScatterWin();
            if (scaterWin > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetScatterPositionsArray(extraSymbol),
                    Id = EXTRA_LINE,
                    Win = scaterWin * bet * numberOfLines,
                    WinningElement = (byte)extraSymbol
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Transformiše matricu za igru 'TwentyFruits' i njene klonove('TwentyDice ') u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombinationNew(Matrix20MegaFlames matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformation20MegaFlames(matrix, numberOfLines, bet, 1, -1, null, UnicornGlobalData.GameLineShifted,
                matrix.GetScatterWin(0, Matrix20MegaFlames.WinForScatter20MegaFlames), 0);
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
        public void MatrixToCombinationNew(MatrixGreatWhale matrix, int numberOfLines, int bet, bool gratisGame, ref byte[] previousAdditionalArray, byte fslFileId)
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
                        if (j != 0 && j != 4)
                        {
                            Matrix[i, j] = 1;
                        }
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
        public void MatrixToCombinationNew(MatrixIslandRespins matrix, int numberOfLines, int bet, bool gratisGame)
        {
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
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

        /// <summary>
        /// Transformiše matricu za igru 'MiniMegaCash' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixMiniMegaCash matrix, int numberOfLines, int bet)
        {
            matrix.SetScatters();
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, UnicornGlobalData.GameLineShifted);
            var scatter = matrix.GetScatterMultiplier();
            if (scatter > 0)
            {
                var win = scatter * bet * numberOfLines;
                var li = LinesInformation.ToList();
                li.Add(new LineInfo { Win = win, Id = EXTRA_LINE, WinningElement = 0, WinningPosition = matrix.GetScatterPositionArray() });
                LinesInformation = li.ToArray();
                NumberOfWinningLines++;
                TotalWin += win;
            }
        }

        /// <summary>
        /// Transformiše matricu za igru 'MatrixIslandRespins2' u kombinaciju
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <param name="addInfo"></param>
        public void MatrixToCombination(MatrixIslandRespins2 matrix, int numberOfLines, int bet, bool gratisGame, byte addInfo)
        {
            Matrix = new byte[5, 5];
            var sevenCount = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if (j > 0 && j < 4 && Matrix[i, j] == 0)
                    {
                        sevenCount++;
                    }
                }
            }

            GratisGame = sevenCount > (gratisGame ? 0 : 2);
            NumberOfGratisGames = GratisGame ? 1 : 0;

            CreateLinesInformationIslandRespins2(matrix, numberOfLines, bet, -1, null, UnicornGlobalData.GameLineShifted, GratisGame, 0);

            AdditionalInformation = (byte)(GratisGame ? addInfo + 1 : 0);
        }

        /// <summary>
        /// Transformiše matricu za igru 'SurfinHeat' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGamesLeft"></param>
        /// <param name="addInfo"></param>
        public void MatrixToCombination(MatrixSurfinHeat matrix, int numberOfLines, int bet, int gratisGamesLeft, byte addInfo)
        {
            var gratisGame = gratisGamesLeft > 0;
            Matrix = new byte[5, 5];
            var scat = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if (j > 0 && j < 4 && Matrix[i, j] == 0)
                    {
                        scat++;
                    }
                }
            }

            LineInfo scatLi = null;
            if (scat > 2)
            {
                if (!gratisGame)
                {
                    GratisGame = true;
                    NumberOfGratisGames = 10;
                }
                else
                {
                    scatLi = new LineInfo { Id = EXTRA_LINE, Win = MatrixSurfinHeat.PossibleWinsForScatter[(addInfo & 0x0F) - 1] * bet * numberOfLines, WinningElement = 0, WinningPosition = matrix.GetPositionsArray(0) };
                }
            }

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, UnicornGlobalData.GameLineShifted);
            if (scatLi != null)
            {
                var li = LinesInformation.ToList();
                li.Add(scatLi);
                LinesInformation = li.ToArray();
                TotalWin += scatLi.Win;
                NumberOfWinningLines++;
            }
            WinFor2 = bet;
            if (GratisGame || gratisGamesLeft > 1)
            {
                AdditionalInformation = (byte)((addInfo << 4) + MatrixSurfinHeat.GetScatterProbIndex());
            }
            else
            {
                AdditionalInformation = 0;
            }
        }

        /// <summary>
        /// Transformiše matricu za igru 'Unicorn40MegaFlames' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="numberOfLines"></param>
        public void MatrixToCombination(Matrix40MegaFlames matrix, int bet, int numberOfLines)
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

            CreateLinesInformations40MegaFlames(matrix, numberOfLines, bet, -1, null, UnicornGlobalData.GameLine40MegaFlames,
                matrix.GetNoLineWin(7, Matrix40MegaFlames.WinForScatter40MegaFlames), 7);
        }

        /// <summary>
        /// Transformiše matricu za igru 'UnicornWinterFruits' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="numberOfLines"></param>
        public void MatrixToCombination(MatrixWinterFruits matrix, int bet, int numberOfLines)
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

            CreateLinesInformationsWinterFruits(matrix, numberOfLines, bet, 0, MatrixWinterFruits.WinForWildWinterFruits, UnicornGlobalData.GameLineWinterFruits, matrix.GetNoLineWin(0, MatrixWinterFruits.WinForScatterWinterFruits), 0);
        }

        /// <summary>
        /// Transformiše matricu za igru 'FasFruits' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixFastFruits matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, UnicornGlobalData.GameLineShifted);
        }

        /// <summary>
        /// Transformiše matricu za igru 'MoneyStandardWild' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixMoneyStandardWild matrix, int numberOfLines, int bet)
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

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, MatrixMoneyStandardWild.WinForWildMoneyStandardWild, UnicornGlobalData.GameLineTwenties);
        }

        /// <summary>
        /// Transformiše matricu za igru 'Matrix20HotStrike' u kombinaciju
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        public void MatrixToCombination(Matrix20HotStrike matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformation20HotStrike(matrix, numberOfLines, bet, 0);
        }

        /// <summary>
        /// Transformiše matricu za igru 'CoyoteSevens' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixCoyoteSevens matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, UnicornGlobalData.GameLineShifted);
        }

        /// <summary>
        /// Transformiše matricu za igru 'FruitWildLines' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="wild">Vajld</param>
        public void MatrixToCombination(MatrixFruitWildLines matrix, int numberOfLines, int bet, int wild)
        {
            Matrix = new byte[5, 7];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 7; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            WinFor2 = wild;

            matrix.TransformMatrix(numberOfLines, wild);

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, MatrixFruitWildLines.GameLineFruitWild);
        }

        /// <summary>
        /// Transformiše matricu za igru 'Unicorn40FruitReels' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="numberOfLines"></param>
        public void MatrixToCombination(Matrix40FruitReels matrix, int bet, int numberOfLines)
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
                    Win = win * bet,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, -1, null, win, UnicornGlobalData.GameLineWinterFruits)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, UnicornGlobalData.GameLineWinterFruits).GetLinesPositions(UnicornGlobalData.GameLineWinterFruits, i, -1, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Transformiše matricu za igru '5HotStrike' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(Matrix5HotStrike matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 5];
            var scatNum = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if (j > 0 && j < 4 && Matrix[i, j] == 0)
                    {
                        scatNum++;
                    }
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

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
                    Win = win * bet,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, win)
                };
                lineInfo.WinningPosition = matrix.GetLinesPositions(i, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (scatNum > 2)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetScatterPositionsArray(0),
                    Id = EXTRA_LINE,
                    Win = Matrix5HotStrike.WinForScatter5HotStrike[scatNum - 1] * bet * numberOfLines,
                    WinningElement = 0
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Transformiše matricu za igru 'BikiniFruits' u kombinaciju
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        public void MatrixToCombination(MatrixBikiniFruits matrix, int numberOfLines, int bet, int gratisGamesLeft, byte addInfo)
        {
            var multiplicators = new[] { 2, 3, 5 };
            Matrix = new byte[5, 5];
            var scatCount = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if (j > 0 && j < 4 && Matrix[i, j] == 0)
                    {
                        scatCount++;
                    }
                }
            }

            GratisGame = scatCount >= 3;
            NumberOfGratisGames = GratisGame ? scatCount * 3 : 0;
            if (gratisGamesLeft > 0)
            {
                AdditionalInformation = addInfo;
            }
            else if (GratisGame)
            {
                AdditionalInformation = (byte)scatCount;
            }

            PositionFor2 = new byte[4];
            if (gratisGamesLeft == 0)
            {
                PositionFor2[0] += 1;
                if (!GratisGame)
                {
                    PositionFor2[0] += 2;
                }
                else
                {
                    PositionFor2[1] += 2;
                }
            }
            else
            {
                PositionFor2[(addInfo * 3 - gratisGamesLeft) / addInfo + 1] += 1;
                if (gratisGamesLeft - 1 == 0)
                {
                    PositionFor2[0] += 2;
                }
                else
                {
                    PositionFor2[(addInfo * 3 - (gratisGamesLeft - 1)) / addInfo + 1] += 2;
                }
            }

            TotalWin = 0;
            var mult = gratisGamesLeft > 0 ? multiplicators[(addInfo * 3 - gratisGamesLeft) / addInfo] : 1;
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
                    Win = win * bet * mult,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, 1, MatrixBikiniFruits.WinForWildBikiniFruits, win, UnicornGlobalData.GameLineShifted)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, UnicornGlobalData.GameLineShifted).GetLinesPositions(UnicornGlobalData.GameLineShifted, i, 1, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (GratisGame)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetScatterPositionsArray(0),
                    Id = EXTRA_LINE,
                    Win = 0,
                    WinningElement = 0
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Transformiše matricu za igru 'QueenOfPyramid' u kombinaciju
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        public void MatrixToCombination(MatrixQueenOfPyramids matrix, int numberOfLines, int bet, int gratisGamesLeft, byte addInfo)
        {
            var multiplicators = new[] { 2, 3, 5 };
            Matrix = new byte[5, 5];
            var scatCount = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if (j > 0 && j < 4 && Matrix[i, j] == 0)
                    {
                        scatCount++;
                    }
                }
            }

            GratisGame = scatCount >= 3;
            NumberOfGratisGames = GratisGame ? scatCount * 3 : 0;
            if (gratisGamesLeft > 0)
            {
                AdditionalInformation = addInfo;
            }
            else if (GratisGame)
            {
                AdditionalInformation = (byte)scatCount;
            }

            PositionFor2 = new byte[4];
            if (gratisGamesLeft == 0)
            {
                PositionFor2[0] += 1;
                if (!GratisGame)
                {
                    PositionFor2[0] += 2;
                }
                else
                {
                    PositionFor2[1] += 2;
                }
            }
            else
            {
                PositionFor2[(addInfo * 3 - gratisGamesLeft) / addInfo + 1] += 1;
                if (gratisGamesLeft - 1 == 0)
                {
                    PositionFor2[0] += 2;
                }
                else
                {
                    PositionFor2[(addInfo * 3 - (gratisGamesLeft - 1)) / addInfo + 1] += 2;
                }
            }

            TotalWin = 0;
            var mult = gratisGamesLeft > 0 ? multiplicators[(addInfo * 3 - gratisGamesLeft) / addInfo] : 1;
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
                    Win = win * bet * mult,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, 1, MatrixQueenOfPyramids.WinForWildQueenOfPyramids, win, UnicornGlobalData.GameLineShifted)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, UnicornGlobalData.GameLineShifted).GetLinesPositions(UnicornGlobalData.GameLineShifted, i, 1, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (GratisGame)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetScatterPositionsArray(0),
                    Id = EXTRA_LINE,
                    Win = 0,
                    WinningElement = 0
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Transformiše matricu za igru 'UnicornFireStars' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="numberOfLines"></param>
        public void MatrixToCombination(MatrixFireStars matrix, int bet, int numberOfLines)
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
                    Win = win * bet,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, -1, null, win, UnicornGlobalData.GameLineWinterFruits)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, UnicornGlobalData.GameLineWinterFruits).GetLinesPositions(UnicornGlobalData.GameLineWinterFruits, i, -1, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            var addExtraLine = matrix.GetNoLineWin(0, MatrixFireStars.WinForScatterFireStars);
            if (addExtraLine > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(0),
                    Id = EXTRA_LINE,
                    Win = addExtraLine * bet,
                    WinningElement = 0
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Transformiše matricu za igru 'SimplySevens' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixSimplySevens matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 5];
            var scatNum = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if (j > 0 && j < 4 && Matrix[i, j] == 10)
                    {
                        scatNum++;
                    }
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

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
                    Win = win * bet,
                    WinningElement = (byte)matrix.GetLine(i, UnicornGlobalData.GameLineShifted).GetElement(0)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, UnicornGlobalData.GameLineShifted).GetLinesPositions(UnicornGlobalData.GameLineShifted, i, -1, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (scatNum == 3)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetJackpotPositionsArray(),
                    Id = EXTRA_LINE,
                    Win = MatrixSimplySevens.JACKPOT * bet * numberOfLines,
                    WinningElement = 0
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Transformiše matricu za igru '10JingleFruits' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(Matrix10JingleFruits matrix, int numberOfLines, int bet)
        {
            var powerPlay = true;
            var scatCount = 0;
            Matrix = new byte[5, 5];
            var scatPos = new byte[5] { 255, 255, 255, 255, 255 };
            var next = 0;
            for (var i = 0; i < 5; i++)
            {
                var reelHaveScatter = false;
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if (j > 0 && j < 4 && Matrix[i, j] > 6)
                    {
                        reelHaveScatter = true;
                        scatCount += Matrix[i, j] - 6;
                        scatPos[next++] = (byte)(j * 5 + i);
                    }
                }
                powerPlay = powerPlay && reelHaveScatter;
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            WinFor2 = scatCount << 8;
            if (powerPlay)
            {
                scatCount = Matrix10JingleFruits.GetPowerPlay(scatCount);
            }
            WinFor2 |= scatCount;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, UnicornGlobalData.GameLineShifted);
            if (scatCount >= 5)
            {
                var linInfo = LinesInformation.ToList();
                var li = new LineInfo { Id = EXTRA_LINE, Win = Matrix10JingleFruits.WinForJingles[scatCount] * bet * numberOfLines, WinningElement = 7, WinningPosition = scatPos };
                linInfo.Add(li);
                LinesInformation = linInfo.ToArray();
                TotalWin += li.Win;
            }
        }

        /// <summary>
        /// Transformiše matricu za igru 'UnicornGoldLine' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination2(MatrixGoldLine matrix, int bet, bool goldLine)
        {
            if (goldLine)
            {
                matrix.SetGoldenLineNew();
            }
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= 11; i++)
            {
                var win = matrix.CalculateWinLine(i);
                if (win == 0)
                {
                    continue;
                }
                var winPosition = matrix.GetLinesPositions(i, out int elem);
                var lineInfo = new LineInfo
                {
                    Id = (byte)((i + 9) % 11),
                    Win = win * bet * (i == 1 ? 10 : 1),
                    WinningElement = (byte)elem,
                    WinningPosition = winPosition
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
            if (matrix.JackpotWin(out byte[] positionJackpot))
            {
                NumberOfWinningLines = 1;
                LinesInformation = new LineInfo[1];
                LinesInformation[0] = new LineInfo
                {
                    WinningPosition = positionJackpot,
                    Id = EXTRA_LINE,
                    Win = MatrixGoldLine.WIN_FOR_JACKPOT * bet * 20,
                    WinningElement = 10
                };
                TotalWin = LinesInformation[0].Win;
            }
        }

        /// <summary>
        /// Transformiše matricu za igru 'ChristmasPresents' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixChristmasPresents matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 5];
            WinFor2 = 0;
            var scatNum = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if (j > 0 && j < 4 && Matrix[i, j] >= 7)
                    {
                        scatNum++;
                    }
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.CalculateWinLine(i);
                if (win == 0)
                {
                    continue;
                }
                var winPosition = matrix.GetLinesPositions(i, win, out int elem);
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet,
                    WinningElement = (byte)elem,
                    WinningPosition = winPosition
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            var usageMatrix = new bool[5, 3];
            foreach (var li in linesInfo)
            {
                for (var i = 0; i < li.WinningPosition.Length; i++)
                {
                    if (li.WinningPosition[i] < 20)
                    {
                        usageMatrix[li.WinningPosition[i] % 5, li.WinningPosition[i] / 5 - 1] = true;
                    }
                }
            }
            var throwList = new List<byte>();
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (Matrix[i, j] >= 7)
                    {
                        if (usageMatrix[i, j - 1] || scatNum == 3)
                        {
                            var win = MatrixChristmasPresents.Multiplicator[Matrix[i, j]];
                            var lineInfo = new LineInfo
                            {
                                Id = EXTRA_LINE,
                                Win = win * bet * numberOfLines,
                                WinningElement = Matrix[i, j],
                                WinningPosition = new byte[] { (byte)(j * 5 + i), 255, 255, 255, 255 }
                            };
                            TotalWin += lineInfo.Win;
                            linesInfo.Add(lineInfo);
                        }
                        throwList.Add((byte)(j * 5 + i));
                    }
                }
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
            if (TotalWin == 0 && throwList.Count == 1 && SoftwareRng.Next(4) == 0)
            {
                WinFor2 = throwList[0];
            }
            if (TotalWin == 0 && throwList.Count == 2)
            {
                if (SoftwareRng.Next(20) == 0)
                {
                    WinFor2 = (throwList[0] << 8) + throwList[1];
                }
                else if (SoftwareRng.Next(5) == 0)
                {
                    WinFor2 = throwList[(int)SoftwareRng.Next(2)];
                }
            }
            if (TotalWin > 0 && throwList.Count == 1 && SoftwareRng.Next(2) == 0)
            {
                WinFor2 = throwList[0];
            }
            if (TotalWin > 0 && throwList.Count == 2)
            {
                if (SoftwareRng.Next(4) == 0)
                {
                    WinFor2 = (throwList[0] << 8) + throwList[1];
                }
                else if (SoftwareRng.Next(2) == 0)
                {
                    WinFor2 = throwList[(int)SoftwareRng.Next(2)];
                }
            }
            if (TotalWin > 0 && throwList.Count == 3)
            {
                if (SoftwareRng.Next(10) == 0)
                {
                    WinFor2 = (throwList[0] << 16) + (throwList[1] << 8) + throwList[2];
                }
                else if (SoftwareRng.Next(40) > 10)
                {
                    throwList.RemoveAt((int)SoftwareRng.Next(3));
                    WinFor2 = (throwList[0] << 8) + throwList[1];
                }
                else if (SoftwareRng.Next(35) > 10)
                {
                    WinFor2 = throwList[(int)SoftwareRng.Next(3)];
                }
            }
            if (matrix.GoldJackpotWin(out byte[] positionJackpot))
            {
                NumberOfWinningLines = 1;
                LinesInformation = new LineInfo[1];
                LinesInformation[0] = new LineInfo
                {
                    WinningPosition = positionJackpot,
                    Id = EXTRA_LINE,
                    Win = MatrixChristmasPresents.WIN_FOR_JACKPOT * bet * numberOfLines,
                    WinningElement = 25
                };
                TotalWin = LinesInformation[0].Win;
            }
        }

        /// <summary>
        /// Transformiše matricu za igru 'BigSpinSevens' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="wild">Vajld</param>
        public void MatrixToCombination(MatrixBigSpinSevens matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 7];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 7; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            matrix.TransformMatrix();

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, UnicornGlobalData.GameLineDoubleShift);
        }

        public void MatrixToCombination(MatrixBuffaloSevens matrix, int numberOfLines, int bet)
        {
            WinFor2 = 0;
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, UnicornGlobalData.GameLineShifted);

            if (matrix.FullDeck())
            {
                TotalWin *= 10;
                WinFor2 = 1;
            }
        }

        public void MatrixToCombination(MatrixFrootClassic matrix, int numberOfLines, int bet)
        {
            WinFor2 = 0;
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, UnicornGlobalData.GameLineShifted);
        }

        public void MatrixToCombination(MatrixStickyHot matrix, int numberOfLines, int bet, int gratisGamesLeft, ref byte[] addArray)
        {
            if (gratisGamesLeft == MatrixStickyHot.GRATIS_GAMES && addArray[15] == 1)
            {
                for (var i = 0; i < 15; i++)
                {
                    addArray[i] = 0;
                }
            }
            GratisGame = false;
            NumberOfGratisGames = 0;
            WinFor2 = 0;
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }
            var winForScatters = matrix.GetNoLineWins(0, MatrixStickyHot.WinForScatterStickyHot);
            if (gratisGamesLeft > 0)
            {
                for (var i = 0; i < 15; i++)
                {
                    var tmp = matrix.GetElement(i % 5, i / 5 + 1);
                    if (addArray[i] > 0)
                    {
                        matrix.SetElement(i % 5, i / 5 + 1, 1);
                        addArray[i] = 2;
                    }
                    if (tmp == 1 && addArray[i] == 0)
                    {
                        addArray[i] = 1;
                    }
                    if (addArray[i] > 0)
                    {
                        Matrix[i % 5, i / 5 + 1] = 1;
                    }
                }
                if (gratisGamesLeft == 1 && MatrixStickyHot.ShouldRetriggerBonus(addArray[15]))
                {
                    GratisGame = true;
                    NumberOfGratisGames = MatrixStickyHot.GRATIS_GAMES;
                    addArray[15] = 1;
                }
            }
            else
            {
                if (winForScatters > 0)
                {
                    GratisGame = true;
                    NumberOfGratisGames = MatrixStickyHot.GRATIS_GAMES;
                    for (var i = 0; i < 16; i++)
                    {
                        addArray[i] = 0;
                    }
                }
                else
                {
                    if (addArray.Any(s => s > 0))
                    {
                        for (var i = 0; i < 16; i++)
                        {
                            addArray[i] = 0;
                        }
                    }
                }
            }
            AdditionalArray = addArray;
            CreateLinesInformations(matrix, numberOfLines, bet, 1, 1, MatrixStickyHot.WinForWildStickyHot, UnicornGlobalData.GameLineShifted);
            if (winForScatters > 0)
            {
                var linesInfo = LinesInformation.ToList();
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(0, Matrix),
                    Id = EXTRA_LINE,
                    Win = winForScatters * bet * numberOfLines,
                    WinningElement = 0
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
                NumberOfWinningLines++;
                LinesInformation = linesInfo.ToArray();
            }
        }

        #region GamePumpkinHorror

        private LineInfo GetScatterLineInfoPumpkinHorror(ref MatrixPumpkinHorror matrix, int bet, int numberOfLines, out bool jackpot)
        {
            var scatCount = 0;
            var position = new List<byte>();
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 3; j++)
                {
                    if (matrix.GetElement(i, j) == 8 && matrix.GetElement(i, j + 1) == 9)
                    {
                        scatCount++;
                        position.Add((byte)(j * 5 + i));
                    }
                }
            }
            jackpot = scatCount == 5;
            if (scatCount >= 2)
            {
                return new LineInfo { Id = 254, Win = MatrixPumpkinHorror.WinForScatterPumpkinHorror[scatCount - 1] * bet * numberOfLines, WinningElement = 10, WinningPosition = position.ToArray() };
            }
            return null;
        }

        private int NormalisedTransform(int numberOfSymbols)
        {
            var sum = 0.0;
            var rand = SoftwareRng.Next();
            var normalisedProbs = new double[6][];
            normalisedProbs[0] = new[] { 1.0 };
            normalisedProbs[1] = new[] { 0.597826087, 0.402173913 };
            normalisedProbs[2] = new[] { 0.55555556, 0.37373737, 0.07070707 };
            normalisedProbs[3] = new[] { 0.551102204, 0.370741483, 0.070140281, 0.008016032 };
            normalisedProbs[4] = new[] { 0.550055, 0.370037, 0.070007, 0.008001, 0.0019 };
            normalisedProbs[5] = new[] { 0.55, 0.37, 0.07, 0.008, 0.0019, 0.0001 };
            for (var i = 0; i < numberOfSymbols; i++)
            {
                sum += normalisedProbs[numberOfSymbols - 1][i];
                if (rand < sum)
                {
                    return i + 1;
                }
            }
            return -1;
        }

        public void TransformMatrixPumpkinHorror(ref MatrixPumpkinHorror matrix, out LineInfo transormLineInfo)
        {
            var wildCount = 0;
            transormLineInfo = null;
            var count = new int[6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    var elem = matrix.GetElement(i, j);
                    if (elem == 0)
                    {
                        wildCount++;
                        matrix.SetElement(i, j, 1);
                    }
                    if (elem == 10)
                    {
                        matrix.SetElement(i, j, 1);
                    }
                    if (elem >= 2 && elem <= 7)
                    {
                        count[elem - 2]++;
                    }
                }
            }
            count[4]++;
            if (wildCount == 0)
            {
                return;
            }
            var changes = NormalisedTransform(count.Count(x => x > 0));
            var sortList = new List<byte>();
            for (var i = 0; i < 6; i++)
            {
                var max = -1;
                var maxInd = -1;
                for (var j = 0; j < 6; j++)
                {
                    if (count[j] >= max)
                    {
                        max = count[j];
                        maxInd = j;
                    }
                }
                if (count[maxInd] > 0 && sortList.Count < changes)
                {
                    sortList.Add((byte)(maxInd + 2));
                }
                count[maxInd] = -1;
            }
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (sortList.Contains((byte)matrix.GetElement(i, j)))
                    {
                        matrix.SetElement(i, j, 1);
                    }
                }
            }
            transormLineInfo = new LineInfo { Win = 0, WinningElement = 0, Id = 253, WinningPosition = sortList.ToArray() };
        }

        /// <summary>
        /// Transformiše matricu za igru 'PumpkinHorror' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixPumpkinHorror matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 5];
            WinFor2 = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if (Matrix[i, j] == 10)
                    {
                        Matrix[i, j] = 20;
                    }
                    if (Matrix[i, j] == 8)
                    {
                        Matrix[i, j] = 10;
                    }
                    if (Matrix[i, j] == 9)
                    {
                        Matrix[i, j] = 11;
                    }
                }
            }
            GratisGame = false;
            NumberOfGratisGames = 0;

            TransformMatrixPumpkinHorror(ref matrix, out LineInfo transLineInfo);

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 1, MatrixPumpkinHorror.WinForWildPumpkinHorror, UnicornGlobalData.GameLineShifted);

            var li = GetScatterLineInfoPumpkinHorror(ref matrix, bet, numberOfLines, out bool jackpot);
            if (jackpot)
            {
                TotalWin = li.Win;
                LinesInformation = new LineInfo[] { li };
                NumberOfWinningLines = 1;
                return;
            }
            if (li != null)
            {
                var linfos = LinesInformation.ToList();
                linfos.Add(li);
                LinesInformation = linfos.ToArray();
                NumberOfWinningLines++;
                TotalWin += li.Win;
            }
            if (transLineInfo != null)
            {
                var linfos = LinesInformation.ToList();
                linfos.Add(transLineInfo);
                LinesInformation = linfos.ToArray();
                NumberOfWinningLines++;
            }
        }

        #endregion

        public void MatrixToCombination(MatrixHitLine matrix, int numberOfLines, int bet, bool cheat = false)
        {
            if (!cheat)
            {
                matrix.SetRewards();
            }

            WinFor2 = 0;
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

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
                    Win = win * bet,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, -1, null, win, UnicornGlobalData.GameLineShifted)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, UnicornGlobalData.GameLineShifted).GetLinesPositions(UnicornGlobalData.GameLineShifted, i, -1, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            var mult = 1;
            var ind = 0;
            var rewardPosition = new byte[] { 255, 255, 255, 255, 255 };
            for (var i = 1; i < 4; i++)
            {
                if (matrix.GetElement(i, 2) >= 10)
                {
                    mult *= MatrixHitLine.MultiplierReward[matrix.GetElement(i, 2) - 10];
                    rewardPosition[ind++] = (byte)(10 + i);
                }
            }
            if (mult > 1)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = rewardPosition,
                    Id = EXTRA_LINE,
                    Win = bet * mult * numberOfLines,
                    WinningElement = 10
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Transformiše matricu za igru '20SuperFlames' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(Matrix20SuperFlames matrix, int numberOfLines, int bet)
        {
            WinFor2 = 0;
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, UnicornGlobalData.GameLineShifted);
            var scatWin = matrix.GetScatterWin();
            if (scatWin > 0)
            {
                var linfos = LinesInformation.ToList();
                var li = new LineInfo { Id = EXTRA_LINE, Win = scatWin * numberOfLines * bet, WinningElement = 0, WinningPosition = matrix.GetScatterPositionsArray() };
                linfos.Add(li);
                LinesInformation = linfos.ToArray();
                NumberOfWinningLines++;
                TotalWin += li.Win;
            }
        }

        /// <summary>
        /// Transformiše matricu za igru 'Matrix20HotStrikeJackpot' u kombinaciju
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        public void MatrixToCombination(Matrix20HotStrikeJackpot matrix, int numberOfLines, int bet, bool cheat = false)
        {
            var newMatrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    newMatrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            var jackpotPos = new byte[3];
            if (cheat)
            {
                for (var i = 1; i < 4; i++)
                {
                    for (var j = 1; j < 4; j++)
                    {
                        if (newMatrix[i, j] == 10)
                        {
                            matrix.SetElement(i, j, 0);
                            jackpotPos[i - 1] = (byte)j;
                        }
                    }
                }
                if (jackpotPos[0] > 0 && jackpotPos[1] > 0 && jackpotPos[2] > 0)
                {
                    Matrix = newMatrix;
                    TotalWin = Matrix20HotStrikeJackpot.JACKPOT_WIN * numberOfLines * bet;
                    LinesInformation = new LineInfo[1] { new LineInfo { Id = EXTRA_LINE, Win = TotalWin, WinningElement = 10, WinningPosition = new byte[] { (byte)(jackpotPos[0] * 5 + 1), (byte)(jackpotPos[1] * 5 + 2), (byte)(jackpotPos[2] * 5 + 3), 255, 255 } } };
                    return;
                }
            }
            else
            {
                Matrix20HotStrikeJackpot.FixScatterMatrixArray(ref newMatrix, out jackpotPos);
            }
            Matrix = newMatrix;
            if (jackpotPos != null)
            {
                TotalWin = Matrix20HotStrikeJackpot.JACKPOT_WIN * numberOfLines * bet;
                LinesInformation = new LineInfo[1] { new LineInfo { Id = EXTRA_LINE, Win = TotalWin, WinningElement = 10, WinningPosition = jackpotPos } };
                return;
            }

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
                    Win = win * bet,
                    WinningElement = (byte)matrix.GetWinningElement(i, win)
                };
                lineInfo.WinningPosition = matrix.GetLinesPositions(i, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            var scaterWin = matrix.GetScatterWin();
            if (scaterWin > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetScatterPositionsArray(0),
                    Id = EXTRA_LINE,
                    Win = scaterWin * bet * numberOfLines,
                    WinningElement = 0
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Transformiše matricu za igru 'EpicMegaCash' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixEpicMegaCash matrix, int numberOfLines, int bet)
        {
            matrix.SetScatters();
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, UnicornGlobalData.GameLineShifted);
            var scatter = matrix.GetScatterMultiplier();
            if (scatter > 0)
            {
                var win = scatter * bet * numberOfLines;
                var li = LinesInformation.ToList();
                li.Add(new LineInfo { Win = win, Id = EXTRA_LINE, WinningElement = 0, WinningPosition = matrix.GetScatterPositionArray() });
                LinesInformation = li.ToArray();
                NumberOfWinningLines++;
                TotalWin += win;
            }
        }
        /// <summary>
        /// Transformiše matricu za igru 'FasFruits' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixTopPrizeWilds matrix, int numberOfLines, int bet, int diamondCount)
        {
            var diamondMaskArray = MatrixTopPrizeWilds.GetDiamondMaskArray();
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    var element = (byte)matrix.GetElement(i, j);
                    if (element == 0)
                    {
                        Matrix[i, j] = (byte)diamondMaskArray[i - 1];
                    }
                    else
                    {
                        Matrix[i, j] = element;
                    }
                }
            }
            if (!(diamondCount == 3 && MatrixTopPrizeWilds.AreAllMasksEqual(diamondMaskArray)))
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
                        Win = win * bet,
                        WinningElement = (byte)matrix.GetWinningElementForLine(i, 0, null, win, UnicornGlobalData.GameLineShifted)
                    };
                    lineInfo.WinningPosition = matrix.GetLine(i, UnicornGlobalData.GameLineShifted).GetLinesPositions(UnicornGlobalData.GameLineShifted, i, 0, lineInfo.WinningElement);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }
                var diamondPairId = MatrixTopPrizeWilds.GetDiamondPairId(Matrix, diamondMaskArray);
                if (diamondPairId != -1)
                {
                    var lineInfo = new LineInfo
                    {
                        WinningPosition = matrix.GetPositionsArray(diamondPairId),
                        Id = EXTRA_LINE,
                        Win = bet * numberOfLines * MatrixTopPrizeWilds.PairMultipliers[diamondPairId - 8],
                        WinningElement = (byte)diamondPairId
                    };
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }
                else if (diamondCount == 3)
                {
                    var lineInfo = new LineInfo
                    {
                        WinningPosition = matrix.GetPositionsArray(0),
                        Id = EXTRA_LINE,
                        Win = bet * numberOfLines * MatrixTopPrizeWilds.MixMultiplier,
                        WinningElement = (byte)diamondMaskArray[0]
                    };
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }
                NumberOfWinningLines = (byte)linesInfo.Count;
                LinesInformation = linesInfo.ToArray();
            }
            else
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(0),
                    Id = EXTRA_LINE,
                    Win = bet * numberOfLines * MatrixTopPrizeWilds.TripletMultipliers[diamondMaskArray[0] - 8],
                    WinningElement = (byte)diamondMaskArray[0]
                };
                TotalWin = lineInfo.Win;
                LinesInformation = new LineInfo[1];
                LinesInformation[0] = lineInfo;
            }

            GratisGame = false;
            NumberOfGratisGames = 0;
        }

        public void MatrixToCombination(MatrixBigHitSevens matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, UnicornGlobalData.GameLineShifted);
        }
    }
}
