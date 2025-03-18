using MathBaseProject.BaseMathData;

namespace MathForGames.GameHotStars
{
    public class LineHotStars : Line
    {
        /// <summary>
        /// Računa dobitak linije sleva na desno.
        /// </summary>
        /// <param name="winForLines"></param>
        /// <param name="wild"></param>
        /// <returns></returns>
        public int CalculateLeftLineWin(int[,] winForLines, int wild)
        {
            var sL = GetSymbolAndPositions(wild);
            return winForLines[sL.Symbol, sL.Positions];
        }

        /// <summary>
        /// Računa dobitak linije zdesna na levo.
        /// </summary>
        /// <param name="winForLines"></param>
        /// <param name="wild"></param>
        /// <returns></returns>
        public int CalculateRightLineWin(int[,] winForLines, int wild)
        {
            InvertLine();
            var sR = GetSymbolAndPositions(wild);
            InvertLine();
            return winForLines[sR.Symbol, sR.Positions];
        }
    }
}
