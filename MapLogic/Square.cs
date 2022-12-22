namespace First_Semester_Project.MapLogic
{
    //All the things that you may find on the map.
    enum SquareTypes
    {
        Empty = 32,
        Wall = 124,
        Entry = 69,
        Exit = 88,
        Player = 3,
        Enemy = 49,
        Chest = 35,
        RevealedTrap = 48,
        DamagingTrap = 84
    }

    //Each symbol of map
    internal class Square
    {
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
                    ActorOnSquare = null;
                    break;

                case SquareTypes.Wall:
                    Symbol = '█';
                    Color = ConsoleColor.White;
                    ActorOnSquare = null;
                    break;

                case SquareTypes.Entry:
                    Color = ConsoleColor.DarkYellow;
                    Symbol = 'E';
                    ActorOnSquare = null;
                    break;

                case SquareTypes.Exit:
                    Symbol = 'X';
                    Color = ConsoleColor.DarkGreen;
                    ActorOnSquare = null;
                    break;

                case SquareTypes.Player:
                    Symbol = '♥';//☺☻♥♦♣♠•◘○◙♂♀♪♫☼►◄↕‼¶§▬↨↑↓→←∟↔▲▼
                    Color = ConsoleColor.DarkMagenta;
                    ActorOnSquare = new Player(x,y,this);
                    break;

                case SquareTypes.RevealedTrap:
                    Symbol = 'T';
                    Color = ConsoleColor.DarkRed;
                    ActorOnSquare = null;
                    break;

                case SquareTypes.DamagingTrap:
                    Symbol = ' ';
                    Color = ConsoleColor.DarkRed;
                    ActorOnSquare = new Trap(x, y, SquareTypes.DamagingTrap,this);
                    break;
            }
        }

        public Square(SquareTypes type, int x, int y, int level, int number)
        {
            Entity = type;
            Symbol = '☻';
            Color = ConsoleColor.DarkRed;
            ActorOnSquare = new Enemy(x, y, level, number, this);
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