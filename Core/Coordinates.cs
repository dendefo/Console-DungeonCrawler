namespace First_Semester_Project.Core
{
    //My Int Vector2 Implemintation
    struct Coordinates
    {
        public int X = 0;
        public int Y = 0;

        static public Coordinates Up = new Coordinates(0, -1);
        static public Coordinates Down = new Coordinates(0, 1);
        static public Coordinates Left = new Coordinates(-1, 0);
        static public Coordinates Right = new Coordinates(1, 0);

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static float Distance(Coordinates left, Coordinates right)
        {
            var temp = Square(Abs(left, right), 2);
            return MathF.Pow(temp.X + temp.Y, 0.5f);
        }

        public static Coordinates Square(Coordinates coor, float pow)
        {
            return new((int)Math.Pow(coor.X, pow), (int)Math.Pow(coor.Y, pow));
        }

        
        /// <summary>
        /// Summs values of two coordinates
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
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
        public static Coordinates Abs(Coordinates left, Coordinates right)
        {
            return new(Math.Abs(left.X - right.X), Math.Abs(left.Y - right.Y));
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
            return left.X != right.X || left.Y != right.Y;
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
}
