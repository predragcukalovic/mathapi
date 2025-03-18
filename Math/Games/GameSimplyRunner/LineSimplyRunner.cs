using MathForGames.GameVegasHot;

namespace GameSimplyRunner
{
    public class LineSimplyRunner : LineVegasHot
    {
        public static readonly int[] WinSimplyRunner = { 40, 16, 16, 16, 16, 8, 8, 8, 8 };

        public override int CalculateLineWin()
        {
            if ((Line[0] == 0 || Line[0] > 8) && (Line[1] == 0 || Line[1] > 8) && (Line[2] == 0 || Line[2] > 8))
            {
                return WinSimplyRunner[0];
            }
            int winSymb;
            if (Line[0] != 0)
            {
                winSymb = Line[0] % 9;
            }
            else if (Line[1] != 0)
            {
                winSymb = Line[1] % 9;
            }
            else
            {
                winSymb = Line[2] % 9;
            }
            if ((Line[0] != 0 && Line[0] % 9 != winSymb) || (Line[1] != 0 && Line[1] % 9 != winSymb) || (Line[2] != 0 && Line[2] % 9 != winSymb))
            {
                return 0;
            }
            return WinSimplyRunner[winSymb];
        }

        public int GetWinningElement()
        {
            if (Line[0] != 0)
            {
                return Line[0] % 9;
            }
            if (Line[1] != 0)
            {
                return Line[1] % 9;
            }
            return Line[2] % 9;
        }
    }
}
