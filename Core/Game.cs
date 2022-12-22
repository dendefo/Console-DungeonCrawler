namespace First_Semester_Project.Core
{
    internal class Game
    {
        public Player User = null;
        Map LevelMap = null;
        Player UserAtStart;
        Data log = new(1);
        bool inMenu = true;
        bool inCastomization = false;

        public void Run()
        {
            ControlSystem();
        }

        public void ControlSystem()
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

            ConsoleKey key = Console.ReadKey(true).Key;

            if (inCastomization)
            {
                switch (key)
                {
                    case ConsoleKey.Escape:
                        inCastomization = false;
                        Menu.MainMenu();
                        ControlSystem();
                        break;

                    default:
                        break;
                }
            }
            else if (inMenu)
            {
                switch (key)
                {
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.N:
                        inMenu = false;
                        Start(1, true);
                        break;
                    case ConsoleKey.C:
                        inMenu = false;
                        Console.Clear();
                        if (User == null) Start(1, true);
                        break;
                    case ConsoleKey.B:
                        Menu.Controls();
                        Menu.MainMenu();
                        ControlSystem();
                        break;
                    case ConsoleKey.R:
                        inMenu = false;
                        Start(log._currentlevel, false);
                        break;
                    case ConsoleKey.P:
                        inCastomization = true;
                        Menu.Castomization();
                        ControlSystem();
                        break;

                    default:
                        ControlSystem();
                        break;
                }
                return;
            }
            else
            {
                switch (key)
                {
                    case ConsoleKey.Escape:
                        log._cancelToken.Cancel();
                        Menu.MainMenu();
                        inMenu = true;
                        Thread.Sleep(10);
                        ControlSystem();

                        break;
                    case ConsoleKey.A://Left

                        LevelMap.Move(User, "Left");

                        break;
                    case ConsoleKey.D://Right

                        LevelMap.Move(User, "Right");

                        break;
                    case ConsoleKey.S://Down

                        LevelMap.Move(User, "Down");

                        break;
                    case ConsoleKey.W://Up

                        LevelMap.Move(User, "Up");
                        break;

                    case ConsoleKey.D0:
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                    case ConsoleKey.D6:
                    case ConsoleKey.D7:
                    case ConsoleKey.D8:
                    case ConsoleKey.D9: //Share same logic of numbers from 0-9

                        User.Use((int)key - 48, log); //Use item
                        break;
                    case ConsoleKey.H:
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

            if (gameReStart) User = new Player(0, 0,new Square(SquareTypes.Player,0,0));
            else User = new Player(UserAtStart.XCoordinate, UserAtStart.YCoordinate, UserAtStart);

            for (int currentLevel = level; currentLevel <= 10; currentLevel++)
            {
                LevelMap = new(currentLevel, User, log);

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
                if (User.CurrentHP == 0)
                {
                    log._cancelToken.Cancel();
                    Menu.EndOfGame();
                    inMenu= true;
                    ControlSystem();
                }
                lock (log) { LevelMap.Refresh(); }
            }
        }
        
    }
}
