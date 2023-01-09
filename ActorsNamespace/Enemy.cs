namespace First_Semester_Project.ActorsNamespace
{

    //Class that represents Enemy
    internal class Enemy : Actor
    {
        static public int Difficulty = 3;
        //if player was at range at least once = true
        bool isTriggerd = false;

        //Basic constructor
        public Enemy(Coordinates coor, int level, Square square, Weapon weapon, Shield shield, Item item) : base(coor)
        {
            MaxHP = level * Difficulty;
            CurrentHP = MaxHP;
            StandsOn = new Square(SquareTypes.Empty, coor);
            ActorsSquare = square;
            EquipedWeapon = weapon;
            EquipedShield = shield;
            ItemToDrop = item;
            Evasion = level * 5;
        }

        //If enemy dies, he dropes chest with loot
        public Item Die()
        {
            ActorsSquare.MakeEmpty();
            return ItemToDrop;
            //Random rand = new Random();
            //if (EquipedWeapon.Name != "Fists" && EquipedShield.Name != "Abs")
            //{
            //    switch (rand.Next(0, 3))
            //    {
            //        case 0: return EquipedWeapon;
            //        case 1: return EquipedShield;
            //        case 2: return new Potion(PotionTypes.GreatHealingPotion);
            //    }
            //}
            //else if (EquipedShield.Name != "Abs")
            //{
            //    switch (rand.Next(0, 2))
            //    {
            //        case 0: return EquipedShield;
            //        case 1: return new Potion(PotionTypes.HealingPotion);
            //    }
            //}
            //else if (EquipedWeapon.Name != "Fists")
            //{
            //    switch (rand.Next(0, 2))
            //    {
            //        case 0: return EquipedWeapon;
            //        case 1: return new Potion(PotionTypes.HealingPotion);
            //    }
            //}
            //return new Potion(PotionTypes.SmallHealingPotion);
        }

        public static void EnemiesMoving(Map LevelMap, Player player)
        {

            for (int i = 0; i < LevelMap.Enemies.Count; i++)
            {

                Enemy enemy = LevelMap.Enemies[i];
                if (enemy == null) continue; //if there is no enemy
                if (enemy.CurrentHP == 0) continue; //if enemy died

                if (!(Coordinates.Abs(player.Coor, enemy.Coor) < 6) && !enemy.isTriggerd) continue; //If player isn't in range, but enemy saw him, he will never abandon him no matter what
                enemy.isTriggerd = true; //Start never-ending chase

                //I burned 5 hours to understand and make this algorithm, used pseudo code from one site as reverence and tryed to implement it for 2 hours
                List<Node> path = Node.BuildPath(enemy.Pathfinder(LevelMap, player.Coor));
                if (path ==null) continue;
                CollisionLogic.CollisionCheck(LevelMap, path[path.Count - 2].Coor - enemy.Coor, enemy);
            }
        }

    }
}
