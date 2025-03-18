namespace Papi.GameServer.Math.Contracts.StructuresV3
{
    public class HelpConfigV3<T>
    {
        public HelpSymbolConfigV3<T>[] symbols { get; set; }
        public HelpLineConfigV3[] lines { get; set; }
        public decimal? rtp { get; set; }
        public long gambleLimit { get; set; }
    }

    public class HelpLineConfigV3
    {
        public int id { get; set; }
        public int[] positions { get; set; }
    }

    public class HelpSymbolConfigV3<T>
    {
        public T extra { get; set; }
        public int id { get; set; }
        public int[] coefficients { get; set; }
        public HelpSymbolFeatureV3[] features { get; set; }
    }

    public enum HelpSymbolFeatureV3
    {
        Regular = 0,
        Scatter = 1,
        FreeSpin = 2,
        Respin = 3,
        Bonus = 4
    }

    public class HelpSymbolExtraV3
    {
        public int[] freeSpins { get; set; }
    }
}