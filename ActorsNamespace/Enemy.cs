using System.Drawing.Text;

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
            Random rand = new Random();
            switch (rand.Next(0, 101))
            {
                case < 25:
                    EquipedWeapon = new Weapon(WeaponTypes.Fists);
                    EquipedShield = new Shield(ShieldTypes.Abs);
                    break;
                case < 30:
                    EquipedWeapon = new Weapon(WeaponTypes.Sword);
                    EquipedShield = new Shield(ShieldTypes.Abs);
                    break;
                case < 35:
                    EquipedWeapon = new Weapon(WeaponTypes.Fists);
                    EquipedShield = new Shield(ShieldTypes.Buckler);
                    break;
                case < 40:
                    EquipedWeapon = new Weapon(WeaponTypes.Sword);
                    EquipedShield = new Shield(ShieldTypes.Buckler);
                    break;
                case < 45:
                    EquipedWeapon = new Weapon(WeaponTypes.Axe);
                    EquipedShield = new Shield(ShieldTypes.Abs);
                    break;
                case < 50:
                    EquipedWeapon = new Weapon(WeaponTypes.Axe);
                    EquipedShield = new Shield(ShieldTypes.Buckler);
                    break;
                case < 55:
                    EquipedWeapon = new Weapon(WeaponTypes.Axe);
                    EquipedShield = new Shield(ShieldTypes.Robust);
                    break;
                case < 60:
                    EquipedWeapon = new Weapon(WeaponTypes.Sword);
                    EquipedShield = new Shield(ShieldTypes.Robust);
                    break;
                case < 65:
                    EquipedWeapon = new Weapon(WeaponTypes.Fists);
                    EquipedShield = new Shield(ShieldTypes.Robust);
                    break;
                case < 70:
                    EquipedWeapon = new Weapon(WeaponTypes.Nunchucks);
                    EquipedShield = new Shield(ShieldTypes.Abs);
                    break;
                case < 75:
                    EquipedWeapon = new Weapon(WeaponTypes.Nunchucks);
                    EquipedShield = new Shield(ShieldTypes.Buckler);
                    break;
                case < 80:
                    EquipedWeapon = new Weapon(WeaponTypes.Nunchucks);
                    EquipedShield = new Shield(ShieldTypes.Robust);
                    break;
                case < 85:
                    EquipedWeapon = new Weapon(WeaponTypes.Fists);
                    EquipedShield = new Shield(ShieldTypes.Kite);
                    break;
                case < 90:
                    EquipedWeapon = new Weapon(WeaponTypes.Sword);
                    EquipedShield = new Shield(ShieldTypes.Kite);
                    break;
                case < 95:
                    EquipedWeapon = new Weapon(WeaponTypes.Axe);
                    EquipedShield = new Shield(ShieldTypes.Kite);
                    break;
                case <= 100:
                    EquipedWeapon = new Weapon(WeaponTypes.Nunchucks);
                    EquipedShield = new Shield(ShieldTypes.Kite);
                    break;
            }
            Evasion = level * 5;
            NumberInArray = numberInArray;
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

            for (int i = 0; i < LevelMap.Enemies.Length; i++)
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

            List<Node> reachable = new List<Node>
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
                    level.ChoseCollision(node.X - XCoordinate, node.Y - YCoordinate, this); //Moving the enemy
                    return;
                }

                visited.Add(node); //Already been there, don't need to check nodes twice
                reachable.RemoveAt(0);
                List<Node> new_reachable = new List<Node>(); //Adding from 1 to 4 nodes, that we can reach from thi node

                //Block of four checks (For the four directions). Each block has check if coordinates are exists on map, if this node already been visited and if enemy can actualy walk there

                SquareTypes entity;
                if (node.X > 0 && !visited.Exists(n => n.X == node.X - 1 && n.Y == node.Y))
                {
                    entity = level.MapArray[node.Y][node.X - 1].Entity;

                    if (entity == SquareTypes.Empty || entity == SquareTypes.Player || entity == SquareTypes.Enemy)
                    {
                        new_reachable.Add(new Node(node.X - 1, node.Y, node));
                    }
                }
                if (node.Y > 0 && !visited.Exists(n => n.X == node.X && n.Y == node.Y - 1))
                {
                    entity = level.MapArray[node.Y - 1][node.X].Entity;

                    if (entity == SquareTypes.Empty || entity == SquareTypes.Player || entity == SquareTypes.Enemy)
                    {
                        new_reachable.Add(new Node(node.X, node.Y - 1, node));
                    }
                }
                if (node.X < level.MapArray[node.Y].Length - 1 && !visited.Exists(n => n.X == node.X + 1 && n.Y == node.Y))
                {
                    entity = level.MapArray[node.Y][node.X + 1].Entity;

                    if (entity == SquareTypes.Empty || entity == SquareTypes.Player || entity == SquareTypes.Enemy)
                    {
                        new_reachable.Add(new Node(node.X + 1, node.Y, node));
                    }
                }
                if (node.Y < level.MapArray.Length - 1 && !visited.Exists(n => n.X == node.X && n.Y == node.Y + 1))
                {
                    entity = level.MapArray[node.Y + 1][node.X].Entity;

                    if (entity == SquareTypes.Empty || entity == SquareTypes.Player || entity == SquareTypes.Enemy)
                    {
                        new_reachable.Add(new Node(node.X, node.Y + 1, node));
                    }
                }

                //Adding those nodes to "reachable" if they are not there
                foreach (Node adjacment in new_reachable)
                {
                    if (!reachable.Exists(n => n.X == adjacment.X && n.Y == adjacment.Y)) //Checks ONLY Y and X, without checking "previos" field
                    {
                        reachable.Add(adjacment);
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
