using System.Collections.Generic;
using Papi.GameServer.Utils.Enums;
using GameJokerQueen;
using MathForGames.BasicGameData;
using MathForGames.Game3WildFruits;
using MathForGames.GameBurningIce;
using MathForGames.GameFenixPlay;
using MathForGames.GameHot777;
using MathForGames.GameMagicFruits;
using MathForGames.GameTripleHot;
using MathForGames.GameVegasHot;

namespace MathCombination.CombinationData
{
    public class Combination3 : ICombination
    {
        #region Constructor or Singleton implementation

        /// <summary>
        /// Konstruktor za kombinaciju
        /// </summary>
        public Combination3()
        {
            Matrix = new byte[3, 3];
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
        /// Kreira dodatne informacije o linijama
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="twoelements"></param>
        /// <param name="gratisMultiplicator"></param>
        protected void CreateLinesInformations(MatrixVegasHot matrix, int numberOfLines, int bet, bool twoelements = false, int gratisMultiplicator = 1)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var winOfLine = matrix.CalculateWinOfLine(i);
                if (winOfLine == 0)
                    continue;
                var win = winOfLine * bet * gratisMultiplicator;
                var winningElement = (byte)matrix.GetWinningElementForLine(i);
                var lineInfo = new LineInfo
                {
                    WinningPosition = new byte[5],
                    Id = (byte)(i - 1),
                    Win = win,
                    WinningElement = winningElement
                };
                TotalWin += win;
                CreateWinningLinePositions(ref lineInfo.WinningPosition, i, twoelements);
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        protected void CreateLinesInformationJokerQueen(MatrixJokerQueen matrix, int numberOfLines, int bet)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var winOfLine = matrix.CalculateLineWin(i);
                if (winOfLine == 0)
                    continue;
                var win = winOfLine * bet;
                var winningElement = (byte)matrix.GetWinningElementForLine(i);
                var lineInfo = new LineInfo
                {
                    WinningPosition = new byte[3],
                    Id = (byte)(i - 1),
                    Win = win,
                    WinningElement = winningElement
                };
                lineInfo.WinningPosition = matrix.GetPositionArrayForLine(i, winningElement);
                TotalWin += win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Kreira dodatne informacije o linijama za igru 'magicFruits'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        protected void CreateLinesInformationsFruits(MatrixMagicFruits matrix, int numberOfLines, int bet)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var winOfLine = matrix.CalculateWinOfLine(i);
                if (winOfLine == 0)
                    continue;
                var win = winOfLine * bet;
                var winningElement = (byte)matrix.GetWinningElementForLine(i);
                var lineInfo = new LineInfo
                {
                    WinningPosition = new byte[5],
                    Id = (byte)(i - 1),
                    Win = win,
                    WinningElement = (winOfLine == 200) ? (byte)0xF : (byte)(winningElement & 7)
                };
                TotalWin += win;
                CreateWinningLinePositions(ref lineInfo.WinningPosition, i);
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Pozicije dobitnih elemenata u matrici.
        /// </summary>
        /// <param name="array">Niz u koji se pamte pozicije</param>
        /// <param name="line">Broj linije</param>
        /// <param name="twoElements">Da li mogu dva elementa da daju dobitak? (za igru Hot777)</param>
        protected void CreateWinningLinePositions(ref byte[] array, int line, bool twoElements = false)
        {
            for (var i = 0; i < 3; i++)
            {
                array[i] = (byte)(GlobalData.GameLineVegasHot[line - 1, i] * 3 + i);
            }
            if (twoElements && Matrix[1, GlobalData.GameLineVegasHot[line - 1, 1]] != Matrix[2, GlobalData.GameLineVegasHot[line - 1, 2]])
            {
                array[2] = 255;
            }
            array[3] = 255;
            array[4] = 255;
        }

        /// <summary>
        /// Kreira dodatne informacije o linijama za igru BurningIce
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="bet"></param>
        /// <param name="mysteryWin"></param>
        protected void CreateLinesInformationsBurningIce(MatrixBurningIce matrix, int bet, int mysteryWin)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= 27; i++)
            {
                var winOfLine = matrix.GetLine(i).CalculateLineWin();
                if (winOfLine == 0)
                    continue;
                var win = winOfLine * bet;
                var winningElement = (byte)matrix.GetWinningElementForLine(i);
                var lineInfo = new LineInfo
                {
                    WinningPosition = new byte[5],
                    Id = (byte)(i - 1),
                    Win = win,
                    WinningElement = winningElement
                };
                TotalWin += win;
                CreateWinningLinePositionsCrissCross(ref lineInfo.WinningPosition, i);
                linesInfo.Add(lineInfo);
            }
            if (mysteryWin > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = new byte[5],
                    Id = 254,
                    Win = mysteryWin * bet,
                    WinningElement = 8
                };
                TotalWin += lineInfo.Win;
                CreatePositionArray(lineInfo.WinningPosition, 8);
                linesInfo.Add(lineInfo);
            }
            if (matrix.GetNumberOfElement(6) == 3 || matrix.GetNumberOfElement(7) == 3)
            {
                AdditionalInformation = 10;
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Kreira dodatne informacije o linijama za igru 3WildFruits
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="bet"></param>
        /// <param name="mysteryWin"></param>
        protected void CreateLinesInformations3WildFruits(Matrix3WildFruits matrix, int bet, int mysteryWin)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= 27; i++)
            {
                var winOfLine = matrix.GetLine(i).CalculateLineWin();
                if (winOfLine == 0)
                    continue;
                var win = winOfLine * bet;
                var winningElement = (byte)matrix.GetWinningElementForLine(i);
                var lineInfo = new LineInfo
                {
                    WinningPosition = new byte[5],
                    Id = (byte)(i - 1),
                    Win = win,
                    WinningElement = winningElement
                };
                TotalWin += win;
                CreateWinningLinePositionsCrissCross(ref lineInfo.WinningPosition, i);
                linesInfo.Add(lineInfo);
            }
            if (mysteryWin > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = new byte[5],
                    Id = 254,
                    Win = mysteryWin * bet,
                    WinningElement = 9
                };
                TotalWin += lineInfo.Win;
                CreatePositionArray(lineInfo.WinningPosition, 9);
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Kreira pozicije za dobitne linije.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="lineNumber"></param>
        protected static void CreateWinningLinePositionsCrissCross(ref byte[] p, int lineNumber)
        {
            lineNumber--;
            p[0] = (byte)((lineNumber / 9) * 3);
            lineNumber %= 9;
            p[1] = (byte)((lineNumber / 3) * 3 + 1);
            lineNumber %= 3;
            p[2] = (byte)(lineNumber * 3 + 2);
            p[3] = 255;
            p[4] = 255;
        }

