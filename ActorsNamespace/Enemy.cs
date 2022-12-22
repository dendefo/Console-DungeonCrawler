
namespace First_Semester_Project.ActorsNamespace
{

    //Class that represents Enemy
    internal class Enemy : Actor
    {
        //if player was at range at least once = true
        bool isTriggerd = false;

        //Enemies "Id number"
        public int NumberInArray { get; protected set; }

        //Basic constructor
        public Enemy(int xCoordinate, int yCoordinate, int level, int numberInArray, Square square) : base(xCoordinate, yCoordinate)
        {
            MaxHP = level * 3;
            CurrentHP = MaxHP;
            StandsOn = new Square(SquareTypes.Empty, xCoordinate, yCoordinate);
            ActorsSquare = square;
            EquipedWeapon = new Weapon(WeaponTypes.Fists);
            EquipedShield = new Shield(ShieldTypes.Abs);
            Evasion = level * 5;
            NumberInArray = numberInArray;
        }

        //If enemy dies, he dropes chest with loot
        public Item Die()
        {
            ActorsSquare.MakeEmpty();
            if (EquipedWeapon.Name != "Fists")return EquipedWeapon;
            if (EquipedShield.Name != "Abs") return EquipedShield;
            return new Potion(PotionTypes.SmallHealingPotion);
        }

        public static void EnemiesMoving(Map LevelMap, Player player)
        {

            for (int i = 0; i < LevelMap.Enemies.Length; i++)
            {

                Enemy enemy = LevelMap.Enemies[i];
                if (enemy == null) continue; //if there is no enemy
                if (enemy.CurrentHP == 0) continue; //if enemy died
                int xDif = Math.Abs(player.XCoordinate - enemy.XCoordinate); //Distance between player and enemy on X-coordinate
                int yDif = Math.Abs(player.YCoordinate - enemy.YCoordinate); //Distance between player and enemy on Y-coordinate

                if (!(xDif < 5 && yDif < 5) &&!enemy.isTriggerd) continue; //If player isn't in range, but enemy saw him, he will never abandon him no matter what
                enemy.isTriggerd= true; //Start never-ending chase

                //TODO a pathfinder that allows enemy not to stuck in walls and to fin shortest way to the player
                if (xDif >= yDif) 
                {
                    if (player.XCoordinate > enemy.XCoordinate)
                    {
                        LevelMap.Move(enemy, "Right");
                        continue;
                    }
                    else if (player.XCoordinate < enemy.XCoordinate)
                    {
                        LevelMap.Move(enemy, "Left");
                        continue;
                    }
                }
                else
                {
                    if (player.YCoordinate > enemy.YCoordinate)
                    {
                        LevelMap.Move(enemy, "Down");
                        continue;
                    }
                    else if (player.YCoordinate < enemy.YCoordinate)
                    {
                        LevelMap.Move(enemy, "Up");
                        continue;
                    }
                }

            }
        }

        private void Pathfinder(Map level, Enemy enemy, Player player)
        {
            //Node node = new(enemy.XCoordinate,enemy.YCoordinate,level);
            List<Square> reachable = new List<Square>
            {
                level.MapArray[YCoordinate][XCoordinate]
            };
            List<Square> visited = new List<Square>();
            while (reachable.Count != 0)
            {
                Square node = reachable[0];
            }
            //return "Up";
        }
    }


    //class Node
    //{
    //    Node previous;
    //}
    //class Node
    //{
    //    Node[] connectedNodes = new Node[4];
    //    bool isVisited = false;
    //    int x;
    //    int y;
    //    public Node(int x, int y, Map map)
    //    {
    //        this.x = x;
    //        this.y = y;
    //        if (x - 1 > 0) //Check if x is in map boundries
    //        {
    //            if (map.MapArray[y][x].Entity==SquareTypes.Empty) //If next Node is walkable
    //            {

    //                connectedNodes[0] = new Node(x-1, y, map); //Creates new connected Node
    //            }
    //        }
    //    }
    //    public void Connect(Node[] connectedNodes)
    //    {
    //        this.connectedNodes = connectedNodes;
    //    }
    //    public void Visit()
    //    {
    //        isVisited = true;
    //    }
    //}
}
