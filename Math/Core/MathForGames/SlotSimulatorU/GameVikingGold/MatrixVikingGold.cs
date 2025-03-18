using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameVikingGold
{
    public class MatrixVikingGold : Matrix
    {
        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesVikingGold, LineWinsForGames.WinForWildsVikingGold, 0, 1);
        }

        /// <summary>
        /// Daje dobitni simbol za traženu liniju.
        /// </summary>
        /// <param name="gameLines"></param>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public int GetWinningElementForLine(int[,] gameLines, int lineNumber)
        {
            return GetElement(0, gameLines[lineNumber - 1, 0]);
        }

        /// <summary>
        /// Broji koliko bonus simbola ima u matrici.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        private static int CountMatrixArrayBonusSymbols(byte[,] matrix)
        {
            var count = 0;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
