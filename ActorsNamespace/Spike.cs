using System.Diagnostics;

namespace First_Semester_Project.ActorsNamespace
{

    internal class Spike : Actor
    {
        public bool VerticalDirection { get; private set; } // true is for "Up", false is for "Down"
        public bool HorizontalDirection { get; private set; } //true is for "Right", false is for "Left"
        public bool DimentionOfMoving { get; private set; } //true is for Vertical, false is for Horizontal
        public Spike(int xCoordinate, int yCoordinate, bool verticalDirection, bool horizontalDirection,bool dimention, Square actorSquare) : base(xCoordinate, yCoordinate)
        {
            VerticalDirection = verticalDirection;
            HorizontalDirection = horizontalDirection;
            DimentionOfMoving = dimention;
            ActorsSquare = actorSquare;
            StandsOn = new Square(SquareTypes.Empty, xCoordinate, yCoordinate);
        }
        public void ChangeDirection(int x, int y)
        {
            if (y == -1) VerticalDirection = false;
            else VerticalDirection = true;
            if (x == 1) HorizontalDirection = false;
            else HorizontalDirection = true;
        }
    }
}
