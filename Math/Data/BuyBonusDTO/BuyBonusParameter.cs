namespace BuyBonusDTO
{
    public class BuyBonusParameter
    {
        private int _Type;
        private int _Lines;
        private int _Multiplier;
        private int _Rtp;

        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public int Lines
        {
            get { return _Lines; }
            set { _Lines = value; }
        }

        public int Multiplier
        {
            get { return _Multiplier; }
            set { _Multiplier = value; }
        }

        public int Rtp
        {
            get { return _Rtp; }
            set { _Rtp = value; }
        }
    }
}
