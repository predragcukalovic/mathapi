using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Collections.Generic;

namespace GameTurboStars40
{
    public class CombinationTurboStars40 : Combination
    {
        public void MatrixToCombination(MatrixTurboStars40 matrix, int numberOfLines, int bet, byte stars,
            bool addFenix = true)
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

        protected void CreateLinesInformationsStars(MatrixTurboStars40 matrix, int numberOfLines, int bet)
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
                    lineInfo.WinningPosition = matrix.GetLine(i, GlobalData.GameLineExtra)
                        .GetLinesPositions(GlobalData.GameLineExtra, i, 0, lineInfo.WinningElement);
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
    }
}