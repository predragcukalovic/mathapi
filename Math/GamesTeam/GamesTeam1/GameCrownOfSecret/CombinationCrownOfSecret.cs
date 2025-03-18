using GameBonusEpicCrown;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCrownOfSecret
{
    public class CombinationCrownOfSecret : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'CrownOfSecret' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="addArray">Informacije u bonus igri - koji simbol je x2/x3/respin i koliko je aktivnih rilova i da li je respin iskoriscen</param>
        public void MatrixToCombinationCrownOfSecret(MatrixCrownOfSecret matrix, int bet, bool gratisGame,
            int numberOfLines, ref byte[] addArray, bool isNonWinning = false)
        {
            FillMatrixArray(matrix);

            CreateEmptyArray(PositionFor2);
            var bonusSymbols = matrix.GetNumberOfElement(9);
            GratisGame = bonusSymbols >= 3 && !gratisGame;
            NumberOfGratisGames = GratisGame ? 1 : 0;

            AdditionalInformation = 0;

            // u regularnoj smo igri, resetujemo addArray
            if (!GratisGame && !gratisGame)
            {
                addArray = new byte[5];
            }

            LineInfo li9 = null;

            if (gratisGame)
            {
                AdditionalInformation = 1; // Da bi u konverziji razlikovali ruku koja nas uvodi u bonus i samu ruku u bonusu

                var bonusData = BonusDataCrownOfSecret.FromByteArray(addArray);

                var wonSymbol = matrix.GetElement(0, 0); // svi su isti

                if (!isNonWinning)
                {
                    if (wonSymbol == bonusData.SpecialSymbols.Respin && !bonusData.WasRespinUsed)
                    {
                        NumberOfGratisGames = 1;
                        GratisGame = true;
                        addArray[4] = 1; // respin used
                    }

                    var multiplier = wonSymbol == bonusData.SpecialSymbols.X2
                        ? 2
                        : wonSymbol == bonusData.SpecialSymbols.X3
                            ? 3
                            : 1;

                    if (wonSymbol == 9)
                    {
                        addArray[3]++;
                        GratisGame = true;
                        NumberOfGratisGames = 1;
                    }
                    CreateLinesInformationForBonusGame(matrix, numberOfLines, bet, multiplier, 0,
                        MatrixBonusEpicCrown.WinForWildBonusEpicCrown,
                        GlobalData.GameLineExtra, bonusData.NumberOfActiveReels, wonSymbol);
                }
                else
                {
                    CreateLinesInformationForNonWinningCombination();
                }
            }
            else
            {
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

                CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, MatrixBonusEpicCrown.WinForWildBonusEpicCrown,
                    GlobalData.GameLineExtra);

                // ruka koja nas uvodi u bonus, postavljamo inicijalno specijalne simbole
                if (bonusSymbols >= 3)
                {
                    addArray = new byte[5];
                    int[] specialSymbols = PickSpecialSymbols();

                    for (var i = 0; i < specialSymbols.Length; i++)
                    {
                        addArray[i] = (byte)specialSymbols[i];
                    }

                    addArray[3] = (byte)bonusSymbols; // postavljamo broj aktivnih rilova
                    addArray[4] = 0; // postavljamo da respin nije iskoriscen

                    li9 = new LineInfo
                    {
                        WinningPosition = matrix.GetPositionsArray(9),
                        Id = EXTRA_LINE,
                        Win = 0,
                        WinningElement = 9
                    };
                }
            }

            if (li9 != null)
            {
                var li = LinesInformation.ToList();
                li.Add(li9);
                NumberOfWinningLines++;
                LinesInformation = li.ToArray();
            }

            AdditionalArray = addArray;
            PositionFor2 = matrix.FixExpand(LinesInformation, PositionFor2);
        }

        /// <summary>
        /// Kreira niz LinesInformation za Bonus igru.
        /// </summary>
        /// <param name="matrix">Matrica</param>
        /// <param name="numberOfLines">Broj linija</param>
        /// <param name="bet">Ulog</param>
        /// <param name="multiplier">Množilac za vreme gratis igara</param>
        /// <param name="wild">Wild element</param>
        /// <param name="winForWild">Dobitak za wild</param>
        /// <param name="gameLines">Linije na koje se igra</param>
        /// <param name="numberOfActiveReels">Koliko je rilova otkljucano</param>
        /// <param name="landedSymbol">Koji se simbol pao</param>
        protected void CreateLinesInformationForBonusGame(MatrixCrownOfSecret matrix, int numberOfLines, int bet, int multiplier,
            int wild, int[] winForWild, int[,] gameLines, int numberOfActiveReels, int landedSymbol)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.CalculateWinLineForBonusGame(i, numberOfActiveReels, landedSymbol);
                if (win == 0)
                {
                    continue;
                }

                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet * multiplier,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, wild, winForWild, win, gameLines)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, gameLines)
                    .GetLinesPositions(gameLines, i, wild, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }

            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        protected void CreateLinesInformationForNonWinningCombination()
        {
            NumberOfWinningLines = 0;
            LinesInformation = Array.Empty<LineInfo>();
            TotalWin = 0;
        }

        /// <summary>
        /// Returns array containing ids for x2/x3/respin symbol
        /// x2 is first, x3 is second, respin is third
        /// </summary>
        /// <returns></returns>
        private int[] PickSpecialSymbols()
        {
            HashSet<int> pickedSymbolIds = new HashSet<int>();

            while (pickedSymbolIds.Count < 3)
            {
                var randomNumber = SoftwareRng.Next(1, 9);
                pickedSymbolIds.Add((int)randomNumber);
            }

            return pickedSymbolIds.ToArray();
        }
    }
}
