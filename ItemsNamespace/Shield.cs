namespace First_Semester_Project.ItemsNamespace
{
    //Shields
    internal class Shield : Item
    {
        public int Block { get; private set; }
        public int BlockChance { get; private set; }
        public Shield(ShieldTypes shield)
        {
            Type = ItemTypes.Shield;
            switch (shield)
            {
                case ShieldTypes.Abs:
                    Name = "Abs";
                    Block = 1;
                    BlockChance = 25;
                    break;
                case ShieldTypes.Buckler:
                    Name = "Buckler";
                    Block = 2;
                    BlockChance = 30;
                    break;
                case ShieldTypes.Robust:
                    Name = "Robust Shield";
                    Block = 3;
                    BlockChance = 40;
                    break;
                case ShieldTypes.Kite:
                    Name = "Kite Shield";
                    Block = 5;
                    BlockChance = 30;
                    break;
            }//Describing each Weapon by its characteristics
        }
    }
}
