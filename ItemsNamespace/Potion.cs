namespace First_Semester_Project.ItemsNamespace
{
    internal class Potion : Item
    {
        public PotionTypes PotionType { get; private set; }
        public int Heal { get; private set; }
        public int Damage { get; private set; }
        public Potion(PotionTypes type)
        {
            PotionType = type;
            Type = ItemTypes.Potion;
            switch (type)
            {
                case PotionTypes.SmallHealingPotion:
                    Heal = 3;
                    Name = "Small Healing Potion";
                    break;
                case PotionTypes.HealingPotion:
                    Heal = 5;
                    Name = "Healing Potion";
                    break;
                case PotionTypes.GreatHealingPotion:
                    Heal = 8;
                    Name = "Great Healing Potion";
                    break;
                case PotionTypes.ExplosivePotion:
                    Damage = 10;
                    Name = "Exploading Potion";
                    break;
            }
        }
    }
}
