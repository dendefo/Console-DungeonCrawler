﻿namespace First_Semester_Project.MapLogic
{
    internal class Map
    {

        public List<Enemy> Enemies = new(); //Array with enemies on map to make them walk 
        public List<Square> Spikes { get; private set; }
        GraphicEngine engine;

        public Data Log;
        public Player User { get; private set; } //Player's Actor

        public Square[][] MapArray { get; private set; } //Array with each tile of map
        Coordinates _exit;

        /// <summary>
        /// Returnes Square from given coordinates
        /// </summary>
        /// <param name="coor"></param>
        /// <returns></returns>
        public Square GetFromMap(Coordinates coor)
        {
            return MapArray[coor.Y][coor.X];
        }
        public void SetToMap(Coordinates coor, Square square)
        {
            if (square == null)
            {
                GetFromMap(coor).MakeEmpty();
                return;
            }
            MapArray[coor.Y][coor.X] = square;
        }
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

            List<Node> path= Node.BuildPath(User.Pathfinder(this, _exit));
            if (path != null)
            {
                Log.AwayFromExit = path.Count;
            }
            else Log.AwayFromExit = 0;

            Log.Output(User);

        } //Printing map
        public void ActorMoveOnMap(Actor actor, Coordinates coor, Coordinates delta)
        {
            Coordinates sum = coor + delta;
            Square temp = GetFromMap(sum);
            SetToMap(sum, actor.ActorsSquare);
            SetToMap(coor, actor.StandsOn);

            actor.Move(delta, temp);
        }
    }
}