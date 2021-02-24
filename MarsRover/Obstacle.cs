namespace MarsRover
{
    public class Obstacle : Point
    {
        
    }
    
    public class Point
    {
        public static int MINIMAL_COORDINATE_VALUE = 1;
        public static int MAXIMAL_COORDINATE_VALUE = 5;
        protected int x;
        protected int y;

        public int X() => x;
        public int Y() => y;
    }
}