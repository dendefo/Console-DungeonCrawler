using static System.Console;
using static System.ConsoleColor;

namespace First_Semester_Project.Output
{
    //Class that represents all the text parts of the game
    internal class Data
    {
        public int CurrentLevel;
        public string GreenAction;
        public string RedAction;
        private int _damage;
        public int Damage
        {
            get{ return _damage; }
            set
            {
                _damage = value;
                if (Damage > 0)
                {
                    RedAction = $"Enemy attacked you and dealt {_damage} Damage";
                }
            }
        }
        public int AwayFromExit;

        public CancellationTokenSource CoinCancelToken { get; set; }
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
        public void EraseLine()
        {
            Write("                                                                     ");
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

            //Printing the inventory
            SetCursorPosition(110, 0);
            ForegroundColor = Blue;
            Write($"   Inventory: {player.Inventory.Count} items  ");
            for (int j = 0; j < 18; j++) //Clearing Inventory that was before
            {
                SetCursorPosition(90, j + 2);
                Write("                                                                      ");
            }
            int i = 0;
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

            SetCursorPosition(3, 5);
            ForegroundColor = Red;
            Write("                            ");
            SetCursorPosition(7, 7);
            Write($"Your HP is {player.CurrentHP} of {player.MaxHP}  ");
            PixelArt.Heart(player, 5, 8);

            SetCursorPosition(35, 26);
            EraseLine();
            SetCursorPosition(35, 28);
            EraseLine();
            ForegroundColor = Green;
            SetCursorPosition(35, 26);
            Write(GreenAction);
            ForegroundColor = DarkRed;
            SetCursorPosition(35, 28);
            Write(RedAction);
            GreenAction = "";
            RedAction = "";
            Damage = 0;
            ForegroundColor = Yellow;
            SetCursorPosition(10, 18);
            Write($"${player.Coins / 100 % 10}{player.Coins / 10 % 10}{player.Coins % 10}$");
            ForegroundColor = Green;
            SetCursorPosition(2, 19);
            Write(AwayFromExit != 0 ? $"You are {AwayFromExit} moves             \n  Away from exit     " : "Can't find a way to the exit\n  Try to move around           ");

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

        public void PrintGUI()
        {
            lock (this)
            {
                for (int i = 0; i < 30; i++)
                {
                    SetCursorPosition(32, i);
                    Write("║");
                    if (i > 25) continue;
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
                        SetCursorPosition(i, 25);
                        Write("╠");
                        SetCursorPosition(i, 2);
                        Write("╠");
                    }
                    else if (i == 87)
                    {
                        SetCursorPosition(i, 2);
                        Write("╣");
                        SetCursorPosition(i, 25);
                        Write("╩");
                    }
                    else if (i < 87)
                    {
                        SetCursorPosition(i, 25);
                        Write("═");
                        SetCursorPosition(i, 2);
                        Write("═");
                    }

                    else
                    {
                        SetCursorPosition(i, 25);
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
}
