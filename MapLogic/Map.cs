namespace First_Semester_Project.MapLogic
{
    internal class Map
    {

        public Enemy[] Enemies = new Enemy[10]; //Array with enemies on map to make them walk 
        private int _enemyCount; //Only for creating purposes
        GraphicEngine engine;

        public Data Log;
        public Player User { get; private set; } //Player's Actor

        public Square[][] MapArray { get; private set; } //Array with each tile of map

        //Map constructor. Filling MapArray with tiles from map-file
        public Map(int Level, Player player, Data log)
        {
            _enemyCount = 0;
            Log = log;
            string[] file = FileReader.Read(Level); //Accepting level from file
            MapArray = new Square[file.Length][];
            for (int y = 0; y < file.Length; y++) //Going throw each row of level
            {
                string row = file[y];
                Square[] maprow = new Square[row.Length];
                for (int x = 0; x < row.Length; x++) //Going throw each Square of row
                {

                    maprow[x] = Spawn(row[x], Level, player, x, y);

                }
                MapArray[y] = maprow;
            }
            Console.Clear();
            lock (Log) { Refresh(); }//Printing a map
        }


        private Square Spawn(char type, int Level, Player player, int x, int y)
        {
            Square square;
            switch ((SquareTypes)type)
            {
                case SquareTypes.Enemy:
                    square = new(SquareTypes.Enemy, x, y, Level, _enemyCount);
                    Enemies[_enemyCount] = (Enemy)square.ActorOnSquare;
                    _enemyCount++;
                    break;
                case SquareTypes.Entry:
                    square = new Square(SquareTypes.Entry, x, y);
                    player.StandsOn = square;
                    player.Move(y - player.YCoordinate, x - player.XCoordinate, square);
                    User = player;
                    break;
                case SquareTypes.Chest:
                    square = new Square(SquareTypes.Chest, x, y, new Weapon(WeaponTypes.Sword));
                    break;
                case SquareTypes.RevealedTrap:
                    square = new Square(SquareTypes.RevealedTrap, x, y);
                    break;
                default:
                    square = new Square((SquareTypes)type, x, y); //Spawning Square based of his type
                    break;
            }
            return square;
        }

        public void Refresh()
        {
            Console.SetCursorPosition(0, 0);
            engine = new();
            foreach (Square[] row in MapArray)
            {
                foreach (Square tile in row)
                {
                    engine.Push(tile.Color, tile.Symbol);

                }
                engine.Push(ConsoleColor.White, '\n');
            }
            engine.Print();

            Console.ResetColor();
            Log.Output(User);

        } //Printing map
        public void Move(Actor actor, string direction)
        {
            int deltaY = 0;
            int deltaX = 0;

            switch (direction)
            {
                case "Left":
                    deltaX -= 1;
                    break;
                case "Right":
                    deltaX += 1;
                    break;
                case "Down":
                    deltaY += 1;
                    break;
                case "Up":
                    deltaY -= 1;
                    break;
                default:
                    return;
            }
            ChoseCollision(deltaX, deltaY, actor);

        } //Moving any actor across the map by 1 tile in 4 directions

        private void ActorMoveOnMap(Actor actor, int y, int deltaY, int x, int deltaX)
        {
            Square temp = MapArray[y + deltaY][x + deltaX];
            MapArray[y + deltaY][x + deltaX] = actor.ActorsSquare;
            MapArray[y][x] = actor.StandsOn;

            actor.Move(deltaY, deltaX, temp);
        }

        public void ChoseCollision(int deltaX, int deltaY, Actor actor)
        {
            if (actor == null) return;

            int y = actor.YCoordinate;
            int x = actor.XCoordinate;

            switch (MapArray[y + deltaY][x + deltaX].Entity)
            {

                case SquareTypes.Empty: //If Actor steps on Empty square
                    ActorMoveOnMap(actor, y, deltaY, x, deltaX);
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break; //If it's not the Player
                    Log.action = "You moved";

                    break;

                case SquareTypes.Exit:
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    ActorMoveOnMap(actor, y, deltaY, x, deltaX);

                    Log.action = "You moved to the next level! Yay";
                    Task.Run(SoundEffects.NewLevel);
                    break;

                case SquareTypes.Enemy:
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;
                    Enemy enemy = (Enemy)MapArray[y + deltaY][x + deltaX].ActorOnSquare;
                    Actor.Battle(this, enemy, y, deltaY, x, deltaX, true);
                    Task.Run(SoundEffects.Attack);

                    break;

                case SquareTypes.Wall:
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    Log.action = "You can't go there";
                    break;

                case SquareTypes.Entry:
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;
                    Log.action = "You came from here, no way back";
                    break;

                case SquareTypes.Player:
                    if (actor.ActorsSquare.Entity == SquareTypes.Enemy)
                    {
                        Actor.Battle(this, (Enemy)actor, y, deltaY, x, deltaX, false);
                    }

                    break;

                case SquareTypes.Chest:
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;
                    Chest che = (Chest)MapArray[y + deltaY][x + deltaX].ActorOnSquare;

                    if (che.Inside != null)
                    {
                        Log.action = $"Yay, you got some {che.Inside.Name}";
                        User.GiveItem(che.Open());
                        MapArray[y + deltaY][x + deltaX].MakeEmpty();// = new Square(SquareTypes.Empty, x,y);
                    }
                    else { Log.action = "There is nothing in this chest."; }

                    break;

                case SquareTypes.DamagingTrap:

                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;
                    Trap trap = (Trap)MapArray[y + deltaY][x + deltaX].ActorOnSquare;
                    //SquareTypes temp = trap.TrapType;

                    MapArray[y + deltaY][x + deltaX] = actor.ActorsSquare;
                    MapArray[y][x] = actor.StandsOn;
                    actor.Move(deltaY, deltaX, new Square(SquareTypes.RevealedTrap, x+deltaX,y+deltaY));
                    Log.action = "Oh, you just walked on a trap. Something just happened";
                    break;
            }
        }

        public void ChangeSquare(Square square, int y, int x)
        {
            if (square == null)
            {
                MapArray[y][x].MakeEmpty();
                return;
            }

            MapArray[y][x] = square;
        }
    }
}