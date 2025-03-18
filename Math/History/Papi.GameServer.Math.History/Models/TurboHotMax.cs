using Papi.GameServer.Math.Contracts.StructuresV3;

namespace Papi.GameServer.Math.History.Models
{
    public class TurboHotMax
    {
        public int[] symbols { get; set; }
        public int[] upperRow { get; set; }
        public int[] bottomRow { get; set; }
        public int totalSum { get; set; }
        public byte noWinLines { get; set; }
        public int numberOfFreeSpins { get; set; }
        public bool isGratis { get; set; }
        public int[] expandWild { get; set; }
        public LineInfoJson[] winStruct { get; set; }
    }
}