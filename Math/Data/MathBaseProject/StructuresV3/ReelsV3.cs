namespace MathBaseProject.StructuresV3
{
    public class ReelsV3
    {
        private int[][] _Regular;
        private int[][] _FreeSpin;
        private int[][] _Respin;

        public int[][] regular
        {
            get { return _Regular; }
            set { _Regular = value; }
        }

        public int[][] respin
        {
            get { return _Respin; }
            set { _Respin = value; }
        }

        public int[][] freeSpin
        {
            get { return _FreeSpin; }
            set { _FreeSpin = value; }
        }

        public ReelsV3()
        {

        }
    }
}
