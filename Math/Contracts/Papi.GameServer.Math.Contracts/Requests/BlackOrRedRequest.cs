namespace Papi.GameServer.Math.Contracts.Requests
{
    public class BlackOrRedRequest
    {
        public long Win { get; set; }
        public bool AlwaysWin { get; set; }
        public bool ReturnJson { get; set; }
        public int GameId { get; set; }
    }
}
