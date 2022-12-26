namespace First_Semester_Project.ActorsNamespace
{
    //Class that represents Player
    internal class Player : Actor
    {
        //Players Inventory in format <Item-object, amount>
        public Dictionary<Item, int> Inventory { get; protected set; }

        public int Exp { get; private set; }
        public int KillCount { get; private set; }
        public int Level { get; private set; }

        //Constructor
        public Player(int xCoordinate, int yCoordinate, Square square) : base(xCoordinate, yCoordinate)
        {
            MaxHP = 10; //Player's start HP is 10
            CurrentHP = MaxHP;
            Level = 1;

            ActorsSquare = square;
            EquipedWeapon = new Weapon(WeaponTypes.Fists);
            EquipedShield = new Shield(ShieldTypes.Abs);
            Evasion = 10;
            Inventory = new();
        }

        //Only for "Deep copying" purposes
        public Player(int xCoordinate, int yCoordinate, Player player) : base(xCoordinate, yCoordinate)
        {
            XCoordinate = player.XCoordinate;
            YCoordinate = player.YCoordinate;
            MaxHP = player.MaxHP;
            CurrentHP = player.CurrentHP;
            Evasion = player.Evasion;
            StandsOn = player.StandsOn;
            ActorsSquare = player.ActorsSquare;
            EquipedWeapon = player.EquipedWeapon;
            EquipedShield = player.EquipedShield;
            Inventory = new Dictionary<Item, int>(player.Inventory);
        }

        //Adding "item" to the Inventory
        public void GiveItem(Item item)
        {
            List<Item> keys = Inventory.Keys.ToList(); //Checking each item in Inventory
            foreach (Item key in keys)
            {
                if (key.Name == item.Name) //If it has same name as the item we need to give
                {
                    Inventory[key]++; //Then add +1 to amount

                    return; //Exit
                }

            }
            Inventory.Add(item, 1); //If no sutable item in inventory, Add new 
        }

        //Substract "item" from Inventory
        private void TakeItem(Item item, int amount)
        {
            Inventory[item] -= amount;
            if (Inventory[item] <= 0)
            {
                Inventory.Remove(item);
            }
        }

        //Healing Player and check for it not to be more than MaxHp
        public void Heal(int heal)
        {
            CurrentHP += heal;
            if (CurrentHP > MaxHP) CurrentHP = MaxHP;
            Task.Run(SoundEffects.Heal);

        }

        //This Method is a huge If, but it works just fine.
        public void FullHeal()
        {
            Potion small = null, medium = null, big = null;
            int toHeal = MaxHP - CurrentHP;
            foreach (Item item in Inventory.Keys)
            {
                if (item.Name == "Small Healing Potion") small = (Potion)item;
                if (item.Name == "Healing Potion") medium = (Potion)item;
                if (item.Name == "Great Healing Potion") big = (Potion)item;
            }
            int potionAmount;
            if (big != null)
            {
                if (big.Heal * Inventory[big] >= toHeal)
                {
                    potionAmount = toHeal / big.Heal;
                    Heal(potionAmount * big.Heal);
                    TakeItem(big, potionAmount);
                    toHeal -= potionAmount * big.Heal;
                }
                else
                {
                    toHeal -= big.Heal * Inventory[big];
                    Heal(Inventory[big] * big.Heal);
                    TakeItem(big, Inventory[big]);
                }
            }
            if (medium != null)
            {
                if (medium.Heal * Inventory[medium] >= toHeal)
                {
                    potionAmount = toHeal / medium.Heal;
                    Heal(potionAmount * medium.Heal);
                    TakeItem(medium, potionAmount);
                    toHeal -= potionAmount * medium.Heal;
                }
                else
                {
                    toHeal -= medium.Heal * Inventory[medium];
                    Heal(Inventory[medium] * medium.Heal);
                    TakeItem(medium, Inventory[medium]);

                }
            }
            if (small != null)
            {
                if (small.Heal * Inventory[small] >= toHeal)
                {
                    potionAmount = toHeal / small.Heal + 1;
                    Heal(potionAmount * small.Heal);
                    TakeItem(small, potionAmount);

                    toHeal -= potionAmount * small.Heal;
                }
                else
                {
                    toHeal -= small.Heal * Inventory[small];
                    Heal(Inventory[small] * small.Heal);
                    TakeItem(small, Inventory[small]);

                }
            }
        }

        //Using item by the player
        public void Use(int number, Data log, Map level)
        {
            List<Item> keys = Inventory.Keys.ToList();
            if (number >= keys.Count) return;
            switch (keys[number].Type) //Defines by item type
            {
                case ItemTypes.Weapon: //Changing Weapon
                    Weapon weapon = EquipedWeapon;
                    EquipedWeapon = (Weapon)keys[number];
                    TakeItem(keys[number], 1);

                    if (weapon.Name != "Fists") GiveItem(weapon);

                    log.action = $"You equiped {EquipedWeapon.Name}";
                    break;

                case ItemTypes.Shield: //Changing shield 
                    Shield shield = EquipedShield;
                    EquipedShield = (Shield)keys[number];
                    TakeItem(keys[number], 1);

                    if (shield.Name != "Abs") GiveItem(shield);
                    log.action = $"You equiped {EquipedShield.Name}";
                    break;

                case ItemTypes.Potion: //Using potions

                    switch (((Potion)keys[number]).PotionType)
                    {
                        case PotionTypes.SmallHealingPotion:
                        case PotionTypes.HealingPotion:
                        case PotionTypes.GreatHealingPotion: //All three potions has the same logic, but different amount of healing
                            Heal(((Potion)keys[number]).Heal);
                            log.action = $"You used {keys[number].Name} and healed {((Potion)keys[number]).Heal} Health Points";
                            break;
                        case PotionTypes.ExplosivePotion:
                            int overAllDamage = 0;
                            foreach (Enemy enemy in level.Enemies)
                            {
                                if (enemy == null) continue;
                                if (enemy.CurrentHP == 0) continue;
                                if (Math.Abs(enemy.XCoordinate - XCoordinate) < 3 && Math.Abs(enemy.YCoordinate - YCoordinate) < 3)
                                {
                                    enemy.DealDamage(((Potion)keys[number]).Damage);
                                    overAllDamage += ((Potion)keys[number]).Damage;
                                    if (enemy.CurrentHP == 0)
                                    {
                                        Killed(enemy);
                                        level.ChangeSquare(new Square(SquareTypes.Chest, enemy.XCoordinate, enemy.YCoordinate, enemy.Die()), enemy.YCoordinate, enemy.XCoordinate);


                                    }
                                }
                            }

                            int countWalls =0;
                            foreach (int y in new List<int> { -1, 0, 1 })
                            {
                                foreach (int x in new List<int> { -1, 0, 1 })
                                {
                                    if (y == 0 && x == 0) continue;
                                    
                                    if (level.MapArray[YCoordinate + y][XCoordinate + x].Entity == SquareTypes.CrackedWall)
                                    {
                                        level.MapArray[YCoordinate + y][XCoordinate + x] = new Square(SquareTypes.Empty,XCoordinate+x,YCoordinate+y);
                                        countWalls++;
                                    }
                                }
                            }
                            log.action = $"There was an explosion! {overAllDamage} Damage dealed to enemies, {countWalls} wall destoyed";
                            break;
                    }
                    TakeItem(keys[number], 1);
                    break;

            }
        }

        public void Killed(Enemy enemy)
        {
            Exp += enemy.MaxHP / Enemy.Difficulty;
            KillCount++;
            if (Exp >= 5 * Level)
            {
                LevelUp();
            }
        }
        private void LevelUp()
        {
            Level++;
            Exp %= 5;
            MaxHP += 3;
            CurrentHP = MaxHP;
        }
    }
}