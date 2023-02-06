using static System.ConsoleKey;

namespace First_Semester_Project.Core
{
    internal class Game
    {
        
        public Player User = null;
        Map _map;
        Player _userAtStart;
        Data _log = new(1);
        bool _inMenu = true;
        bool _inOptions = false;
        int _position = 0;

        public void Run()
        {
            while (true)
            {
                ControlSystem();
            }
        }

        public void ControlSystem()
        {
            
            ConsoleKey key = Console.ReadKey(true).Key;
            //If player is in Option Menu
            if (_inOptions)
            {
                switch (key)
                {
                    //Down
                    case S:
                    case DownArrow:
                        _position++;
                        if (_position > 6) _position = 0;
                        Menu.PrintCursor(_position, 64, 12);
                        break;
                    //Up
                    case W:
                    case UpArrow:
                        _position--;
                        if (_position < 0) _position = 6;
                        Menu.PrintCursor(_position, 64, 12);
                        break;
                    //Chose
                    case Spacebar:
                    case Enter:
                        switch (_position)
                        {
                            //Choose Player Color
                            case 0:
                                Console.Clear();
                                Square.PlayerColor = Menu.ColorChose(0);
                                Menu.OptionsMenu();
                                Menu.PrintCursor(_position, 64, 12);
                                break;
                            //Chose Enemy Color
                            case 1:
                                Console.Clear();
                                Square.EnemyColor = Menu.ColorChose(0);
                                Menu.OptionsMenu();
                                Menu.PrintCursor(_position, 64, 12);
                                break;
                            //Chose Player Symbol
                            case 2:
                                Console.Clear();
                                Console.WriteLine("Please type the character");
                                Square.PlayerAvatar = (char)Console.Read();
                                Menu.OptionsMenu();
                                Menu.PrintCursor(_position, 64, 12);
                                break;
                            //Chose Enemy Symbol
                            case 3:
                                Console.Clear();
                                Console.WriteLine("Please type the character");
                                Square.EnemyAvatar = (char)Console.Read();
                                Menu.OptionsMenu();
                                Menu.PrintCursor(_position, 64, 12);
                                break;
                            //Chose Defficulty
                            case 4:
                                Console.Clear();
                                Console.WriteLine("1. Child\n2. Easy\n3. Medium\n4. Medium Rare\n5. Impossible");
                                switch (Console.ReadKey().Key)
                                {
                                    case D1:
                                        Enemy.Difficulty = 1; break;
                                    case D2:
                                        Enemy.Difficulty = 2; break;
                                    case D3:
                                        Enemy.Difficulty = 3; break;
                                    case D4:
                                        Enemy.Difficulty = 4; break;
                                    case D5:
                                        Enemy.Difficulty = 5; break;
                                }
                                Menu.OptionsMenu();
                                break;
                            //Reset
                            case 5:
                                Square.PlayerAvatar = '♥';
                                Square.EnemyAvatar = '☻';
                                Square.PlayerColor = ConsoleColor.DarkMagenta;
                                Square.EnemyColor = ConsoleColor.DarkRed;
                                Enemy.Difficulty = 3;
                                Menu.OptionsMenu();
                                break;
                            //Exit
                            case 6:
                                _inOptions = false;
                                Menu.MainMenu();
                                _position = 0;
                                Menu.PrintCursor(_position, 71, 11);
                                break;
                        }
                        break;
                    //Exit
                    case Escape:
                        _inOptions = false;
                        Menu.MainMenu();
                        _position = 0;
                        Menu.PrintCursor(_position, 71, 11);
                        break;

                    default:
                        return;
                }
            }
            //If player is in Main Menu
            else if (_inMenu)
            {
                switch (key)
                {
                    //Down
                    case S:
                    case DownArrow:
                        _position++;
                        if (_position > 6) _position = 0;
                        Menu.PrintCursor(_position, 71, 11);
                        break;
                    //Up
                    case W:
                    case UpArrow:
                        _position--;
                        if (_position < 0) _position = 6;
                        Menu.PrintCursor(_position, 71, 11);
                        break;
                    //Chose
                    case Spacebar:
                    case Enter:
                        switch (_position)
                        {
                            //New Game
                            case 0:
                                _inMenu = false;
                                _log.CurrentLevel = 1;
                                Start(_log.CurrentLevel, true);
                                break;
                            //Restart the level
                            case 1:
                                _inMenu = false;
                                Start(_log.CurrentLevel, _log.CurrentLevel == 1 ? true : false);
                                break;
                            //Continue the Game
                            case 2:
                                _inMenu = false;
                                Console.Clear();
                                if (User == null || User.CurrentHP == 0)
                                {
                                    _log.CurrentLevel = 1;
                                    Start(_log.CurrentLevel, true);
                                }
                                _log.PrintGRID();
                                break;
                            //Controls
                            case 3:
                                Menu.Controls();
                                Menu.MainMenu();
                                Menu.PrintCursor(_position, 71, 11);
                                break;
                            //Options
                            case 4:
                                _inOptions = true;
                                _position = 0;
                                Menu.OptionsMenu();
                                Menu.PrintCursor(_position, 64, 12);
                                break;
                            //Credits
                            case 5:
                                Menu.Credits();
                                Menu.MainMenu();
                                Menu.PrintCursor(_position, 71, 11);
                                break;
                            //Exit
                            case 6:
                                Environment.Exit(0);
                                break;
                        }
                        break;
                    //Exit
                    case Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        return;
                }
                return;
            }
            //If player is in game
            else
            {
                switch (key)
                {
                    //Leave to main Menu
                    case Escape:
                        _position = 0;
                        lock (_log)
                        {
                            Menu.MainMenu();
                            Menu.PrintCursor(_position, 71, 11);
                        }
                        _inMenu = true;
                        Thread.Sleep(10);
                        break;
                    //Left
                    case A:
                    case LeftArrow:
                        Physics.CollisionCheck(_map, Coordinates.Left, User);
                        break;
                    //Right
                    case D:
                    case RightArrow:
                        Physics.CollisionCheck(_map, Coordinates.Right, User);
                        break;
                    //Down
                    case S:
                    case DownArrow:
                        Physics.CollisionCheck(_map, Coordinates.Down, User);
                        break;
                    //Up
                    case W:
                    case UpArrow:
                        Physics.CollisionCheck(_map, Coordinates.Up, User);
                        break;
                    //Use item
                    case D0:
                    case D1:
                    case D2:
                    case D3:
                    case D4:
                    case D5:
                    case D6:
                    case D7:
                    case D8:
                    case D9: //Share same logic of numbers from 0-9
                        User.Use((int)key - 48, _log, _map); 
                        break;
                    //Heal Max HP
                    case H:
                        User.FullHeal();
                        break;

                    default:
                        return;
                }
            }
        }
        //Start a new game
        public void Start(int level, bool gameReStart)
        {
            if (gameReStart) User = new Player(new(), new Square(SquareTypes.Player, new()));
            else User = new Player(_userAtStart.Coor, _userAtStart);    // in case of restarting the level

            for (int currentLevel = level; currentLevel <= 5; currentLevel++) //I made only 5 levels
            {
                _map = new(currentLevel, User, _log);
                _log.PrintGRID();
                PlayTheLevel();
                _log.CurrentLevel++;
            }
            Console.Clear();
            Console.WriteLine("Congrats, you won!");
        }
        //Play each level
        private void PlayTheLevel()
        {
            _userAtStart = new Player(User.Coor, User);

            while (User.StandsOn.Entity != SquareTypes.Exit)
            {
                if (User.CurrentHP == 0) //Plyer is dead
                {
                    Menu.EndOfGame();
                    _inMenu = true;
                    _position = 0;
                    while (true)
                    {
                        ControlSystem();
                    }
                }
                User.Turn(); //PotionEffects--
                ControlSystem(); //User Input
                if (!_inMenu)
                {
                    if (User.CurrentEffect != EffectType.Invisibl) 
                    { 
                        //Moving Enemies
                        Enemy.EnemiesMoving(_map, User); 
                        foreach (Snake snake in _map.SnakesHeads)
                        {
                            if (snake != null) snake.SnakeMove(_map, User);
                        }
                       
                    }
                    //Moving spykes
                    Spike.SpykeMoving(_map);
                    lock (_log) { _map.Refresh(); }
                }
            }
            //Each 2 levels player can buy/sell items
            if (_log.CurrentLevel % 2 == 0) Menu.Market(User, _log);
        }
    }
}
