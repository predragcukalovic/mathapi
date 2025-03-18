namespace Papi.GameServer.Math.Contracts.Responses
{
    public class LineInfoModel
    {
        public byte Id { get; set; }
        public int Win { get; set; }
        public byte WinningElement { get; set; }
        public byte[] WinningPosition { get; set; }
    }
}