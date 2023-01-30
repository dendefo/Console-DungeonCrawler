namespace First_Semester_Project.ActorsNamespace
{
    //Class that represents Chests
    internal class Chest : Actor
    {
        //Basic constructor
        public Chest(Coordinates coor) : base(coor) { }

        public void PutInside(Item inside)
        {
            ItemToDrop = inside;
        }

        public Item Open()
        {
            Item temp = ItemToDrop;
            ItemToDrop = null;
            return temp;
        }
    }
}
