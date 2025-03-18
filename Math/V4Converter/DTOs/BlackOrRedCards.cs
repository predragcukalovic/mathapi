namespace V4Converter.DTOs
{
    public class BlackOrRedCards
    {
        public byte Number;
        public byte Sign;

        public BlackOrRedCards(byte number, byte sign)
        {
            Number = number;
            Sign = sign;
        }
    }
}
