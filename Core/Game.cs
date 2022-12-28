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
        int _position = 0;

        public void Run()
        {
            ControlSystem();
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
                        ControlSystem();
                        break;

                    case W:
                    case UpArrow:
                        _position--;
                        if (_position < 0) _position = 6;
                        Menu.PrintCursor(_position, 64, 12);
                        ControlSystem();
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
                                ControlSystem();
                                break;

                            case 1:
                                Console.Clear();
                                Square.EnemyColor = Menu.ColorChose(0);
                                Menu.OptionsMenu();
                                Menu.PrintCursor(_position, 64, 12);
                                ControlSystem();
                                break;

                            case 2:
                                Console.Clear();
                                Console.WriteLine("Please type the character");
                                Square.PlayerAvatar = (char)Console.Read();
                                Menu.OptionsMenu();
                                Menu.PrintCursor(_position, 64, 12);
                                ControlSystem();
                                break;

                            case 3:
                                Console.Clear();
                                Console.WriteLine("Please type the character");
                                Square.EnemyAvatar = (char)Console.Read();
                                Menu.OptionsMenu();
                                Menu.PrintCursor(_position, 64, 12);
                                ControlSystem();
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
                                ControlSystem();
                                break;
                            case 5:
                                Square.PlayerAvatar = '♥';
                                Square.EnemyAvatar = '☻';
                                Square.PlayerColor = ConsoleColor.DarkMagenta;
                                Square.EnemyColor = ConsoleColor.DarkRed;
                                Enemy.Difficulty = 3;
                                Menu.OptionsMenu();
                                ControlSystem();
                                break;
                            case 6:
                                inOptions = false;
                                Menu.MainMenu();
                                _position = 0;
                                Menu.PrintCursor(_position, 71, 11);
                                ControlSystem();
                                break;
                        }
                        break;

                    case Escape:
                        inOptions = false;
                        Menu.MainMenu();
                        _position = 0;
                        Menu.PrintCursor(_position, 71, 11);
                        ControlSystem();
                        break;

                    default:
                        ControlSystem();
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
                        if (_position > 5) _position = 0;
                        Menu.PrintCursor(_position, 71, 11);
                        ControlSystem();
                        break;
                    case W:
                    case UpArrow:
                        _position--;
                        if (_position < 0) _position = 5;
                        Menu.PrintCursor(_position, 71, 11);
                        ControlSystem();
                        break;

                    case Spacebar:
                    case Enter:
                        switch (_position)
                        {
                            case 0:
                                inMenu = false;
                                Start(1, true);
                                break;

                            case 1:
                                inMenu = false;
                                Start(log._currentlevel, log._currentlevel==1?true:false);
                                break;

                            case 2:
                                inMenu = false;
                                Console.Clear();
                                if (User == null) Start(1, true);
                                log._cancelToken = new();
                                Task.Factory.StartNew(() =>
                                {
                                    while (true)
                                    {
                                        Thread.Sleep(150);
                                        if (log._cancelToken.IsCancellationRequested)
                                        {
                                            break;
                                        }
                                        lock (log)
                                        {
                                            log.Coin();
                                        }


                                    }

                                }, log._cancelToken.Token);
                                break;

                            case 3:
                                Menu.Controls();
                                _position = 0;
                                Menu.MainMenu();
                                Menu.PrintCursor(_position, 71, 11);
                                ControlSystem();
                                break;

                            case 4:
                                inOptions = true;
                                _position = 0;
                                Menu.OptionsMenu();
                                Menu.PrintCursor(_position, 64, 12);
                                ControlSystem();
                                break;

                            case 5:
                                Environment.Exit(0);
                                break;
                        }
                        break;

                    case Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        ControlSystem();
                        break;
                }
                return;
            }
            //If player is in game
            else
            {
                switch (key)
                {
                    case Escape:
                        log._cancelToken.Cancel();
                        _position = 0;
                        Menu.MainMenu();
                        Menu.PrintCursor(_position, 71, 11);
                        inMenu = true;
                        Thread.Sleep(10);
                        ControlSystem();
                        break;

                    case A:
                    case LeftArrow://Left
                        LevelMap.Move(User, "Left");
                        break;

                    case D:
                    case RightArrow://Right
                        LevelMap.Move(User, "Right");
                        break;

                    case S:
                    case DownArrow://Down
                        LevelMap.Move(User, "Down");
                        break;

                    case W:
                    case UpArrow://Up
                        LevelMap.Move(User, "Up");
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
                        break;
                }
            }
        }

        public void Start(int level, bool gameReStart)
        {
            log._cancelToken = new();
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(150);
                    if (log._cancelToken.IsCancellationRequested)
                    {
                        break;
                    }
                    lock (log)
                    {
                        log.Coin();
                    }


                }

            }, log._cancelToken.Token);//Printing Coin Animation

            if (gameReStart) User = new Player(0, 0, new Square(SquareTypes.Player, 0, 0));
            else User = new Player(UserAtStart.XCoordinate, UserAtStart.YCoordinate, UserAtStart);

            for (int currentLevel = level; currentLevel <= 10; currentLevel++)
            {
                LevelMap = new(currentLevel, User, log);

                Data.PrintGUI();
                PlayTheLevel();
                log._currentlevel++;

            }
        }
        private void PlayTheLevel()
        {
            UserAtStart = new Player(User.XCoordinate, User.YCoordinate, User);

            while (User.StandsOn.Entity != SquareTypes.Exit)
            {
                inMenu = false;
                ControlSystem();
                //Thread.Sleep(1000);

                Enemy.EnemiesMoving(LevelMap, User);
                SpykeMoving(LevelMap, User);
                if (User.CurrentHP == 0)
                {
                    log._cancelToken.Cancel();
                    Menu.EndOfGame();
                    inMenu = true;
                    ControlSystem();
                }
                lock (log) { LevelMap.Refresh(); }
            }
        }
        private static void SpykeMoving(Map level, Player player)
        {
            foreach (Square spyke in level.Spikes)
            {

                level.Move(spyke.ActorOnSquare, ((Spike)spyke.ActorOnSquare).DimentionOfMoving ? ((Spike)spyke.ActorOnSquare).Direction ? "Up" : "Down" : ((Spike)spyke.ActorOnSquare).Direction ? "Right" : "Left");

            }
        }
    }
}
