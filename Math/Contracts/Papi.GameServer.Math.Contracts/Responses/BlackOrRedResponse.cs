namespace Papi.GameServer.Math.Contracts.Responses
{
    public class BlackOrRedResponse
    {
        public byte CardValue { get; set; }
        public byte CardSign { get; set; }
        public long CurrentWin { get; set; }
        public long NextWin { get; set; }
    }
}