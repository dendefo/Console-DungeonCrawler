namespace First_Semester_Project.Core
{
    internal struct Coordinates
    {
        public int X = 0;
        public int Y = 0;

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Coordinates(Directions direction)
        {
            switch (direction)
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

        public static Coordinates operator +(Coordinates left, Coordinates right)
        {
            return new(left.X + right.X, left.Y + right.Y);
        }

        /// <summary>
        /// Apllyes - to each value of "coor"
        /// </summary>
        /// <param name="coor"></param>
        /// <returns></returns>
        public static Coordinates operator -(Coordinates coor)
        {
            return new(-coor.X, -coor.Y);
        }

        public static Coordinates operator -(Coordinates left, Coordinates right)
        {
            return new(left.X - right.X, left.Y - right.Y);
        }

        /// <summary>
        /// Returns Abs of two coordinates, order isn't important
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Coordinates operator |(Coordinates first, Coordinates second)
        {
            return new(Math.Abs(first.X - second.X), Math.Abs(first.Y - second.Y));
        }

        /// <summary>
        /// Returns Square from array by coordinates
        /// </summary>
        /// <param name="coor"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Square operator ^(Coordinates coor, Square[][] map)
        {
            return map[coor.Y][coor.X];
        }

        /// <summary>
        /// Returnes true if both X and Y of Coordinates are same
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Coordinates left, Coordinates right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        /// <summary>
        /// Returnes true if X and Y of Coordinates are not the same
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Coordinates left, Coordinates right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        /// <summary>
        /// Returnes True if X and Y are less than value
        /// </summary>
        /// <param name="coor"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool operator <(Coordinates coor, int value)
        {
            return coor.X < value && coor.Y < value;
        }

        /// <summary>
        /// Returnes True if X and Y are greater than value
        /// </summary>
        /// <param name="coor"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool operator >(Coordinates coor, int value)
        {
            return coor.X > value && coor.Y > value;
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
