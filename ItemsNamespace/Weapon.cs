namespace First_Semester_Project.ItemsNamespace
{
    internal class Weapon : Item
    {
        //Damage is virtual only because of override in Fang (Weapon of Snake)
        virtual public int Damage { get; private set; }
        public int HitChance { get; protected set; }
        public Weapon(WeaponTypes weapon)
        {
            Type = ItemTypes.Weapon;
            switch (weapon)
            {
                case WeaponTypes.Fists:
                    Name = "Fists";
                    Damage = 1;
                    HitChance = 50;
                    break;
                case WeaponTypes.Sword:
                    Name = "Sword";
                    Damage = 3;
                    HitChance = 90;
                    break;
                case WeaponTypes.Axe:
                    Name = "Axe";
                    Damage = 4;
                    HitChance = 70;
                    break;
                case WeaponTypes.Nunchucks:
                    Name = "Nunchucks";
                    Damage = 6;
                    HitChance = 50;
                    break;
            }//Describing each Weapon by its characteristics
        }
    }
}
