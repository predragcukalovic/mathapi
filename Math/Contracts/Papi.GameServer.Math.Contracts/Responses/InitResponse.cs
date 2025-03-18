using Papi.GameServer.Math.Contracts.StructuresV3;

namespace Papi.GameServer.Math.Contracts.Responses
{
    public class InitResponse
    {
        public HelpConfigV3<object> HelpConfig { get; set; }
        public ReelsV3 Reels { get; set; }
    }
}