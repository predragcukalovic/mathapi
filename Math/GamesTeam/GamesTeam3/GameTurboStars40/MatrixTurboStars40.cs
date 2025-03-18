using MathForGames.BasicGameData;
using MathForGames.GameHotStars;

namespace GameTurboStars40
{
    public class MatrixTurboStars40 : MatrixHotStars
    {
        public new LineHotStars GetLine(int lineNumber)
        {
            var line = new LineHotStars();
            for (var i = 0; i < 5; i++)
            {
                line.SetElement(i, GetElement(i, GlobalData.GameLineExtra[lineNumber - 1, i]));
            }

            return line;
        }

        public override int CalculateLeftWinOfLine(int lineNumber)
        {
            return GetLine(lineNumber).CalculateLeftLineWin(LineWinsForGames.WinForLinesHotStars, 0);
        }

        public override int CalculateRightWinOfLine(int lineNumber)
        {
            return GetLine(lineNumber).CalculateRightLineWin(LineWinsForGames.WinForLinesHotStars, 0);
        }
    }
}