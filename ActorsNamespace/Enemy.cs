namespace First_Semester_Project.ActorsNamespace
{

    //Class that represents Enemy
    internal class Enemy : Unit
    {
        //List of Pathfinding walkable tiles
        public static List<SquareTypes> ToPlayer = new List<SquareTypes> { SquareTypes.Empty, SquareTypes.Player, SquareTypes.Coin };
        public static int Difficulty = 3;

        private Coordinates spawnCoord;
        private Coordinates TargetCoords = new(0, 0);

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
        public override void Die(Map map)
        {
            ActorsSquare.MakeEmpty();
            base.Die(map);
        }

        public static void EnemiesMoving(Map map, Player player)
        {

            for (int i = 0; i < map.Enemies.Count; i++)
            {

                Enemy enemy = map.Enemies[i];
                if (enemy == null) continue; //if there is no enemy
                if (enemy.CurrentHP == 0) continue; //if enemy died

                if (!Physics.Raycast(map, enemy.Coor, player.Coor, Map.NotVisibleThrow, 6)) //If enemy doesn't sees the player
                {
                    if (enemy.TargetCoords == new Coordinates(0, 0)) continue; //Then no target
                }
                else enemy.TargetCoords = player.Coor; //Else Player's coords is target

                if (enemy.Coor == enemy.TargetCoords) { enemy.TargetCoords = enemy.spawnCoord; } //If stands on target then new target is spawnPoint
                if (enemy.TargetCoords == enemy.spawnCoord && enemy.TargetCoords == enemy.Coor) { enemy.TargetCoords = new Coordinates(0, 0); } //If stands on spawn point and has no target


                //I burned 5 hours to understand and make this algorithm, used pseudo code from one site as reference and tryed to implement it for 2 hours
                List<Node> path = Node.BuildPath(enemy.Pathfinder(map, enemy.TargetCoords, ToPlayer));
                if (path == null) { enemy.TargetCoords = new(); continue; }
                Physics.CollisionCheck(map, path[path.Count - 2].Coor - enemy.Coor, enemy);
            }
        }

    }
}
