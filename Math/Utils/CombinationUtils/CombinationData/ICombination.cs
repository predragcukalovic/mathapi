using Papi.GameServer.Utils.Enums;
using System.Collections.Generic;

namespace MathCombination.CombinationData
{
    public class LineInfo
    {
        public byte Id;
        public int Win;
        public byte WinningElement;
        public byte[] WinningPosition;
    }

    public class LineInfoJson
    {
        public byte lineId;
        public int totalWin;
        public byte winningElement;
        public int[] symbolPositions;
    }

    /// <summary>
    /// Matrix - Matrica simbola koji treba da se prikažu
    /// GratisGame - Da li kombinacija daje gratis igre?
    /// NumberOfGratisGames - Koliko gratis igara daje?
    /// WinFor2 - Dobitak za sketere
    /// MultiplyFor2 - Neizabrani množioci za sketere
    /// MultiplyFor2Alpinist - Dobici za bernardince (alpinist); prve dve vrednosti su izabrani, ostali neizabrani
    /// PositionFor2 - Pozicije u matrici gde se nalaze sketeri
    /// NumberOfWinningLines - Broj dobitnih linija
    /// LinesInformation - Broj linije, dobitak, dobitni simbol i pozicije svih dobitnih linija
    /// GratisGamesValues - Vrednosti gratis simbola (WildWest)
    /// GratisGamesPositions - Pozicije gratis simbola u matrici (WildWest)
    /// AdditionalInformation - Dodatna informacija za neke igre (npr Fruits)
    /// TotalWin - Ukupni dobitak
    /// CascadeList - Za kaskadne igre (VikingGold)
    /// ToByteArray - Pretvara strukturu u niz bajtova
    /// </summary>
    public interface ICombination
    {
        byte[,] Matrix { get; }
        bool GratisGame { get; }
        int NumberOfGratisGames { get; }
        int WinFor2 { get; }
        byte[] MultiplyFor2 { get; }
        byte[] MultiplyFor2Alpinist { get; }
        byte[] PositionFor2 { get; }
        byte NumberOfWinningLines { get; }
        LineInfo[] LinesInformation { get; }
        byte[] GratisGamesValues { get; }
        byte[] GratisGamesPositions { get; }
        byte AdditionalInformation { get; }
        int TotalWin { get; }
        byte[] AdditionalArray { get; }
        List<ICombination> CascadeList { get; }
        byte[] ToByteArray(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis, ICombination combination);
        object ToJson(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis, ICombination combination);
        object ToGameData(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis, ICombination combination, bool json);
        bool IsBonus(Games game, bool gratisGame, byte addInfo, byte[] addArray);
    }
	
}
