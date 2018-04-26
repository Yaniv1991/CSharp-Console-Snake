namespace Snake
{
    class Point
    {
        int _x, _y;
        Details _detail;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public Details Detail { get => _detail; set => _detail = value; }

        public Point()
        {

        }
        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }
        public Point(int x, int y, Details detail)
            :this(x,y)
        {
            _detail = detail;
        }
    }
}
