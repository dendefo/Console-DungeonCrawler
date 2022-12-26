using System.Diagnostics;

namespace First_Semester_Project.ActorsNamespace
{

    internal class Spike : Actor
    {
        public bool Direction { get; private set; } // true is for "Up" or "Right", false is for "Down" or "Left"
        public bool DimentionOfMoving { get; private set; } //true is for Vertical, false is for Horizontal
        public Spike(int xCoordinate, int yCoordinate, bool dimention, Square actorSquare) : base(xCoordinate, yCoordinate)
        {
            DimentionOfMoving = dimention;
            ActorsSquare = actorSquare;
            StandsOn = new Square(SquareTypes.Empty, xCoordinate, yCoordinate);
        }
        public void ChangeDirection(int x, int y)
        {
            Direction = !Direction;
        }
    }
}
