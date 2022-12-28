namespace First_Semester_Project.MapLogic
{
    //All the things that you may find on the map.
    enum SquareTypes
    {
        Empty = 32,
        Wall = 124,
        SpykeWall = 100,
        CrackedWall = 120,
        Entry = 69,
        Exit = 88,
        Player = 3,
        Enemy = 49,
        Chest = 35,
        RevealedTrap = 48,
        DamagingTrap = 84,
        Coin = 36,
        Market = 109,
    }

    //Each symbol of map
    internal class Square
    {
        static public char PlayerAvatar = '♥'; //☺☻♥♦♣♠•◘○◙♂♀♪♫☼►◄↕‼¶§▬↨↑↓→←∟↔▲▼
        static public ConsoleColor PlayerColor = ConsoleColor.DarkMagenta; 
        static public char EnemyAvatar = '☻';
        static public ConsoleColor EnemyColor = ConsoleColor.DarkRed;
        public ConsoleColor Color { get; private set; }
        public SquareTypes Entity { get; private set; }
        public char Symbol { get; private set; }
        public Actor ActorOnSquare { get; set; }

        public Square(SquareTypes type, int x,int y)
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

                case SquareTypes.Market:
                    Symbol = 'M';
                    Color = ConsoleColor.Yellow;
                    break;
                case SquareTypes.Wall:
                    Symbol = '█';
                    Color = ConsoleColor.White;
                    break;

                case SquareTypes.SpykeWall:
                    Symbol = '¤';
                    Color = EnemyColor;
                    ActorOnSquare = new Spike(x, y, false, this);
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
                    Color = ConsoleColor.DarkGreen;
                    break;

                case SquareTypes.Player:
                    Symbol = PlayerAvatar;
                    Color = PlayerColor;
                    ActorOnSquare = new Player(x,y,this);
                    break;

                case SquareTypes.RevealedTrap:
                    Symbol = '¤';
                    Color = ConsoleColor.DarkRed;
                    break;

                case SquareTypes.DamagingTrap:
                    Symbol = ' ';
                    Color = ConsoleColor.DarkRed;
                    ActorOnSquare = new Trap(x, y, SquareTypes.DamagingTrap,this);
                    break;
            }
        }

        public Square(SquareTypes type, int x, int y, int level, int weapon, int shield, int item)
        {
            Entity = type;
            Symbol = EnemyAvatar;
            Color = EnemyColor;
            ActorOnSquare = new Enemy(x, y, level,this, new((WeaponTypes)weapon), new((ShieldTypes)shield), Item.ItemParse(item));
        }
        public Square(SquareTypes type, int x, int y, Item item)
        {
            Entity = type;
            Symbol = '#';
            Color = ConsoleColor.Blue;
            ActorOnSquare = new Chest(x, y);
            ((Chest)ActorOnSquare).PutInside(item);
        }
        public void MakeEmpty()
        {
            Color = ConsoleColor.White;
            Entity = SquareTypes.Empty;
            Symbol = ' ';
            ActorOnSquare = null;
        }
    }
}