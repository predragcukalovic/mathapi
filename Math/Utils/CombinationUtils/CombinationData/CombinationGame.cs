using System;
using System.Collections.Generic;
using System.Linq;
using Papi.GameServer.Utils.Enums;
using MathBaseProject.BaseMathData;
using MathCombination.ReelsData;
using MathForGames.BasicGameData;
using MathForGames.GameCrystalsOfMagic;
using MathForGames.GameGoldenClover;
using MathForGames.GameTemplarsQuest;
using RNGUtils.RandomData;

namespace MathCombination.CombinationData
{
    public class CombinationGame : ICombination
    {
        #region Constructor or Singleton implementation

        /// <summary>
        /// Konstruktor za kombinaciju
        /// </summary>
        public CombinationGame()
        {
            Matrix = new byte[5, 5];
            PositionFor2 = new byte[5];
            MultiplyFor2 = new byte[4];
            MultiplyFor2Alpinist = new byte[5];
            GratisGamesValues = new byte[5];
            GratisGamesPositions = new byte[5];
            CascadeList = null;
        }

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
        protected void CreateLinesInformations(Matrix matrix, int numberOfLines, int bet, int gratisGame,
            int wild, int[] winForWild, int[,] gameLines)
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

            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Kreira niz LinesInformation za igru 'Templars Quest'
        /// </summary>
        /// <param name="matrix">Matrica za koju se vrše izračunavanja</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Množilac za gratis igre</param>
        protected void CreateLinesInformationsTemplars(MatrixTemplarsQuest matrix, int numberOfLines, int bet, int gratisGame)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var leftWin = matrix.CalculateLeftWinOfLine(i);
                var line = matrix.GetLine(i);
                var leftElement = (byte)line.GetWinningElement(0, leftWin, LineWinsForGames.WinForWildsTemplarsQuest);
                if (leftWin != 0)
                {
                    var lineInfo = new LineInfo
                    {
                        Id = (byte)(i - 1),
                        Win = leftWin * bet * gratisGame,
                        WinningElement = leftElement
                    };
                    lineInfo.WinningPosition = line.GetLinesPositions(GlobalData.GameLineExtra, i, 0, lineInfo.WinningElement);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }
                var rightWin = matrix.CalculateRightWinOfLine(i);
                var rightElement = (byte)line.GetRightWinningElement(0, rightWin, LineWinsForGames.WinForWildsTemplarsQuest);
                if (rightWin != 0)
                {
                    var lineInfo = new LineInfo
                    {
                        Id = (byte)(i - 1),
                        Win = rightWin * bet * gratisGame,
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
            if (matrix.GetNumberOfElement(9) == 2 && (matrix.IsReelHave(2, 10) || matrix.IsReelHave(2, 11)))
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(9),
                    Id = 254,
                    Win = 0,
                    WinningElement = 9
                };
                var tmpArray = matrix.GetPositionsArray(10);
                if (tmpArray[0] != 255)
                {
                    lineInfo.WinningPosition[2] = tmpArray[0];
                }
                tmpArray = matrix.GetPositionsArray(11);
                if (tmpArray[0] != 255)
                {
                    lineInfo.WinningPosition[2] = tmpArray[0];
                }
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Popunjava matricu Matrix vrednostima iz matrice.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="m"></param>
        protected void FillMatrixArray(Matrix matrix, int m = 5)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }
        }

