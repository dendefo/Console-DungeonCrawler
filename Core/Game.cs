using static System.ConsoleKey;
namespace First_Semester_Project.Core
{
    internal class Game
    {
        public Player User = null;
        Map LevelMap;
        Player UserAtStart;
        Data log = new(1);
        bool inMenu = true;
        bool inOptions = false;
        bool inMarket = false;
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
            {
                //bool flag = false;
                //if (!Console.KeyAvailable && !inMenu)
                //{
                //    Thread.Sleep(50);
                //    for (int i = 0; i < 4; i++)
                //    {

                //        if (Console.KeyAvailable) flag = true;
                //        Thread.Sleep(50);
                //    }

                //    if (!flag) return;
                //}
            }
            ConsoleKey key = Console.ReadKey(true).Key;
            //If player is in Option Menu
            if (inOptions)
            {
                switch (key)
                {
                    case S:
                    case DownArrow:
                        _position++;
                        if (_position > 6) _position = 0;
                        Menu.PrintCursor(_position, 64, 12);
                        //ControlSystem();
                        break;

                    case W:
                    case UpArrow:
                        _position--;
                        if (_position < 0) _position = 6;
                        Menu.PrintCursor(_position, 64, 12);
                        //ControlSystem();
                        break;

                    case Spacebar:
                    case Enter:
                        switch (_position)
                        {
                            case 0:
                                Console.Clear();
                                Square.PlayerColor = Menu.ColorChose(0);
                                Menu.OptionsMenu();
                                Menu.PrintCursor(_position, 64, 12);
                                //ControlSystem();
                                break;

                            case 1:
                                Console.Clear();
                                Square.EnemyColor = Menu.ColorChose(0);
                                Menu.OptionsMenu();
                                Menu.PrintCursor(_position, 64, 12);
                                //ControlSystem();
                                break;

                            case 2:
                                Console.Clear();
                                Console.WriteLine("Please type the character");
                                Square.PlayerAvatar = (char)Console.Read();
                                Menu.OptionsMenu();
                                Menu.PrintCursor(_position, 64, 12);
                                //ControlSystem();
                                break;

                            case 3:
                                Console.Clear();
                                Console.WriteLine("Please type the character");
                                Square.EnemyAvatar = (char)Console.Read();
                                Menu.OptionsMenu();
                                Menu.PrintCursor(_position, 64, 12);
                                //ControlSystem();
                                break;

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
                                //ControlSystem();
                                break;
                            case 5:
                                Square.PlayerAvatar = '♥';
                                Square.EnemyAvatar = '☻';
                                Square.PlayerColor = ConsoleColor.DarkMagenta;
                                Square.EnemyColor = ConsoleColor.DarkRed;
                                Enemy.Difficulty = 3;
                                Menu.OptionsMenu();
                                //ControlSystem();
                                break;
                            case 6:
                                inOptions = false;
                                Menu.MainMenu();
                                _position = 0;
                                Menu.PrintCursor(_position, 71, 11);
                                //ControlSystem();
                                break;
                        }
                        break;

                    case Escape:
                        inOptions = false;
                        Menu.MainMenu();
                        _position = 0;
                        Menu.PrintCursor(_position, 71, 11);
                        //ControlSystem();
                        break;

                    default:
                        return;
                        //ControlSystem();
                        break;
                }
            }
            //If player is in Main Menu
            else if (inMenu)
            {
                switch (key)
                {
                    case S:
                    case DownArrow:
                        _position++;
                        if (_position > 6) _position = 0;
                        Menu.PrintCursor(_position, 71, 11);
                        //ControlSystem();
                        break;
                    case W:
                    case UpArrow:
                        _position--;
                        if (_position < 0) _position = 6;
                        Menu.PrintCursor(_position, 71, 11);
                        //ControlSystem();
                        break;

                    case Spacebar:
                    case Enter:
                        switch (_position)
                        {
                            case 0:
                                inMenu = false;
                                log.CurrentLevel = 1;
                                Start(log.CurrentLevel, true);
                                break;

                            case 1:
                                inMenu = false;
                                Start(log.CurrentLevel, log.CurrentLevel == 1 ? true : false);
                                break;

                            case 2:
                                inMenu = false;
                                Console.Clear();
                                if (User == null || User.CurrentHP == 0)
                                {
                                    log.CurrentLevel = 1;
                                    Start(log.CurrentLevel, true);
                                }
                                log.PrintGUI();

                                break;

                            case 3:
                                Menu.Controls();
                                Menu.MainMenu();
                                Menu.PrintCursor(_position, 71, 11);
                                //ControlSystem();
                                break;

                            case 4:
                                inOptions = true;
                                _position = 0;
                                Menu.OptionsMenu();
                                Menu.PrintCursor(_position, 64, 12);
                                //ControlSystem();
                                break;

                            case 5:
                                Menu.Credits();
                                Menu.MainMenu();
                                Menu.PrintCursor(_position, 71, 11);
                                break;

                            case 6:
                                Environment.Exit(0);
                                break;
                        }
                        break;

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
                    case Escape:
                        _position = 0;
                        lock (log)
                        {
                            Menu.MainMenu();
                            Menu.PrintCursor(_position, 71, 11);
                        }
                        inMenu = true;
                        Thread.Sleep(10);
                        //ControlSystem();
                        break;

                    case A:
                    case LeftArrow://Left
                        CollisionLogic.CollisionCheck(LevelMap, new(Directions.Left), User);
                        break;

                    case D:
                    case RightArrow://Right
                        CollisionLogic.CollisionCheck(LevelMap, new(Directions.Right), User);
                        break;

                    case S:
                    case DownArrow://Down
                        CollisionLogic.CollisionCheck(LevelMap, new(Directions.Down), User);
                        break;

                    case W:
                    case UpArrow://Up
                        CollisionLogic.CollisionCheck(LevelMap, new(Directions.Up), User);
                        break;

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
                        User.Use((int)key - 48, log, LevelMap); //Use item
                        break;

                    case H:
                        User.FullHeal();
                        break;

                    default:
                        return;
                        break;
                }
            }
        }

        public void Start(int level, bool gameReStart)
        {

            if (gameReStart) User = new Player(new(), new Square(SquareTypes.Player, new()));
            else User = new Player(UserAtStart.Coor, UserAtStart);

            for (int currentLevel = level; currentLevel <= 10; currentLevel++)
            {
                //Menu.Market(User, log);
                LevelMap = new(currentLevel, User, log);
                log.PrintGUI();
                PlayTheLevel();
                log.CurrentLevel++;

            }
        }
        private void PlayTheLevel()
        {
            UserAtStart = new Player(User.Coor, User);

            while (User.StandsOn.Entity != SquareTypes.Exit)
            {
                if (User.CurrentHP == 0)
                {
                    Menu.EndOfGame();
                    inMenu = true;
                    _position = 0;
                    while (true)
                    {
                        ControlSystem();
                    }
                }
                User.Turn();
                ControlSystem();
                if (!inMenu)
                {

                    if (User.CurrentEffect != EffectType.Invisibl) Enemy.EnemiesMoving(LevelMap, User);

                    Spike.SpykeMoving(LevelMap);
                    lock (log) { LevelMap.Refresh(); }
                }
            }
            if (log.CurrentLevel % 2 == 0) Menu.Market(User, log);
        }

    }
}
