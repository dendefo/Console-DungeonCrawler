namespace First_Semester_Project.MapLogic
{
    
    internal class Map
    {

        public List<Enemy> Enemies = new(); //All the Enemies on the level
        public List<Square> Spikes { get; private set; } //All the Spikes on the level
        public List<Snake> SnakesHeads { get; private set; } = new(); //All the Snakes of the level

        GraphicEngine _engine; //Graphic output

        public Data Log; //All the data output
        public Player User { get; private set; } //Player's Actor

        //Raycast Visible Objects
        public static List<SquareTypes> NotVisibleThrow = new List<SquareTypes>() { SquareTypes.Wall }; 
        //Raycast Unvisible Objects
        public static List<SquareTypes> VisibleThrow = new List<SquareTypes>() { SquareTypes.Player, SquareTypes.Empty, SquareTypes.Chest, SquareTypes.Coin, SquareTypes.RevealedTrap, SquareTypes.Enemy, SquareTypes.DamagingTrap, SquareTypes.Exit, SquareTypes.SpykeWall, SquareTypes.HorizontalSpykeWall, SquareTypes.CrackedWall,SquareTypes.SnakeHead,SquareTypes.SnakeSegment };

        private Square[][] _mapArray { get; set; } //Array with each tile of map

        /// <summary>
        /// Returns Square from map by coordinates
        /// </summary>
        /// <param name="coor"></param>
        /// <returns></returns>
        public Square this[Coordinates coor]
        {
            get
            {
                return _mapArray[coor.Y][coor.X];
            }
            set
            {
                _mapArray[coor.Y][coor.X] = value;
            }
        }
        Coordinates _exit; //Coordinates of exit

        string[] fileSpawn = new string[2];
        /// <summary>
        /// Map constructor. Filling MapArray with tiles from map-file
        /// </summary>
        /// <param name="Level"></param>
        /// <param name="player"></param>
        /// <param name="log"></param>
        public Map(int Level, Player player, Data log)
        {
            Spikes = new List<Square>();
            Log = log;
            string[] file = FileReader.Read(Level); //Getting level from file

            fileSpawn[0] = file[file.Length - 2]; //Contains items of chests
            fileSpawn[1] = file[file.Length - 1]; //Contains items of enemies
            file[file.Length - 2] = null;
            file[file.Length - 1] = null;


            _mapArray = new Square[file.Length - 2][];
            for (int y = 0; y < file.Length; y++) //Going throw each row of level
            {
                if (file[y] == null) continue;
                string row = file[y];
                Square[] maprow = new Square[row.Length];
                for (int x = 0; x < row.Length; x++) //Going throw each Square of row
                {

                    maprow[x] = Spawn(row[x], Level, player, x, y);

                }
                _mapArray[y] = maprow;
            }
            Console.Clear();
            foreach (var snake in SnakesHeads) //Creating a snakes
            {
                if (snake != null) snake.FindNext(this);
            }
            

            lock (Log) { Refresh(); }//Printing a map
        }
        //Spawning the entities on map
        private Square Spawn(char type, int Level, Player player, int x, int y)
        {
            Square square;
            Coordinates coor = new Coordinates(x, y);

            switch ((SquareTypes)type) //Unique cases are listed down here
            {
                case SquareTypes.Enemy:
                    int weapon = fileSpawn[1][0];
                    int shield = fileSpawn[1][1];
                    int item = fileSpawn[1][2];
                    square = new(SquareTypes.Enemy, coor, Level, weapon, shield, item);
                    Enemies.Add((Enemy)square.ActorOnSquare);
                    fileSpawn[1] = fileSpawn[1].Remove(0, 3);
                    break;

                case SquareTypes.SnakeHead:
                    square = new(SquareTypes.SnakeHead, coor,Level,0,0,0);
                    SnakesHeads.Add((Snake)square.ActorOnSquare);
                    break;

                case SquareTypes.SnakeSegment:
                    square = new(SquareTypes.SnakeSegment, coor);
                    break;

                case SquareTypes.Entry:
                    square = new Square(SquareTypes.Entry, coor);
                    player.Move(coor - player.Coor, square);
                    User = player;
                    break;

                case SquareTypes.Exit:
                    square = new Square(SquareTypes.Exit, coor);
                    _exit = coor;
                    break;

                case SquareTypes.Chest:
                    int itemInt = fileSpawn[0][0];
                    fileSpawn[0] = fileSpawn[0].Remove(0, 1);
                    square = new Square(SquareTypes.Chest, coor, Item.ItemParse(itemInt));
                    break;

                case SquareTypes.RevealedTrap:
                    square = new Square(SquareTypes.RevealedTrap, coor);
                    break;

                case SquareTypes.SpykeWall:
                    square = new Square(SquareTypes.SpykeWall, coor);
                    Spikes.Add(square);
                    break;

                case SquareTypes.HorizontalSpykeWall:
                    square = new Square(SquareTypes.HorizontalSpykeWall, coor);
                    Spikes.Add(square);
                    break;

                default:
                    square = new Square((SquareTypes)type, coor); 
                    break;
            }
            return square;
        }

        //Sends map to GraphicEngine and prints it
        public void Refresh()
        {
            Console.SetCursorPosition(0, 0);
            _engine = new();

            //Player's path to the exit
            List<Node> path = Node.BuildPath(User.Pathfinder(this, _exit, VisibleThrow));
            if (path != null)
            {
                Log.AwayFromExit = path.Count-1; //Length of path
            }
            else Log.AwayFromExit = 0; //There is no way to the exit

            
            for (int i = 0; i < _mapArray.Length; i++)
            {
                if (Math.Abs(i - User.Coor.Y) >= 4) continue; //If not in distance
                Square[] row = _mapArray[i];
                for (int j = 0; j < row.Length; j++)
                {
                    if (Math.Abs(j - User.Coor.X) >= 9) continue; //If not in distance
                    Square tile = row[j];

                    if (Physics.Raycast(this, User.Coor, new(j, i), NotVisibleThrow, 9)|| User.CurrentEffect == EffectType.HawkEye) //If player can see it
                    {
                        if (User.CurrentEffect == EffectType.HawkEye && tile.Entity == SquareTypes.DamagingTrap) { _engine.Push(Square.EnemyColor, '¤'); continue; } //If HawkEye Effect

                        if (path != null) if (path.Exists(n => n.Coor == new Coordinates(j, i)) && tile.Symbol==' ') _engine.Push(Square.PlayerColor, '.'); // If this is on player's path to exit 
                        else _engine.Push(tile.Color, tile.Symbol);
                    }
                    else if (Coordinates.Abs(User.Coor, new Coordinates(j, i)) < 9) _engine.Push(ConsoleColor.White, ' ');

                    //Different output options that i used before and want to safe
                    {
                        //if (this[new(j, i)].seen) engine.Push(tile.Color, tile.Symbol);
                        //else if (Math.Abs(User.Coor.Y - i) <= 4) continue;
                        //if (Math.Pow(User.Coor.Y - i, 2) + Math.Pow(User.Coor.X - j, 2) / 2 > 9 && User.CurrentEffect != EffectType.HawkEye) { this[new(j, i)].seen = false; engine.Push(ConsoleColor.White, ' '); continue; }
                        //else { engine.Push(tile.Color, tile.Symbol); this[new(j, i)].seen = true; continue; }

                        //engine.Push(tile.Color, tile.Symbol);
                    }
                }
                _engine.Push(ConsoleColor.Black, '\n'); //new line
            }
            _engine.Print();
            Console.ResetColor();

            Log.Output(User);

        } //Printing map
        
        /// <summary>
        /// Moving actor on map from coor to coor+delta
        /// </summary>
        /// <param name="actor"> actor to move </param>
        /// <param name="coor"> coor of actor</param>
        /// <param name="delta"> direction</param>
        public void MoveActorOnMap(Actor actor, Coordinates coor, Coordinates delta)
        {
            Coordinates sum = coor + delta; //Get new coordinates of the actor
            Square temp = this[sum];        //Save the information about the square that actor is going to stand on
            this[sum] = actor.ActorsSquare; //Place the actor on th next square
            this[coor] = actor.StandsOn;    //Load the square that actor have been stayed on

            actor.Move(delta, temp);        //Change actor's coordinates and save the square that actors is stands on
        }
    }
}