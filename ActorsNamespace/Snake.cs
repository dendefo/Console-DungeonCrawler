namespace First_Semester_Project.ActorsNamespace
{
    //Big enemy
    internal class Snake : Unit
    {
        //Is it head of the snake
        public bool Head;
        public Snake Previous = null; 
        public Coordinates LastPreviosCoords; //Coordinates of "parent" before it's moving
        public Snake Next = null;

        static List<SquareTypes> _toSnake = new List<SquareTypes>() { SquareTypes.Empty, SquareTypes.SnakeHead, SquareTypes.SnakeSegment };
        public Snake(Coordinates coor, int level, Square square, Weapon weapon, Shield shield, Item item, bool head) : base(coor)
        {
            MaxHP = level * Enemy.Difficulty;
            CurrentHP = MaxHP;
            StandsOn = new Square(SquareTypes.Empty, coor);
            ActorsSquare = square;
            EquipedWeapon = new Fang(WeaponTypes.Fists,this) ;
            EquipedShield = shield;
            ItemToDrop = item;
            Evasion = level * 5;
            Head = head;
        }

        //After creating a map, making snakes
        public void FindNext(Map map)
        {
            if (map[Coor + Coordinates.Up].Entity == SquareTypes.SnakeSegment)
            {
                var temp = (Snake)map[Coor + Coordinates.Up].ActorOnSquare;
                if (temp.Previous == null && !temp.Head)
                { Next = temp; temp.Previous = this; }
            }
            if (map[Coor + Coordinates.Down].Entity == SquareTypes.SnakeSegment)
            {
                var temp = (Snake)map[Coor + Coordinates.Down].ActorOnSquare;
                if (temp.Previous == null && !temp.Head)
                { Next = temp; temp.Previous = this; }
            }
            if (map[Coor + Coordinates.Left].Entity == SquareTypes.SnakeSegment)
            {
                var temp = (Snake)map[Coor + Coordinates.Left].ActorOnSquare;
                if (temp.Previous == null && !temp.Head)
                { Next = temp; temp.Previous = this; }
            }
            if (map[Coor + Coordinates.Right].Entity == SquareTypes.SnakeSegment)
            {
                var temp = (Snake)map[Coor + Coordinates.Right].ActorOnSquare;
                if (temp.Previous == null && !temp.Head)
                { Next = temp; temp.Previous = this; }
            }
            if (Previous != null) LastPreviosCoords = Previous.Coor;
            if (Next == null) return;
            Next.FindNext(map);
        }

        public void SnakeMove(Map map, Player player)
        {
            if (CurrentHP == 0) return;
            if (Head)
            {
                if (Coordinates.Distance(Coor, player.Coor) > 5) return;


                List<Node> path = Node.BuildPath(Pathfinder(map, player.Coor, Enemy.ToPlayer));
                if (path == null) return;
                Physics.CollisionCheck(map, path[path.Count - 2].Coor - Coor, this);
                if (Next == null) return;
                Next.SnakeMove(map, player);
            }
            else
            {
                List<Node> path = Node.BuildPath(Pathfinder(map, LastPreviosCoords, _toSnake));
                LastPreviosCoords = Previous.Coor;
                if (path == null) return;
                Physics.CollisionCheck(map, path[path.Count - 2].Coor - Coor, this);
                if (Next == null) return;
                Next.SnakeMove(map, player);
            }
        }
        public override void Die(Map map)
        {
            ActorsSquare.MakeEmpty();

            
            base.Die(map);
            if (Head && Next != null) { Next.Head = true; Next.Die(map); }
            else if (Next != null) Next.BecomeHead(map);

            if (Next!= null) Next.Previous = null;
            if (Previous != null) Previous.Next = null;
        }
        public void BecomeHead(Map map)
        {
            Head = true;
            Previous = null;
            ActorsSquare.MakeSnakeHead();
            map.SnakesHeads.Add(this);
        }

        //Snake's special weapon, that damage depends on Snake's length
        private class Fang : Weapon
        {
            //overriding base Weapon damage function to new special one
            public override int Damage
            {
                get
                {
                    int i = 0;
                    var head = _head;
                    while (head != null)
                    {
                        i++;
                        head = head.Next;
                    }
                    return 1 + (i / 2);
                }
            }
            private Snake _head;
            public Fang(WeaponTypes weapon, Snake Head) : base(weapon)
            {
                _head = Head;
                HitChance = 50;
                Name = "Fang";
            }
        }
    }
}
