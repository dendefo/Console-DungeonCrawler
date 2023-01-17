namespace First_Semester_Project.ActorsNamespace
{

    internal class Spike : Actor
    {
        public bool Direction { get; private set; } // true is for "Up" or "Right", false is for "Down" or "Left"
        public bool DimentionOfMoving { get; private set; } //true is for Vertical, false is for Horizontal
        public Spike(Coordinates coor, bool dimention, Square actorSquare) : base(coor)
        {
            DimentionOfMoving = dimention;
            ActorsSquare = actorSquare;
            StandsOn = new Square(SquareTypes.Empty, coor);
        }
        public void ChangeDirection()
        {
            Direction = !Direction;
        }
        public static void SpykeMoving(Map level)
        {
            foreach (Square spyke in level.Spikes)
            {
                bool axis = ((Spike)spyke.ActorOnSquare).DimentionOfMoving;
                bool direction = ((Spike)spyke.ActorOnSquare).Direction;
                CollisionLogic.CollisionCheck(level, new(axis? direction ? Directions.Up:Directions.Down: direction ? Directions.Right:Directions.Left), spyke.ActorOnSquare);
            }
        }
    }
}
