
namespace First_Semester_Project.ActorsNamespace
{
    internal struct Coordinates
    {
        public int X =0;
        public int Y =0;

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Coordinates(Directions direction)
        {
            switch(direction)
            {
                case Directions.Up:
                    X = 0;
                    Y = -1;
                    break;
                case Directions.Down:
                    X = 0;
                    Y = 1;
                    break;
                case Directions.Left:
                    X = -1;
                    Y = 0;
                    break;
                case Directions.Right:
                    X = 1;
                    Y = 0;
                    break;

                case Directions.Center:
                default:
                    break;
            }
        }

        public static Coordinates operator+(Coordinates left, Coordinates right) 
        { 
            return new(left.X+right.X, left.Y+right.Y);
        }
        public static bool operator ==(Coordinates left, Coordinates right)
        {
            return left.X==right.X && left.Y==right.Y;
        }
        public static bool operator !=(Coordinates left, Coordinates right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

    }
    enum Directions
    {
        Center,
        Up,
        Down, 
        Left, 
        Right,
    }
}
