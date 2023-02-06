namespace First_Semester_Project.ActorsNamespace
{
    //Class that represents all types of traps
    internal class Trap : Actor
    {
        //Defines the type of the trap
        public SquareTypes TrapType { get; private set; } //Didn't implemented it, but let it be here

        //Basic constructor
        public Trap(Coordinates coor, SquareTypes type, Square square) : base(coor)
        {
            StandsOn = square; //Trap is hiden, so user will see Empty square
            ActorsSquare = new Square(SquareTypes.RevealedTrap, coor);
            TrapType = type;
        }
    }
}
