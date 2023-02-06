namespace First_Semester_Project.ActorsNamespace
{

    internal class Spike : Actor
    {
        private bool _direction { get; set; } // true is for "Up" or "Right", false is for "Down" or "Left"
        private bool _dimentionOfMoving { get;  set; } //true is for Vertical, false is for Horizontal
        public Spike(Coordinates coor, bool dimention, Square actorSquare) : base(coor)
        {
            _dimentionOfMoving = dimention;
            ActorsSquare = actorSquare;
            StandsOn = new Square(SquareTypes.Empty, coor);
        }
        public void ChangeDirection()
        {
            _direction = !_direction;
        }
        public static void SpykeMoving(Map map)
        {
            foreach (Square spyke in map.Spikes)
            {
                bool axis = ((Spike)spyke.ActorOnSquare)._dimentionOfMoving;
                bool direction = ((Spike)spyke.ActorOnSquare)._direction;
                Physics.CollisionCheck(map, (axis? direction ? Coordinates.Up:Coordinates.Down: direction ? Coordinates.Right:Coordinates.Left), spyke.ActorOnSquare);
            }
        }
    }
}
