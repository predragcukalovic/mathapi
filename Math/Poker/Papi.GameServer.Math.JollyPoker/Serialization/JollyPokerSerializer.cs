namespace Papi.GameServer.Math.JollyPoker.Serialization
{
    public static class JollyPokerSerializer
    {
        public static byte[] Serialize(this PokerCombination.PokerCombination _)
        {
            var array = new byte[20];

            for (var i = 0; i < 5; i++)
            {
                array[i * 2] = _.CardValue[i];
                array[i * 2 + 1] = _.CardSign[i];
                array[10 + i] = _.GetHoldCards()[i];
            }
            for (var i = 3; i >= 0; i--)
            {
                array[18 - i] = (byte)(_.Win >> (8 * i));
            }
            array[19] = (byte)_.WinType;
            return array;
        }
    }
}