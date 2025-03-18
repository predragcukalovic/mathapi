namespace CombinationExtras.ConversionData.V3Conversion.OtherStructuresV3
{
    public class WildExpandV3
    {
        private string _Type;
        private CoordinateV3 _Origin;
        private CoordinateV3[] _Coordinates;

        public string type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public CoordinateV3 origin
        {
            get { return _Origin; }
            set { _Origin = value; }
        }

        public CoordinateV3[] coordinates
        {
            get { return _Coordinates; }
            set { _Coordinates = value; }
        }
    }

    public class CoordinateV3
    {
        private int _Reel;
        private int _Row;

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
    }
}
