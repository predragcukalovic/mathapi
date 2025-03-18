using MathBaseProject.BaseMathData;

namespace GameCrownOfSecret
{
    public class LineCrownOfSecret : Line
    {
        public int CalculateLineWinForBonus(int[,] winForLines, int numberOfActiveReels, int landedSymbol)
        {
            return winForLines[landedSymbol, numberOfActiveReels - 1];
        }
    }
}
