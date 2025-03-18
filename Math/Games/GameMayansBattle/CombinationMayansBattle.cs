using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Linq;

namespace GameMayansBattle
{
    public class CombinationMayansBattle : Combination
    {
        public void MatrixToCombination(MatrixMayansBattle matrix, int numberOfLines, int bet, bool gratisGame, bool cheatTool = false)
        {
            if (!cheatTool)
            {
                matrix.BuildMatrix(gratisGame);
            }
            FillMatrixArray(matrix);

            CreateEmptyArray(PositionFor2);
            var scatNum = matrix.GetNumberOfElement(10);
            GratisGame = scatNum >= 7 && !gratisGame;
            NumberOfGratisGames = GratisGame ? MatrixMayansBattle.NumberOfGratis[scatNum - 7] : 0;

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, MatrixMayansBattle.WinForWildMayansBattle, GlobalData.GameLineExtra);

            if (scatNum >= 7)
            {
                var li = new LineInfo { Id = EXTRA_LINE, Win = MatrixMayansBattle.SCATTER_WIN * numberOfLines * bet, WinningElement = 10 };
                var pos = new byte[scatNum];
                var nextPosition = 0;
                for (var i = 1; i < 4; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        if (matrix.GetElement(i, j) == 10)
                        {
                            pos[nextPosition++] = (byte)(j * 5 + i);
                        }
                    }
                }
                li.WinningPosition = pos;
                var linfo = LinesInformation.ToList();
                linfo.Add(li);
                NumberOfWinningLines++;
                TotalWin += li.Win;
                LinesInformation = linfo.ToArray();
            }
        }
    }
}
