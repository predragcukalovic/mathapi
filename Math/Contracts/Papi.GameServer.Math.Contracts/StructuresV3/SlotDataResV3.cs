namespace Papi.GameServer.Math.Contracts.StructuresV3
{
    public class SlotDataResV3
    {
        public long totalWin { get; set; }
        public int gamesLeft { get; set; }
        public int[,] symbols { get; set; }
        public WinLineV3[] wins { get; set; }
        public long win { get; set; }
        public object extra { get; set; }
        public object external { get; set; }
        public bool played { get; set; }
        public bool gratisGame { get; set; }
    }

    public class WinLineV3
    {
        public int lineId { get; set; }
        public int soundId { get; set; }
        public long win { get; set; }
        public WinSymbolV3[] symbols { get; set; }
    }

    public class WinSymbolV3
    {
        public int reel { get; set; }
        public int row { get; set; }
        public int id { get; set; }
        public long value { get; set; }
    }
}