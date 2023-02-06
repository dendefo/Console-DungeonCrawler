namespace First_Semester_Project.ItemsNamespace
{
    //This is really minimalistic class, but it was easier to make a class for coin, than implement something new instead
    internal class Coin :Item
    {
        public Coin()
        {
            Type = ItemTypes.Coin;
            Name = "Coin";
        }
    }
}
