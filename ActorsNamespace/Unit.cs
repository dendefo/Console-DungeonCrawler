namespace First_Semester_Project.ActorsNamespace
{
    internal class Unit :Actor
    {
        public Weapon EquipedWeapon { get; protected set; }
        public Shield EquipedShield { get; protected set; }


        public int MaxHP { get; protected set; }
        public int CurrentHP { get; protected set; }
        protected int Evasion { get; set; }

        public Unit(Coordinates coor) : base(coor) { }


        //Damage-Dealing system
        public int Attack(Unit defender)
        {
            Random rand = new Random();

            if (rand.Next(0, 101) > EquipedWeapon.HitChance) return 0; //Checking if Attack is Unsuccesfull
            else return defender.BlockCheck(EquipedWeapon.Damage); // If Succesfull
        } //Checking for Attack Chance
        private int BlockCheck(int dmg)
        {
            Random rand = new Random();
            if (rand.Next(0, 101) < EquipedShield.BlockChance) //Checking if damage is blocked by shield
            {
                if (dmg - EquipedShield.Block <= 0) return 0; //if damage after blocking is less then zero, then no damage
                return GetHit(dmg - EquipedShield.Block); //if damage grater than zero, then:
            }
            else return GetHit(dmg); //If not blocked
        } //Checking for block Chance
        private int GetHit(int dmg)
        {
            Random rand = new Random();
            if (rand.Next(0, 101) < Evasion) return 0; //If Evaded
            int temp = CurrentHP;

            if (CurrentHP <= dmg) CurrentHP = 0;
            else DealDamage(dmg); //Dealing Damage

            return temp - CurrentHP;
        } //Checking for Evading
        public void DealDamage(int damage)
        {
            CurrentHP -= damage;
            if (CurrentHP < 0) CurrentHP = 0;
        } //Deal pure damage

        public static void Battle(Map map, Enemy enemy, Coordinates coor, bool isPlayerAttacked)
        {

            if (isPlayerAttacked)
            {
                int userD;
                if (map.User.CurrentEffect == EffectType.Accuracy)
                {
                    userD = map.User.EquipedWeapon.Damage;
                    enemy.DealDamage(userD);
                }
                else
                {
                    userD = map.User.Attack(enemy);
                }
                if (enemy.CurrentHP == 0)
                {
                    map.User.Killed(enemy);
                    map[coor] = new Square(SquareTypes.Chest, coor, enemy.Die());
                    map.Log.GreenAction = $"Enemy died and dropped chest with {((Chest)map[coor].ActorOnSquare).ItemToDrop.Name} for you";
                    return;
                }
                map.Log.GreenAction = $"You dealed {userD} damage to the enemy. Now his HP is {enemy.CurrentHP}";
                return;
            }
            map.Log.Damage += enemy.Attack(map.User);
        }

        /// <summary>
        /// Pathfinding algorithm
        /// </summary>
        /// <param name="level"> Map of current level</param>
        /// <param name="target"> Target on the map</param>
        public Node Pathfinder(Map level, Coordinates target)
        {

            List<Node> reachable = new()
            {
                new Node(Coor,null,Coordinates.Distance(Coor,target))
            }; //Adding List of Nodes that will be used in algorithm as Nodes that actor is possibly can get to and adding starting node (Of Actor)

            List<Node> visited = new List<Node>(); //List of Nodes that we already checked

            while (reachable.Count != 0) //If reachable is 0 and algothm didn't made return, enemy can't reach the player
            {
                Node node = reachable[0];
                foreach (Node node1 in reachable)
                {
                    if (node1.Distance < node.Distance) node = node1; //Taking Node with less distance to the target
                }

                if (node.Coor == target) //If node's coor-s are same to player's, than we found a path and it's the shortest one
                {
                    return node;
                    //node = Node.BuildPath(node); //Algorith that gives us next node to move to
                    //CollisionLogic.CollisionCheck(level, node.Coor - Coor, this); //Moving the enemy
                }

                visited.Add(node); //Already been there, don't need to check nodes twice
                reachable.RemoveAt(0);

                SquareTypes entity;
                foreach (int y in new List<int> { -1, 0, 1 }) //For y axis
                {
                    foreach (int x in new List<int> { -1, 0, 1 }) //For X axis
                    {
                        Coordinates newCoords = new(x, y);
                        newCoords += node.Coor;
                        if (y * y == x * x || visited.Exists(n => n.Coor == newCoords)) continue; //If it is diagonal or 0,0 OR if it is already been checked
                        //It leaves only four directions (0,1)(0,-1)(1,0)(-1,0). No diagonal moving

                        entity = level[newCoords].Entity; //Looking what's on the Square


                        if (!(entity == SquareTypes.Empty || entity == SquareTypes.Player || entity == SquareTypes.Coin || entity == SquareTypes.Exit)) continue; //If it's not walkable, then continue

                        Node adjacent = new(newCoords, node, Coordinates.Distance(newCoords, target)); //Create new node to check
                        if (reachable.Exists(n => n.Coor == adjacent.Coor)) continue; //If it's already awaits for check then continue

                        reachable.Add(adjacent); // Add to List
                    }
                }
            }
            return null; //There is no path
        }
    }
    class Node //Representation of map as tree of walkable Nodes, that starts at Actor's node. We search for Target node at that tree
    {
        public Coordinates Coor; //Coordinates of Node
        public Node previous; //Node that linked us to this one. 
        public float Distance; 
        public Node(Coordinates coor, Node node, float distance)
        {
            previous = node;
            Coor = coor;
            Distance = distance;
        }
        static public List<Node> BuildPath(Node to_node) //Gives node that is next to enemy, that he need to move to
        {
            if (to_node == null) return null; //If there is no path
            List<Node> path = new();
            while (to_node != null)
            {
                path.Add(to_node);
                to_node = to_node.previous;
            } //Making a list of nodes that starts at player Node and ends on Enemy node. It is a shortest way
            return path;
        }
    }
}
