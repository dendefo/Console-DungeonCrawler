using static System.Console;
using static System.ConsoleColor;

namespace First_Semester_Project.Output
{
    //Class that represents all the text parts of the game
    internal class Data
    {
        public int _currentlevel;
        private int _count;
        public string action;
        public string action2;

        public CancellationTokenSource _cancelToken;
        public static void SetUp()
        {
            SetWindowSize(160, 30);
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
            Write(String.Concat(Enumerable.Repeat(" ",  30 * player.Exp / (player.Level * 5))));
            BackgroundColor = White;
            Write(String.Concat(Enumerable.Repeat(" ", 30 - (30 * player.Exp / (player.Level * 5)))));
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
                        }

                        break;
                }
                SetCursorPosition(142, i * 2 + 2);
                Write($"Amount: {player.Inventory[item]}");
                i++;
            }

            ForegroundColor = White;
            SetCursorPosition(40, 0);
            Write($"Current dungeon: {_currentlevel}");
            SetCursorPosition(1, 2);

            Write($"Weapon : {player.EquipedWeapon.Name}.             ");
            SetCursorPosition(20, 2);
            Write($"Damage : {player.EquipedWeapon.Damage} ");

            SetCursorPosition(1, 3);
            Write($"Shield : {player.EquipedShield.Name}. ");
            SetCursorPosition(20, 3);
            Write($"Block  : {player.EquipedShield.Block} ");

            SetCursorPosition(3, 5);
            ForegroundColor = Red;
            Write("                            ");
            SetCursorPosition(3, 5);
            Write($"Your HP is {player.CurrentHP} of {player.MaxHP}");
            Heart(player);

            SetCursorPosition(35, 26);
            EraseLine();
            SetCursorPosition(35, 28);
            EraseLine();
            ForegroundColor = Green;
            SetCursorPosition(35, 26);
            Write(action);
            ForegroundColor = DarkRed;
            SetCursorPosition(35, 28);
            Write(action2);
            action = "";
            action2 = "";
            ForegroundColor = Yellow;
            SetCursorPosition(10, 18);
            Write($"${player.Coins/100%10}{player.Coins/10%10}{player.Coins%10}$");
            ForegroundColor = White;
        }

        static public void PrintGUI()
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
                //else 
            }
            //ForegroundColor = White;
            
        }
        //TODO it Procedural
        private void Heart(Player player)
        {
            //Code below is printing cute pixel heart.
            {
                ForegroundColor = White;
                SetCursorPosition(5, 6);
                Write("████      ████");
                SetCursorPosition(3, 7);
                Write("██    ██  ██    ██");
                SetCursorPosition(1, 8);
                Write("██        ██        ██");
                SetCursorPosition(1, 9);
                Write("██                  ██");
                SetCursorPosition(3, 10);
                Write("██              ██");
                SetCursorPosition(5, 11);
                Write("██          ██");
                SetCursorPosition(7, 12);
                Write("██      ██");
                SetCursorPosition(9, 13);
                Write("██  ██");
                SetCursorPosition(11, 14);
                Write("██");
            }
            //Code below is filling heart with red pixels. Please, don't try to read it. 
            //Yes i made it little bit more effecient. Now it's 50 lines of code instead 100
            //THERE IS a way to make it less code, but f this, i have a lot of other stuff to do here. Maybe later
            {
                int count = (int)(player.CurrentHP / (float)player.MaxHP * 9);
                ForegroundColor = Red;


                SetCursorPosition(5, 7);
                int printCount = count == 1 || count == 2 ? count - 1 : count == 0 ? 0 : 2;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));

                SetCursorPosition(15, 7);
                printCount = count == 7 ? 1 : count >= 8 ? 2 : 0;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));


                SetCursorPosition(3, 8);
                printCount = count <= 4 ? count : 4;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));

                SetCursorPosition(13, 8);
                printCount = count >= 6 ? count - 5 : 0;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));


                SetCursorPosition(3, 9);
                Write(string.Join("", Enumerable.Repeat("██", count)));


                SetCursorPosition(5, 10);
                printCount = count > 0 && count <= 8 ? count - 1 : count == 0 ? 0 : 7;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));


                SetCursorPosition(7, 11);
                printCount = count > 2 && count <= 7 ? count - 2 : count <= 2 ? 0 : 5;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));


                SetCursorPosition(9, 12);
                printCount = count >= 4 && count <= 6 ? count - 3 : count <= 3 ? 0 : 3;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));

                SetCursorPosition(11, 13);
                printCount = count == 5 ? 1 : count < 5 ? 0 : 1;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));
            }

        }
        private void Sword()
        {
            SetCursorPosition(90, 21);
            ForegroundColor = White;
            Write("██");
            ForegroundColor = DarkGray;
            Write("██");

            SetCursorPosition(88, 22);
            ForegroundColor = White;
            Write("██");
            ForegroundColor = Gray;
            Write("██");
            ForegroundColor = DarkGray;
            Write("██");

            SetCursorPosition(86, 23);
            ForegroundColor = White;
            Write("██");
            ForegroundColor = Gray;
            Write("██");
            ForegroundColor = DarkGray;
            Write("██");

            SetCursorPosition(78, 24);
            ForegroundColor = Yellow;
            Write("██");
            ForegroundColor = DarkYellow;
            Write("██  ");
            ForegroundColor = White;
            Write("██");
            ForegroundColor = Gray;
            Write("██");
            ForegroundColor = DarkGray;
            Write("██");

            SetCursorPosition(78, 25);
            ForegroundColor = Yellow;
            Write("██");
            ForegroundColor = DarkYellow;
            Write("██");
            ForegroundColor = White;
            Write("██");
            ForegroundColor = Gray;
            Write("██");
            ForegroundColor = DarkGray;
            Write("██");

            SetCursorPosition(80, 26);
            ForegroundColor = Gray;
            Write("██");
            ForegroundColor = Yellow;
            Write("██");
            ForegroundColor = DarkGray;
            Write("██");

            SetCursorPosition(78, 27);
            ForegroundColor = Gray;
            Write("██");
            ForegroundColor = DarkGray;
            Write("████");
            ForegroundColor = DarkYellow;
            Write("████");


            SetCursorPosition(76, 28);
            ForegroundColor = Yellow;
            Write("██");
            ForegroundColor = Gray;
            Write("██");
            ForegroundColor = DarkGray;
            Write("██  ");
            ForegroundColor = DarkYellow;
            Write("████");

            SetCursorPosition(76, 29);
            Write("████");
        }
        public void Coin()
        {
            int x = 2;
            int y = 19;
            switch (_count)
            {
                case 0:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(x, y);
                        Write("                    ");

                        SetCursorPosition(x, y+1);
                        Write("      ████████      ");


                        SetCursorPosition(x, y+2);
                        Write("    ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        ForegroundColor = Yellow;
                        Write("██████");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");


                        SetCursorPosition(x, y+3);
                        Write("  ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = DarkYellow;
                        Write("████");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Yellow;
                        Write("██");
                        ForegroundColor = Black;
                        Write("██  ");


                        SetCursorPosition(x, y+4);
                        Write("  ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██  ");

                        SetCursorPosition(x, y+5);
                        Write("  ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██  ");

                        SetCursorPosition(x, y+6);
                        Write("  ██");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Yellow;
                        Write("████");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██  ");

                        SetCursorPosition(x, y+7);
                        Write("    ██");
                        ForegroundColor = DarkYellow;
                        Write("██████");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(x, y+8);
                        Write("      ████████      ");

                        SetCursorPosition(x, y+9);

                        Write("                    ");

                    }
                    break;
                case 1:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(x, y);
                        Write("                    ");

                        SetCursorPosition(x, y+1);
                        Write("        ████        ");


                        SetCursorPosition(x, y+2);
                        Write("      ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██      ");


                        SetCursorPosition(x, y+3);
                        Write("    ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓▓▓");
                        BackgroundColor = DarkGray;
                        BackgroundColor = DarkGray;
                        ForegroundColor = Yellow;
                        Write("██");
                        ForegroundColor = Black;
                        Write("██    ");


                        SetCursorPosition(x, y+4);
                        Write("    ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = DarkYellow;
                        Write("██");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(x, y+5);
                        Write("    ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = DarkYellow;
                        Write("██");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(x, y+6);
                        Write("    ██");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(x, y+7);
                        Write("      ██");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██      ");

                        SetCursorPosition(x, y+8);
                        Write("        ████        ");

                        SetCursorPosition(x, y+9);

                        Write("                    ");

                    }
                    break;
                case 2:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(x, y);
                        Write("                    ");

                        SetCursorPosition(x, y+1);
                        Write("        ████        ");

                        for (int i = y+2; i <= y+7; i++)
                        {
                            SetCursorPosition(x, i);
                            Write("      ██");
                            ForegroundColor = DarkYellow;
                            Write("██");
                            ForegroundColor = Yellow;
                            Write("▓▓");
                            ForegroundColor = Black;
                            Write("██      ");
                        }

                        SetCursorPosition(x, y+8);
                        Write("        ████        ");

                        SetCursorPosition(x, y+9);

                        Write("                    ");

                    }
                    break;
                case 3:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(x, y);
                        Write("                    ");

                        SetCursorPosition(x, y+1);
                        Write("        ████        ");


                        SetCursorPosition(x, y+2);
                        Write("      ██");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██      ");


                        SetCursorPosition(x, y+3);
                        Write("    ██");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");


                        SetCursorPosition(x, y+4);
                        Write("    ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Yellow;
                        Write("██");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(x, y+5);
                        Write("    ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Yellow;
                        Write("██");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(x, y+6);
                        Write("    ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = DarkYellow;
                        Write("██");
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(x, y+7);
                        Write("      ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██      ");

                        SetCursorPosition(x, y+8);
                        Write("        ████        ");

                        SetCursorPosition(x, y+9);

                        Write("                    ");

                    }
                    break;
                case 4:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = White;
                        SetCursorPosition(x, y);
                        Write("                    ");

                        SetCursorPosition(x, y+1);
                        Write("      ████");
                        ForegroundColor = Black;
                        Write("████      ");


                        SetCursorPosition(x, y+2);
                        ForegroundColor = White;
                        Write("    ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        ForegroundColor = Yellow;
                        Write("██████");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");


                        SetCursorPosition(x, y+3);
                        ForegroundColor = White;
                        Write("  ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = DarkYellow;
                        Write("████");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Yellow;
                        Write("██");
                        ForegroundColor = Black;
                        Write("██  ");


                        SetCursorPosition(x, y+4);
                        Write("  ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██  ");

                        SetCursorPosition(x, y+5);
                        Write("  ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██  ");

                        SetCursorPosition(x, y+6);
                        Write("  ██");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Yellow;
                        Write("████");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██  ");

                        SetCursorPosition(x, y+7);
                        Write("    ██");
                        ForegroundColor = DarkYellow;
                        Write("██████");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(x, y+8);
                        Write("      ████████      ");

                        SetCursorPosition(x, y+9);

                        Write("                    ");

                    }
                    break;
                case 5:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(x, y);
                        Write("                    ");

                        SetCursorPosition(x, y+1);
                        Write("        ████        ");


                        SetCursorPosition(x, y+2);
                        Write("      ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        ForegroundColor = White;
                        BackgroundColor = DarkGray;
                        Write("████      ");


                        SetCursorPosition(x, y+3);
                        ForegroundColor = Black;
                        Write("    ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        ForegroundColor = White;
                        Write("▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Yellow;
                        Write("██");
                        ForegroundColor = Black;
                        Write("██    ");


                        SetCursorPosition(x, y+4);
                        Write("    ██");
                        ForegroundColor = White;
                        Write("████");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(x, y+5);
                        ForegroundColor = White;
                        Write("    ████");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(x, y+6);
                        ForegroundColor = White;
                        Write("    ██");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(x, y+7);
                        Write("      ██");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██      ");

                        SetCursorPosition(x, y+8);
                        Write("        ████        ");

                        SetCursorPosition(x, y+9);

                        Write("                    ");

                    }
                    break;
                case 6:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(x, y);
                        Write("                    ");

                        SetCursorPosition(x, y+1);
                        Write("        ████        ");

                        for (int i = y+2; i <= y+7; i++)
                        {
                            SetCursorPosition(x, i);
                            ForegroundColor = i == y+7 ? White : Black;
                            Write("      ██");
                            ForegroundColor = i == y+6 || i == y+7 ? White : DarkYellow;
                            Write("██");
                            ForegroundColor = i == y+5 || i == y+6 ? White : Yellow;
                            BackgroundColor = i == y+5 || i == y+6 ? White : DarkGray;
                            Write("▓▓");
                            BackgroundColor = DarkGray;
                            ForegroundColor = i == y+4 || i == y+5 ? White : Black;
                            Write("██      ");
                        }

                        SetCursorPosition(x, y+8);
                        Write("        ████        ");

                        SetCursorPosition(x, y+9);

                        Write("                    ");

                    }
                    break;
                case 7:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(x, y);
                        Write("                    ");

                        SetCursorPosition(x, y+1);
                        Write("        ████        ");


                        SetCursorPosition(x, y+2);
                        Write("      ██");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██      ");


                        SetCursorPosition(x, y+3);
                        Write("    ██");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");


                        SetCursorPosition(x, y+4);
                        Write("    ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Yellow;
                        Write("██");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(x, y+5);
                        Write("    ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Yellow;
                        Write("██");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = White;
                        Write("██    ");

                        SetCursorPosition(x, y+6);
                        ForegroundColor = Black;
                        Write("    ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = DarkYellow;
                        Write("██");
                        ForegroundColor = White;
                        Write("██    ");

                        SetCursorPosition(x, y+7);
                        ForegroundColor = Black;
                        Write("      ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = DarkGray;
                        ForegroundColor = White;
                        Write("██      ");

                        SetCursorPosition(x, y+8);
                        Write("        ████        ");

                        SetCursorPosition(x, y+9);

                        Write("                    ");

                    }
                    break;
            }
            ResetColor();
            _count++;
            _count %= 8;
        }

        public static void Tiltan()
        {
            BackgroundColor = White;
            SetCursorPosition(0, 14);
            Write("                              ");
            SetCursorPosition(0, 15);
            ForegroundColor = Black;
            Write("      ██████      ██████      ");

            SetCursorPosition(0, 16);
            ForegroundColor = Black;
            Write("    ██");
            ForegroundColor = DarkGreen;
            Write("██");
            ForegroundColor = Green;
            Write("████");
            ForegroundColor = Black;
            Write("██  ██");
            ForegroundColor = Green;
            Write("██████");
            ForegroundColor = Black;
            Write("██    ");

            SetCursorPosition(0, 17);
            ForegroundColor = Black;
            Write("  ██");
            ForegroundColor = DarkGreen;
            Write("██");
            ForegroundColor = Green;
            Write("██████");
            ForegroundColor = DarkGreen;
            Write("██");
            ForegroundColor = Black;
            Write("██");
            ForegroundColor = DarkGreen;
            Write("██");
            ForegroundColor = Green;
            Write("██");
            ForegroundColor = Gray;
            Write("██");
            ForegroundColor = Green;
            Write("████");
            ForegroundColor = Black;
            Write("██  ");

            SetCursorPosition(0, 18);
            ForegroundColor = Black;
            Write("  ██");
            ForegroundColor = DarkGreen;
            Write("██");
            ForegroundColor = Green;
            Write("████████");
            ForegroundColor = DarkGreen;
            Write("██");
            ForegroundColor = Green;
            Write("██████");
            ForegroundColor = Gray;
            Write("██");
            ForegroundColor = Green;
            Write("██");
            ForegroundColor = Black;
            Write("██  ");

            SetCursorPosition(0, 19);
            ForegroundColor = Black;
            Write("  ██");
            ForegroundColor = DarkGreen;
            Write("██");
            ForegroundColor = Green;
            Write("████████████████████");
            ForegroundColor = Black;
            Write("██  ");

            SetCursorPosition(0, 20);
            ForegroundColor = Black;
            Write("    ██");
            ForegroundColor = DarkGreen;
            Write("██");
            ForegroundColor = Green;
            Write("██████████████");
            ForegroundColor = DarkGreen;
            Write("██");
            ForegroundColor = Black;
            Write("██    ");

            SetCursorPosition(0, 21);
            ForegroundColor = Black;
            Write("      ██");
            ForegroundColor = DarkGreen;
            Write("██");
            ForegroundColor = Green;
            Write("██████████");
            ForegroundColor = DarkGreen;
            Write("██");
            ForegroundColor = Black;
            Write("██      ");

            SetCursorPosition(0, 22);
            ForegroundColor = Black;
            Write("    ██");
            ForegroundColor = Green;
            Write("██████████████████");
            ForegroundColor = Black;
            Write("██    ");

            SetCursorPosition(0, 23);
            ForegroundColor = Black;
            Write("  ██");
            ForegroundColor = DarkGreen;
            Write("██");
            ForegroundColor = Green;
            Write("████████████████████");
            ForegroundColor = Black;
            Write("██  ");

            SetCursorPosition(0, 24);
            ForegroundColor = Black;
            Write("  ██");
            ForegroundColor = DarkGreen;
            Write("██");
            ForegroundColor = Green;
            Write("████████");
            ForegroundColor = DarkGreen;
            Write("██");
            ForegroundColor = Green;
            Write("██████████");
            ForegroundColor = Black;
            Write("██  ");

            SetCursorPosition(0, 25);
            ForegroundColor = Black;
            Write("  ██");
            ForegroundColor = DarkGreen;
            Write("████");
            ForegroundColor = Green;
            Write("██████");
            ForegroundColor = Black;
            Write("██");
            ForegroundColor = DarkGreen;
            Write("██");
            ForegroundColor = Green;
            Write("████████");
            ForegroundColor = Black;
            Write("██  ");

            SetCursorPosition(0, 26);
            ForegroundColor = Black;
            Write("    ██");
            ForegroundColor = DarkGreen;
            Write("██████");
            ForegroundColor = Black;
            Write("██████");
            ForegroundColor = DarkGreen;
            Write("██████");
            ForegroundColor = Black;
            Write("██    ");

            SetCursorPosition(0, 27);
            Write("      ██████  ██  ██████      ");

            SetCursorPosition(0, 28);
            Write("              ██              ");
            SetCursorPosition(0, 29);
            Write("            ██       Tiltan(c)");
            ResetColor();
        }
        public Data(int currentlevel)
        {
            _currentlevel = currentlevel;
        }
    }
}
