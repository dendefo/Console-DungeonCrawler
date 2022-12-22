namespace First_Semester_Project.ActorsNamespace
{
    //Class that represents Chests
    internal class Chest : Actor
    {
        //Item inside the Chest
        public Item Inside { get; private set; }

        //Basic constructor
        public Chest(int xCoordinate, int yCoordinate) : base(xCoordinate, yCoordinate)
        {
        }

        public void PutInside(Item inside)
        {
            Inside = inside;
        }

        public Item Open()
        {
            Item temp = Inside;
            Inside = null;
            return temp;
        }
    }
}
