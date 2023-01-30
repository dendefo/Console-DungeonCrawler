namespace First_Semester_Project.MapLogic
{
    internal class Map
    {

        public List<Enemy> Enemies = new(); //Array with enemies on map to make them walk 
        public List<Square> Spikes { get; private set; }
        GraphicEngine engine;

        public Data Log;
        public Player User { get; private set; } //Player's Actor

        public Square[][] MapArray { get; private set; } //Array with each tile of map

        public static List<SquareTypes> notVisibleThrow = new List<SquareTypes>() { SquareTypes.Wall,SquareTypes.CrackedWall };

        /// <summary>
        /// Returns Square from map by coordinates
        /// </summary>
        /// <param name="coor"></param>
        /// <returns></returns>
        public Square this[Coordinates coor]
        {
            get
            {
                return MapArray[coor.Y][coor.X];
            }
            set
            {
                MapArray[coor.Y][coor.X] = value;
            }
        }
        Coordinates _exit;

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
            string[] file = FileReader.Read(Level); //Accepting level from file

            fileSpawn[0] = file[file.Length - 2]; //Contains items of chests
            fileSpawn[1] = file[file.Length - 1]; //Contains items of enemies
            file[file.Length - 2] = null;
            file[file.Length - 1] = null;


            MapArray = new Square[file.Length - 2][];
            for (int y = 0; y < file.Length; y++) //Going throw each row of level
            {
                if (file[y] == null) continue;
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
            Coordinates coor = new Coordinates(x, y);

            switch ((SquareTypes)type)
            {
                case SquareTypes.Enemy:
                    int weapon = fileSpawn[1][0];
                    int shield = fileSpawn[1][1];
                    int item = fileSpawn[1][2];
                    square = new(SquareTypes.Enemy, coor, Level, weapon, shield, item);
                    Enemies.Add((Enemy)square.ActorOnSquare);
                    fileSpawn[1] = fileSpawn[1].Remove(0, 3);
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
                    square = new Square((SquareTypes)type, coor); //Spawning Square based of his type
                    break;
            }
            return square;
        }
        public void Refresh()
        {
            Console.SetCursorPosition(0, 0);
            engine = new();
            for (int i = 0; i < MapArray.Length; i++)
            {
                Square[] row = MapArray[i];
                for (int j = 0; j < row.Length; j++)
                {
                    Square tile = row[j];

                    if (Physics.Raycast(this,User.Coor, new(j, i), notVisibleThrow,7)) { engine.Push(tile.Color, tile.Symbol); }
                    
                    //if (this[new(j, i)].seen) engine.Push(tile.Color, tile.Symbol);
                    else engine.Push(ConsoleColor.White, ' '); 

                    //if (Math.Pow(User.Coor.Y - i, 2) + Math.Pow(User.Coor.X - j, 2) / 2 > 9 && User.CurrentEffect != EffectType.HawkEye) { this[new(j, i)].seen = false; engine.Push(ConsoleColor.White, ' '); continue; }
                    //else { engine.Push(tile.Color, tile.Symbol); this[new(j, i)].seen = true; continue; }
                    //if (User.CurrentEffect == EffectType.HawkEye && tile.Entity == SquareTypes.DamagingTrap) { engine.Push(Square.EnemyColor, '¤'); continue; }
                    //engine.Push(tile.Color, tile.Symbol);
                }
                engine.Push(ConsoleColor.Black, '\n');
            }
            engine.Print();
            Console.ResetColor();

            List<Node> path = Node.BuildPath(User.Pathfinder(this, _exit));
            if (path != null)
            {
                Log.AwayFromExit = path.Count;
            }
            else Log.AwayFromExit = 0;

            Log.Output(User);

        } //Printing map
        public void MoveActorOnMap(Actor actor, Coordinates coor, Coordinates delta)
        {
            Coordinates sum = coor + delta; //Get new coordinates of the actor
            Square temp = this[sum];        //Save the information about the square that actor is going to stand on
            this[sum] = actor.ActorsSquare; //Place the actor on th next square
            this[coor] = actor.StandsOn;    //Load the square that actor have been stayed on
            
            actor.Move(delta, temp);        //Change actor's coordinates and save the square that actors is stands on
            actor.ActorsSquare.seen = actor.StandsOn.seen;

        }

        
    }
}