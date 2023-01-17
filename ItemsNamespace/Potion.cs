namespace First_Semester_Project.ItemsNamespace
{
    internal class Potion : Item
    {
        public PotionTypes PotionType { get; private set; }
        public int Heal { get; private set; }
        public int Damage { get; private set; }

        public EffectType Effect { get; private set; }
        public int Turns { get; private set; }
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
                    Name = "Exploasive Potion";
                    break;
                case PotionTypes.InvisibilityPotion:
                    Effect = EffectType.Invisibl;
                    Turns = 5;
                    Name = "Invisibility Potion";
                    break;
                case PotionTypes.HawkEyePotion:
                    Effect = EffectType.HawkEye;
                    Turns = 3;
                    Name = "Hawk Eye Potion";
                    break;
                case PotionTypes.AccuracyPotion:
                    Effect = EffectType.Accuracy;
                    Turns = 4;
                    Name = "Potion of Accuracy";
                    break;
                case PotionTypes.InvulnerabilityPotion:
                    Effect = EffectType.Invulnerbl;
                    Turns = 5;
                    Name = "Potion of Invincibility";
                    break;
            }
        }
    }
}
