namespace First_Semester_Project.ItemsNamespace
{
    internal class Item
    {
        public ItemTypes Type { get; protected set; }
        public string Name { get; protected set; }


        public static Item ItemParse(int itemInt)
        {

            switch (itemInt)
            {
                case 43:
                    return new Coin();
                case < 60:
                    return new Weapon((WeaponTypes)itemInt);
                case < 70:
                    return new Shield((ShieldTypes)itemInt);
                case >= 70:
                    return new Potion((PotionTypes)itemInt);
            }
        }
    }
}
