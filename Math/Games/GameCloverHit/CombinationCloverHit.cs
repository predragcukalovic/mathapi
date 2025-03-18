using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using MathForGames.GameCloverCash;
using RNGUtils.RandomData;
using System.Collections.Generic;
using System.Linq;

namespace GameCloverHit
{
    public class CombinationCloverHit : Combination
    {
        public void MatrixToCombinationCloverHit(MatrixCloverCash matrix, int numberOfLines, int bet, bool gratisGame, ref byte[] addArray)
        {
            WinFor2 = bet;
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
                            addArray[i] = (byte)(index + 1);
                            addArray[15] = 3;
                            elemNum++;
                            lockWins.Add(new LineInfo { Id = EXTRA_LINE, WinningElement = 11, Win = lockwin * bet, WinningPosition = new byte[] { (byte)i, 255, 255, 255, 255 } });
                        }
                    }
                }
                for (var i = 0; i < 5; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        Matrix[i, j] = (byte)(addArray[5 * j + i] == 0 ? 12 : 11);
                    }
                }
                LinesInformation = new LineInfo[0];
                AdditionalArray = addArray;
                if (elemNum == 15)
                {
                    TotalWin += MatrixCloverCash.GRAND_JACKPOT * bet;
                    lockWins.Add(new LineInfo { Id = 253, WinningElement = 11, Win = MatrixCloverCash.GRAND_JACKPOT * bet, WinningPosition = new byte[] { 255, 255, 255, 255, 255 } });
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
                if (!GratisGame)
                {
                    for (var i = 0; i < 15; i++)
                    {
                        if (addArray[i] > 0)
                        {
                            TotalWin += MatrixCloverCash.GetWinByIndex(addArray[i] - 1, table) * bet;
                        }
                    }
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

            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, LineWinsForGames.WinForWildsCloverCash, GlobalData.GameLineExtra);

            var lockit = matrix.GetNumberOfElement(11);
            if (lockit >= 6)
            {
                if (lockit == 15)
                {
                    TotalWin += MatrixCloverCash.GRAND_JACKPOT * bet;
                    lockWins.Add(new LineInfo { Id = 252, WinningElement = 11, Win = MatrixCloverCash.GRAND_JACKPOT * bet, WinningPosition = new byte[] { 255, 255, 255, 255, 255 } });
                }
                else
                {
                    GratisGame = true;
                    NumberOfGratisGames = 1;
                }
                addArray[15] = 3;
                for (var i = 0; i < 5; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        if (matrix.GetElement(i, j) == 11)
                        {
                            var index = MatrixCloverCash.GetRandomIndexByTable(table);
                            var lockwin = MatrixCloverCash.GetWinByIndex(index, table);
                            if (lockit == 15)
                            {
                                TotalWin += lockwin * bet;
                            }
                            addArray[5 * j + i] = (byte)(index + 1);
                            lockWins.Add(new LineInfo { Id = EXTRA_LINE, WinningElement = 11, Win = lockwin * bet, WinningPosition = new byte[] { (byte)(5 * j + i), 255, 255, 255, 255 } });
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
    }
}