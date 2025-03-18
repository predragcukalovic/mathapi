using MathCombination.CombinationData;
using MathForGames.GameMagicOfTheRing;
using RNGUtils.RandomData;
using System.Collections.Generic;

namespace MathForNovomatic.GameBookOfRaDeluxe
{
    public class CombinationBookOfRa : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru `Book Of Ra`
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame">Da li je gratis igra</param>
        /// <param name="gratisElement">Gratis element u toku gratis igara, 0 inače</param>
        public void MatrixToCombination(MatrixBookOfRa matrix, int numberOfLines, int bet, bool gratisGame, byte gratisElement)
        {
            AdditionalInformation = !gratisGame ? (byte)0 : gratisElement;
            FillMatrixArray(matrix);

            GratisGame = matrix.GetNumberOfElement((byte)BookOfRaSymbols.Book) >= 3;
            WinFor2 = 0;
            NumberOfGratisGames = GratisGame ? MatrixBookOfRa.GRATIS_GAMES : 0;

            CreateLinesInformationsRing(
                matrix, numberOfLines, bet, AdditionalInformation,
                MatrixBookOfRa.WinForGratisBookOfRa,
                MatrixBookOfRa.WinForLinesBookOfRaDeluxe,
                MatrixBookOfRa.WinForWildsBookOfRaDeluxe);

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
        /// Kreira niz LinesInformation za igre 'Magic of the ring' i 'Spellbook'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisElement"></param>
        /// <param name="noLineWins"></param>
        /// <param name="winForLines"></param>
        /// <param name="winForWilds"></param>
        protected void CreateLinesInformationsRing(MatrixMagicOfTheRing matrix,
                                                   int numberOfLines, int bet, byte gratisElement,
                                                   int[] noLineWins, int[,] winForLines, int[] winForWilds)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var winOfLine = matrix.CalculateWinLine(i, gratisElement);
                var winningElement = (byte)matrix.GetWinningElementForLine(i, (byte)BookOfRaSymbols.Book, winForWilds, winOfLine, MatrixBookOfRa.GameLines);
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
                        matrix.GetLine(i, MatrixBookOfRa.GameLines)
                            .GetLinesPositions(MatrixBookOfRa.GameLines, i, (byte)BookOfRaSymbols.Book, lineInfo.WinningElement);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }
            }

            var scatterWin = matrix.GetNoLineWin((byte)BookOfRaSymbols.Book, noLineWins);
            if ((gratisElement != -1 && (matrix.IsCanBeTransformed(gratisElement) || linesInfo.Count > 0)) || scatterWin > 0)
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
                    lineInfo15.WinningPosition = matrix.GetPositionsArray((byte)BookOfRaSymbols.Book);
                }
                linesInfo.Add(lineInfo15);
            }

            if (gratisElement != -1 && matrix.IsCanBeTransformed(gratisElement))
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
                    lineInfo.WinningPosition = matrix.GetLine(i, MatrixBookOfRa.GameLines).GetLinesPositionsNonOrder(MatrixBookOfRa.GameLines, i, lineInfo.WinningElement);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }
    }
}
