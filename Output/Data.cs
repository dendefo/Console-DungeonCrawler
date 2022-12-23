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
            SetWindowSize(120, 30);
            CursorVisible = false;
        }
        public void EraseLine()
        {
            WriteLine("                                                                   ");
        }
        public void Output(Player player)
        {
            //Wtinig the inventory
            SetCursorPosition(40, 0);
            ForegroundColor = Blue;
            Write($"                                 Inventory: {player.Inventory.Count} items");
            for (int j = 0; j < 18; j++) //Clearing Inventory that was before
            {
                SetCursorPosition(50, j + 2);
                Write("                                                                      ");
            }
            int i = 0;
            foreach (Item item in player.Inventory.Keys) // Printing each item in inventory with it's effect and amount
            {
                if (i == 9) break; //Printing only 10 First Items, TODO 
                SetCursorPosition(50, i * 2 + 2);

                switch (item.Type)
                {
                    case ItemTypes.Weapon:
                        Write($"{i}. {item.Name} with {((Weapon)item).Damage} Damage");
                        break;
                    case ItemTypes.Shield:
                        Write($"{i}. {item.Name} with {((Shield)item).Block} Damage Blocking");
                        break;
                    case ItemTypes.Potion:
                        Write($"{i}. {item.Name} with {((Potion)item).Heal} Healing");
                        break;
                }
                SetCursorPosition(102, i * 2 + 2);
                Write($"Amount: {player.Inventory[item]}");
                i++;
            }
            SetCursorPosition(40, 1);
            SetCursorPosition(0, 20);
            Write($"You are at level: {_currentlevel}");
            SetCursorPosition(0, 22);
            EraseLine();
            EraseLine();
            SetCursorPosition(0, 24);
            WriteLine("____________________________________________________");
            EraseLine();
            EraseLine();
            ForegroundColor = Green;
            SetCursorPosition(0, 22);
            Write(action);
            ForegroundColor = DarkRed;
            SetCursorPosition(0, 23);
            Write(action2);
            action = "";
            action2 = "";
            ForegroundColor = White;
            SetCursorPosition(0, 24);
            WriteLine("____________________________________________________");
            WriteLine($"You use {player.EquipedWeapon.Name} with {player.EquipedWeapon.Damage} damage as your Weapon\n" +
                $"You use {player.EquipedShield.Name} with {player.EquipedShield.Block} block as your Shield\n");
            SetCursorPosition(97, 20);
            ForegroundColor = Red;
            Write("                                            ");
            SetCursorPosition(97, 20);
            Write($"Your HP is {player.CurrentHP} of {player.MaxHP}");
            Heart(player);
            //Sword();
            
            //Coin();
        }

        //TODO it Procedural
        private void Heart(Player player)
        {
            //Code below is printing cute pixel heart.
            {
                ForegroundColor = White;
                SetCursorPosition(100, 21);
                Write("████      ████");
                SetCursorPosition(98, 22);
                Write("██    ██  ██    ██");
                SetCursorPosition(96, 23);
                Write("██        ██        ██");
                SetCursorPosition(96, 24);
                Write("██                  ██");
                SetCursorPosition(98, 25);
                Write("██              ██");
                SetCursorPosition(100, 26);
                Write("██          ██");
                SetCursorPosition(102, 27);
                Write("██      ██");
                SetCursorPosition(104, 28);
                Write("██  ██");
                SetCursorPosition(106, 29);
                Write("██");
            }
            //Code below is filling heart with red pixels. Please, don't try to read it. 
            //Yes i made it little bit more effecient. Now it's 50 lines of code instead 100
            //THERE IS a way to make it less code, but f this, i have a lot of other stuff to do here. Maybe later
            {
                int count = (int)(player.CurrentHP / (float)player.MaxHP * 9);
                ForegroundColor = Red;


                SetCursorPosition(100, 22);
                int printCount = count == 1 || count == 2 ? count - 1 : count == 0 ? 0 : 2;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));

                SetCursorPosition(110, 22);
                printCount = count == 7 ? 1 : count >= 8 ? 2 : 0;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));


                SetCursorPosition(98, 23);
                printCount = count <= 4 ? count : 4;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));

                SetCursorPosition(108, 23);
                printCount = count >= 6 ? count - 5 : 0;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));


                SetCursorPosition(98, 24);
                Write(string.Join("", Enumerable.Repeat("██", count)));


                SetCursorPosition(100, 25);
                printCount = count > 0 && count <= 8 ? count - 1 : count == 0 ? 0 : 7;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));


                SetCursorPosition(102, 26);
                printCount = count > 2 && count <= 7 ? count - 2 : count <= 2 ? 0 : 5;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));


                SetCursorPosition(104, 27);
                printCount = count >= 4 && count <= 6 ? count - 3 : count <= 3 ? 0 : 3;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));

                SetCursorPosition(106, 28);
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
            switch (_count)
            {
                case 0:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(70, 20);
                        Write("                    ");

                        SetCursorPosition(70, 21);
                        Write("      ████████      ");


                        SetCursorPosition(70, 22);
                        Write("    ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        ForegroundColor = Yellow;
                        Write("██████");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");


                        SetCursorPosition(70, 23);
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


                        SetCursorPosition(70, 24);
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

                        SetCursorPosition(70, 25);
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

                        SetCursorPosition(70, 26);
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

                        SetCursorPosition(70, 27);
                        Write("    ██");
                        ForegroundColor = DarkYellow;
                        Write("██████");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(70, 28);
                        Write("      ████████      ");

                        SetCursorPosition(70, 29);

                        Write("                    ");

                    }
                    break;
                case 1:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(70, 20);
                        Write("                    ");

                        SetCursorPosition(70, 21);
                        Write("        ████        ");


                        SetCursorPosition(70, 22);
                        Write("      ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██      ");


                        SetCursorPosition(70, 23);
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


                        SetCursorPosition(70, 24);
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

                        SetCursorPosition(70, 25);
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

                        SetCursorPosition(70, 26);
                        Write("    ██");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(70, 27);
                        Write("      ██");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██      ");

                        SetCursorPosition(70, 28);
                        Write("        ████        ");

                        SetCursorPosition(70, 29);

                        Write("                    ");

                    }
                    break;
                case 2:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(70, 20);
                        Write("                    ");

                        SetCursorPosition(70, 21);
                        Write("        ████        ");

                        for (int i = 22; i <= 27; i++)
                        {
                            SetCursorPosition(70, i);
                            Write("      ██");
                            ForegroundColor = DarkYellow;
                            Write("██");
                            ForegroundColor = Yellow;
                            Write("▓▓");
                            ForegroundColor = Black;
                            Write("██      ");
                        }

                        SetCursorPosition(70, 28);
                        Write("        ████        ");

                        SetCursorPosition(70, 29);

                        Write("                    ");

                    }
                    break;
                case 3:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(70, 20);
                        Write("                    ");

                        SetCursorPosition(70, 21);
                        Write("        ████        ");


                        SetCursorPosition(70, 22);
                        Write("      ██");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██      ");


                        SetCursorPosition(70, 23);
                        Write("    ██");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");


                        SetCursorPosition(70, 24);
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

                        SetCursorPosition(70, 25);
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

                        SetCursorPosition(70, 26);
                        Write("    ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = DarkYellow;
                        Write("██");
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(70, 27);
                        Write("      ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██      ");

                        SetCursorPosition(70, 28);
                        Write("        ████        ");

                        SetCursorPosition(70, 29);

                        Write("                    ");

                    }
                    break;
                case 4:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = White;
                        SetCursorPosition(70, 20);
                        Write("                    ");

                        SetCursorPosition(70, 21);
                        Write("      ████");
                        ForegroundColor = Black;
                        Write("████      ");


                        SetCursorPosition(70, 22);
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


                        SetCursorPosition(70, 23);
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


                        SetCursorPosition(70, 24);
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

                        SetCursorPosition(70, 25);
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

                        SetCursorPosition(70, 26);
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

                        SetCursorPosition(70, 27);
                        Write("    ██");
                        ForegroundColor = DarkYellow;
                        Write("██████");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(70, 28);
                        Write("      ████████      ");

                        SetCursorPosition(70, 29);

                        Write("                    ");

                    }
                    break;
                case 5:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(70, 20);
                        Write("                    ");

                        SetCursorPosition(70, 21);
                        Write("        ████        ");


                        SetCursorPosition(70, 22);
                        Write("      ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        ForegroundColor = White;
                        BackgroundColor = DarkGray;
                        Write("████      ");


                        SetCursorPosition(70, 23);
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


                        SetCursorPosition(70, 24);
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

                        SetCursorPosition(70, 25);
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

                        SetCursorPosition(70, 26);
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

                        SetCursorPosition(70, 27);
                        Write("      ██");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██      ");

                        SetCursorPosition(70, 28);
                        Write("        ████        ");

                        SetCursorPosition(70, 29);

                        Write("                    ");

                    }
                    break;
                case 6:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(70, 20);
                        Write("                    ");

                        SetCursorPosition(70, 21);
                        Write("        ████        ");

                        for (int i = 22; i <= 27; i++)
                        {
                            SetCursorPosition(70, i);
                            ForegroundColor = i == 27 ? White : Black;
                            Write("      ██");
                            ForegroundColor = i == 26 || i == 27 ? White : DarkYellow;
                            Write("██");
                            ForegroundColor = i == 25 || i == 26 ? White : Yellow;
                            BackgroundColor = i == 25 || i == 26 ? White : DarkGray;
                            Write("▓▓");
                            BackgroundColor = DarkGray;
                            ForegroundColor = i == 24 || i == 25 ? White : Black;
                            Write("██      ");
                        }

                        SetCursorPosition(70, 28);
                        Write("        ████        ");

                        SetCursorPosition(70, 29);

                        Write("                    ");

                    }
                    break;
                case 7:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(70, 20);
                        Write("                    ");

                        SetCursorPosition(70, 21);
                        Write("        ████        ");


                        SetCursorPosition(70, 22);
                        Write("      ██");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██      ");


                        SetCursorPosition(70, 23);
                        Write("    ██");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");


                        SetCursorPosition(70, 24);
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

                        SetCursorPosition(70, 25);
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

                        SetCursorPosition(70, 26);
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

                        SetCursorPosition(70, 27);
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

                        SetCursorPosition(70, 28);
                        Write("        ████        ");

                        SetCursorPosition(70, 29);

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
