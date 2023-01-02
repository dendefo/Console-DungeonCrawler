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


        string[] fileSpawn = new string[2];

        //Map constructor. Filling MapArray with tiles from map-file
        public Map(int Level, Player player, Data log)
        {
            Spikes = new List<Square>();
            Log = log;
            string[] file = FileReader.Read(Level); //Accepting level from file

            fileSpawn[0] = file[file.Length - 2]; //Contains items of chests
            fileSpawn[1] = file[file.Length - 1]; //Contains items of enemies
            file[file.Length - 2] = null;
            file[file.Length - 1] = null;


            MapArray = new Square[file.Length-2][];
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
            Coordinates coor = new Coordinates(x,y);

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
                    player.StandsOn = square;
                    player.Move(coor-player.Coor, square);
                    User = player;
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
            foreach (Square[] row in MapArray)
            {
                foreach (Square tile in row)
                {
                    engine.Push(tile.Color, tile.Symbol);

                }
                engine.Push(ConsoleColor.Black, '\n');
            }
            engine.Print();

            Console.ResetColor();
            Log.Output(User);

        } //Printing map
        public void Move(Actor actor, Directions direction)
        {
            Coordinates delta = new Coordinates(direction);

            CollisionLogic.Collision(this,delta, actor);

        } //Moving any actor across the map by 1 tile in 4 directions

        public void ActorMoveOnMap(Actor actor, Coordinates coor, Coordinates delta)
        {
            Coordinates sum = coor + delta;
            Square temp = sum ^ MapArray;//MapArray[coor.Y + delta.Y][coor.X + delta.X];
            MapArray[coor.Y + delta.Y][coor.X + delta.X] = actor.ActorsSquare;
            MapArray[coor.Y][coor.X] = actor.StandsOn;

            actor.Move(delta, temp);
        }

        public void ChangeSquare(Square square, Coordinates coor)
        {
            if (square == null)
            {
                (coor^MapArray).MakeEmpty();
                return;
            }

            MapArray[coor.Y][coor.X] = square;
        }
    }
}