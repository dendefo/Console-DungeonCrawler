using static System.Console;
using static System.ConsoleColor;

namespace First_Semester_Project.Output
{
    //Class that represents all the text parts of the game
    internal class Data
    {
        public int CurrentLevel;
        private Queue<DataString> _logs = new Queue<DataString>();
        public Queue<DataString> Logs
        {
            get
            {
                if (_logs.Count > 7)
                {
                    _logs.Dequeue();
                }
                return _logs;
            }
            set
            {
                _logs = value;
                if (_logs.Count > 7)
                {
                    _logs.Dequeue();
                }


            }
        }
        public int AwayFromExit;  //Amount of turns to exit

        public CancellationTokenSource CoinCancelToken { get; set; } //For coin printing
        public static void SetUp()
        {
            try
            {
                SetWindowSize(160, 30);
            }
            catch
            {
                WriteLine("Sorry, your OC can't run this game");
                Environment.Exit(0);
            }
            CursorVisible = false;
        }
        public void Output(Player player)
        {
            //Printing the exp Bar
            SetCursorPosition(1, 0);
            Write($"Your level is {player.Level}. Exp: {player.Exp}/{player.Level * 5}   ");
            SetCursorPosition(1, 1);
            ForegroundColor = Black;
            BackgroundColor = DarkGreen;
            Write(string.Concat(Enumerable.Repeat(" ", 30 * player.Exp / (player.Level * 5))));
            BackgroundColor = White;
            Write(string.Concat(Enumerable.Repeat(" ", 30 - (30 * player.Exp / (player.Level * 5)))));
            BackgroundColor = Black;


            SetCursorPosition(110, 0);
            ForegroundColor = Blue;
            Write($"   Inventory: {player.Inventory.Count} items   ");
            for (int j = 0; j < 28; j++) //Clearing Inventory that was before
            {
                SetCursorPosition(90, j + 2);
                Write("                                                                      ");
            }
            int i = 0;
            //Printing the inventory
            foreach (Item item in player.Inventory.Keys) // Printing each item in inventory with it's effect and amount
            {
                if (i == 9) break; //Printing only 10 First Items, TODO 
                SetCursorPosition(90, i * 2 + 2);

                switch (item.Type)
                {
                    case ItemTypes.Weapon:
                        Write($"{i}. {item.Name} with {((Weapon)item).Damage} Damage");
                        break;
                    case ItemTypes.Shield:
                        Write($"{i}. {item.Name} with {((Shield)item).Block} Damage Blocking");
                        break;
                    case ItemTypes.Potion:
                        switch (((Potion)item).PotionType)
                        {
                            case PotionTypes.ExplosivePotion:
                                Write($"{i}. {item.Name} with {((Potion)item).Damage} Damage");
                                break;
                            case PotionTypes.SmallHealingPotion:
                            case PotionTypes.HealingPotion:
                            case PotionTypes.GreatHealingPotion:
                                Write($"{i}. {item.Name} with {((Potion)item).Heal} Healing");
                                break;
                            default:
                                Write($"{i}. {item.Name} with {((Potion)item).Turns} Turns");
                                break;
                        }

                        break;
                }
                SetCursorPosition(142, i * 2 + 2);
                Write($"Amount: {player.Inventory[item]}");
                i++;
            }

            //Prints player info
            ForegroundColor = White;
            SetCursorPosition(40, 0);
            Write($"Dungeon depth: {CurrentLevel}");
            SetCursorPosition(1, 2);

            Write($"Weapon : {player.EquipedWeapon.Name}.             ");
            SetCursorPosition(20, 2);
            Write($"Damage : {player.EquipedWeapon.Damage} ");

            SetCursorPosition(1, 3);
            Write($"Shield : {player.EquipedShield.Name}. ");
            SetCursorPosition(20, 3);
            Write($"Block  : {player.EquipedShield.Block} ");

            SetCursorPosition(1, 4);
            Write($"Effect : {player.CurrentEffect}.           ");
            SetCursorPosition(20, 4);
            Write($"Turns  : {player.Countdown} ");

            //And Player's HP
            SetCursorPosition(3, 5);
            ForegroundColor = Red;
            Write("                            ");
            SetCursorPosition(7, 7);
            Write($"Your HP is {player.CurrentHP} of {player.MaxHP}  ");
            PixelArt.Heart(player, 5, 8);
            
            //And activities 
            int k = 0;
            foreach (var item in Logs)
            {
                SetCursorPosition(34, 15 + k);
                ForegroundColor = item.Color;
                Write(item.Str);
                k++;
                SetCursorPosition(34, 15 + k);
                Write(item.Str2);
                k++;
            }
            //And amount of coins
            ForegroundColor = Yellow;
            SetCursorPosition(10, 18);
            Write($"${player.Coins / 100 % 10}{player.Coins / 10 % 10}{player.Coins % 10}$");
            ForegroundColor = Green;
            SetCursorPosition(2, 19);
            Write(AwayFromExit != 0 ? $"You are {AwayFromExit} moves             \n  Away from exit     " : "Can't find a way to the exit\n  Try to move around           ");

            //And small help window
            SetCursorPosition(2, 22);
            ForegroundColor = Square.PlayerColor;
            Write($"{Square.PlayerAvatar} - Player");
            SetCursorPosition(2, 23);
            ForegroundColor = Square.EnemyColor;
            Write($"{Square.EnemyAvatar} - Enemy  ||  ¤ - Obstacle");
            ForegroundColor = Yellow;
            SetCursorPosition(2, 24);
            Write("$ - Coins");
            ForegroundColor = DarkYellow;
            SetCursorPosition(2, 25);
            Write("E - Entry");
            SetCursorPosition(2, 26);
            ForegroundColor = Blue;
            Write("# - Chest");
            SetCursorPosition(2, 27);
            ForegroundColor = Green;
            Write("X - Exit");


            ForegroundColor = White;
        }
        //Prints white stripes for better looking
        public void PrintGRID()
        {
            lock (this)
            {
                for (int i = 0; i < 30; i++)
                {
                    SetCursorPosition(32, i);
                    Write("║");
                    SetCursorPosition(87, i);
                    Write("║");
                }
                for (int i = 0; i < 160; i++)
                {
                    if (i <= 31)
                    {
                        SetCursorPosition(i, 17);
                        Write("═");
                    }
                    else if (i == 32)
                    {
                        Write("╣");
                        SetCursorPosition(i, 14);
                        Write("╠");
                        SetCursorPosition(i, 2);
                        Write("╠");
                    }
                    else if (i == 87)
                    {
                        SetCursorPosition(i, 2);
                        Write("╣");
                        SetCursorPosition(i, 14);
                        Write("╣");
                    }
                    else if (i < 87)
                    {
                        SetCursorPosition(i, 14);
                        Write("═");
                        SetCursorPosition(i, 2);
                        Write("═");
                    }
                }
            }
        }

        public Data(int currentlevel)
        {
            CurrentLevel = currentlevel;
        }
    }

    //Log type of output
    public struct DataString
    {
        //Has two strings and color
        public string Str;
        public ConsoleColor Color;
        public string Str2 = "".PadRight(52, ' '); //Second string is in case the first string is too long

        public DataString(string str, ConsoleColor color = ConsoleColor.Green)
        {
            Str = str.PadRight(52, ' ');
            Color = color;
            if (Str.Length > 52) { Str2 = Str.Substring(52); Str = Str.Substring(0, 52); }
        }
    }
}
