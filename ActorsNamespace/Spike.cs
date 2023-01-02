using System.Diagnostics;

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
    }
}
