namespace First_Semester_Project.MapLogic
{
    internal class Map
    {

        public Enemy[] Enemies = new Enemy[10]; //Array with enemies on map to make them walk 
        private int _enemyCount; //Only for creating purposes
        public List<Square> Spykes { get; private set; }
        GraphicEngine engine;

        public Data Log;
        public Player User { get; private set; } //Player's Actor

        public Square[][] MapArray { get; private set; } //Array with each tile of map
        string[] fileSpawn;

        //Map constructor. Filling MapArray with tiles from map-file
        public Map(int Level, Player player, Data log)
        {
            Spykes= new List<Square>();
            _enemyCount = 0;
            Log = log;
            string[] file = FileReader.Read(Level); //Accepting level from file
            fileSpawn = FileReader.ReadSpawnConfig(Level);
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
                    int weapon = fileSpawn[1][_enemyCount*2];
                    int shield = fileSpawn[1][_enemyCount*2+1];
                    square = new(SquareTypes.Enemy, x, y, Level, _enemyCount ,weapon ,shield);
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
                    int itemInt = fileSpawn[0][0];
                    fileSpawn[0] = fileSpawn[0].Remove(0, 1);
                    square = new Square(SquareTypes.Chest, x, y, ItemParse(itemInt));
                    break;

                case SquareTypes.RevealedTrap:
                    square = new Square(SquareTypes.RevealedTrap, x, y);
                    break;

                case SquareTypes.SpykeWall:
                    square = new Square(SquareTypes.SpykeWall, x, y);
                    Spykes.Add(square);
                    break;

                default:
                    square = new Square((SquareTypes)type, x, y); //Spawning Square based of his type
                    break;
            }
            return square;
        }

        private Item ItemParse(int itemInt)
        {

            switch (itemInt)
            {
                case < 60:
                    return new Weapon((WeaponTypes)itemInt);
                case < 70:
                    return new Shield((ShieldTypes)itemInt);
                case >= 70:
                    return new Potion((PotionTypes)itemInt);
            }
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
            CollisionLogic.Collision(this,deltaX, deltaY, actor);

        } //Moving any actor across the map by 1 tile in 4 directions

        public void ActorMoveOnMap(Actor actor, int y, int deltaY, int x, int deltaX)
        {
            Square temp = MapArray[y + deltaY][x + deltaX];
            MapArray[y + deltaY][x + deltaX] = actor.ActorsSquare;
            MapArray[y][x] = actor.StandsOn;

            actor.Move(deltaY, deltaX, temp);
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