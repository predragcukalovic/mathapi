using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using MathForGames.GameHotStars;
using RNGUtils.RandomData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCrownsAndStars
{
    public class CombinationCrownsAndStars : Combination
    {
        private static readonly double[] _RespinMegapotProbs = { 0, 0, 0.2, 0.001, 0.00001 };

        private static int CountMegapots(byte[] megapots)
        {
            if (megapots == null || megapots.Length == 0) return 0;
            int count = 0;

            for (int i = 0; i < 5; i++)
            {
                if (megapots[i] > 0)
                    count++;
            }

            return count;
        }

        private static bool IsMegapotInReel(MatrixHotStars matrix, int reel, byte[] megapots)
        {
            return megapots[reel] > 0 || matrix.GetElement(reel, 0) == 9 || matrix.GetElement(reel, 1) == 9 ||
                   matrix.GetElement(reel, 2) == 9;
        }

        private static void AddMegapotRespin(MatrixHotStars matrix, byte[] megapots, int numberOfMegapots)
        {
            var rnd = SoftwareRng.Next();
            var targetMegapotCount = 0;
            for (var i = 5; i > numberOfMegapots; i--)
            {
                if (rnd < _RespinMegapotProbs[i - 1])
                {
                    targetMegapotCount = i;
                    break;
                }
            }

            var megapotsLeftToAdd = targetMegapotCount - numberOfMegapots;

            while (megapotsLeftToAdd > 0)
            {
                var targetReel = (int)SoftwareRng.Next(0, 5);

                if (!IsMegapotInReel(matrix, targetReel, megapots))
                {
                    matrix.SetElement(targetReel, (int)SoftwareRng.Next(3), 9);
                    megapotsLeftToAdd--;
                }
            }
        }

        private static void AddMegapotInitial(MatrixHotStars matrix, byte[] megapots, int megapotsToAdd)
        {
            var megapotsLeftToAdd = megapotsToAdd;
            while (megapotsLeftToAdd > 0)
            {
                var targetReel = megapotsToAdd == 1 ? (int)SoftwareRng.Next(1, 4) : (int)SoftwareRng.Next(5);
                if (!IsMegapotInReel(matrix, targetReel, megapots))
                {
                    matrix.SetElement(targetReel, (int)SoftwareRng.Next(3), 9);
                    megapotsLeftToAdd--;
                }
            }
        }

        protected void TransformMatrix(MatrixHotStars matrix)
        {
            var nextPosition = 0;
            for (var i = 1; i < 4; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (matrix.GetElement(i, j) == 0)
                    {
                        matrix.SetElement(i, 0, 0);
                        matrix.SetElement(i, 1, 0);
                        matrix.SetElement(i, 2, 0);
                        PositionFor2[nextPosition++] = (byte)(j * 5 + i);
                        break;
                    }
                }
            }
        }

        public void MatrixToCombination(MatrixCrownsAndStars matrix, int numberOfLines, int bet, int megapotsToAdd,
            ref byte[] megapots, bool cheatTool = false)
        {
            int numberOfMegapotsFromPrev = CountMegapots(megapots);

            if (numberOfMegapotsFromPrev > 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (!cheatTool)
                            matrix.SetElement(i, j, 254);
                        else if (cheatTool && matrix.GetElement(i, j) != 9)
                            matrix.SetElement(i, j, 254);
                    }
                }

                AddMegapotRespin(matrix, megapots, numberOfMegapotsFromPrev);
            }
            else
            {
                if (megapotsToAdd > 0)
                    AddMegapotInitial(matrix, megapots, megapotsToAdd);
            }

            FillMatrixArray(matrix);
            CreateEmptyArray(PositionFor2);
            GratisGame = false;
            NumberOfGratisGames = 0;

            byte[] prevMegapots = new byte[5];
            Array.Copy(megapots, prevMegapots, 5);
            int numberOfMegapots = numberOfMegapotsFromPrev;

            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (Matrix[i, j] == 9)
                    {
                        megapots[i] = (byte)(j + 1);
                        if (prevMegapots[i] ==
                            0)
                        {
                            GratisGame = true;
                            NumberOfGratisGames = 1;
                            numberOfMegapots++;
                        }
                    }
                }

                var posInReel = megapots[i];
                if (posInReel > 0)
                    matrix.SetElement(i, posInReel - 1, 9);
            }

            if (numberOfMegapots == 1 || numberOfMegapots == 5)
            {
                GratisGame = false;
                NumberOfGratisGames = 0;
                megapots = new byte[] { 0, 0, 0, 0, 0 };
            }

            FillMatrixArray(matrix);
            TransformMatrix(matrix);

            if (megapots.ToArray().SequenceEqual(prevMegapots))
            {
                megapots = new byte[] { 0, 0, 0, 0, 0 };
            }


            WinFor2 = 0;
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            CreateLinesInformation(matrix, numberOfLines, bet, 0, MatrixCrownsAndStars.WinForWildCrownsAndStars,
                GlobalData.GameLineExtra,
                GratisGame ? 0 : matrix.GetNoLineWin(9, MatrixCrownsAndStars.WinForScatterCrownsAndStars), 9);

            PositionFor2 = CheckExpandingWild(LinesInformation, PositionFor2, matrix);
            AdditionalArray = megapots;
        }

        private static byte[] CheckExpandingWild(IEnumerable<LineInfo> lineInfo, byte[] position2,
            MatrixCrownsAndStars matrix)
        {
            var shouldBeFixed = new[] { true, true, true };

            foreach (var info in lineInfo)
            {
                for (var i = 0; i < 5; i++)
                {
                    var el = info.WinningPosition[i];
                    if (el < 15 && matrix.GetElement(el % 5, el / 5) == 0)
                    {
                        //Remove expanding if no win line contains it
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

        protected void CreateLinesInformation(MatrixCrownsAndStars matrix, int numberOfLines, int bet, int wild,
            int[] winForWild, int[,] gameLines, int addExtraLine = 0, int extraSymbol = -1)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();

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

            if (matrix.GetNumberOfElement(254) == 0)
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

                    lineInfo.WinningPosition = matrix.GetLine(i, gameLines)
                        .GetLinesPositions(gameLines, i, wild, lineInfo.WinningElement);
                    TotalWin += lineInfo.Win;
                    linesInfo.Add(lineInfo);
                }

            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }
    }
}