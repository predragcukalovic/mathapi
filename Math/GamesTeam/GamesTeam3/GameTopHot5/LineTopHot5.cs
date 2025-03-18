using MathForGames.GameVegasHot;

namespace GameTopHot5
{
    public class LineTopHot5 : LineVegasHot
    {
        public override int CalculateLineWin()
        {
            if (Line[0] == Line[1] && Line[1] == Line[2])
            {
                return MatrixTopHot5.WinForTopHot5[Line[0]];
            }
            return 0;
        }
    }
}
