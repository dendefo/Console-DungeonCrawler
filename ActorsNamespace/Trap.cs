namespace First_Semester_Project.ActorsNamespace
{
    //Class that represents all types of traps
    internal class Trap : Actor
    {
        //Defines the type of the trap
        public SquareTypes TrapType { get; private set; }

        //Basic constructor
        public Trap(int xCoordinate, int yCoordinate, SquareTypes type, Square square) : base(xCoordinate, yCoordinate)
        {
            StandsOn = square; //Trap is hided, so user will see Empty square
            ActorsSquare = new Square(SquareTypes.RevealedTrap, xCoordinate, yCoordinate);
            TrapType = type;
        }
    }
}
