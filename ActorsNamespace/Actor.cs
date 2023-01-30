namespace First_Semester_Project.ActorsNamespace
{
    //Anything interactable is called Actor.
    //All the Actor Childs are represented in this namespace
    internal class Actor
    {
        // Square that the Actor is standing on (Not showed)
        public Square StandsOn { get; set; }
        //Square of the Actor (Showed)
        public Square ActorsSquare { get; protected set; }
        public Item ItemToDrop { get; protected set; }

        //Coordination System and moving
        public Coordinates Coor { get; protected set; }
        public void Move(Coordinates coor, Square stansdOn)
        {
            Coor += coor;
            StandsOn = stansdOn;
        }


        //Constractor
        public Actor(Coordinates coor)
        {
            Coor = coor;
        }
    }
}
