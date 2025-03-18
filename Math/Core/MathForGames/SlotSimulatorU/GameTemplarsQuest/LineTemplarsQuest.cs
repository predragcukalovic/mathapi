using MathBaseProject.BaseMathData;

namespace MathForGames.GameTemplarsQuest
{
    public class LineTemplarsQuest : Line
    {
        /// <summary>
        /// Računa dobitak linije sleva na desno.
        /// </summary>
        /// <param name="winForLines"></param>
        /// <param name="wild"></param>
        /// <param name="winForWilds"></param>
        /// <param name="wildMultiply"></param>
        /// <returns></returns>
        public int CalculateLeftLineWin(int[,] winForLines, int wild, int[] winForWilds, int wildMultiply)
        {
            return CalculateLineWin(winForLines, winForWilds, wild, wildMultiply);
        }

        /// <summary>
        /// Računa dobitak linije zdesna na levo.
        /// </summary>
        /// <param name="winForLines"></param>
        /// <param name="wild"></param>
        /// <param name="winForWilds"></param>
        /// <param name="wildMultiply"></param>
        /// <returns></returns>
        public int CalculateRightLineWin(int[,] winForLines, int wild, int[] winForWilds, int wildMultiply)
        {
            InvertLine();
            var sR = CalculateLineWin(winForLines, winForWilds, wild, wildMultiply);
            InvertLine();
            return sR;
        }

        /// <summary>
        /// Daje dobitni element za dobitnu liniju zdesna na levo.
        /// </summary>
        /// <param name="wild"></param>
        /// <param name="lineWin"></param>
        /// <param name="winForWilds"></param>
        /// <returns></returns>
        public int GetRightWinningElement(int wild, int lineWin, int[] winForWilds)
        {
            InvertLine();
            var elem = GetWinningElement(wild, lineWin, winForWilds);
            InvertLine();
            return elem;
        }
    }
}
