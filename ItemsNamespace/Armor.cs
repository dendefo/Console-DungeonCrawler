namespace First_Semester_Project.ItemsNamespace
{
    internal class Armor : Item
    {
        public int Defence { get; private set; }
        public Armor(string name, int defence)
        {
            Name = name;
            Type = ItemTypes.Armor;
            Defence = defence;
        }
    }
}
