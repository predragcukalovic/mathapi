using MathBaseProject.BaseMathData;
using MathForGames.BasicGameData;

namespace MathForGames.GameHotParty
{
    public class MatrixHotParty : Matrix
    {
        #region Public methods

        /// <summary>
        /// Učitava matricu iz niza bajtova
        /// </summary>
        /// <param name="array">Niz iz kog učitava</param>
        public override void FromByteArray(byte[] array)
        {
            if (array.Length != 16)
            {
                return;
            }
            var next = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (next % 2 == 0)
                    {
                        SetElement(i, j, array[next / 2] >> 4);
                    }
                    else
                    {
                        SetElement(i, j, array[next / 2] & 0x0F);
                    }
                    next++;
                }
            }
            GratisGame = false;
            Bonus = 0;
        }

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineRing).CalculateLineWin(LineWinsForGames.WinForLinesHotParty, null, -1, 1);
        }

        #endregion
    }
}
