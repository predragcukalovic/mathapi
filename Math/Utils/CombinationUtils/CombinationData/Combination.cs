using System;
using System.Collections.Generic;
using System.Linq;
using Papi.GameServer.Utils.Enums;
using GameBonusBells;
using GameBookOfDouble;
using GameElGrandeToro;
using GameLollasWorld;
using GameSevenClassicHot;
using GameWildClover506;
using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;
using MathForGames.GameAlpinist;
using MathForGames.GameBookOfBruno;
using MathForGames.GameBookOfMayanGold;
using MathForGames.GameBurstingHot5;
using MathForGames.GameCaptainShark;
using MathForGames.GameCloverCash;
using MathForGames.GameCrystalHot40Deluxe;
using MathForGames.GameDiamonds;
using MathForGames.GameForestFruits;
using MathForGames.GameFruits;
using MathForGames.GameFruityJokerHot;
using MathForGames.GameGoldenCrown;
using MathForGames.GameHotParty;
using MathForGames.GameHotStars;
using MathForGames.GameJuicyHot;
using MathForGames.GameJungle;
using MathForGames.GameLuckyTwister;
using MathForGames.GameMagicOfTheRing;
using MathForGames.GameMagicTarget;
using MathForGames.GameMegaCubesDeluxe;
using MathForGames.GameMegaHot;
using MathForGames.GameMonsters;
using MathForGames.GameNeonHot5;
using MathForGames.GamePirates;
using MathForGames.GamePokerSlot;
using MathForGames.GamePostman;
using MathForGames.GamePyramid;
using MathForGames.GameSpellbook;
using MathForGames.GameStarGems;
using MathForGames.GameTropicalHot;
using MathForGames.GameTurboHot40;
using MathForGames.GameVikingGold;
using MathForGames.GameWildClover40;
using MathForGames.GameWildWest;
using MathForGames.GameWizard;
using RNGUtils.RandomData;

namespace MathCombination.CombinationData
{
    public class Combination : ICombination
    {
        #region Constructor or Singleton implementation

        /// <summary>
        /// Konstruktor za kombinaciju
        /// </summary>
        public Combination()
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
        /// Kreira niz LinesInformation za igre 'Magic of the ring' i 'Spellbook'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisElement"></param>
        /// <param name="noLineWins"></param>
        /// <param name="winForLines"></param>
        /// <param name="winForWilds"></param>
        protected void CreateLinesInformationsRing(MatrixMagicOfTheRing matrix, int numberOfLines, int bet, byte gratisElement, int[] noLineWins, int[,] winForLines, int[] winForWilds)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var winOfLine = matrix.CalculateWinLine(i, gratisElement);
                var winningElement = (byte)matrix.GetWinningElementForLine(i, 0, winForWilds, winOfLine, GlobalData.GameLineExtra);
                if (winOfLine == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = winOfLine * bet,
                    WinningElement = winningElement
                };
                if (!(gratisElement != 0 && gratisElement == winningElement))
                {
                    lineInfo.WinningPosition =
                        matrix.GetLine(i, GlobalData.GameLineExtra)
                            .GetLinesPositions(GlobalData.GameLineExtra, i, 0, lineInfo.WinningElement);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }
            }

