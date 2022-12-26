namespace First_Semester_Project.ActorsNamespace
{

    //Class that represents Enemy
    internal class Enemy : Actor
    {
        static public int Difficulty = 3;
        //if player was at range at least once = true
        bool isTriggerd = false;

        //Basic constructor
        public Enemy(int xCoordinate, int yCoordinate, int level, Square square, Weapon weapon, Shield shield) : base(xCoordinate, yCoordinate)
        {
            MaxHP = level * Difficulty;
            CurrentHP = MaxHP;
            StandsOn = new Square(SquareTypes.Empty, xCoordinate, yCoordinate);
            ActorsSquare = square;
            EquipedWeapon = weapon;
            EquipedShield = shield;
            Evasion = level * 5;
        }

        //If enemy dies, he dropes chest with loot
        public Item Die()
        {
            ActorsSquare.MakeEmpty();
            Random rand = new Random();
            if (EquipedWeapon.Name != "Fists" && EquipedShield.Name != "Abs")
            {
                switch (rand.Next(0, 3))
                {
                    case 0: return EquipedWeapon;
                    case 1: return EquipedShield;
                    case 2: return new Potion(PotionTypes.GreatHealingPotion);
                }
            }
            else if (EquipedShield.Name != "Abs")
            {
                switch (rand.Next(0, 2))
                {
                    case 0: return EquipedShield;
                    case 1: return new Potion(PotionTypes.HealingPotion);
                }
            }
            else if (EquipedWeapon.Name != "Fists")
            {
                switch (rand.Next(0, 2))
                {
                    case 0: return EquipedWeapon;
                    case 1: return new Potion(PotionTypes.HealingPotion);
                }
            }
            return new Potion(PotionTypes.SmallHealingPotion);
        }

        public static void EnemiesMoving(Map LevelMap, Player player)
        {

            for (int i = 0; i < LevelMap.Enemies.Count; i++)
            {

                Enemy enemy = LevelMap.Enemies[i];
                if (enemy == null) continue; //if there is no enemy
                if (enemy.CurrentHP == 0) continue; //if enemy died
                int xDif = Math.Abs(player.XCoordinate - enemy.XCoordinate); //Distance between player and enemy on X-coordinate
                int yDif = Math.Abs(player.YCoordinate - enemy.YCoordinate); //Distance between player and enemy on Y-coordinate

                if (!(xDif < 5 && yDif < 5) && !enemy.isTriggerd) continue; //If player isn't in range, but enemy saw him, he will never abandon him no matter what
                enemy.isTriggerd = true; //Start never-ending chase

                //I burned 5 hours to understand and make this algorithm, used pseudo code from one site as reverence and tryed to implement it for 2 hours
                enemy.Pathfinder(LevelMap, player);
            }
        }

        private void Pathfinder(Map level, Player player)
        {

            List<Node> reachable = new()
            {
                new Node(XCoordinate,YCoordinate,null)
            }; //Adding List of Nodes that will be used in algorithm as Nodes that enemy is possibly can get to and addint starting node (Of Enemy)

            List<Node> visited = new List<Node>(); //List of Nodes that we already checked

            while (reachable.Count != 0) //If reachable is 0 and algothm didn't made return, enemy can't reach the player
            {
                Node node = reachable[0]; //Taking first Node of list to check

                if (node.X == player.XCoordinate && node.Y == player.YCoordinate) //If node's coor-s are same to player's, than we found a path and it's the shortest one
                {
                    node = Node.BuildPath(node); //Algorith that gives us next node to move to
                    CollisionLogic.Collision(level,node.X - XCoordinate, node.Y - YCoordinate, this); //Moving the enemy
                    return;
                }

                visited.Add(node); //Already been there, don't need to check nodes twice
                reachable.RemoveAt(0);

                SquareTypes entity;
                foreach (int y in new List<int> { -1, 0, 1 }) //For y axis
                {
                    foreach (int x in new List<int> { -1, 0, 1 }) //For X axis
                    {
                        if (y * y == x * x || visited.Exists(n => n.X == node.X + x && n.Y == node.Y + y)) continue; //If it is diagonal or 0,0 OR if it is already been checked
                        //It leaves only four directions (0,1)(0,-1)(1,0)(-1,0). No diagonal moving

                        entity = level.MapArray[node.Y + y][node.X + x].Entity; //Looking what's on the Square
                        if (!(entity == SquareTypes.Empty || entity == SquareTypes.Player || entity == SquareTypes.Enemy)) continue; //If it's not walkable, then continue

                        Node adjacent = new Node(node.X + x, node.Y + y, node); //Create new node to check
                        if (reachable.Exists(n => n.X == adjacent.X && n.Y == adjacent.Y)) continue; //If it's already awaits for check then continue

                        reachable.Add(adjacent); // Add to List
                    }
                }
            }
        }
    }
    class Node //Representation of map as tree of walkable for Enemy Nodes, that starts at Enemy's node. We search for Player's node at that tree
    {
        public int X, Y; //Coordinates of Node
        public Node previous; //Node that linked us to this one. 
        public Node(int x, int y, Node node)
        {
            previous = node;
            X = x;
            Y = y;
        }
        static public Node BuildPath(Node to_node) //Gives node that is next to enemy, that he need to move to
        {
            List<Node> path = new();
            while (to_node != null)
            {
                path.Add(to_node);
                to_node = to_node.previous;
            } //Making a list of nodes that starts at player Node and ends on Enemy node. It is a shortest way
            return path[path.Count - 2];//We need to get node that stands before last one
        }
    }
}
