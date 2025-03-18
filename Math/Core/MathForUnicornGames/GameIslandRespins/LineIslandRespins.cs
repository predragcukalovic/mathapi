using MathBaseProject.BaseMathData;
using System;

namespace MathForUnicornGames.GameIslandRespins
{
    public class LineIslandRespins : Line
    {
        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="winForLines">Matrica dobitaka</param>
        /// <param name="winForSemiWild">Dobici za wild</param>
        /// <param name="semiWild">Simbol koji menja odredjene simbole</param>
        /// <param name="substitutionSymbol"> Simbol koji jedini moze biti zamenjen semiWild symbolom</param>
        /// <returns></returns>
        public int CalculateLineWinWithSemiLines(int[,] winForLines, int[] winForSemiWild, int semiWild, int substitutionSymbol)
        {
            var s = GetSymbolAndPositions(-1);
            if (s.Symbol != semiWild && s.Symbol != substitutionSymbol)
            {
                return winForLines[s.Symbol, s.Positions];
            }
            var sWithSemiWild = GetSymbolAndPositions(semiWild);
            if (s.Symbol == sWithSemiWild.Symbol)
            {
                return winForLines[sWithSemiWild.Symbol, sWithSemiWild.Positions];
            }
            if (sWithSemiWild.Symbol != substitutionSymbol)
            {
                return winForLines[s.Symbol, s.Positions];
            }
            return Math.Max(winForLines[sWithSemiWild.Symbol, sWithSemiWild.Positions], winForSemiWild[s.Positions]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="semiWild"></param>
        /// <param name="substitutionSymbol"></param>
        /// <param name="lineWin"></param>
        /// <param name="winForSemiWilds"></param>
        /// <returns></returns>
        public int GetWinningElement(int semiWild, int substitutionSymbol, int lineWin, int[] winForSemiWilds)
        {
            var symb = GetSymbolAndPositions(-1);
            if (symb.Symbol != semiWild)
            {
                return symb.Symbol;
            }
            if (CalculateLineWildWin(winForSemiWilds, semiWild) == lineWin)
            {
                return semiWild;
            }
            return substitutionSymbol;
        }
    }
}