        /// <summary>
        /// Popunjava matricu Matrix istom vrednošću.
        /// </summary>
        /// <param name="singleValue"></param>
        protected void FillMatrixArray(int singleValue)
        {
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)singleValue;
                }
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
        /// Postavlja bonus simbol u poslednjem rilu na vrednost od 10 do 13.
        /// </summary>
        /// <param name="matrix"></param>
        protected static void SetLastReelSymbol(Matrix matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                if (matrix.GetElement(4, i) == 9)
                {
                    matrix.SetElement(4, i, (int)(SoftwareRng.Next(4) + 10));
                }
            }
        }

        /// <summary>
        /// Daje tip bonusa koji se pao.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        protected static int GetBonusType(Matrix matrix)
        {
            if (matrix.GetNumberOfElement(9) == 2)
            {
                if (matrix.IsReelHave(4, 10))
                {
                    return 1;
                }
                if (matrix.IsReelHave(4, 11))
                {
                    return 2;
                }
                if (matrix.IsReelHave(4, 12))
                {
                    return 3;
                }
                if (matrix.IsReelHave(4, 13))
                {
                    return 4;
                }
            }

            return 0;
        }

        /// <summary>
        /// Postavlja bonus simbol u srednjem rilu na vrednost 10 ili 11.
        /// </summary>
        /// <param name="matrix"></param>
        protected static void SetTemplarBonusSymbol(Matrix matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                if (matrix.GetElement(2, i) == 9)
                {
                    matrix.SetElement(2, i, SoftwareRng.Next(4) == 0 ? 11 : 10);
                }
            }
        }

        /// <summary>
        /// Promešani brojevi 0, 1 i 2; indeksi za nizove dobitaka.
        /// </summary>
        /// <returns></returns>
        protected static int[] GetShuffledIndexArray()
        {
            var array = new[] { 0, 1, 2 };
            for (var i = 3; i > 1; i--)
            {
                var rand = SoftwareRng.Next(i);
                var t = array[i - 1];
                array[i - 1] = array[rand];
                array[rand] = t;
            }
            return array;
        }

        /// <summary>
        /// Promeša niz.
        /// </summary>
        /// <returns></returns>
        protected static void ShuffleArray(ref byte[] array)
        {
            var n = array.Length;
            for (var i = n; i > 1; i--)
            {
                var rand = SoftwareRng.Next(i);
                var t = array[i - 1];
                array[i - 1] = array[rand];
                array[rand] = t;
            }
        }

        /// <summary>
        /// Postavlja sve potrebne parametre za kartmenov bonus.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="bet"></param>
        /// <param name="selectedField">Izabrano polje</param>
        protected void SetFirstBonus(ref byte[] array, int bet, int selectedField)
        {
            AdditionalInformation = 1;
            TotalWin = 0;
            FillMatrixArray(10);
            LinesInformation = new LineInfo[0];
            var lastWin = BitConverter.ToInt32(array, 10);
            var wins = new[] { 15, 20, 25, 30, 60 };
            var possible = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var newGame = true;
            for (var i = 1; i <= 8; i++)
            {
                if (array[i] != 0)
                {
                    newGame = false;
                    possible.Remove(array[i]);
                }
            }
            if (newGame)
            {
                possible.Remove(7);
            }
            if (array[0] == 0)
            {
                possible.Remove(6);
                possible.Remove(8);
            }
            var randomNumber = possible[(int)SoftwareRng.Next(possible.Count)];
            array[14] = (byte)randomNumber;
            array[selectedField] = (byte)randomNumber;
            if (randomNumber >= 7)
            {
                for (var i = 0; i < 8; i++)
                {
                    array[i + 1] = 0;
                }
                if (randomNumber == 8)
                {
                    array[9]--;
                    if (array[9] == 0)
                    {
                        return;
                    }
                }
            }
            if (randomNumber == 6)
            {
                TotalWin = lastWin;
            }
            if (randomNumber < 6)
            {
                TotalWin = wins[randomNumber - 1] * bet;
            }
            if (TotalWin + lastWin >= MatrixCrystalsOfMagic.MAX_WIN * bet)
            {
                TotalWin = MatrixCrystalsOfMagic.MAX_WIN * bet - lastWin;
                return;
            }
            GratisGame = true;
            NumberOfGratisGames = 1;
            lastWin += TotalWin;
            var byteArray = BitConverter.GetBytes(lastWin);
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(byteArray);
            }
            for (var i = 0; i < 4; i++)
            {
                array[10 + i] = byteArray[i];
            }
            array[0]++;
            if (array[0] == 255)
            {
                array[0] = 1;
            }
        }

        /// <summary>
        /// Postavlja sve potrebne parametre za kenijev bonus.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="bet"></param>
        protected void SetSecondBonus(ref byte[] array, int bet)
        {
            const string game = "$x$x?$$x$$xx$?xx$$x?$$x$?x$x$x$$?x$$xx";
            var dollarWin = new[] { 5, 20, 100 };
            var xWin = new[] { 0, 10, 50 };
            var multiplyArray = new[] { 2, 3, 4 };
            var randIndex = GetShuffledIndexArray();
            TotalWin = 0;
            array[7] = 0;
            array[1] = (byte)Math.Max(array[1] - 1, 0);
            if (array[1] == 0)
            {
                array[2] = 1;
            }
            var mult = array[2];
            switch (game[array[0]])
            {
                case 'x':
                    array[4] = (byte)xWin[randIndex[0]];
                    array[5] = (byte)xWin[randIndex[1]];
                    array[6] = (byte)xWin[randIndex[2]];
                    if (array[4] == 0)
                    {
                        array[3]--;
                    }
                    break;
                case '$':
                    array[4] = (byte)dollarWin[randIndex[0]];
                    array[5] = (byte)dollarWin[randIndex[1]];
                    array[6] = (byte)dollarWin[randIndex[2]];
                    break;
                case '?':
                    array[1] += (byte)(SoftwareRng.Next(3) + 3);
                    array[2] = (byte)multiplyArray[randIndex[0]];
                    array[4] = 0;
                    array[5] = (byte)multiplyArray[randIndex[1]];
                    array[6] = (byte)multiplyArray[randIndex[2]];
                    array[7] = 1;
                    break;
            }
            array[0]++;
            FillMatrixArray(11);
            if (array[3] > 0 && array[0] < game.Length)
            {
                GratisGame = true;
                NumberOfGratisGames = 1;
            }
            WinFor2 = 0;
            TotalWin += array[4] * mult * bet;
            if (array[0] == game.Length && array[3] > 0)
            {
                TotalWin += 2500 * bet;
                WinFor2 = 2500 * bet;
            }
            if (game[array[0] - 1] == '?')
            {
                array[4] = array[2];
            }
            LinesInformation = new LineInfo[0];
            AdditionalInformation = 2;
        }

        /// <summary>
        /// Postavlja sve potrebne parametre za kajlov bonus.
        /// </summary>
        /// <param name="bet"></param>
        protected void SetThirdBonus(int bet)
        {
            var b = SoftwareRng.Next(5);
            WinFor2 = (int)(b + 1);
            switch (b)
            {
                case 0:
                    TotalWin *= 2;
                    break;
                case 1:
                    TotalWin *= 10;
                    break;
                case 2:
                    TotalWin += 50 * bet;
                    break;
                case 3:
                    TotalWin += 500 * bet;
                    break;
                default:
                    GratisGame = true;
                    NumberOfGratisGames = 3;
                    break;
            }
        }

        /// <summary>
        /// Postavlja sve potrebne parametre za stenov bonus.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="stickyWilds">Lepljivi wildovi</param>
        protected void SetFourthBonus(ref MatrixCrystalsOfMagic matrix, ref byte[] stickyWilds)
        {
            stickyWilds[15]++;
            for (var i = 0; i < 5; i++)
            {
                var a = SoftwareRng.Next((stickyWilds[15] + 4) / 4 * (stickyWilds[15] + 4) / 4 + 4);
                if (a == 0)
                {
                    matrix.SetElement(i, (int)SoftwareRng.Next(3), 0);
                }
            }
            for (var i = 0; i < 15; i++)
            {
                if (matrix.GetElement(i % 5, i / 5) == 0 && stickyWilds[i] == 0)
                {
                    stickyWilds[i] += 3;
                }
                if (stickyWilds[i] > 0)
                {
                    matrix.SetElement(i % 5, i / 5, 0);
                    stickyWilds[i]--;
                    Matrix[i % 5, i / 5] = 15;
                    GratisGame = true;
                    NumberOfGratisGames = 1;
                }
            }
            AdditionalInformation = 4;
        }

        /// <summary>
        /// Postavlja wildove na nekim pozicijama (kartman bonus).
        /// </summary>
        /// <param name="matrix"></param>
        protected void SetThirdWildFeature(ref MatrixCrystalsOfMagic matrix)
        {
            WinFor2 = 13;
            var rnd = SoftwareRng.Next(31);
            var start = 0;
            var end = 2;
            if (rnd >= 16)
            {
                start++;
            }
            if (rnd >= 21)
            {
                start++;
            }
            if (rnd <= 15 && rnd > 0)
            {
                end--;
            }
            if (rnd <= 10 && rnd > 0)
            {
                end--;
            }
            PositionFor2[0] = (byte)(start + 1);
            PositionFor2[1] = (byte)(end + 1);
            for (var i = start; i <= end; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    matrix.SetElement(j, i, 0);
                }
            }
        }

        /// <summary>
        /// Postavlja wildove na nekim pozicijama (TiF bonus).
        /// </summary>
        /// <param name="matrix"></param>
        protected void SetSecondWildFeature(ref MatrixCrystalsOfMagic matrix)
        {
            WinFor2 = 12;
            var start = (int)SoftwareRng.Next(1, 4);
            PositionFor2[0] = (byte)(10 + start);
            matrix.SetElement(start, 2, 0);
            var next = (int)SoftwareRng.Next(Math.Max(start - 1, 1), Math.Min(start + 2, 4));
            PositionFor2[1] = (byte)(5 + next);
            matrix.SetElement(next, 1, 0);
            var last = (int)SoftwareRng.Next(Math.Max(next - 1, 1), Math.Min(next + 2, 4));
            PositionFor2[2] = (byte)(last);
            matrix.SetElement(last, 0, 0);
        }

        /// <summary>
        /// Postavlja wildove na nekim pozicijama (govance bonus).
        /// </summary>
        /// <param name="matrix"></param>
        protected void SetFirstWildFeature(ref MatrixCrystalsOfMagic matrix)
        {
            WinFor2 = 11;
            PositionFor2[0] = (byte)(SoftwareRng.Next(3) * 5);
            PositionFor2[1] = (byte)(SoftwareRng.Next(3) * 5 + 2);
            PositionFor2[2] = (byte)(SoftwareRng.Next(3) * 5 + 4);

            matrix.SetElement(PositionFor2[0] % 5, PositionFor2[0] / 5, 0);
            matrix.SetElement(PositionFor2[1] % 5, PositionFor2[1] / 5, 0);
            matrix.SetElement(PositionFor2[2] % 5, PositionFor2[2] / 5, 0);
        }

        /// <summary>
        /// Темплари, бонус са новчићима.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="bet"></param>
        /// <param name="selectedField"></param>
        protected void SetTemplarCoinBonus(ref byte[] array, int bet, int selectedField)
        {
            TotalWin = 0;
            AdditionalInformation = 1;
            GratisGame = false;
            NumberOfGratisGames = 0;
            LinesInformation = new LineInfo[0];
            if (array[selectedField + 1] != 0)
            {
                throw new Exception("Already selected field!");
            }
            var open = 0;
            for (var i = 0; i < 15; i++)
            {
                if (array[i + 1] == 2)
                {
                    array[i + 1] = 1;
                }
                if (array[i + 1] == 1)
                {
                    open++;
                }
            }
            array[selectedField + 1] = 2;
            if (open == 14)
            {
                TotalWin = 50 * bet * array[0];
                return;
            }
            var newWin = SoftwareRng.Next(15 - open);
            if (newWin > 0)
            {
                TotalWin = 50 * bet * array[0];
                GratisGame = true;
                NumberOfGratisGames = 1;
            }
        }

        /// <summary>
        /// Темплари, бонус са јахачима.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="bet"></param>
        public void SetTemplarKnightBonus(ref byte[] array, int bet)
        {
            AdditionalInformation = 2;
            GratisGame = true;
            NumberOfGratisGames = 1;
            WinFor2 = 0;
            LinesInformation = new LineInfo[0];
            array[1]++;
            array[5] = 0;
            array[6] = array[7] = array[8] = array[9] = 0;
            var choose = new byte[] { 0, 100, 150, 200, 250 };
            var barrel = new byte[] { 10, 20, 40, 80 };
            if (array[1] % 3 == 1)
            {
                array[2] = barrel[SoftwareRng.Next(4)];
            }
            else if (array[1] % 3 == 2)
            {
                ShuffleArray(ref choose);
                array[2] = choose[0];
                array[6] = choose[1];
                array[7] = choose[2];
                array[8] = choose[3];
                array[9] = choose[4];
            }
            else
            {
                if (SoftwareRng.Next(2) == 0)
                {
                    array[2] = 0;
                    array[3]--;
                    array[5] = 1;
                }
                else
                {
                    array[2] = 150;
                    array[4]--;
                    array[5] = 2;
                }
            }
            if (array[1] > 15 || array[3] > 3 || array[4] > 3)
            {
                throw new Exception("Bad Knight Bonus parameters! Step " + array[1] + ", Lives [" + array[3] + "/" + array[4] + "]");
            }
            TotalWin = array[2] * bet;
            if (array[3] == 0)
            {
                GratisGame = false;
                NumberOfGratisGames = 0;
            }
            if (array[4] == 0)
            {
                GratisGame = false;
                NumberOfGratisGames = 0;
                TotalWin += 500 * bet;
                WinFor2 = 500 * bet;
            }
        }

        protected static LineInfo GetJackpotLineInfo(int position, int jackpotType, int bet, int mini, int major, byte lineId = 253)
        {
            var win = 0;
            if (jackpotType % 10 == 1)
            {
                win = MatrixGoldenClover.GoldJackpotWins[jackpotType / 10] * 50 * bet;
            }
            if (jackpotType == 2)
            {
                win = mini;
            }
            if (jackpotType == 3)
            {
                win = major;
            }
            if (win > 0 || jackpotType % 10 == 5 || jackpotType == 4)
            {
                var pos = new byte[5];
                CreateEmptyArray(pos);
                pos[0] = (byte)((position / 3) + (position % 3) * 5);
                var lineInfo = new LineInfo
                {
                    WinningPosition = pos,
                    Id = lineId,
                    Win = win,
                    WinningElement = (byte)(11 + jackpotType % 10)
                };
                return lineInfo;
            }
            return null;
        }

        protected void GlobalJackpotCheck(ref byte[] addArray, ref List<LineInfo> lineInfos, ref MatrixGoldenClover.GoldenCloverConfig jpData)
        {
            if (addArray[18] == 0 && addArray[16] == 1)
            {
                addArray[18] = 1;
                GratisGamesValues[0] = 1;
                var values = BitConverter.GetBytes((int) jpData.MajorJpId);
                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(values);
                }
                for (var i = 0; i < 4; i++)
                {
                    GratisGamesValues[i + 1] = values[i];
                }
            }
            if (addArray[19] == 0 && addArray[17] == 7)
            {
                addArray[19] = 1;
                GratisGamesPositions[0] = 1;
                var values = BitConverter.GetBytes((int) jpData.GrandJpId);
                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(values);
                }
                for (var i = 0; i < 4; i++)
                {
                    GratisGamesPositions[i + 1] = values[i];
                }
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Transformiše matricu za igru 'Crystals of magic' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        /// <param name="addInfo"></param>
        /// <param name="additionalArrayData">Lepljivi wildovi za četvrti bonus</param>
        /// <param name="selectedField">Izabrano polje za prvi bonus</param>
        public void MatrixToCombination(MatrixCrystalsOfMagic matrix, int numberOfLines, int bet, bool gratisGame, byte addInfo, ref byte[] additionalArrayData, int selectedField)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            WinFor2 = 0;
            CreateEmptyArray(PositionFor2);
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);
            if (!gratisGame)
            {
                additionalArrayData = new byte[16];
            }

            if (gratisGame && addInfo == 1)
            {
                if (selectedField <= 0 || selectedField > 8 || additionalArrayData[selectedField] != 0)
                {
                    throw new Exception("Crystals of Magic Bonus Error: Field is already selected!");
                }
                SetFirstBonus(ref additionalArrayData, bet, selectedField);
                AdditionalArray = additionalArrayData;
                return;
            }

            if (gratisGame && addInfo == 2)
            {
                SetSecondBonus(ref additionalArrayData, bet);
                AdditionalArray = additionalArrayData;
                return;
            }

            SetLastReelSymbol(matrix);
            if (gratisGame && addInfo == 3)
            {
                if (SoftwareRng.Next(4) == 0)
                {
                    matrix.SetElement(4, (int)SoftwareRng.Next(3), 14);
                }
                AdditionalInformation = addInfo;
            }
            if (!gratisGame)
            {
                if (SoftwareRng.Next(300) == 0)
                {
                    SetThirdWildFeature(ref matrix);
                }
                else if (SoftwareRng.Next(50) == 0)
                {
                    SetFirstWildFeature(ref matrix);
                }
                else if (SoftwareRng.Next(50) == 0)
                {
                    SetSecondWildFeature(ref matrix);
                }
            }
            FillMatrixArray(matrix);
            if (gratisGame && addInfo == 4)
            {
                SetFourthBonus(ref matrix, ref additionalArrayData);
            }

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, LineWinsForGames.WinForWildsCrystalsOfMagic,
                GlobalData.GameLineExtra);

            var bonusType = GetBonusType(matrix);
            if (bonusType > 0)
            {
                switch (bonusType)
                {
                    case 1:
                        GratisGame = true;
                        NumberOfGratisGames = 1;
                        AdditionalInformation = 1;
                        additionalArrayData[9] = 2;
                        break;
                    case 2:
                        GratisGame = true;
                        NumberOfGratisGames = 1;
                        AdditionalInformation = 2;
                        additionalArrayData[2] = 1;
                        additionalArrayData[3] = 3;
                        break;
                    case 3:
                        GratisGame = true;
                        NumberOfGratisGames = 10;
                        AdditionalInformation = 3;
                        break;
                    case 4:
                        GratisGame = true;
                        NumberOfGratisGames = 1;
                        additionalArrayData[7] = 2;
                        AdditionalInformation = 4;
                        break;
                }
            }
            if (matrix.IsReelHave(4, 14))
            {
                SetThirdBonus(bet);
            }
            AdditionalArray = additionalArrayData;
        }

        /// <summary>
        /// Transformiše matricu za igru 'Templars Quest' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        /// <param name="add">Vrednost množioca za gratis</param>
        /// <param name="addArray"></param>
        /// <param name="selectedField"></param>
        public void MatrixToCombinationPrivate(MatrixTemplarsQuest matrix, int bet, bool gratisGame, byte add, ref byte[] addArray, int selectedField)
        {
            if (add == 1 && gratisGame)
            {
                SetTemplarCoinBonus(ref addArray, bet, selectedField);
                addArray[0] = 1;
                AdditionalArray = addArray;
                return;
            }
            if (add == 2 && gratisGame)
            {
                SetTemplarKnightBonus(ref addArray, bet);
                addArray[0] = 1;
                AdditionalArray = addArray;
                return;
            }
            for (var i = 0; i < 15; i++)
            {
                addArray[i + 1] = 0;
            }
            var bonus = matrix.GetNumberOfElement(9) == 3;
            SetTemplarBonusSymbol(matrix);
            for (var i = 1; i < 4; i += 2)
            {
                if (!bonus && SoftwareRng.Next(60) == 0)
                {
                    matrix.SetElement(i, (int)SoftwareRng.Next(3), 12);
                }
            }
            var mult = addArray[0];
            if (mult != 1 && mult != 2 && mult != 3 && mult != 5)
            {
                mult = 1;
            }
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            if (matrix.IsReelHave(1, 12))
            {
                matrix.SetElement(1, 0, 0);
                matrix.SetElement(1, 1, 0);
                matrix.SetElement(1, 2, 0);
            }
            if (matrix.IsReelHave(3, 12))
            {
                matrix.SetElement(3, 0, 0);
                matrix.SetElement(3, 1, 0);
                matrix.SetElement(3, 2, 0);
            }

            CreateEmptyArray(PositionFor2);
            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            CreateLinesInformationsTemplars(matrix, 10, bet, mult);
            addArray[0] = 1;
            if (LinesInformation.Length > 0)
            {
                addArray[0] = (byte)Math.Min(5, mult + 1 + (mult / 3));
            }
            if (bonus)
            {
                if (matrix.IsReelHave(2, 10))
                {
                    GratisGame = true;
                    NumberOfGratisGames = 1;
                    AdditionalInformation = 1;
                    addArray[0] = 1;
                }
                else if (matrix.IsReelHave(2, 11))
                {
                    GratisGame = true;
                    NumberOfGratisGames = 1;
                    AdditionalInformation = 2;
                    addArray[3] = 3;
                    addArray[4] = 3;
                    addArray[0] = 1;
                }
            }
            AdditionalArray = addArray;
        }

        /// <summary>
        /// Transformiše matricu za igru 'Templars Quest' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Broj preostalih gratis igara</param>
        /// <param name="addInfo"></param>
        /// <param name="addArray"></param>
        /// <param name="selectedField"></param>
        /// <param name="reels"></param>
        public void MatrixToCombination(MatrixTemplarsQuest matrix, int bet, bool gratisGame, byte addInfo, ref byte[] addArray, int selectedField, params List<byte>[] reels)
        {
            var matrixArray = ReelsReader.ReadMatrixArrayFromReels(reels);
            matrix.FromMatrixArray(matrixArray);
            MatrixToCombinationPrivate(matrix, bet, gratisGame, addInfo, ref addArray, selectedField);
            CascadeList = new List<ICombination>();
            if (TotalWin == 0 || GratisGame || (addInfo > 0 && gratisGame))
            {
                return;
            }
            bool b;
            do
            {
                matrixArray = ReelsReader.ReadMatrixArrayFromReels(reels);
                matrix.FromMatrixArray(matrixArray);
                var cmb = new CombinationGame();
                cmb.MatrixToCombinationPrivate(matrix, bet, gratisGame, addInfo, ref addArray, selectedField);
                b = cmb.TotalWin > 0 && !cmb.GratisGame;
                GratisGame = cmb.GratisGame;
                NumberOfGratisGames = cmb.NumberOfGratisGames;
                AdditionalInformation = cmb.AdditionalInformation;
                CascadeList.Add(cmb);
            } while (b);
        }

        /// <summary>
        /// Transformiše matricu za igru 'Golden Clover' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        /// <param name="addArray"></param>
        /// <param name="jackpotData"></param>
        public void MatrixToCombination(MatrixGoldenClover matrix, int numberOfLines, int bet, bool gratisGame, ref byte[] addArray, MatrixGoldenClover.GoldenCloverConfig jackpotData)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            WinFor2 = 0;
            CreateEmptyArray(PositionFor2);
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);
            var lineInfos = new List<LineInfo>();
            if (gratisGame)
            {
                if (addArray[15] == 4)
                {
                    for (var i = 0; i < 5; i++)
                    {
                        for (var j = 0; j < 3; j++)
                        {
                            if (addArray[i * 3 + j] != 0)
                            {
                                var lijk = GetJackpotLineInfo(i * 3 + j, addArray[i * 3 + j], bet, jackpotData.MiniFixedValue, jackpotData.MinorFixedValue, 246);
                                if (lijk != null)
                                {
                                    lineInfos.Add(lijk);
                                }
                            }
                        }
                    }
                    addArray[15] = 3;
                }
                addArray[15] = (byte) Math.Max(addArray[15] - 1, 0);
                var allFill = true;
                //var newClovers = new List<int>();
                for (var i = 0; i < 5; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        if (addArray[i*3 + j] == 0)
                        {
                            var a = SoftwareRng.Next(13);
                            if (a == 0)
                            {
                                addArray[15] = 3;
                                var jck = MatrixGoldenClover.GetJackpotCoefficiet(i, ref addArray[16], ref addArray[17]);
                                addArray[i * 3 + j] = (byte)jck;
                                //newClovers.Add(i * 3 + j);
                                var lijk = GetJackpotLineInfo(i*3 + j, jck, bet, jackpotData.MiniFixedValue,
                                    jackpotData.MinorFixedValue);
                                if (lijk != null)
                                {
                                    lineInfos.Add(lijk);
                                }
                            }
                            else
                            {
                                allFill = false;
                            }
                        }
                    }
                }
                if (addArray[15] != 0 && !allFill)
                {
                    GratisGame = true;
                    NumberOfGratisGames = 1;
                }
                for (var i = 0; i < 15; i++)
                {
                    var elem = 11 + addArray[i]%10;
                    matrix.SetElement(i/3, i%3, elem);
                    if (!GratisGame && elem == 16 && addArray[19] == 0)
                    {
                        lineInfos.Add(GetJackpotLineInfo(i, addArray[i] - 4, bet, jackpotData.MiniFixedValue,
                            jackpotData.MinorFixedValue, 248));
                    }
                }
                FillMatrixArray(matrix, 3);
                if (allFill)
                {
                    addArray[15] = 0;
                    var doubleWin = 0;
                    for (var i = 0; i < 15; i++)
                    {
                        if (addArray[i]%10 == 1)
                        {
                            doubleWin += MatrixGoldenClover.GoldJackpotWins[addArray[i] / 10] * 50 * bet;
                        }
                        if (addArray[i] == 2)
                        {
                            doubleWin += jackpotData.MiniFixedValue;
                        }
                        if (addArray[i] == 3)
                        {
                            doubleWin += jackpotData.MinorFixedValue;
                        }
                        if (addArray[i]%10 == 5 && addArray[19] == 0)
                        {
                            doubleWin += MatrixGoldenClover.GoldJackpotWins[addArray[i] / 10] * 50 * bet;
                        }
                    }
                    var li = new LineInfo
                    {
                        WinningPosition = new byte[5],
                        Id = 249,
                        Win = doubleWin,
                        WinningElement = 11
                    };
                    CreateEmptyArray(li.WinningPosition);
                    lineInfos.Add(li);
                }
                GlobalJackpotCheck(ref addArray, ref lineInfos, ref jackpotData);
                foreach (var info in lineInfos)
                {
                    TotalWin += info.Win;
                }
                for (var i = 0; i < 15; i++)
                {
                    //if (!newClovers.Contains(i))
                    //{
                        var elem = addArray[i];
                        if (!GratisGame && elem%10 == 5 && addArray[19] == 0)
                        {
                            elem -= 4;
                        }
                        if (elem%10 < 4)
                        {
                            var linfo = GetJackpotLineInfo(i, elem, bet, jackpotData.MiniFixedValue,
                                jackpotData.MinorFixedValue, 250);
                            if (linfo != null)
                            {
                                lineInfos.Add(linfo);
                            }
                        }
                   // }
                }
                LinesInformation = lineInfos.ToArray();
                if (!GratisGame)
                {
                    addArray[16] = 0;
                    addArray[17] = 0;
                    addArray[18] = 0;
                    addArray[19] = 0;
                }
                AdditionalArray = addArray;
                return;
            }
            var scatterNum = matrix.GetNumberOfElement(9);
            var scatterNum2 = matrix.GetNumberOfElement(10);
            LineInfo lineInfo = null;
            if (scatterNum >= 3)
            {
                lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(9),
                    Id = 254,
                    Win = LineWinsForGames.WinForScattersGoldenClover[scatterNum - 1] * bet * numberOfLines,
                    WinningElement = 9
                };
            }
            if (scatterNum2 == 3)
            {
                PositionFor2 = matrix.GetPositionsArray(10);
                var shuffleScatter = (byte[])MatrixGoldenClover.ScatterValues.Clone();
                ShuffleArray(ref shuffleScatter);
                MultiplyFor2[0] = shuffleScatter[0];
                MultiplyFor2[1] = shuffleScatter[1];
                MultiplyFor2[2] = shuffleScatter[2];
                WinFor2 = shuffleScatter[0] * bet * numberOfLines;
            }
            FillMatrixArray(matrix, 3);
            //var winForBonus = 0;
            var displayValues = new List<LineInfo>();
            if (matrix.IsGratis())
            {
                for (var i = 0; i < 5; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        var elem = matrix.GetElement(i, j);
                        var index = i * 3 + j;
                        addArray[index] = (byte)(elem > 11 ? elem - 11 : 0);
                        if (elem == 12)
                        {
                            addArray[index] = (byte)(1 + 10 * MatrixGoldenClover.GetRandomGoldJackpotIndex());
                        }
                        if (elem == 16)
                        {
                            addArray[index] = (byte)(5 + 10 * MatrixGoldenClover.GetRandomGoldJackpotIndex());
                        }
                        if (elem > 11)
                        {
                            var jckli = GetJackpotLineInfo(index, addArray[index], bet, jackpotData.MiniFixedValue, jackpotData.MinorFixedValue, 250);
                            if (jckli != null)
                            {
                                //lineInfos.Add(jckli);
                                //winForBonus += jckli.Win;
                                displayValues.Add(jckli);
                            }
                        }
                    }
                }
                GratisGame = true;
                addArray[15] = 4;
                NumberOfGratisGames = 1;
            }
            for (var i = 1; i < 4; i++)
            {
                if (matrix.IsReelHave(i, 0))
                {
                    matrix.SetElement(i, 0, 0);
                    matrix.SetElement(i, 1, 0);
                    matrix.SetElement(i, 2, 0);
                }
            }

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, LineWinsForGames.WinForWildGoldenClover, GlobalData.GameLineExtra);
            GlobalJackpotCheck(ref addArray, ref lineInfos, ref jackpotData);
            if (lineInfos.Count > 0)
            {
                var li = LinesInformation.ToList();
                foreach (var info in lineInfos)
                {
                    TotalWin += info.Win;
                    li.Add(info);
                }
                LinesInformation = li.ToArray();
            }
            if (lineInfo != null)
            {
                TotalWin += lineInfo.Win;
                var li = LinesInformation.ToList();
                li.Insert(0, lineInfo);
                LinesInformation = li.ToArray();
            }
            TotalWin += WinFor2;
            if (!GratisGame)
            {
                for (var i = 0; i < 5; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        if (Matrix[i, j] == 12)
                        {
                            displayValues.Add(new LineInfo
                            {
                                Id = 250,
                                Win = MatrixGoldenClover.GoldJackpotWins[MatrixGoldenClover.GetRandomGoldJackpotIndex()] * bet * 50,
                                WinningElement = 11,
                                WinningPosition = new byte[] {(byte) (5*j + i), 255, 255, 255, 255}
                            });
                        }
                    }
                }
            }
            if (displayValues.Count > 0)
            {
                var li = LinesInformation.ToList();
                li.AddRange(displayValues);
                LinesInformation = li.ToArray();
            }
            /*if (winForBonus > 0)
            {
                var values = BitConverter.GetBytes(winForBonus);
                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(values);
                }
                for (var i = 0; i < 4; i++)
                {
                    addArray[20 + i] = values[i];
                }
            }*/
            if (!GratisGame)
            {
                for (var i = 16; i < 20; i++)
                {
                    addArray[i] = 0;
                }
            }
            AdditionalArray = addArray;
        }

        public byte[] ToByteArray(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis,
            ICombination combination)
        {
            return null;
        }

        public object ToJson(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis, ICombination combination)
        {
            /*switch (game)
            {
                case Games.CrystalsOfMagic:
                    return GameCrystalsOfMagicConversion.ToJsonObject(combination, numOfGratisGames, isCurrentGameGratis);
                case Games.TemplarsQuest:
                    return GameTemplarsQuestConversion.ToJsonObject(combination, numOfGratisGames, isCurrentGameGratis);
                case Games.GoldenClover:
                    return GameGoldenCloverConversion.ToSlotDataResV3(combination, numOfGratisGames, isCurrentGameGratis);
                default:
                    return null;
            }*/
            return null;
        }

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
            if ((game == Games.CrystalsOfMagic || game == Games.TemplarsQuest) && gratisGame && (addInfo == 1 || addInfo == 2))
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
