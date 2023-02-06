namespace First_Semester_Project.ItemsNamespace
{
    //Do i have to explain this?
    internal abstract class Item
    {
        public ItemTypes Type { get; protected set; }
        public string Name { get; protected set; }

        //Create item by name
        public static Item NameParser(string name)
        {
            switch (name)
            {
                case "Fists":
                    return new Weapon(WeaponTypes.Fists);
                case "Sword":
                    return new Weapon(WeaponTypes.Sword);
                case "Axe":
                    return new Weapon(WeaponTypes.Axe);
                case "Nunchucks":
                    return new Weapon(WeaponTypes.Nunchucks);

                case "Abs":
                    return new Shield(ShieldTypes.Abs);
                case "Buckler":
                    return new Shield(ShieldTypes.Buckler);
                case "Robust Shield":
                    return new Shield(ShieldTypes.Robust);
                case "Kite Shield":
                    return new Shield(ShieldTypes.Kite);

                case "Small Healing Potion":
                    return new Potion(PotionTypes.SmallHealingPotion);
                case "Healing Potion":
                    return new Potion(PotionTypes.HealingPotion);
                case "Great Healing Potion":
                    return new Potion(PotionTypes.GreatHealingPotion);
                case "Exploasive Potion":
                    return new Potion(PotionTypes.ExplosivePotion);

                case "Invisibility Potion":
                    return new Potion(PotionTypes.InvisibilityPotion);
                case "Hawk Eye Potion":
                    return new Potion(PotionTypes.HawkEyePotion);
                case "Potion of Accuracy":
                    return new Potion(PotionTypes.AccuracyPotion);
                case "Potion of Invincibility": 
                    return new Potion(PotionTypes.InvulnerabilityPotion);

                default: return new Potion(PotionTypes.SmallHealingPotion);
            }
        }

        //Create item by number
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
