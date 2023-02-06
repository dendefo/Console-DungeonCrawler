namespace First_Semester_Project.MapLogic
{
    //All the things that you may find on the map.
    enum SquareTypes
    {
        Empty = 32,                // SPACE
        Wall = 124,                // |
        SpykeWall = 100,           // d
        HorizontalSpykeWall = 101, // e
        CrackedWall = 120,         // x
        Entry = 69,                // E
        Exit = 88,                 // X
        Player = 3,                // ♥  Do not put on map. Use Entry instead
        Enemy = 49,                // 1
        Chest = 35,                // #
        RevealedTrap = 48,         // 0  Do not use on map. Place another trap instead 
        DamagingTrap = 84,         // T
        Coin = 36,                 // $
        SnakeHead = 83,            // S
        SnakeSegment = 115         // s
    }

    //Each tile of map
    internal class Square
    {
        //Changable in options
        static public char PlayerAvatar = '♥'; 
        static public ConsoleColor PlayerColor = ConsoleColor.DarkMagenta;
        static public char EnemyAvatar = '☻';
        static public ConsoleColor EnemyColor = ConsoleColor.DarkRed;

        
        public ConsoleColor Color { get; private set; } //Printing color
        public SquareTypes Entity { get; private set; } //Type
        public char Symbol { get; private set; }        //Printing cymbol
        public Actor ActorOnSquare { get; set; }        //Interactble

        //Basic constructor
        public Square(SquareTypes type, Coordinates coor)
        {
            Entity = type;
            ActorOnSquare = null;

            switch (type)
            {
                case SquareTypes.Empty:
                    Symbol = ' ';
                    Color = ConsoleColor.White;
                    break;

                case SquareTypes.Coin:
                    Symbol = '$';
                    Color = ConsoleColor.Yellow;
                    break;

                case SquareTypes.Wall:
                    Symbol = '█';
                    Color = ConsoleColor.White;
                    break;

                case SquareTypes.SpykeWall:
                    Symbol = '¤';
                    Color = EnemyColor;
                    ActorOnSquare = new Spike(coor, true, this);
                    break;

                case SquareTypes.HorizontalSpykeWall:
                    Symbol = '¤';
                    Color = EnemyColor;
                    Entity = SquareTypes.SpykeWall;
                    ActorOnSquare = new Spike(coor, false, this);
                    break;

                case SquareTypes.CrackedWall:
                    Symbol = '▒';
                    Color = ConsoleColor.White;
                    break;

                case SquareTypes.Entry:
                    Color = ConsoleColor.DarkYellow;
                    Symbol = 'E';
                    break;

                case SquareTypes.Exit:
                    Symbol = 'X';
                    Color = ConsoleColor.Green;
                    break;

                case SquareTypes.Player:
                    Symbol = PlayerAvatar;
                    Color = PlayerColor;
                    ActorOnSquare = new Player(coor, this);
                    break;

                case SquareTypes.RevealedTrap:
                    Symbol = '¤';
                    Color = ConsoleColor.DarkRed;
                    break;

                case SquareTypes.DamagingTrap:
                    Symbol = ' ';
                    Color = ConsoleColor.White;
                    ActorOnSquare = new Trap(coor, SquareTypes.DamagingTrap, this);
                    break;

                case SquareTypes.SnakeSegment:
                    Symbol = 'o';
                    Color = EnemyColor;
                    ActorOnSquare = new Snake(coor, 1, this, new(WeaponTypes.Fists), new(ShieldTypes.Abs), new Coin(), false);
                    break;
            }
        }

        //Constructor for tile with enemy
        public Square(SquareTypes type, Coordinates coor, int level, int weapon, int shield, int item)
        {
            Entity = type;
            if (type == SquareTypes.SnakeHead)
            {
                Symbol = 'Q';
                Color = EnemyColor;
                ActorOnSquare = new Snake(coor, level, this, new(WeaponTypes.Fists), new(ShieldTypes.Abs), new Potion(PotionTypes.InvulnerabilityPotion), true);
                return;
            }
            
            Symbol = EnemyAvatar;
            Color = EnemyColor;
            ActorOnSquare = new Enemy(coor, level, this, new((WeaponTypes)weapon), new((ShieldTypes)shield), Item.ItemParse(item));
        }
        //Constructor with chest
        public Square(SquareTypes type, Coordinates coor, Item item)
        {
            Entity = type;
            Symbol = '#';
            Color = ConsoleColor.Blue;
            ActorOnSquare = new Chest(coor);
            ((Chest)ActorOnSquare).PutInside(item);
        }
        //Clear the tile
        public void MakeEmpty()
        {
            Color = ConsoleColor.White;
            Entity = SquareTypes.Empty;
            Symbol = ' ';
            ActorOnSquare = null;
        }
        //Snake Segment may become Snake Head
        public void MakeSnakeHead()
        {
            Color = EnemyColor;
            Entity = SquareTypes.SnakeHead;
            Symbol = 'Q';
        }
    }
}