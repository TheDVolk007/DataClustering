namespace Domain
{
    public class KPoint
    {
        public int x;
        public int y;
        public int clusterNo = -1;

        public KPoint()
        {
            clusterNo = -1;
        }

        public KPoint(int x, int y)
            : base()
        {
            this.x = x;
            this.y = y;
        }
    }
}