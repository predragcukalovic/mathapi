namespace MathBaseProject.StructuresV3
{
    public class SlotDataResV3
    {
        private int[,] _Symbols;
        private WinLineV3[] _Wins;
        private long _Win;
        private object _Extra;
        private long _TotalWin;
        private int _GamesLeft;
        private bool _Played;
        private bool _GratisGame;

        public long totalWin
        {
            get { return _TotalWin; }
            set { _TotalWin = value; }
        }

        public int gamesLeft
        {
            get { return _GamesLeft; }
            set { _GamesLeft = value; }
        }

        public int[,] symbols
        {
            get { return _Symbols; }
            set { _Symbols = value; }
        }

        public WinLineV3[] wins
        {
            get { return _Wins; }
            set { _Wins = value; }
        }

        public long win
        {
            get { return _Win; }
            set { _Win = value; }
        }

        public object extra
        {
            get { return _Extra; }
            set { _Extra = value; }
        }

        public bool played
        {
            get { return _Played; }
            set { _Played = value; }
        }

        public bool gratisGame
        {
            get { return _GratisGame; }
            set { _GratisGame = value; }
        }

        public SlotDataResV3()
        {

        }
    }

    public class WinLineV3
    {
        private int _LineId;
        private WinSymbolV3[] _Symbols;
        private int _SoundId;
        private long _Win;

        public int lineId
        {
            get { return _LineId; }
            set { _LineId = value; }
        }

        public int soundId
        {
            get { return _SoundId; }
            set { _SoundId = value; }
        }

        public long win
        {
            get { return _Win; }
            set { _Win = value; }
        }

        public WinSymbolV3[] symbols
        {
            get { return _Symbols; }
            set { _Symbols = value; }
        }

        public WinLineV3()
        {

        }
    }

    public class WinSymbolV3
    {
        private int _Reel;
        private int _Row;
        private int _Id;
        private long _Value;

        public int reel
        {
            get { return _Reel; }
            set { _Reel = value; }
        }

        public int row
        {
            get { return _Row; }
            set { _Row = value; }
        }

        public int id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public long value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public WinSymbolV3()
        {

        }

    }
}
