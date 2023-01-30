namespace First_Semester_Project.ActorsNamespace
{

    //Class that represents Enemy
    internal class Enemy : Unit
    {
        static public int Difficulty = 3;
        private Coordinates spawnCoord;
        private Coordinates TargetCoords = new(0,0);
        //if player was at range at least once = true

        //Basic constructor
        public Enemy(Coordinates coor, int level, Square square, Weapon weapon, Shield shield, Item item) : base(coor)
        {
            spawnCoord = coor;
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
            
        }

        public static void EnemiesMoving(Map LevelMap, Player player)
        {

            for (int i = 0; i < LevelMap.Enemies.Count; i++)
            {

                Enemy enemy = LevelMap.Enemies[i];
                if (enemy == null) continue; //if there is no enemy
                if (enemy.CurrentHP == 0) continue; //if enemy died

                if (!Physics.Raycast(LevelMap, enemy.Coor, player.Coor, Map.notVisibleThrow, 6)) //If enemy sees the player
                {
                    if (enemy.TargetCoords == new Coordinates(0, 0)) continue;
                }
                else enemy.TargetCoords = player.Coor;
                if (enemy.Coor == enemy.TargetCoords) { enemy.TargetCoords = enemy.spawnCoord; }
                if(enemy.TargetCoords == enemy.spawnCoord&& enemy.TargetCoords == enemy.Coor) { enemy.TargetCoords = new Coordinates(0, 0);}
                //I burned 5 hours to understand and make this algorithm, used pseudo code from one site as reverence and tryed to implement it for 2 hours
                List<Node> path = Node.BuildPath(enemy.Pathfinder(LevelMap, enemy.TargetCoords));
                if (path == null) { enemy.TargetCoords = new(); continue; }
                Physics.CollisionCheck(LevelMap, path[path.Count - 2].Coor - enemy.Coor, enemy);
            }
        }

    }
}
