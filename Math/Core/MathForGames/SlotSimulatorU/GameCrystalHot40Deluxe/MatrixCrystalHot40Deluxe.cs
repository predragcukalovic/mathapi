using MathForGames.BasicGameData;
using MathForGames.GameTurboHot40;

namespace MathForGames.GameCrystalHot40Deluxe
{
    public class MatrixCrystalHot40Deluxe : MatrixTurboHot40
    {
        public int CalculateLeftLineWin(int line)
        {
            var l = GetLine(line, GlobalData.GameLineTurbo);
            return l.CalculateLineWin(LineWinsForGames.WinForLinesTurboHot40, LineWinsForGames.WinForWildsTurboHot40, 0, 1);
        }

        public int CalculateRightLineWin(int line)
        {
            var l = GetLine(line, GlobalData.GameLineTurbo);
            return l.CalculateRightLineWin(LineWinsForGames.WinForLinesTurboHot40, LineWinsForGames.WinForWildsTurboHot40, 0, 1);
        }
    }
}