            var scatterWin = matrix.GetNoLineWin(0, noLineWins);
            if ((gratisElement != 0 && (matrix.IsCanBeTransformed(gratisElement) || linesInfo.Count > 0)) || scatterWin > 0)
            {
                var lineInfo15 = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(-1),
                    Id = EXTRA_LINE,
                    Win = 0,
                    WinningElement = 0
                };
                CreateEmptyArray(lineInfo15.WinningPosition);
                if (scatterWin > 0)
                {
                    lineInfo15.Win = scatterWin * bet * numberOfLines;
                    TotalWin += lineInfo15.Win;
                    lineInfo15.WinningPosition = matrix.GetPositionsArray(0);
                }
                linesInfo.Add(lineInfo15);
            }

            if (gratisElement != 0 && matrix.IsCanBeTransformed(gratisElement))
            {
                matrix.Transform(gratisElement);
                for (var i = 1; i <= numberOfLines; i++)
                {
                    var winOfLine = matrix.CalculateNonOrderWinOfLine(i, gratisElement, winForLines);
                    if (winOfLine == 0)
                        continue;
                    var lineInfo = new LineInfo
                    {
                        Id = (byte)(i - 1),
                        Win = winOfLine * bet,
                        WinningElement = gratisElement
                    };
                    lineInfo.WinningPosition = matrix.GetLine(i, GlobalData.GameLineExtra).GetLinesPositionsNonOrder(GlobalData.GameLineExtra, i, lineInfo.WinningElement);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Kreira niz LinesInformation za igre 'Book of Double'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisElement2"></param>
        /// <param name="noLineWins"></param>
        /// <param name="winForLines"></param>
        /// <param name="winForWilds"></param>
        /// <param name="gratisElement1"></param>
        protected void CreateLinesInformationsDouble(MatrixBookOfDouble matrix, int numberOfLines, int bet, byte gratisElement1, byte gratisElement2, int[] noLineWins, int[,] winForLines, int[] winForWilds)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var winOfLine = matrix.CalculateWinLine(i, gratisElement1, gratisElement2);
                var winningElement = (byte)matrix.GetWinningElementForLine(i, 0, winForWilds, winOfLine, GlobalData.GameLineExtra);
                if (winOfLine == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = winOfLine * bet,
                    WinningElement = winningElement,
                    WinningPosition = matrix.GetLine(i, GlobalData.GameLineExtra).GetLinesPositions(GlobalData.GameLineExtra, i, 0, winningElement)
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }

            var mat2 = matrix.Clone();

            var scatterWin = matrix.GetNoLineWin(0, noLineWins);
            if (scatterWin > 0)
            {
                var lineInfo15 = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(0),
                    Id = EXTRA_LINE,
                    Win = scatterWin * bet * numberOfLines,
                    WinningElement = 0
                };
                TotalWin += lineInfo15.Win;
                linesInfo.Add(lineInfo15);
            }

            if (gratisElement1 > 0)
            {
                linesInfo.Add(new LineInfo
                {
                    Id = EXTRA_LINE - 1,
                    Win = 0,
                    WinningElement = gratisElement1,
                    WinningPosition = new byte[5]
                });
            }
            if (gratisElement1 != 0 && matrix.IsCanBeTransformed(gratisElement1))
            {
                matrix.Transform(gratisElement1);
                for (var i = 1; i <= numberOfLines; i++)
                {
                    var winOfLine = matrix.CalculateNonOrderWinOfLine(i, gratisElement1, winForLines);
                    if (winOfLine == 0)
                        continue;
                    var lineInfo = new LineInfo
                    {
                        Id = (byte)(i - 1),
                        Win = winOfLine * bet,
                        WinningElement = gratisElement1
                    };
                    lineInfo.WinningPosition = matrix.GetLine(i, GlobalData.GameLineExtra).GetLinesPositionsNonOrder(GlobalData.GameLineExtra, i, lineInfo.WinningElement);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }
            }
            if (gratisElement2 > 0)
            {
                linesInfo.Add(new LineInfo
                {
                    Id = EXTRA_LINE - 2,
                    Win = 0,
                    WinningElement = gratisElement2,
                    WinningPosition = new byte[5]
                });
            }
            if (gratisElement2 != 0 && mat2.IsCanBeTransformed(gratisElement2))
            {
                mat2.Transform(gratisElement2);
                for (var i = 1; i <= numberOfLines; i++)
                {
                    var winOfLine = mat2.CalculateNonOrderWinOfLine(i, gratisElement2, winForLines);
                    if (winOfLine == 0)
                        continue;
                    var lineInfo = new LineInfo
                    {
                        Id = (byte)(i - 1),
                        Win = winOfLine * bet,
                        WinningElement = gratisElement2
                    };
                    lineInfo.WinningPosition = mat2.GetLine(i, GlobalData.GameLineExtra).GetLinesPositionsNonOrder(GlobalData.GameLineExtra, i, lineInfo.WinningElement);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Kreira niz LinesInformation za igru 'Diamonds'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="addExtraLine"></param>
        protected void CreateLinesInformationsDiamonds(MatrixDiamonds matrix, int numberOfLines, int bet, int addExtraLine = 0)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.CalculateWinOfLine(i);
                if (win != 0)
                {
                    var lineInfo = new LineInfo
                    {
                        Id = (byte)(i - 1),
                        Win = win * bet,
                        WinningElement = (byte)matrix.GetLine(i).GetWinningElement()
                    };
                    lineInfo.WinningPosition = matrix.GetLine(i).GetLinesPositionsDiamonds(i, lineInfo.WinningElement);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }
            }
            if (addExtraLine > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(0),
                    Id = EXTRA_LINE,
                    Win = addExtraLine * bet * numberOfLines,
                    WinningElement = 0
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Kreira niz LinesInformation za igru 'HotStars'
        /// </summary>
        /// <param name="matrix">Matrica za koju se vrše izračunavanja</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        protected void CreateLinesInformationsStars(MatrixHotStars matrix, int numberOfLines, int bet)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var leftWin = matrix.CalculateLeftWinOfLine(i);
                var leftElement = (byte)matrix.GetLine(i).GetElement(0);
                if (leftWin != 0)
                {
                    var lineInfo = new LineInfo
                    {
                        Id = (byte)(i - 1),
                        Win = leftWin * bet,
                        WinningElement = leftElement
                    };
                    lineInfo.WinningPosition = matrix.GetLine(i, GlobalData.GameLineExtra).GetLinesPositions(GlobalData.GameLineExtra, i, 0, lineInfo.WinningElement);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }
                var rightWin = matrix.CalculateRightWinOfLine(i);
                var rightElement = (byte)matrix.GetLine(i).GetElement(4);
                if (rightWin != 0)
                {
                    var lineInfo = new LineInfo
                    {
                        Id = (byte)(i - 1),
                        Win = rightWin * bet,
                        WinningElement = rightElement
                    };
                    if (!(leftWin == rightWin && leftElement == rightElement))
                    {
                        lineInfo.WinningPosition =
                            matrix.GetLine(i, GlobalData.GameLineExtra)
                                .GetLinesPositionsRight(GlobalData.GameLineExtra, i, lineInfo.WinningElement);
                        TotalWin += lineInfo.Win;
                        linesInfo.Add(lineInfo);
                    }
                }
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
        protected void CreateLinesInformationsTurbo(MatrixTurboHot40 matrix, int numberOfLines, int bet,
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
        protected void CreateLinesInformationsCrystalDeluxe(MatrixCrystalHot40Deluxe matrix, int numberOfLines, int bet,
            int wild, int[] winForWild, int[,] gameLines, int addExtraLine = 0, int extraSymbol = -1)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var leftWin = matrix.CalculateLeftLineWin(i);
                if (leftWin != 0)
                {
                    var lineInfo = new LineInfo
                    {
                        Id = (byte)(i - 1),
                        Win = leftWin * bet,
                        WinningElement = (byte)matrix.GetWinningElementForLine(i, wild, winForWild, leftWin, gameLines)
                    };
                    lineInfo.WinningPosition = matrix.GetLine(i, gameLines).GetLinesPositions(gameLines, i, wild, lineInfo.WinningElement);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }
                var rightWin = matrix.CalculateRightLineWin(i);
                if (rightWin != 0)
                {
                    var lineInfo = new LineInfo
                    {
                        Id = (byte)(i - 1),
                        Win = rightWin * bet,
                        WinningElement = (byte)matrix.GetRightWinningElementForLine(i, wild, winForWild, rightWin, gameLines)
                    };
                    lineInfo.WinningPosition = matrix.GetLine(i, gameLines).GetLinesPositionsRight(gameLines, i, lineInfo.WinningElement, wild);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }
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
        /// <param name="gratisGame">Množilac za vreme gratis igara</param>
        /// <param name="wild">Wild element</param>
        /// <param name="winForWild">Dobitak za wild</param>
        /// <param name="gameLines">Linije na koje se igra</param>
        /// <param name="addExtraLine">Dobitak elemenata van linije</param>
        /// <param name="extraSymbol">Simbol koji daje dobitak van linije</param>
        protected void CreateLinesInformation40WildClover6(Matrix40WildClover6 matrix, int numberOfLines, int bet, int gratisGame,
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
        /// Promeša niz
        /// </summary>
        /// <param name="array">Niz koji je potrebno promešati</param>
        protected static void MixArray(byte[] array)
        {
            var length = array.Length;
            for (var i = 0; i < length - 1; i++)
            {
                var rnd = (int)SoftwareRng.Next(length - i);
                var tmp = array[length - 1 - i];
                array[length - 1 - i] = array[rnd];
                array[rnd] = tmp;
            }
        }

        /// <summary>
        /// Popunjava niz vrednostima 255
        /// </summary>
        /// <param name="array">Niz koji se popunjava</param>
        protected static void CreateEmptyArray(byte[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = 255;
            }
        }

        /// <summary>
        /// Transformiše maticu za igru 'Alpinist' tako što postavi wild elemente
        /// </summary>
        /// <param name="matrix">Matrica koja se transformiše</param>
        /// <param name="alpinistLevel">Koliko rila se menja</param>
        protected static void TransformAlpinistMatrix(ref MatrixAlpinist matrix, int alpinistLevel)
        {
            for (var i = 1; i <= alpinistLevel; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    matrix.SetElement(i, j, 0);
                }
            }
            matrix.SetBonusAndGratis(0, false);
        }

        /// <summary>
        /// Transformiše maticu za igru 'Pyramid' tako što postavi wild elemente
        /// </summary>
        /// <param name="matrix">Matrica koja se transformiše</param>
        /// <param name="piramideLevel">Koji rilovi se menjaju</param>
        protected static void TransformPiramideMatrix(Matrix matrix, byte piramideLevel)
        {
            for (var i = 0; i < 3; i++)
            {
                if ((piramideLevel & (1 << i)) != 0)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        matrix.SetElement(i, j, 0);
                    }
                }
            }
            matrix.SetBonusAndGratis(0, false);
        }

        /// <summary>
        /// Transformiše matricu za igre 'Fruits' i 'Pegasus' tako što postavi pegaze gde je to potrebno
        /// </summary>
        /// <param name="matrix">Matrica koja se transformiše</param>
        /// <param name="pegasusGame">Da li se i u kojim linijama pojavljuje pegaz</param>
        protected static void TransformPegasusMatrix(Matrix matrix, byte pegasusGame)
        {
            for (var i = 0; i < 3; i++)
            {
                if ((pegasusGame & (1 << i)) != 0)
                {
                    matrix.SetElement(1, i, 0);
                    matrix.SetElement(2, i, 0);
                    matrix.SetElement(3, i, 0);
                }
            }
        }

        /// <summary>
        /// Transformiše matricu za igru 'HotStars' tako što postavi zvezde gde je to potrebno
        /// </summary>
        /// <param name="matrix">Matrica koja se transformiše</param>
        /// <param name="stars">Da li se i u kojim linijama pojavljuje zvezda</param>
        protected void TransformStarMatrix(MatrixHotStars matrix, byte stars)
        {
            for (var i = 0; i < 3; i++)
            {
                if ((stars & (1 << i)) != 0)
                {
                    matrix.SetElement(i + 1, 0, 0);
                    matrix.SetElement(i + 1, 1, 0);
                    matrix.SetElement(i + 1, 2, 0);
                }
            }
        }

        /// <summary>
        /// Мења вредности двеју промељивих.
        /// </summary>
        /// <param name="a">Прва променљива</param>
        /// <param name="b">Друга променљива</param>
        protected static void Swap(ref byte a, ref byte b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }

        /// <summary>
        /// Proširi element i,j na sve okolne pozicije i pretvori ga u wild.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        protected static void TransformJungleMatrix(ref MatrixJungle matrix, int i, int j)
        {
            for (var k = i - 1; k <= i + 1; k++)
            {
                for (var l = j - 1; l <= j + 1; l++)
                {
                    if (k >= 0 && k <= 4 && l >= 0 && l <= 2)
                    {
                        matrix.SetElement(k, l, 0);
                    }
                }
            }
        }

        /// <summary>
        /// Kreira niz koji određuje dobitke za sketere koji nisu izabrani za Alpinisut
        /// </summary>
        /// <param name="winFor2">Množilac za izabranu dvojku</param>
        protected void CreateArrayMultiplyFor2Alpinist(int winFor2)
        {
            byte[] values = { 10, 10, 20, 150, 150 };
            switch (winFor2)
            {
                case 40: MultiplyFor2Alpinist[0] = 10;
                    MultiplyFor2Alpinist[1] = 10;
                    values[0] = 0;
                    values[1] = 0;
                    break;
                case 30: MultiplyFor2Alpinist[0] = 10;
                    MultiplyFor2Alpinist[1] = 20;
                    values[0] = 0;
                    values[2] = 0;
                    break;
                case 160: MultiplyFor2Alpinist[0] = 10;
                    MultiplyFor2Alpinist[1] = 150;
                    values[0] = 0;
                    values[3] = 0;
                    break;
                case 170: MultiplyFor2Alpinist[0] = 20;
                    MultiplyFor2Alpinist[1] = 150;
                    values[2] = 0;
                    values[3] = 0;
                    break;
                case 600: MultiplyFor2Alpinist[0] = 150;
                    MultiplyFor2Alpinist[1] = 150;
                    values[3] = 0;
                    values[4] = 0;
                    break;
            }
            MixArray(values);
            if (SoftwareRng.Next(2) == 0)
            {
                Swap(ref MultiplyFor2Alpinist[0], ref MultiplyFor2Alpinist[1]);
            }
            var i = 2;
            for (var j = 0; j < 5; j++)
            {
                if (values[j] != 0)
                {
                    MultiplyFor2Alpinist[i++] = values[j];
                }
            }
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

        /// <summary>
        /// Dodaje fenixe slučajno u matrici sa određenom verovatnoćom.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="frequency">Koliko često da ubacuje fenixa u ril.</param>
        protected static void AddFenix(MatrixHotStars matrix, int frequency)
        {
            for (var i = 1; i < 4; i++)
            {
                if (SoftwareRng.Next(frequency) == 0)
                {
                    matrix.SetElement(i, (int)SoftwareRng.Next(3), 0);
                }
            }
        }

        /// <summary>
        /// Kreira niz LinesInformation za igru VikingGold.
        /// </summary>
        /// <param name="matrix">Matrica</param>
        /// <param name="numberOfLines">Broj linija</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Množilac za vreme gratis igara</param>
        /// <param name="wild">Wild element</param>
        /// <param name="gameLines">Linije na koje se igra</param>
        protected void CreateLinesInformationsViking(MatrixVikingGold matrix, int numberOfLines, int bet, int gratisGame,
            int wild, int[,] gameLines)
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
                    WinningElement = (byte)matrix.GetWinningElementForLine(gameLines, i)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, gameLines).GetLinesPositions(gameLines, i, wild, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (matrix.GetNumberOfElement(1) == 3)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(1),
                    Id = EXTRA_LINE,
                    Win = 0,
                    WinningElement = 1
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Učitavanje kaskadne martice na osnovu rilova, ako se počinje nova partija.
        /// </summary>
        /// <param name="gratis"></param>
        /// <param name="addArray"></param>
        /// <param name="reels"></param>
        /// <returns></returns>
        protected static int[,] GetFirstCascadeMatrix(bool gratis, ref byte[] addArray, params List<byte>[] reels)
        {
            var matrix = new int[5, 3];

            var offset = gratis ? 38 : 0;

            for (var i = 0; i < 5; i++)
            {
                var length = reels[i].Count;
                var rand = (int)SoftwareRng.Next(length);
                for (var j = 0; j < 3; j++)
                {
                    matrix[i, j] = reels[i][(rand + j) % length];
                    addArray[offset + j * 5 + i] = (byte)(2 - j);
                }
                addArray[offset + 30 + i] = (byte)((rand + 2) % length);
            }

            return matrix;
        }

        /// <summary>
        /// Učitavanje kaskadne martice na osnovu rilova, ako se igra nastavlja na osnovu prethodne.
        /// </summary>
        /// <param name="gratis"></param>
        /// <param name="addArray"></param>
        /// <param name="reels"></param>
        /// <returns></returns>
        protected static int[,] GetNextCascadeMatrix(bool gratis, ref byte[] addArray, params List<byte>[] reels)
        {
            var matrix = new int[5, 3];

            var offset = gratis ? 38 : 0;

            for (var i = 0; i < 5; i++)
            {
                var r = reels[i].Count;
                if (addArray[offset + 15 + 2 * 5 + i] == 1)
                {
                    if (addArray[offset + 15 + 1 * 5 + i] == 1)
                    {
                        if (addArray[offset + 15 + i] == 1)
                        {
                            addArray[offset + 2 * 5 + i] = (byte)((addArray[offset + i] + 1) % r);
                            addArray[offset + 1 * 5 + i] = (byte)((addArray[offset + i] + 2) % r);
                            addArray[offset + 0 * 5 + i] = (byte)((addArray[offset + i] + 3) % r);
                        }
                        else
                        {
                            addArray[offset + 2 * 5 + i] = addArray[offset + i];
                            addArray[offset + 1 * 5 + i] = (byte)((addArray[offset + i] + 1) % r);
                            addArray[offset + 0 * 5 + i] = (byte)((addArray[offset + i] + 2) % r);
                        }
                    }
                    else
                    {
                        if (addArray[offset + 15 + i] == 1)
                        {
                            addArray[offset + 2 * 5 + i] = addArray[offset + 1 * 5 + i];
                            addArray[offset + 1 * 5 + i] = (byte)((addArray[offset + i] + 1) % r);
                            addArray[offset + 0 * 5 + i] = (byte)((addArray[offset + i] + 2) % r);
                        }
                        else
                        {
                            addArray[offset + 2 * 5 + i] = addArray[offset + 1 * 5 + i];
                            addArray[offset + 1 * 5 + i] = addArray[offset + 0 * 5 + i];
                            addArray[offset + 0 * 5 + i] = (byte)((addArray[offset + i] + 1) % r);
                        }
                    }
                }
                else
                {
                    if (addArray[offset + 15 + 1 * 5 + i] == 1)
                    {
                        if (addArray[offset + 15 + i] == 1)
                        {
                            addArray[offset + 1 * 5 + i] = (byte)((addArray[offset + i] + 1) % r);
                            addArray[offset + 0 * 5 + i] = (byte)((addArray[offset + i] + 2) % r);
                        }
                        else
                        {
                            addArray[offset + 1 * 5 + i] = addArray[offset + 0 * 5 + i];
                            addArray[offset + 0 * 5 + i] = (byte)((addArray[offset + i] + 1) % r);
                        }
                    }
                    else
                    {
                        if (addArray[offset + 15 + i] == 1)
                        {
                            addArray[offset + 0 * 5 + i] = (byte)((addArray[offset + i] + 1) % r);
                        }
                    }
                }
            }

            for (var i = 0; i < 5; i++)
            {
                var r = reels[i].Count;
                for (var j = 0; j < 3; j++)
                {
                    matrix[i, j] = reels[i][(addArray[offset + 30 + i] + r - addArray[offset + j * 5 + i]) % r];
                }
            }

            return matrix;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Transformiše matricu za igru 'Postman' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Da li je gratis igra</param>
        public void MatrixToCombination(MatrixPostman matrix, int numberOfLines, int bet, bool gratisGame)
        {
            var gratisMultiplicator = gratisGame ? MatrixPostman.GRATIS_MULTIPLICATOR : 1;
            FillMatrixArray(matrix);

            GratisGame = matrix.GetNumberOfElement(2) >= 3;
            NumberOfGratisGames = GratisGame ? MatrixPostman.GRATIS_GAMES : 0;
            CreateEmptyArray(PositionFor2);
            CreateEmptyArray(MultiplyFor2);

            CreateLinesInformations(matrix, numberOfLines, bet, gratisMultiplicator, 0, LineWinsForGames.WinForWildsPostman, GlobalData.GameLineExtra, matrix.GetNoLineWin(2, LineWinsForGames.WinForScattersPostman), 2);
        }

        /// <summary>
        /// Transformiše matricu za igru 'JungleExtra' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixJungle matrix, int bet)
        {
            FillMatrixArray(matrix);

            GratisGame = false;
            NumberOfGratisGames = 0;
            WinFor2 = 0;
            CreateEmptyArray(PositionFor2);
            CreateEmptyArray(MultiplyFor2);

            var li = new LineInfo
            {
                WinningPosition = matrix.GetPositionsArray(1),
                Id = EXTRA_LINE,
                Win = matrix.GetNoLineWin(1, LineWinsForGames.WinForScattersJungle) * bet * 20,
                WinningElement = 1
            };

            var next = 0;
            for (var i = 1; i < 4; i += 2)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (matrix.GetElement(i, j) == 2)
                    {
                        TransformJungleMatrix(ref matrix, i, j);
                        PositionFor2[next++] = (byte)(5 * j + i);
                    }
                }
            }

            CreateLinesInformations(matrix, 20, bet, 1, 0, LineWinsForGames.WinForWildsJungle,
                GlobalData.GameLineExtra);
            TotalWin += li.Win;
            if (PositionFor2[0] != 255)
            {
                var list = LinesInformation.ToList();
                list.Insert(0, li);
                LinesInformation = list.ToArray();
                NumberOfWinningLines++;
            }
            else if (li.Win > 0)
            {
                var list = LinesInformation.ToList();
                list.Add(li);
                LinesInformation = list.ToArray();
                NumberOfWinningLines++;
            }
        }

        /// <summary>
        /// Transformiše matricu za igru 'Wizard' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Da li je gratis igra</param>
        public void MatrixToCombination(MatrixWizard matrix, int numberOfLines, int bet, bool gratisGame)
        {
            var gratisMultiplicator = gratisGame ? MatrixWizard.GRATIS_MULTIPLICATOR : 1;
            FillMatrixArray(matrix);

            GratisGame = matrix.GetNumberOfElement(2) >= 3;
            NumberOfGratisGames = GratisGame ? MatrixWizard.GRATIS_GAMES : 0;
            CreateEmptyArray(PositionFor2);
            CreateEmptyArray(MultiplyFor2);

            CreateLinesInformations(matrix, numberOfLines, bet, gratisMultiplicator, 0, LineWinsForGames.WinForWildsWizard,
                GlobalData.GameLineExtra, matrix.GetNoLineWin(2, LineWinsForGames.WinForScattersWizard), 2);
        }

        /// <summary>
        /// Transformiše matricu za igru 'ExtraWildWest' u kombinaciju.
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        /// <param name="additionalInformation"></param>
        public void MatrixToCombination(MatrixWildWest matrix, int bet, bool gratisGame, byte additionalInformation)
        {
            if (gratisGame)
            {
                TransformPegasusMatrix(matrix, additionalInformation);
                AdditionalInformation = additionalInformation;
            }

            CreateEmptyArray(PositionFor2);
            GratisGame = false;
            NumberOfGratisGames = 0;
            FillMatrixArray(matrix);
            if (!gratisGame)
            {
                AdditionalInformation = 0;
                var next = 0;
                for (var i = 0; i < 3; i++)
                {
                    if (Matrix[2, i] == 2)
                    {
                        AdditionalInformation |= (byte)(1 << i);
                        GratisGame = true;
                        NumberOfGratisGames = 3;
                        PositionFor2[next++] = (byte)(i * 5 + 2);
                    }
                }
            }

            WinFor2 = 0;
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            CreateLinesInformations(matrix, 20, bet, 1, 0, LineWinsForGames.WinForWildsWildWest,
                GlobalData.GameLineExtra);
        }

        /// <summary>
        /// Transformiše matricu za igru 'FruitsExtra' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixFruits matrix, int numberOfLines, int bet)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            FillMatrixArray(matrix);

            WinFor2 = 0;
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 2, LineWinsForGames.WinForWildsFruits,
                GlobalData.GameLineExtra, matrix.GetNoLineWin(1, LineWinsForGames.WinForScattersFruits), 1);
        }

        /// <summary>
        /// Transformiše matricu za igru 'Alpinist' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGamesLeft">koliko gratis igara je ostalo</param>
        public void MatrixToCombination(MatrixAlpinist matrix, int bet, int gratisGamesLeft)
        {
            if (gratisGamesLeft > 0)
            {
                var reel = Math.Min(3, 6 - gratisGamesLeft);
                TransformAlpinistMatrix(ref matrix, reel - (gratisGamesLeft > 2 ? 1 : 0));
                matrix.SetElement(Math.Min(3, 6 - gratisGamesLeft), 1, 0);
            }

            FillMatrixArray(matrix);

            GratisGame = matrix.GetNumberOfElement(1) == 3;
            NumberOfGratisGames = GratisGame ? MatrixAlpinist.GRATIS_GAMES : 0;
            WinFor2 = matrix.GetRandomWinOf2Matrix() * bet * 15;
            PositionFor2 = matrix.GetPositionsArray(2);
            if (WinFor2 > 0)
            {
                CreateArrayMultiplyFor2Alpinist(matrix.GetWinOf2Matrix());
            }

            CreateLinesInformations(matrix, 15, bet, 1, 0, LineWinsForGames.WinForWildsAlpinist, GlobalData.GameLine);
            TotalWin += WinFor2;
        }

        /// <summary>
        /// Transformiše matricu za igru 'Pyramid' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Da li je u toku gratid igra</param>
        /// <param name="piramideGame">Da li su i u kojim linijama aktivirani wild rilovi</param>
        public void MatrixToCombination(MatrixPyramid matrix, int numberOfLines, int bet, bool gratisGame, byte piramideGame)
        {
            CreateEmptyArray(PositionFor2);
            AdditionalInformation = 0;
            if (matrix.GetNumberOfElement(1) >= 2)
            {
                for (var i = 0; i < 3; i++)
                {
                    if (matrix.IsReelHave(i, 1))
                    {
                        AdditionalInformation |= (byte)(1 << i);
                        PositionFor2[i] = 1;
                    }
                    else
                    {
                        PositionFor2[i] = 0;
                    }
                }
            }

            if (gratisGame)
            {
                TransformPiramideMatrix(matrix, piramideGame);
                AdditionalInformation = piramideGame;
            }
            FillMatrixArray(matrix);

            if (matrix.GetNumberOfElement(0xC) > 0)
            {
                GratisGame = true;
                NumberOfGratisGames = 1;
                AdditionalInformation = piramideGame;
            }
            else
            {
                var k = matrix.GetNumberOfElement(1);
                GratisGame = k >= 2;
                NumberOfGratisGames = GratisGame ? (k == 3 ? 5 : 3) : 0;
            }
            CreateEmptyArray(GratisGamesPositions);
            CreateEmptyArray(GratisGamesValues);

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, LineWinsForGames.WinForWildsPyramid, GlobalData.GameLineExtra);
        }

        /// <summary>
        /// Transformiše matricu za igru 'HotParty' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixHotParty matrix, int numberOfLines, int bet)
        {
            FillMatrixArray(matrix);

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, GlobalData.GameLineRing,
                matrix.GetNoLineWin(2, LineWinsForGames.WinForScattersHotParty), 2);
        }

        /// <summary>
        /// Transformiše matricu za igru 'JuicyHot' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixJuicyHot matrix, int numberOfLines, int bet)
        {
            FillMatrixArray(matrix);

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, GlobalData.GameLineExtra,
                matrix.GetNoLineWin(2, LineWinsForGames.WinForScattersJuicyHot), 2);
        }

        /// <summary>
        /// Transformiše matricu za igru 'Captain Shark' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Da li je gratis igra</param>
        public void MatrixToCombination(MatrixCaptainShark matrix, int numberOfLines, int bet, bool gratisGame)
        {
            var gratisMultiplicator = gratisGame ? MatrixCaptainShark.GRATIS_MULTIPLICATOR : 1;
            FillMatrixArray(matrix);

            var x = matrix.GetNumberOfElement(2);
            GratisGame = x >= 3;
            NumberOfGratisGames = x >= 3 ? (int)(22.5 * x * x - 142.5 * x + 240) : 0;

            CreateLinesInformations(matrix, numberOfLines, bet, gratisMultiplicator, 0, LineWinsForGames.WinForWildsCaptainShark,
                GlobalData.GameLineRing, matrix.GetNoLineWin(2, LineWinsForGames.WinForGratisCaptainShark), 2);
        }

        /// <summary>
        /// Transformiše matricu za igru 'Magic Target' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Da li je gratis igra</param>
        public void MatrixToCombination(MatrixMagicTarget matrix, int numberOfLines, int bet, bool gratisGame)
        {
            AdditionalInformation = 0;
            WinFor2 = 0;
            FillMatrixArray(matrix);

            GratisGame = matrix.IsMatrixGiveGratisGames() && !gratisGame;
            NumberOfGratisGames = GratisGame ? MatrixMagicTarget.GRATIS_GAMES : 0;

            if (gratisGame && matrix.GetNumberOfElement(1) == 1)
            {
                CreateLinesInformations(matrix, numberOfLines, bet, 2, 0, LineWinsForGames.WinForWildMagicTarget,
                    GlobalData.GameLineRing, matrix.GetNoLineWin(2, LineWinsForGames.WinForGratisMagicTarget), 2);
                AdditionalInformation = 8;
                WinFor2 = 1;
            }
            else
            {
                CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, LineWinsForGames.WinForWildMagicTarget,
                    GlobalData.GameLineRing, matrix.GetNoLineWin(2, LineWinsForGames.WinForGratisMagicTarget), 2);
            }
        }

        /// <summary>
        /// Transformiše matricu za igru 'MagicOfTheRing' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Da li je gratis igra</param>
        /// <param name="gratisElement">Gratis element u toku gratis igara, 0 inače</param>
        public void MatrixToCombination(MatrixMagicOfTheRing matrix, int numberOfLines, int bet, bool gratisGame, byte gratisElement)
        {
            AdditionalInformation = !gratisGame ? (byte)0 : gratisElement;
            FillMatrixArray(matrix);

            GratisGame = matrix.GetNumberOfElement(0) >= 3;
            WinFor2 = 0;
            NumberOfGratisGames = matrix.IsGiveGratisGames() ? MatrixMagicOfTheRing.GRATIS_GAMES : 0;

            CreateLinesInformationsRing(matrix, numberOfLines, bet, AdditionalInformation, LineWinsForGames.WinForGratisMagicOfTheRing, LineWinsForGames.WinForLinesMagicOfTheRing, null);
            if (NumberOfGratisGames > 0 && AdditionalInformation == 0)
            {
                AdditionalInformation = (byte)SoftwareRng.Next(1, 10);
            }
            if (GratisGame || gratisGame)
            {
                WinFor2 = AdditionalInformation;
            }
            if (gratisElement != 0 && matrix.IsCanBeTransformed(gratisElement))
            {
                for (var i = 0; i < 5; i++)
                {
                    PositionFor2[i] = matrix.IsReelHave(i, gratisElement) ? (byte)1 : (byte)0;
                }
            }
        }

        /// <summary>
        /// Transformiše matricu za igru 'MagicSpellbook' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Da li je gratis igra</param>
        /// <param name="gratisElement">Gratis element u toku gratis igara, 0 inače</param>
        public void MatrixToCombination(MatrixSpellbook matrix, int numberOfLines, int bet, bool gratisGame, byte gratisElement)
        {
            AdditionalInformation = !gratisGame ? (byte) 0 : gratisElement;
            FillMatrixArray(matrix);

            GratisGame = matrix.GetNumberOfElement(0) >= 3;
            WinFor2 = 0;
            NumberOfGratisGames = GratisGame ? MatrixSpellbook.GRATIS_GAMES : 0;

            CreateLinesInformationsRing(matrix, numberOfLines, bet, AdditionalInformation,
                LineWinsForGames.WinForGratisSpellbook, LineWinsForGames.WinForLinesSpellbook, LineWinsForGames.WinForWildsSpellbook);
            if (NumberOfGratisGames > 0 && AdditionalInformation == 0)
            {
                AdditionalInformation = (byte) SoftwareRng.Next(1, 10);
            }

            WinFor2 = AdditionalInformation;

            if (gratisElement != 0 && matrix.IsCanBeTransformed(gratisElement))
            {
                for (var i = 0; i < 5; i++)
                {
                    PositionFor2[i] = matrix.IsReelHave(i, gratisElement) ? (byte) 1 : (byte) 0;
                }
            }
        }

        /// <summary>
        /// Transformiše matricu za igru 'Diamonds' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixDiamonds matrix, int numberOfLines, int bet)
        {
            FillMatrixArray(matrix);

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformationsDiamonds(matrix, numberOfLines, bet, matrix.GetNoLineWin(0, LineWinsForGames.WinForScatterDiamonds));
        }

        /// <summary>
        /// Transformiše matricu za igru 'HotStars' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="stars">Da li su i u kojim rilovima aktivirane zvezde - bit najmanje težine je za drugi ril</param>
        /// <param name="addFenix">Da li da dinamički doda fenikse ili ne? (uglavnom za potrebe simulacije)</param>
        public void MatrixToCombination(MatrixHotStars matrix, int numberOfLines, int bet, byte stars, bool addFenix = true)
        {
            if (addFenix)
            {
                AddFenix(matrix, 54);
            }
            FillMatrixArray(matrix);

            CreateEmptyArray(PositionFor2);
            GratisGame = false;
            NumberOfGratisGames = 0;
            AdditionalInformation = stars;
            var nextPosition = 0;
            for (var i = 1; i < 4; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (Matrix[i, j] == 0)
                    {
                        AdditionalInformation |= (byte)(1 << (i - 1));
                        if ((stars & (byte)(1 << (i - 1))) == 0)
                        {
                            GratisGame = true;
                            NumberOfGratisGames = 1;
                        }
                        if ((AdditionalInformation & (byte)(1 << (i - 1))) != 0
                            && (stars & (byte)(1 << (i - 1))) == 0)
                        {
                            PositionFor2[nextPosition++] = (byte)(j * 5 + i);
                        }
                    }
                }
            }
            TransformStarMatrix(matrix, stars);
            FillMatrixArray(matrix);
            TransformStarMatrix(matrix, AdditionalInformation);
            if (AdditionalInformation == stars)
            {
                AdditionalInformation = 0;
            }

            WinFor2 = 0;
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            CreateLinesInformationsStars(matrix, numberOfLines, bet);
        }

        /// <summary>
        /// Transformiše matricu za igru 'MegaHot' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixMegaHot matrix, int bet)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            FillMatrixArray(matrix);

            WinFor2 = matrix.GetMultiplicator();
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            CreateLinesInformations(matrix, 5, bet, WinFor2, -1, null,
                GlobalData.GameLineExtra, matrix.GetNoLineWin(7, LineWinsForGames.WinForScatterMegaHot), 7);
        }

        /// <summary>
        /// Transformiše matricu za igru 'TurboHot40' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="numberOfLines"></param>
        public void MatrixToCombination(MatrixTurboHot40 matrix, int bet, int numberOfLines)
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

            CreateLinesInformationsTurbo(matrix, numberOfLines, bet, 0, LineWinsForGames.WinForWildsTurboHot40, GlobalData.GameLineTurbo,
                matrix.GetNoLineWin(2, LineWinsForGames.WinForScatterTurboHot40), 2);
        }

        /// <summary>
        /// Transformiše matricu za igru 'CrystalHot40Deluxe' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixCrystalHot40Deluxe matrix, int numberOfLines, int bet)
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

            CreateLinesInformationsCrystalDeluxe(matrix, numberOfLines, bet, 0, LineWinsForGames.WinForWildsTurboHot40, GlobalData.GameLineTurbo,
                matrix.GetNoLineWin(2, LineWinsForGames.WinForScatterTurboHot40), 2);
        }

        /// <summary>
        /// Transformiše matricu za igru 'BookOfBruno' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Da li je gratis igra</param>
        /// <param name="gratisElement">Gratis element u toku gratis igara, 0 inače</param>
        public void MatrixToCombination(MatrixBookOfBruno matrix, int numberOfLines, int bet, bool gratisGame, byte gratisElement)
        {
            AdditionalInformation = !gratisGame ? (byte)0 : gratisElement;
            FillMatrixArray(matrix);

            GratisGame = matrix.GetNumberOfElement(0) >= 3;
            WinFor2 = 0;
            NumberOfGratisGames = GratisGame ? MatrixBookOfBruno.GRATIS_GAMES : 0;

            CreateLinesInformationsRing(matrix, numberOfLines, bet, AdditionalInformation, LineWinsForGames.WinForGratisBookOfBruno, LineWinsForGames.WinForLinesBookOfBruno, LineWinsForGames.WinForWildsBookOfBruno);
            if (NumberOfGratisGames > 0 && AdditionalInformation == 0)
            {
                AdditionalInformation = (byte)SoftwareRng.Next(1, 10);
            }

            WinFor2 = AdditionalInformation;
            if (gratisElement != 0 && matrix.IsCanBeTransformed(gratisElement))
            {
                for (var i = 0; i < 5; i++)
                {
                    PositionFor2[i] = matrix.IsReelHave(i, gratisElement) ? (byte)1 : (byte)0;
                }
            }
        }

        /// <summary>
        /// Transformiše matricu za igru 'TropicalHot' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixTropicalHot matrix, int numberOfLines, int bet)
        {
            FillMatrixArray(matrix);

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, GlobalData.GameLineExtra,
                matrix.GetNoLineWin(2, LineWinsForGames.WinForScattersTropicalHot), 2);
        }

        /// <summary>
        /// Transformiše matricu za igru 'Monsters' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        /// <param name="additionalInformation"></param>
        public void MatrixToCombination(MatrixMonsters matrix, int bet, bool gratisGame, byte additionalInformation)
        {
            if (gratisGame)
            {
                TransformPegasusMatrix(matrix, additionalInformation);
                AdditionalInformation = additionalInformation;
            }

            CreateEmptyArray(PositionFor2);
            GratisGame = false;
            NumberOfGratisGames = 0;
            FillMatrixArray(matrix);
            if (!gratisGame)
            {
                AdditionalInformation = 0;
                var next = 0;
                for (var i = 0; i < 3; i++)
                {
                    if (Matrix[2, i] == 2)
                    {
                        AdditionalInformation |= (byte)(1 << i);
                        GratisGame = true;
                        NumberOfGratisGames = 3;
                        PositionFor2[next++] = (byte)(i * 5 + 2);
                    }
                }
            }

            WinFor2 = 0;
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            CreateLinesInformations(matrix, 20, bet, 1, 0, LineWinsForGames.WinForWildsMonsters,
                GlobalData.GameLineExtra);
        }

        /// <summary>
        /// Transformiše matricu za igru 'Viking Gold' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGamesLeft">Broj preostalih gratis igara</param>
        /// <param name="addArray"></param>
        /// <param name="reels"></param>
        protected void MatrixToCombinationPrivate(MatrixVikingGold matrix, int bet, int gratisGamesLeft, ref byte[] addArray, params List<byte>[] reels)
        {
            var offset = gratisGamesLeft > 0 ? 38 : 0;
            var gratisMult = gratisGamesLeft > 0 ? 3 : 1;

            var multiply = addArray[offset + 35];
            var matrixArray = multiply == 1 ? GetFirstCascadeMatrix(gratisGamesLeft > 0, ref addArray, reels) : GetNextCascadeMatrix(gratisGamesLeft > 0, ref addArray, reels);
            matrix.FromMatrixArray(matrixArray);
            FillMatrixArray(matrix);
            CreateLinesInformationsViking(matrix, 20, bet, gratisMult * multiply, 0, GlobalData.GameLineExtra);
            WinFor2 = 0;
            NumberOfGratisGames = 0;
            GratisGame = false;
            if (LinesInformation.Length > 0 && matrix.GetNumberOfElement(1) < 3)
            {
                addArray[offset + 35] = (byte)Math.Min(5, multiply + 1 + (multiply / 3));
            }
            else
            {
                addArray[offset + 35] = 1;
            }
            for (var i = 0; i < 15; i++)
            {
                addArray[offset + 15 + i] = 0;
            }
            AdditionalInformation = multiply;
            foreach (var lineInfo in LinesInformation)
            {
                if (lineInfo.WinningElement == 1)
                {
                    GratisGame = true;
                    NumberOfGratisGames += 10;
                }
                for (var i = 0; i < 5; i++)
                {
                    if (lineInfo.WinningPosition[i] < 15)
                    {
                        addArray[offset + 15 + lineInfo.WinningPosition[i]] = 1;
                    }
                }
            }

            AdditionalArray = (byte[])addArray.Clone();
        }

        /// <summary>
        /// Transformiše matricu za igru 'Viking Gold' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGamesLeft">Broj preostalih gratis igara</param>
        /// <param name="addArray"></param>
        /// <param name="reels"></param>
        public void MatrixToCombination(MatrixVikingGold matrix, int bet, int gratisGamesLeft, ref byte[] addArray, params List<byte>[] reels)
        {
            MatrixToCombinationPrivate(matrix, bet, gratisGamesLeft, ref addArray, reels);
            CascadeList = new List<ICombination>();
            if (TotalWin == 0 || GratisGame)
            {
                return;
            }
            bool b;
            do
            {
                var cmb = new Combination();
                cmb.MatrixToCombinationPrivate(matrix, bet, gratisGamesLeft, ref addArray, reels);
                b = cmb.TotalWin > 0 && !cmb.GratisGame;
                GratisGame = cmb.GratisGame;
                NumberOfGratisGames = cmb.NumberOfGratisGames;
                CascadeList.Add(cmb);
            } while (b);
        }

        /// <summary>
        /// Postavlja polja za Vikinge iz poslednje ruke nakon pucanja.
        /// </summary>
        /// <param name="newCombination"></param>
        public void SetVikingGoldGratisData(ICombination newCombination)
        {
            GratisGame = newCombination.GratisGame;
            NumberOfGratisGames = newCombination.NumberOfGratisGames;
        }

        /// <summary>
        /// Transformiše matricu za igru 'ForestFruits' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixForestFruits matrix, int numberOfLines, int bet)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            FillMatrixArray(matrix);

            WinFor2 = 0;
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 2, LineWinsForGames.WinForWildsForestFruits,
                GlobalData.GameLineExtra, matrix.GetNoLineWin(1, LineWinsForGames.WinForScattersForestFruits), 1);
        }

        /// <summary>
        /// Transformiše matricu za igru 'StarGems' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="stars">Da li su i u kojim rilovima aktivirani wildovi - bit najmanje težine je za drugi ril</param>
        public void MatrixToCombination(MatrixStarGems matrix, int numberOfLines, int bet, byte stars)
        {
            AddFenix(matrix, 34);
            FillMatrixArray(matrix);

            CreateEmptyArray(PositionFor2);
            GratisGame = false;
            NumberOfGratisGames = 0;
            AdditionalInformation = stars;
            var nextPosition = 0;
            for (var i = 1; i < 4; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (Matrix[i, j] == 0)
                    {
                        AdditionalInformation |= (byte)(1 << (i - 1));
                        if ((stars & (byte)(1 << (i - 1))) == 0)
                        {
                            GratisGame = true;
                            NumberOfGratisGames = 1;
                        }
                        if ((AdditionalInformation & (byte)(1 << (i - 1))) != 0
                            && (stars & (byte)(1 << (i - 1))) == 0)
                        {
                            PositionFor2[nextPosition++] = (byte)(j * 5 + i);
                        }
                    }
                }
            }
            TransformStarMatrix(matrix, stars);
            FillMatrixArray(matrix);
            TransformStarMatrix(matrix, AdditionalInformation);
            if (AdditionalInformation == stars)
            {
                AdditionalInformation = 0;
            }

            CreateLinesInformationsStars(matrix, numberOfLines, bet);
        }

        /// <summary>
        /// Transformiše matricu za igru 'NeonHot' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixNeonHot5 matrix, int numberOfLines, int bet)
        {
            FillMatrixArray(matrix);

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, GlobalData.GameLineExtra,
                matrix.GetNoLineWin(0, LineWinsForGames.WinForScattersNeonHot), 0);
        }

        /// <summary>
        /// Transformiše matricu za igru 'Pirates' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Da li je u toku gratid igra</param>
        /// <param name="pirateGame">Da li su i u kojim linijama aktivirani wild rilovi</param>
        public void MatrixToCombination(MatrixPirates matrix, int numberOfLines, int bet, bool gratisGame, byte pirateGame)
        {
            CreateEmptyArray(PositionFor2);
            AdditionalInformation = 0;
            if (matrix.GetNumberOfElement(1) >= 2)
            {
                for (var i = 0; i < 3; i++)
                {
                    if (matrix.IsReelHave(i, 1))
                    {
                        AdditionalInformation |= (byte)(1 << i);
                        PositionFor2[i] = 1;
                    }
                    else
                    {
                        PositionFor2[i] = 0;
                    }
                }
            }

            if (gratisGame)
            {
                TransformPiramideMatrix(matrix, pirateGame);
                AdditionalInformation = pirateGame;
            }
            FillMatrixArray(matrix);

            if (matrix.GetNumberOfElement(0xC) > 0)
            {
                GratisGame = true;
                NumberOfGratisGames = 1;
                AdditionalInformation = pirateGame;
            }
            else
            {
                var k = matrix.GetNumberOfElement(1);
                GratisGame = k >= 2;
                NumberOfGratisGames = GratisGame ? (k == 3 ? 5 : 3) : 0;
            }
            CreateEmptyArray(GratisGamesPositions);
            CreateEmptyArray(GratisGamesValues);

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, LineWinsForGames.WinForWildsPirates, GlobalData.GameLineExtra);
        }

        /// <summary>
        /// Transformiše matricu za igru 'Magic Target' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Da li je gratis igra</param>
        /// <param name="useJokers"></param>
        public void MatrixToCombination(MatrixPokerSlot matrix, int numberOfLines, int bet, bool gratisGame, bool useJokers = true)
        {
            AdditionalInformation = (byte)numberOfLines;
            WinFor2 = 0;
            if (gratisGame)
            {
                WinFor2 = 1;
            }
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            var jokers = 0;
            if (!gratisGame && useJokers)
            {
                var probsJoker = new[] { 9, 6, 3, 6, 9 };
                var prJokerFrom = new[] { 56, 54, 53, 54, 55 };
                for (var i = 0; i < 5; i++)
                {
                    if (SoftwareRng.Next(prJokerFrom[i]) < probsJoker[i])
                    {
                        Matrix[i, SoftwareRng.Next(3)] += 100;
                        jokers++;
                    }
                }
            }

            NumberOfGratisGames = MatrixPokerSlot.NumberOfGratis[jokers];
            GratisGame = NumberOfGratisGames > 0;

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.GetLineWin(i, gratisGame);
                if (win == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte) (i - 1),
                    Win = MatrixPokerSlot.WinToInt(win) * bet,
                    WinningElement = (byte) win,
                    WinningPosition = matrix.GetLine(i).GetLinesPositions(i, win, gratisGame)
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (GratisGame)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = MatrixPokerSlot.GetJokerPositionsArray(Matrix),
                    Id = EXTRA_LINE,
                    Win = 0,
                    WinningElement = 52
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.OrderByDescending(x => x.Win).ToArray();
        }

        /// <summary>
        /// Daje podatke za igru LuckyTwister.
        /// </summary>
        /// <param name="bet"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        protected static List<LineInfo> GetTwisterLineInformation(int bet, MatrixLuckyTwister matrix)
        {
            var lineInfo = new List<LineInfo>();
            var clasters = matrix.GetClasterMatrix();
            var count = new int[30];
            var elem = new int[30];
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    count[clasters[i, j] - 1]++;
                    elem[clasters[i, j] - 1] = matrix.GetElement(i, j + 1);
                }
            }

            for (var i = 0; i < 30; i++)
            {
                if (count[i] >= 9)
                {
                    var li = new LineInfo
                    {
                        Id = (byte)i,
                        WinningElement = (byte)elem[i],
                        Win = MatrixLuckyTwister.Win[count[i] - 9, elem[i]] * bet,
                        WinningPosition = MatrixLuckyTwister.GetPositionsFromClasterMatrix(clasters, i + 1)
                    };
                    lineInfo.Add(li);
                }
            }
            return lineInfo;
        }

        /// <summary>
        /// Transformiše matricu za igru 'Lucky Twister' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixLuckyTwister matrix, int bet)
        {
            Matrix = new byte[6, 7];
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 7; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            LinesInformation = GetTwisterLineInformation(bet, matrix).ToArray();
            TotalWin = LinesInformation.Sum(x => x.Win);

            WinFor2 = matrix.TwinStart;
            AdditionalInformation = (byte)matrix.TwinCount;
        }

        /// <summary>
        /// Transformiše matricu za igru 'Book of Mayan Gold' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Da li je gratis igra</param>
        /// <param name="gratisElement">Gratis element u toku gratis igara, 0 inače</param>
        /// <param name="addArray"></param>
        /// <param name="config"></param>
        public void MatrixToCombination(MatrixBookOfMayanGold matrix, int numberOfLines, int bet, bool gratisGame, byte gratisElement, ref byte[] addArray, MatrixBookOfMayanGold.BookOfMayanGoldConfig config)
        {
            if (config.RespinReelId != -1 && config.RespinBasicBet > 0 && !gratisGame)
            {
                bet = (int)config.RespinBasicBet;
            }

            var oldMatrix = new MatrixBookOfMayanGold();
            if (config.RespinReelId != -1 && !gratisGame)
            {
                for (var i = 0; i < 15; i++)
                {
                    oldMatrix.SetElement(i / 3, i % 3, addArray[i]);
                    if (config.RespinReelId != i / 3)
                    {
                        matrix.SetElement(i / 3, i % 3, addArray[i]);
                    }
                }
            }

            AdditionalInformation = !gratisGame ? (byte)0 : gratisElement;
            FillMatrixArray(matrix);

            var gratisElem = matrix.GetNumberOfElement(0);
            GratisGame = gratisElem >= 3;
            WinFor2 = 0;
            NumberOfGratisGames = GratisGame ? MatrixBookOfMayanGold.GratisGames[gratisElem - 3] : 0;

            CreateLinesInformationsRing(matrix, numberOfLines, bet, AdditionalInformation, LineWinsForGames.WinForGratisBookOfMayanGold, LineWinsForGames.WinForLinesBookOfMayanGold, LineWinsForGames.WinForWildsBookOfMayanGold);
            if (NumberOfGratisGames > 0 && AdditionalInformation == 0)
            {
                AdditionalInformation = (byte)SoftwareRng.Next(1, 10);
            }

            WinFor2 = AdditionalInformation;

            for (var i = 0; i < 15; i++)
            {
                addArray[i] = (byte)matrix.GetElement(i / 3, i % 3);
            }

            if (gratisElement != 0 && matrix.IsCanBeTransformed(gratisElement))
            {
                for (var i = 0; i < 5; i++)
                {
                    PositionFor2[i] = matrix.IsReelHave(i, gratisElement) ? (byte)1 : (byte)0;
                }
            }
        }

        /// <summary>
        /// Ako su samo dobici sačinjeni od dva elementa i ne mora da se širi četvrti ril; nakon širenja vajldova se poziva.
        /// </summary>
        /// <param name="lineInfo"></param>
        /// <param name="position2"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        protected static byte[] FixExpandBursting(IEnumerable<LineInfo> lineInfo, byte[] position2, MatrixBurstingHot5 matrix)
        {
            var shouldBeFixed = new[] { true, true, true };
            foreach (var info in lineInfo)
            {
                for (var i = 0; i < 5; i++)
                {
                    var el = info.WinningPosition[i];
                    if (el < 15 && matrix.GetElement(el % 5, el / 5) == 0)
                    {
                        shouldBeFixed[el % 5 - 1] = false;
                    }
                }
            }
            for (var i = 0; i < 5; i++)
            {
                if (position2[i] < 15)
                {
                    if (shouldBeFixed[position2[i] % 5 - 1])
                    {
                        position2[i] = 255;
                    }
                }
            }
            Array.Sort(position2);
            return position2;
        }

        /// <summary>
        /// Transformiše matricu za igru 'BurstingHot5' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixBurstingHot5 matrix, int numberOfLines, int bet)
        {
            FillMatrixArray(matrix);

            CreateEmptyArray(PositionFor2);
            GratisGame = false;
            NumberOfGratisGames = 0;
            FillMatrixArray(matrix);
            var nextPosition = 0;
            LineInfo li9 = null, li10 = null;
            var no9 = matrix.GetNumberOfElement(9);
            if (no9 >= 3)
            {
                li9 = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(9),
                    Id = EXTRA_LINE,
                    Win = LineWinsForGames.WinForScatter1BurstingHot5[no9 - 1] * bet * numberOfLines,
                    WinningElement = 9
                };
            }
            if (matrix.GetNumberOfElement(10) == 3)
            {
                li10 = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(10),
                    Id = EXTRA_LINE,
                    Win = LineWinsForGames.WIN_FOR_SCATTER2_BURSTING_HOT5 * bet * numberOfLines,
                    WinningElement = 10
                };
            }
            for (var i = 1; i < 4; i++)
            {
                var haveWild = false;
                for (var j = 0; j < 3; j++)
                {
                    if (matrix.GetElement(i, j) == 0)
                    {
                        PositionFor2[nextPosition++] = (byte)(j * 5 + i);
                        haveWild = true;
                    }
                }
                if (haveWild)
                {
                    matrix.SetElement(i, 0, 0);
                    matrix.SetElement(i, 1, 0);
                    matrix.SetElement(i, 2, 0);
                }
            }

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, LineWinsForGames.WinForWildBurstingHot5, GlobalData.GameLineExtra);
            var li = LinesInformation.ToList();
            if (li9 == null && li10 == null && TotalWin > 0 && matrix.GetNumberOfElement(0) > 0)
            {
                li.Insert(0, new LineInfo { Id = EXTRA_LINE, Win = 0, WinningElement = 11, WinningPosition = matrix.GetPositionsArray(11) });
                NumberOfWinningLines++;
            }
            if (li9 != null)
            {
                TotalWin += li9.Win;
                li.Insert(0, li9);
                NumberOfWinningLines++;
            }
            if (li10 != null)
            {
                TotalWin += li10.Win;
                li.Insert(0, li10);
                NumberOfWinningLines++;
            }
            PositionFor2 = FixExpandBursting(LinesInformation, PositionFor2, matrix);
            LinesInformation = li.ToArray();
        }

        /// <summary>
        /// Transformiše matricu za igru 'GoldenCrown' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixGoldenCrown matrix, int numberOfLines, int bet)
        {
            FillMatrixArray(matrix);

            CreateEmptyArray(PositionFor2);
            GratisGame = false;
            NumberOfGratisGames = 0;
            FillMatrixArray(matrix);
            var nextPosition = 0;
            LineInfo li9 = null, li10 = null;
            var no9 = matrix.GetNumberOfElement(9);
            if (no9 >= 3)
            {
                li9 = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(9),
                    Id = EXTRA_LINE,
                    Win = LineWinsForGames.WinForScatter1GoldenCrown[no9 - 1] * bet * numberOfLines,
                    WinningElement = 9
                };
            }
            if (matrix.GetNumberOfElement(10) == 3)
            {
                li10 = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(10),
                    Id = EXTRA_LINE,
                    Win = LineWinsForGames.WIN_FOR_SCATTER2_GOLDEN_CROWN * bet * numberOfLines,
                    WinningElement = 10
                };
            }
            for (var i = 1; i < 4; i++)
            {
                var haveWild = false;
                for (var j = 0; j < 3; j++)
                {
                    if (matrix.GetElement(i, j) == 0)
                    {
                        PositionFor2[nextPosition++] = (byte)(j * 5 + i);
                        haveWild = true;
                    }
                }
                if (haveWild)
                {
                    matrix.SetElement(i, 0, 0);
                    matrix.SetElement(i, 1, 0);
                    matrix.SetElement(i, 2, 0);
                }
            }

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, LineWinsForGames.WinForWildGoldenCrown, GlobalData.GameLineExtra);
            var li = LinesInformation.ToList();
            if (li9 == null && li10 == null && TotalWin > 0 && matrix.GetNumberOfElement(0) > 0)
            {
                li.Insert(0, new LineInfo { Id = EXTRA_LINE, Win = 0, WinningElement = 11, WinningPosition = matrix.GetPositionsArray(11) });
                NumberOfWinningLines++;
            }
            if (li9 != null)
            {
                TotalWin += li9.Win;
                li.Insert(0, li9);
                NumberOfWinningLines++;
            }
            if (li10 != null)
            {
                TotalWin += li10.Win;
                li.Insert(0, li10);
                NumberOfWinningLines++;
            }
            PositionFor2 = FixExpandBursting(LinesInformation, PositionFor2, matrix);
            LinesInformation = li.ToArray();
        }

        /// <summary>
        /// Transformiše matricu za igru 'WildClover40' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        public void MatrixToCombination(MatrixWildClover40 matrix, int numberOfLines, int bet, bool gratisGame)
        {
            Matrix = new byte[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            var scatterNum = matrix.GetNumberOfElement(11);
            GratisGame = scatterNum >= 3;
            NumberOfGratisGames = GratisGame ? MatrixWildClover40.GratisNumber[scatterNum - 3] : 0;

            CreateLinesInformationsTurbo(matrix, numberOfLines, bet, 0, LineWinsForGames.WinForWildsWildClover40, GlobalData.GameLineTurbo);
        }

        /// <summary>
        /// Transformiše matricu za igru 'FruityJokerHot' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixFruityJokerHot matrix, int numberOfLines, int bet)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            FillMatrixArray(matrix);

            WinFor2 = 0;
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, LineWinsForGames.WinForWildsFruityJokerHot,
                GlobalData.GameLineExtra, matrix.GetNoLineWin(7, LineWinsForGames.WinForScatterFruityJokerHot), 7);
        }

        /// <summary>
        /// Transformiše matricu za igru 'CloverCash' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        /// <param name="addArray"></param>
        public void MatrixToCombination(MatrixCloverCash matrix, int numberOfLines, int bet, bool gratisGame, ref byte[] addArray)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            int table;
            var lockWins = new List<LineInfo>();
            if (gratisGame)
            {
                table = addArray[16];
                addArray[15]--;
                var elemNum = 0;
                for (var i = 0; i < 15; i++)
                {
                    if (addArray[i] > 0)
                    {
                        elemNum++;
                    }
                }
                for (var i = 0; i < 15; i++)
                {
                    if (addArray[i] == 0)
                    {
                        if (SoftwareRng.Next() < MatrixCloverCash.GratisLockProbs[elemNum - 6])
                        {
                            var index = MatrixCloverCash.GetRandomIndexByTable(table);
                            var lockwin = MatrixCloverCash.GetWinByIndex(index, table);
                            TotalWin += lockwin * bet;
                            addArray[i] = (byte)(index + 1);
                            addArray[15] = 3;
                            elemNum++;
                            lockWins.Add(new LineInfo { Id = EXTRA_LINE, WinningElement = 11, Win = lockwin, WinningPosition = new byte[] { (byte)i, 0, 0, 0, 0 } });
                        }
                    }
                }
                for (var i = 0; i < 5; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        Matrix[i, j] = addArray[5 * j + i];
                    }
                }
                LinesInformation = new LineInfo[0];
                AdditionalArray = addArray;
                if (elemNum == 15)
                {
                    TotalWin += MatrixCloverCash.GRAND_JACKPOT * bet;
                    lockWins.Add(new LineInfo { Id = 253, WinningElement = 11, Win = MatrixCloverCash.GRAND_JACKPOT * bet, WinningPosition = new byte[] { 0, 0, 0, 0, 0 } });
                }
                if (addArray[15] > 0 && elemNum < 15)
                {
                    GratisGame = true;
                    NumberOfGratisGames = 1;
                }
                if (lockWins.Count > 0)
                {
                    var li = LinesInformation.ToList();
                    li.AddRange(lockWins);
                    LinesInformation = li.ToArray();
                }
                return;
            }
            for (var i = 0; i < 17; i++)
            {
                addArray[i] = 0;
            }
            table = MatrixCloverCash.ChooseTable();
            addArray[16] = (byte)table;
            FillMatrixArray(matrix);

            WinFor2 = 0;
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, LineWinsForGames.WinForWildsCloverCash, GlobalData.GameLineExtra);

            var lockit = matrix.GetNumberOfElement(11);
            if (lockit == 15)
            {
                TotalWin += MatrixCloverCash.GRAND_JACKPOT * bet;
                lockWins.Add(new LineInfo { Id = 253, WinningElement = 11, Win = MatrixCloverCash.GRAND_JACKPOT * bet, WinningPosition = new byte[] { 0, 0, 0, 0, 0 } });
            }
            if (lockit >= 6 && lockit < 15)
            {
                GratisGame = true;
                NumberOfGratisGames = 1;
                addArray[15] = 3;
                for (var i = 0; i < 5; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        if (matrix.GetElement(i, j) == 11)
                        {
                            var index = MatrixCloverCash.GetRandomIndexByTable(table);
                            var lockwin = MatrixCloverCash.GetWinByIndex(index, table);
                            TotalWin += lockwin * bet;
                            addArray[5 * j + i] = (byte)(index + 1);
                            lockWins.Add(new LineInfo { Id = EXTRA_LINE, WinningElement = 11, Win = lockwin, WinningPosition = new byte[] { (byte)(5 * j + i), 0, 0, 0, 0 } });
                        }
                    }
                }
            }
            if (lockWins.Count > 0)
            {
                var li = LinesInformation.ToList();
                li.AddRange(lockWins);
                LinesInformation = li.ToArray();
            }
            AdditionalArray = addArray;
        }

        /// <summary>
        /// Transformiše matricu za igru 'MegaCubesSeluxe' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixMegaCubesDeluxe matrix, int numberOfLines, int bet)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            FillMatrixArray(matrix);

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, GlobalData.GameLineExtra,
                matrix.GetNoLineWin(0, LineWinsForGames.WinForScatterMegaCubesDeluxe), 0);
        }

        /// <summary>
        /// Transformiše matricu za igru 'BookOfLuxorDouble' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Da li je gratis igra</param>
        /// <param name="gratisElement">Gratis element u toku gratis igara, 0 inače</param>
        public void MatrixToCombination(MatrixBookOfDouble matrix, int numberOfLines, int bet, bool gratisGame, byte gratisElement)
        {
            CreateEmptyArray(PositionFor2);
            AdditionalInformation = !gratisGame ? (byte)0 : gratisElement;
            FillMatrixArray(matrix);
            var firstBonus = (byte)(AdditionalInformation >> 4);
            var secondBonus = (byte)(AdditionalInformation & 0x0F);

            GratisGame = matrix.GetNumberOfElement(0) >= 3;
            WinFor2 = 0;
            NumberOfGratisGames = GratisGame ? MatrixBookOfDouble.GRATIS_GAMES : 0;

            if (AdditionalInformation != 0)
            {
                var transform1 = matrix.IsCanBeTransformed(firstBonus);
                var transform2 = matrix.IsCanBeTransformed(secondBonus);
                for (var i = 0; i < 5; i++)
                {
                    PositionFor2[i] = 0;
                    if (transform1 && matrix.IsReelHave(i, firstBonus))
                    {
                        PositionFor2[i] += 2;
                    }
                    if (transform2 && matrix.IsReelHave(i, secondBonus))
                    {
                        PositionFor2[i] += 1;
                    }
                }
            }

            CreateLinesInformationsDouble(matrix, numberOfLines, bet, firstBonus, secondBonus, MatrixBookOfDouble.WinForGratisBookOfDouble, MatrixBookOfDouble.WinForLinesBookOfDouble, MatrixBookOfDouble.WinForWildsBookOfDouble);
            if (NumberOfGratisGames > 0 && AdditionalInformation == 0)
            {
                firstBonus = (byte)SoftwareRng.Next(1, 10);
                secondBonus = (byte)SoftwareRng.Next(1, 9);
                if (firstBonus == secondBonus)
                {
                    secondBonus = 9;
                }
                AdditionalInformation = (byte)((firstBonus << 4) + secondBonus);
            }

            WinFor2 = (firstBonus << 8) + secondBonus;
        }

        /// <summary>
        /// Transformiše matricu za igru 'Matrix40WildClover6' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        public void MatrixToCombination(Matrix40WildClover6 matrix, int numberOfLines, int bet, bool gratisGame)
        {
            Matrix = new byte[6, 6];
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }
            var scattersNumber = matrix.GetNumberOfElement(11);
            GratisGame = scattersNumber >= 3;
            NumberOfGratisGames = GratisGame ? Matrix40WildClover6.GratisNumber[scattersNumber - 3] : 0;

            CreateLinesInformation40WildClover6(matrix, numberOfLines, bet, 1, 0, Matrix40WildClover6.WinForWild40WildClover6, GlobalData.GameLineWildClover6Reels);
        }

        /// <summary>
        /// Transformiše matricu za igru 'LilaWild' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixLilaWild matrix, int numberOfLines, int bet)
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

            CreateLinesInformationsTurbo(matrix, numberOfLines, bet, 0, MatrixLilaWild.WinForWildsLilaWild, GlobalData.GameLineTurbo,
                matrix.GetNoLineWin(9, MatrixLilaWild.WinForScatterLilaWild), 9);
        }

        /// <summary>
        /// Transformiše matricu za igru 'ElGrandeToro' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        /// <param name="addArray"></param>
        public void MatrixToCombination(MatrixElGrandeToro matrix, int numberOfLines, int bet, bool gratisGame, ref byte[] addArray)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            FillMatrixArray(matrix);

            if (gratisGame)
            {
                for (var i = 0; i < 15; i++)
                {
                    var tmp = matrix.GetElement(i % 5, i / 5);
                    if (addArray[i] > 0)
                    {
                        matrix.SetElement(i % 5, i / 5, 0);
                        addArray[i] = 2;
                    }
                    if (tmp == 0 && addArray[i] == 0)
                    {
                        addArray[i] = 1;
                    }
                    if (addArray[i] > 0)
                    {
                        Matrix[i % 5, i / 5] = 11;
                    }
                }
            }
            else
            {
                for (var i = 0; i < 15; i++)
                {
                    addArray[i] = 0;
                }
            }

            WinFor2 = 0;
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, MatrixElGrandeToro.WinForWildElGrandeToro, GlobalData.GameLineExtra);

            if (!gratisGame && matrix.GetNumberOfElement(10) == 3)
            {
                GratisGame = true;
                NumberOfGratisGames = 10;
                var li = LinesInformation.ToList();
                li.Add(new LineInfo { Id = EXTRA_LINE, Win = 0, WinningElement = 10, WinningPosition = matrix.GetPositionsArray(10) });
                LinesInformation = li.ToArray();
            }

            AdditionalArray = addArray;
        }

        /// <summary>
        /// Transformiše matricu za igru 'CrystalHot40Free' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        public void MatrixToCombination(MatrixTurboHot40 matrix, int numberOfLines, int bet, bool gratisGame)
        {
            var gratisCount = new[] { 7, 10, 15 };
            Matrix = new byte[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            var numSket = matrix.GetNumberOfElement(2);
            GratisGame = numSket > 2;
            if (GratisGame)
            {
                NumberOfGratisGames = gratisGame ? 3 : gratisCount[numSket - 3];
            }

            CreateLinesInformationsTurbo(matrix, numberOfLines, bet, 0, LineWinsForGames.WinForWildsTurboHot40, GlobalData.GameLineTurbo);

            if (GratisGame)
            {
                var li = LinesInformation.ToList();
                li.Add(new LineInfo { Id = EXTRA_LINE, Win = 0, WinningElement = 2, WinningPosition = matrix.GetPositionsArray(2) });
                LinesInformation = li.ToArray();
            }
        }

        /// <summary>
        /// Transformiše matricu za igru 'SevenClassicSlot' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Da li je gratis igra</param>
        public void MatrixToCombination(MatrixSevenClassicHot matrix, int numberOfLines, int bet, bool gratisGame)
        {
            FillMatrixArray(matrix);

            GratisGame = matrix.GetNumberOfElement(8) >= 3;
            NumberOfGratisGames = GratisGame ? MatrixSevenClassicHot.GRATIS_GAMES : 0;
            CreateEmptyArray(PositionFor2);
            CreateEmptyArray(MultiplyFor2);

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, MatrixSevenClassicHot.WinForWildSevenClassicHot,
                GlobalData.GameLineExtra, matrix.GetNoLineWin(8, MatrixSevenClassicHot.WinForScatterSevenClassicHot), 8);
        }

        /// <summary>
        /// Transformiše matricu za igru 'BonusBells' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        public void MatrixToCombination(MatrixBonusBells matrix, int numberOfLines, int bet, bool gratisGame)
        {
            if (gratisGame)
            {
                matrix.SetElement(0, 0, 0);
                matrix.SetElement(0, 1, 0);
                matrix.SetElement(0, 2, 0);
                matrix.SetElement(4, 0, 0);
                matrix.SetElement(4, 1, 0);
                matrix.SetElement(4, 2, 0);
            }
            FillMatrixArray(matrix);

            CreateEmptyArray(PositionFor2);
            GratisGame = matrix.GetNumberOfElement(9) == 3 && !gratisGame;
            NumberOfGratisGames = GratisGame ? MatrixBonusBells.GRATIS_GAMES : 0;
            var nextPosition = 0;
            for (var i = 1; i < 4; i++)
            {
                var haveWild = false;
                for (var j = 0; j < 3; j++)
                {
                    if (matrix.GetElement(i, j) == 0)
                    {
                        PositionFor2[nextPosition++] = (byte)(j * 5 + i);
                        haveWild = true;
                    }
                }
                if (haveWild)
                {
                    matrix.SetElement(i, 0, 0);
                    matrix.SetElement(i, 1, 0);
                    matrix.SetElement(i, 2, 0);
                }
            }

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, MatrixBonusBells.WinForWildBonusBells, GlobalData.GameLineExtra);
        }

        public byte[] ToByteArray(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis, ICombination combination)
        {
            /*switch (game)
            {
                case Games.Postman:
                case Games.AlohaCharm:
                case Games.DolphinsShine:
                case Games.SevenClassicHot:
                    return GamePostmanConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.DeepJungle:
                case Games.BurstingHot5:
                case Games.BurstingHot40:
                case Games.GoldenCrown:
                case Games.VeryHot5:
                case Games.VeryHot40:
                case Games.BurstingHot40Mozzart:
                case Games.GoldenCrownMaxbet:
                case Games.BurstingHot5Admiral:
                case Games.GoldenCrown20BalkanBet:
                case Games.BigBuddha:
                    return GameJungleConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.Wizard:
                case Games.KatanasOfTime:
                    return GameWizardConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.FruitsAndStars:
                case Games.FruitsAndStars40:
                case Games.ForestFruits:
                case Games.CubesAndStars:
                case Games.FruityHot:
                case Games.StarsOfOktagon:
                    return GameFruitsConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.HotParty:
                case Games.NeonHot5:
                case Games.NeonDice5:
                case Games.FluoDice5:
                case Games.FluoHot5:
                case Games.MegaCubesDeluxe:
                    return GameHotPartyConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.CaptainShark:
                    return GameCaptainSharkConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.MagicTarget:
                    return GameMagicTargetConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.MagicOfTheRing:
                    return GameMagicOfTheRingConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.Spellbook:
                case Games.BookOfSpells:
                case Games.BookOfBruno:
                case Games.BookOfSpellsV2:
                case Games.DiceOfSpells:
                case Games.BookOfSpellsDeluxe:
                case Games.BookOfLuxorDouble:
                    return GameMagicOfTheRingConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.WildWest:
                case Games.Monsters:
                    return GameWildWestConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.Pyramid:
                case Games.WildKingdom:
                case Games.EyeOfTut:
                    return GamePyramidConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.Alpinist:
                    return GameAlpinistConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.Diamonds:
                case Games.JewelsBeat:
                    return GameDiamondsConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.HotStars:
                case Games.StarGems:
                case Games.SpaceGuardians:
                case Games.Starlight:
                case Games.StarRunner:
                case Games.CrystalWin:
                case Games.CrystalJewels:
                case Games.WinningStars:
                    return GameHotStarsConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.JuicyHot:
                case Games.TwinklingHot5:
                case Games.TwinklingHot40:
                case Games.JazzyFruits:
                case Games.FruityJokerHot:
                case Games.TwinklingHot80:
                case Games.LuckyBrilliants:
                    return GameHotPartyConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.MegaHot:
                    return GameMegaHotConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.TropicalHot:
                    return GameHotPartyConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.Pirates:
                    return GamePyramidConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                default:
                    Logger.LogError(ErrorCodes.Modules.ParameterLoader + ": " + "Error ToByteArray doesnt exist for: " + game);
                    return null;
            }*/
            return null;
        }

        public object ToJson(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis, ICombination combination)
        {
            /*switch (game)
            {
                case Games.TurboHot40:
                case Games.CrystalHot40:
                case Games.TurboDice40:
                case Games.CrystalHot40Gd:
                case Games.CrystalHot80:
                case Games.CrystalHot40Deluxe:
                case Games.CrystalHot40Gd2:
                case Games.WildHot40:
                case Games.FruityWin40:
                case Games.CrystalHot401X:
                case Games.CrystalJokerHot:
                case Games.CrystalHotAdmiral:
                case Games.CrystalHot40Soccer:
                case Games.WildHot40Meridian:
                case Games.CrystalHot40Pw:
                case Games.CrystalHot100:
                case Games.TurboHot80:
                case Games.TurboHot100:
                case Games.TurboPinn40:
                    return GameTurboHot40Conversion.ToJsonObject(combination);
                case Games.ActionHot40:
                    return GameTurboHot40Conversion.ToSlotDataResV3(combination);
                case Games.WildClover40:
                case Games.WildCloverDice40:
                    return GameWildClover40Conversion.ToSlotDataResV3(combination);
                case Games.CrystalHot40Free:
                    return GameWildClover40Conversion.ToJsonObject(combination, numOfGratisGames, isCurrentGameGratis);
                case Games.VikingGold:
                    return GameVikingGoldConversion.ToJsonObject(combination, numOfGratisGames, isCurrentGameGratis);
                case Games.SpinCards:
                    return GameSpinCardsConversion.ToSlotDataResV3(combination);
                case Games.LuckyTwister:
                case Games.BuffaloFortune:
                    return GameLuckyTwisterConversion.ToSlotDataResV3(combination);
                case Games.BookOfMayanGold:
                    return GameBookOfMayanGoldConversion.ToSlotDataResV3(combination);
                case Games.FireClover5:
                case Games.FireCloverDice5:
                case Games.FireClover40:
                case Games.FireDice40:
                    return GameBurstingHot5Conversion.ToSlotDataResV3(combination);
                case Games.CloverHit:
                    return GameCloverCashConversion.ToSlotDataResV3(combination, isCurrentGameGratis);
                case Games.WildCrown10:
                    return GameGoldenCrownConversion.ToSlotDataResV3(combination);
                case Games.WildClover506:
                    return GameWildClover506Conversion.ToSlotDataResV3(combination);
                case Games.LollasWorld:
                    return GameLollasWorldCoversion.ToJsonObject(combination);
               case Games.LostBook:
                    return GameSpellbookConversion.ToSlotDataResV3(combination, isCurrentGameGratis);
                case Games.BookOfScorpionsDouble:
                    return GameBookOfDoubleConversion.ToSlotDataResV3(combination);
                case Games.ElGrandeToro:
                    return GameElGrandeToroConversion.ToJsonObject(combination, numOfGratisGames, isCurrentGameGratis);
                case Games.ActionHot20:
                    return GameFruitsAndStarsConversion.ToSlotDataResV3(combination);
                case Games.BonusBells:
                    return GameBonusBellsConversion.ToSlotDataResV3(combination);
                default:
                    Logger.LogError(ErrorCodes.Modules.ParameterLoader + ": " + "Error ToJSON doesnt exist for: " + game);
                    return null;
            }*/
            return null;
        }

        /// <summary>
        /// Vraća objekat sa svim podacima za igru.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="numOfGratisGames"></param>
        /// <param name="newCreditMeter"></param>
        /// <param name="isCurrentGameGratis"></param>
        /// <param name="combination"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public object ToGameData(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis,
            ICombination combination, bool json)
        {
            if (json)
            {
                return ToJson(game, numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
            }
            return ToByteArray(game, numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
        }

        /// <summary>
        /// Da li je trenutna igra bonus?
        /// </summary>
        /// <param name="game"></param>
        /// <param name="gratisGame"></param>
        /// <param name="addInfo"></param>
        /// <param name="addArray"></param>
        /// <returns></returns>
        public bool IsBonus(Games game, bool gratisGame, byte addInfo, byte[] addArray)
        {
            return false;
        }

        #endregion
    }
}