        /// <summary>
        /// Kreira niz sa pozicijama elementa u matrici.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="element"></param>
        protected void CreatePositionArray(byte[] position, int element)
        {
            var index = 0;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (Matrix[i, j] == element)
                    {
                        position[index] = (byte)(j * 3 + i);
                        index++;
                    }
                }
            }
            for (; index < position.Length; index++)
            {
                position[index] = 255;
            }
        }

        #endregion Private methods

        #region Public methods

        /// <summary>
        /// Pretvara matricu u kombinaciju za igru 'VegasHot'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        public void MatrixToCombination(MatrixVegasHot matrix, int numberOfLines, int bet)
        {
            NumberOfGratisGames = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = matrix.DoubleWin();
            CreateLinesInformations(matrix, numberOfLines, bet, false, GratisGame ? 2 : 1);
        }

        /// <summary>
        /// Pretvara matricu u kombinaciju za igru 'FenixPlay'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        public void MatrixToCombination(MatrixFenixPlay matrix, int numberOfLines, int bet)
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            CreateLinesInformations(matrix, numberOfLines, bet);
        }

        /// <summary>
        /// Pretvara matricu u kombinaciju za igru 'hot777'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <param name="additionalInformation">Broj novčića koji su se pali, ide preko PegasusActivate</param>
        public void MatrixToCombination(MatrixHot777 matrix, int numberOfLines, int bet, bool gratisGame, byte additionalInformation)
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;
            AdditionalInformation = additionalInformation;
            WinFor2 = additionalInformation;
            if (matrix.IsReelHave(2, 10))
            {
                WinFor2++;
                AdditionalInformation++;
                if (AdditionalInformation == 9)
                {
                    AdditionalInformation = 0;
                    GratisGame = true;
                    NumberOfGratisGames = 15;
                }
            }
            if (matrix.GetNumberOfElement(8) == 3)
            {
                GratisGame = true;
                NumberOfGratisGames = 30;
            }
            if (matrix.GetNumberOfElement(9) == 3)
            {
                GratisGame = true;
                NumberOfGratisGames = 45;
            }
            CreateLinesInformations(matrix, numberOfLines, bet, true, gratisGame ? 3 : 1);
        }

        /// <summary>
        /// Pretvara matricu u kombinaciju za igru 'MagicFruits'.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        public void MatrixToCombination(MatrixMagicFruits matrix, int numberOfLines, int bet)
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            CreateLinesInformationsFruits(matrix, numberOfLines, bet);
        }

        /// <summary>
        /// Pretvara matricu u kombinaciju za igru 'BurningIce'.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <param name="addInfo">Ril koji treba da se respinuje</param>
        /// <param name="addArray">Prethodna matrica, za slučaj da se desi respin nekog rila</param>
        /// <param name="simulation">Da li je u pitanju simulacija?</param>
        public void MatrixToCombination(MatrixBurningIce matrix, int bet, bool gratisGame, byte addInfo, ref byte[] addArray, bool simulation = false)
        {
            AdditionalInformation = 0;
            GratisGame = false;
            AdditionalArray = matrix.ToMatrixByteArray();
            int reelToSpin = -1;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (gratisGame && i != addInfo - 1 && addArray != null)
                    {
                        Matrix[i, j] = addArray[i * 3 + j];
                        matrix.SetElement(i, j, Matrix[i, j]);
                    }
                    else
                    {
                        Matrix[i, j] = (byte)matrix.GetElement(i, j);
                        reelToSpin = i;
                    }
                }
            }
            CreatePositionArray(PositionFor2, 17);

            if (!gratisGame && matrix.GetNumberOfElement(6) == 2)
            {
                for (var i = 0; i < 3; i++)
                {
                    if (!matrix.IsReelHave(i, 6))
                    {
                        AdditionalInformation = (byte)(i + 1);
                        GratisGame = true;
                        NumberOfGratisGames = 1;
                        break;
                    }
                }
                CreatePositionArray(PositionFor2, 6);
            }
            else if (!gratisGame && matrix.GetNumberOfElement(7) == 2)
            {
                for (var i = 0; i < 3; i++)
                {
                    if (!matrix.IsReelHave(i, 7))
                    {
                        AdditionalInformation = (byte)(i + 1);
                        GratisGame = true;
                        NumberOfGratisGames = 1;
                        break;
                    }
                }
                CreatePositionArray(PositionFor2, 7);
            }

            WinFor2 = (simulation && !gratisGame) ? matrix.GetMysteryWinSimulation() : matrix.GetMysteryWin();
            CreateLinesInformationsBurningIce(matrix, bet, WinFor2);

            if (gratisGame && reelToSpin > -1 && addArray != null)
            {
                addArray[reelToSpin * 3 + 0] = AdditionalArray[reelToSpin * 3 + 0];
                addArray[reelToSpin * 3 + 1] = AdditionalArray[reelToSpin * 3 + 1];
                addArray[reelToSpin * 3 + 2] = AdditionalArray[reelToSpin * 3 + 2];
            }
            else
            {
                addArray = AdditionalArray;
            }
        }

        /// <summary>
        /// Pretvara matricu u kombinaciju za igru 'TripleHot'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        public void MatrixToCombination(MatrixTripleHot matrix, int numberOfLines, int bet)
        {
            NumberOfGratisGames = 0;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            CreateLinesInformations(matrix, numberOfLines, bet);
        }

        /// <summary>
        /// Pretvara matricu u kombinaciju za igru '3WildFruits'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <param name="addInfo">Ril koji treba da se respinuje</param>
        /// <param name="addArray">Prethodna matrica, za slučaj da se desi respin nekog rila</param>
        public void MatrixToCombination(Matrix3WildFruits matrix, int bet, bool gratisGame, byte addInfo, ref byte[] addArray)
        {
            GratisGame = false;
            AdditionalArray = matrix.ToMatrixByteArray();
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (gratisGame && i != addInfo - 1)
                    {
                        Matrix[i, j] = addArray[i * 3 + j];
                        matrix.SetElement(i, j, Matrix[i, j]);
                    }
                    else
                    {
                        Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    }
                }
            }
            CreatePositionArray(PositionFor2, 17);

            if (!gratisGame && matrix.GetNumberOfElement(9) == 2)
            {
                for (var i = 0; i < 3; i++)
                {
                    if (!matrix.IsReelHave(i, 9))
                    {
                        AdditionalInformation = (byte)(i + 1);
                        GratisGame = true;
                        NumberOfGratisGames = 1;
                        break;
                    }
                }
                CreatePositionArray(PositionFor2, 9);
            }

            WinFor2 = matrix.GetMysteryWin();
            CreateLinesInformations3WildFruits(matrix, bet, WinFor2);
            addArray = AdditionalArray;
        }

        /// <summary>
        /// Pretvara matricu u kombinaciju za igru 'VegasHot'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <param name="additionalInfo"></param>
        /// <param name="additionalArray"></param>
        public void MatrixToCombination(MatrixJokerQueen matrix, int numberOfLines, int bet, bool gratisGame, byte additionalInfo, ref byte[] additionalArray)
        {
            AdditionalInformation = 0;
            Matrix = new byte[3, 5];
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    if (gratisGame && j >= 1 && j <= 3)
                    {
                        if (i == additionalArray[0])
                        {
                            Matrix[i, j] = additionalArray[j + 1];
                            matrix.SetElement(i, j, Matrix[i, j]);
                        }
                        else if (i == additionalArray[1])
                        {
                            Matrix[i, j] = additionalArray[j + 3 + 1];
                            matrix.SetElement(i, j, Matrix[i, j]);
                        }
                        else
                        {
                            Matrix[i, j] = (byte)matrix.GetElement(i, j);
                        }
                    }
                    else
                    {
                        Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    }
                }
            }
            
            CreateLinesInformationJokerQueen(matrix, numberOfLines, bet);

            byte reel1 = 4, reel2 = 4;
            GratisGame = !gratisGame && TotalWin == 0 && matrix.CanRespin(ref reel1, ref reel2);
            NumberOfGratisGames = GratisGame ? 1 : 0;

            if (GratisGame)
            {
                AdditionalArray = new byte[8];
                var aux = new byte[8];
                matrix.InformationForAdditionalArray(reel1, reel2, ref aux);
                for (var i = 0; i < 8; i++)
                {
                    AdditionalArray[i] = aux[i];
                }
            }
            var multiplier = (byte)(matrix.CanMultiply() ? MatrixJokerQueen.rngMultiplier() : 1);
            AdditionalInformation = multiplier;
            TotalWin *= multiplier;
            additionalArray = AdditionalArray;
        }

        public byte[] ToByteArray(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis, ICombination combination)
        {
            /*switch (game)
            {
                case Games.VegasHot:
                    return GameVegasHotConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.FenixPlay:
                    return GameVegasHotConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.Hot777:
                    return GameVegasHotConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.MagicFruits:
                    return GameVegasHotConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.BurningIce:
                case Games.TripleHot:
                case Games.BurningIceDeluxe:
                case Games.TripleDice:
                case Games.FlashingDice:
                case Games.Retro7Hot:
                case Games.BurningIceGd:
                case Games.MysteryJokerHot:
                case Games.HeatingIce:
                case Games.HeatingIceDeluxe:
                case Games.HeatingDice:
                    return GameVegasHotConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);

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
                case Games.JokerQueen:
                    return GameJokerQueenConversion.ToSlotDataResV3(combination);
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

        #endregion Public methods
    }
}
