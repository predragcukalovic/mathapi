using MathCombination.CombinationData;
using System.Collections.Generic;

namespace GameAgeOfRome
{
    public class CombinationAgeOfRome : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'AgeOfRome' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        /// <param name="addArray"></param>
        public void MatrixToCombinationAgeOfRome(MatrixAgeOfRome matrix, int numberOfLines, int bet, bool gratisGame, ref byte[] addArray)
        {
            if (addArray[15] == 3)
            {
                addArray = new byte[16];
            }
            WinFor2 = 1;
            PositionFor2 = new byte[numberOfLines];
            FillMatrixArray(matrix);
            if (gratisGame)
            {
                for (var i = 0; i < 15; i++)
                {
                    if (addArray[i] > 0)
                    {
                        matrix.SetElement(i % 5, i / 5, addArray[i] - 1);
                    }
                }
            }

            var bonusLineInfo = matrix.GetBonusLineInfo();
            GratisGame = !gratisGame && bonusLineInfo != null;
            NumberOfGratisGames = GratisGame ? MatrixAgeOfRome.GRATIS_GAMES : 0;

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                byte winElem;
                byte[] winPos;
                byte wildMultiply;
                var win = matrix.CalculateWinLine(i, out winElem, out winPos, out wildMultiply);
                PositionFor2[i - 1] = wildMultiply;
                if (win == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet,
                    WinningElement = winElem,
                    WinningPosition = winPos
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (bonusLineInfo != null)
            {
                linesInfo.Add(bonusLineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
            var respin = false;
            var gratisInGratis = false;
            if (gratisGame)
            {
                foreach (var lineInfo in LinesInformation)
                {
                    for (var i = 0; i < lineInfo.WinningPosition.Length; i++)
                    {
                        if (lineInfo.WinningPosition[i] < 15)
                        {
                            if (addArray[lineInfo.WinningPosition[i]] == 0)
                            {
                                addArray[lineInfo.WinningPosition[i]] = (byte)(matrix.GetElement(lineInfo.WinningPosition[i] % 5, lineInfo.WinningPosition[i] / 5) + 1);
                                respin = true;
                            }
                        }
                    }
                    if (lineInfo.Id == 254)
                    {
                        gratisInGratis = true;
                        addArray[15] = 2;
                    }
                }
                var fullScreen = true;
                for (var i = 0; i < 15; i++)
                {
                    if (addArray[i] == 0)
                    {
                        fullScreen = false;
                        break;
                    }
                }
                if (respin && !fullScreen)
                {
                    GratisGame = true;
                    NumberOfGratisGames = 1;
                    TotalWin = 0;
                    WinFor2 = 0;
                    addArray[15] = (byte)System.Math.Max(1, (int)addArray[15]);
                }
                else
                {
                    //addArray = new byte[15];
                    addArray[15] = 3;
                    if (gratisInGratis)
                    {
                        GratisGame = true;
                        NumberOfGratisGames = MatrixAgeOfRome.GRATIS_GAMES;
                    }
                }
            }
            AdditionalArray = addArray;
        }
    }
}
