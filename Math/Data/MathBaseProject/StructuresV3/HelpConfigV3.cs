namespace MathBaseProject.StructuresV3
{
    public class HelpConfigV3<T>
    {
        private HelpSymbolConfigV3<T>[] _SymbolsV3;
        private long _GambleLimit;
        private HelpLineConfigV3[] _LinesV3;
        private decimal? _RTP;

        public HelpSymbolConfigV3<T>[] symbols
        {
            get { return _SymbolsV3; }
            set { _SymbolsV3 = value; }
        }

        public HelpLineConfigV3[] lines
        {
            get { return _LinesV3; }
            set { _LinesV3 = value; }
        }

        public decimal? rtp
        {
            get { return _RTP; }
            set { _RTP = value; }
        }

        public long gambleLimit
        {
            get { return _GambleLimit; }
            set { _GambleLimit = value; }
        }

        public HelpConfigV3()
        {

        }
    }

    public class HelpLineConfigV3
    {
        private int _Id;
        private int[] _Positions;

        public int id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int[] positions
        {
            get { return _Positions; }
            set { _Positions = value; }
        }

        public HelpLineConfigV3()
        {

        }
    }

    public class HelpSymbolConfigV3<T>
    {
        private int _Id;
        private HelpSymbolFeatureV3[] _FeaturesV3;
        private int[] _Coefficients;
        private T _Extra;

        public T extra
        {
            get { return _Extra; }
            set { _Extra = value; }
        }

        public int id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int[] coefficients
        {
            get { return _Coefficients; }
            set { _Coefficients = value; }
        }

        public HelpSymbolFeatureV3[] features
        {
            get { return _FeaturesV3; }
            set { _FeaturesV3 = value; }
        }

        public HelpSymbolConfigV3()
        {

        }
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
        private int[] _FreeSpins;

        public int[] freeSpins
        {
            get { return _FreeSpins; }
            set { _FreeSpins = value; }
        }

        public HelpSymbolExtraV3()
        {

        }
    }
}
