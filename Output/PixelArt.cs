using static System.Console;
using static System.ConsoleColor;

namespace First_Semester_Project.Output
{
    //You don't want to scroll over this class. There is too much same lines of code, save your time. Cause i didn't saved it
    
    //After making all this functions i think that it maybe was better to represent is as matrix of dictionaries<symbol, color> or something like this
    //But i already made a bad way
    internal class PixelArt
    {
        public static void Print(int x, int y, ConsoleColor color)
        {
            SetCursorPosition(x, y);
            ForegroundColor = color;
            Write("██");
        }
        public static void Print(int x, int y, ConsoleColor color, string text)
        {
            SetCursorPosition(x, y);
            ForegroundColor = color;
            Write(text);
        }
        public static void Print(ConsoleColor color)
        {
            ForegroundColor = color;
            Write("██");
        }
        public static void Print(string text, ConsoleColor color)
        {
            ForegroundColor = color;
            Write(text);
        }

        public static void Print(string text, ConsoleColor color, ConsoleColor back)
        {
            ConsoleColor before = BackgroundColor;
            BackgroundColor = back;
            ForegroundColor = color;
            Write(text);
            BackgroundColor = before;
        }

        public static void Heart(Player player, int x, int y)
        {
            //Code below is printing cute pixel heart.
            {
                ForegroundColor = White;
                SetCursorPosition(x + 4, y);
                Write("████      ████");
                SetCursorPosition(x + 2, y + 1);
                Write("██    ██  ██    ██");
                SetCursorPosition(x, y + 2);
                Write("██        ██        ██");
                SetCursorPosition(x, y + 3);
                Write("██                  ██");
                SetCursorPosition(x + 2, y + 4);
                Write("██              ██");
                SetCursorPosition(x + 4, y + 5);
                Write("██          ██");
                SetCursorPosition(x + 6, y + 6);
                Write("██      ██");
                SetCursorPosition(x + 8, y + 7);
                Write("██  ██");
                SetCursorPosition(x + 10, y + 8);
                Write("██");
            }
            //Code below is filling heart with red pixels. Please, don't try to read it. 
            //Yes i made it little bit more effecient. Now it's 50 lines of code instead 100
            //THERE IS a way to make it less code, but f this, i have a lot of other stuff to do here. Maybe later
            {
                int count = (int)(player.CurrentHP / (float)player.MaxHP * 9);
                ForegroundColor = Red;


                SetCursorPosition(x + 4, y + 1);
                int printCount = count == 1 || count == 2 ? count - 1 : count == 0 ? 0 : 2;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));

                SetCursorPosition(x + 14, y + 1);
                printCount = count == 7 ? 1 : count >= 8 ? 2 : 0;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));


                SetCursorPosition(x + 2, y + 2);
                printCount = count <= 4 ? count : 4;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));

                SetCursorPosition(x + 12, y + 2);
                printCount = count >= 6 ? count - 5 : 0;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));


                SetCursorPosition(x + 2, y + 3);
                Write(string.Join("", Enumerable.Repeat("██", count)));


                SetCursorPosition(x + 4, y + 4);
                printCount = count > 0 && count <= 8 ? count - 1 : count == 0 ? 0 : 7;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));


                SetCursorPosition(x + 6, y + 5);
                printCount = count > 2 && count <= 7 ? count - 2 : count <= 2 ? 0 : 5;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));


                SetCursorPosition(x + 8, y + 6);
                printCount = count >= 4 && count <= 6 ? count - 3 : count <= 3 ? 0 : 3;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));

                SetCursorPosition(x + 10, y + 7);
                printCount = count == 5 ? 1 : count < 5 ? 0 : 1;
                Write(string.Join("", Enumerable.Repeat("██", printCount)));
            }

        }
        public static void Sword(int x, int y)
        {
            Print(x + 14, y, White);
            Print(DarkGray);

            Print(x + 12, y + 1, White);
            Print(Gray);
            Print(DarkGray);

            Print(x + 10, y + 2, White);
            Print(Gray);
            Print(DarkGray);

            Print(x + 2, y + 3, Yellow);
            Print("██  ", DarkYellow);
            Print(White);
            Print(Gray);
            Print(DarkGray);


            Print(x + 2, y + 4, Yellow);
            Print(DarkYellow);
            Print(White);
            Print(Gray);
            Print(DarkGray);


            Print(x + 4, y + 5, Gray);
            Print(Yellow);
            Print(DarkGray);


            Print(x + 2, y + 6, Gray);
            Print("████", DarkGray);
            Print("████", DarkYellow);

            Print(x, y + 7, Yellow);
            Print(Gray);
            Print("██  ", DarkGray);
            Print("████", DarkYellow);

            Print(x, y + 8, DarkYellow);
            Print(DarkYellow);
        }
        public static void Shield(int x, int y)
        {
            for (int i = 0; i < 6; i++)
            {

                Print(x, y + i, DarkGray);
                Print("██████████", Gray);
                Print(i == 3 || i == 5 ? Gray : White);
                Print(Gray);
                Print(White);
                Print(Gray);
            }
            Print(x, y + 6, DarkGray, "████");
            Print("████████████", Gray);
            Print(White);
            Print(Gray);

            Print(x + 2, y + 7, DarkGray);
            Print("████████████", Gray);
            Print(White);

            Print(x + 2, y + 8, DarkGray, "████");
            Print("██████████", Gray);
            Print(White);

            Print(x + 4, y + 9, DarkGray, "████");
            Print("████████", Gray);

            Print(x + 6, y + 10, DarkGray, "████");
            Print("████", Gray);

            Print(x + 8, y + 11, DarkGray, "████");

        }
        public static void Potion(int x, int y)
        {
            Print(x + 6, y, DarkYellow);
            Print("████", Yellow);
            Print(DarkYellow);

            Print(x + 4, y + 1, DarkYellow, "████████████");

            Print(x + 4, y + 2, White);
            Print("████████", DarkYellow);
            Print(White);

            Print(x + 6, y + 3, White, "████████");

            Print(x + 2, y + 4, Gray);
            Print(White);
            Print("████", DarkRed);
            Print("████", Red);
            Print("████", White);

            Print(x, y + 5, Gray);
            Print(DarkRed);
            Print("████", DarkRed);
            Print(Red);
            Print("████", White);
            Print(Red);
            Print(DarkRed);
            Print(White);

            Print(x, y + 6, Gray);
            Print(DarkRed);
            Print("████", DarkRed);
            Print("████", Red);
            Print(White);
            Print(Red);
            Print(DarkRed);
            Print(White);

            Print(x, y + 7, Gray);
            Print("████", DarkRed);
            Print("████", DarkRed);
            Print("████", Red);
            Print("████", DarkRed);
            Print(White);

            Print(x, y + 8, Gray);
            Print("██████", DarkRed);
            Print("████████", DarkRed);
            Print(DarkRed);
            Print(White);

            Print(x + 2, y + 9, Gray);
            Print("████████████", DarkRed);
            Print(Gray);

            Print(x + 4, y + 10, Gray, "████████████");
        }
        //Especially this function, cause it is 8 images.
        public static void Coin(int x, int y, int count)
        {
            BackgroundColor = DarkGray;
            //y + 0
            Print(x, y, Black, "                    ");
            switch (count)
            {
                case 0:
                    {

                        //y + 1
                        Print(x, y + 1, Black, "      ████████      ");
                        //y + 2
                        Print(x, y + 2, Black, "    ██");
                        Print("▓▓", DarkYellow, Yellow);
                        Print("██████", Yellow);
                        Print("██    ", Black);
                        //y + 3
                        Print(x, y + 3, Black, "  ██");
                        Print("▓▓▓▓", DarkYellow, Yellow);
                        Write("████");
                        Print("▓▓", DarkYellow, Yellow);
                        Print(Yellow);
                        Print("██  ", Black);
                        //y + 4
                        Print(x, y + 4, Black, "  ██");
                        Print("▓▓", DarkYellow, Yellow);
                        Print(DarkYellow);
                        Print("▓▓▓▓", DarkYellow, Yellow);
                        Print(Yellow);
                        Print("▓▓", DarkYellow, Yellow);
                        Print("██  ", Black);
                        //y + 5
                        Print(x, y + 5, Black, "  ██");
                        Print("▓▓", DarkYellow, Yellow);
                        Print(DarkYellow);
                        Print("▓▓▓▓", DarkYellow, Yellow);
                        Print(Yellow);
                        Print("▓▓", DarkYellow, Yellow);
                        Print("██  ", Black);
                        //y + 6
                        Print(x, y + 6, Black, "  ██");
                        Print(DarkYellow);
                        Print("▓▓", DarkYellow, Yellow);
                        Print("████", Yellow);
                        Print("▓▓▓▓", DarkYellow, Yellow);
                        Print("██  ", Black);
                        //y + 7
                        Print(x, y + 7, Black, "    ██");
                        Print("██████", DarkYellow);
                        Print("▓▓", DarkYellow, Yellow);
                        Print("██    ", Black);
                        //y + 8
                        Print(x, y + 8, Black, "      ████████      ");
                    }
                    break;
                case 1:
                    {
                        //y + 1
                        Print(x, y + 1, Black, "        ████        ");
                        //y + 2
                        Print(x, y + 2, Black, "      ██");
                        Print("▓▓", DarkYellow, Yellow);
                        Print(Yellow);
                        Print("██      ", Black);
                        //y + 3
                        Print(x, y + 3, Black, "    ██");
                        Print("▓▓▓▓▓▓", DarkYellow, Yellow);
                        Print(Yellow);
                        Print("██    ", Black);
                        //y + 4 && y + 5
                        for (int i = 0; i < 2; i++)
                        {
                            Print(x, y + 4 + i, Black, "    ██");
                            Print("▓▓", DarkYellow, Yellow);
                            Print(DarkYellow);
                            Print(Yellow);
                            Print("▓▓", DarkYellow, Yellow);
                            Print("██    ", Black);
                        }
                        //y + 6
                        Print(x, y + 6, Black, "    ██");
                        Print(DarkYellow);
                        Print("▓▓▓▓▓▓", DarkYellow, Yellow);
                        Print("██    ", Black);
                        //y + 7
                        Print(x, y + 7, Black, "      ██");
                        Print(DarkYellow);
                        Print("▓▓", DarkYellow, Yellow);
                        Print("██      ", Black);
                        //y + 8
                        Print(x, y + 8, Black, "        ████        ");

                    }
                    break;
                case 2:
                    {
                        //y + 1
                        Print(x, y + 1, Black, "        ████        ");

                        //y + 2,3,4,5,6,7
                        for (int i = y + 2; i <= y + 7; i++)
                        {
                            Print(x, i, Black, "      ██");
                            Print(DarkYellow);
                            Print("▓▓", DarkYellow, Yellow);
                            Print("██      ", Black);
                        }
                        //y + 8
                        Print(x, y + 8, Black, "        ████        ");

                    }
                    break;
                case 3:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(x, y);
                        Write("                    ");

                        SetCursorPosition(x, y + 1);
                        Write("        ████        ");


                        SetCursorPosition(x, y + 2);
                        Write("      ██");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██      ");


                        SetCursorPosition(x, y + 3);
                        Write("    ██");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");


                        SetCursorPosition(x, y + 4);
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

                        SetCursorPosition(x, y + 5);
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

                        SetCursorPosition(x, y + 6);
                        Write("    ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = DarkYellow;
                        Write("██");
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(x, y + 7);
                        Write("      ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██      ");

                        SetCursorPosition(x, y + 8);
                        Write("        ████        ");

                        SetCursorPosition(x, y + 9);

                        Write("                    ");

                    }
                    break;
                case 4:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = White;
                        SetCursorPosition(x, y);
                        Write("                    ");

                        SetCursorPosition(x, y + 1);
                        Write("      ████");
                        ForegroundColor = Black;
                        Write("████      ");


                        SetCursorPosition(x, y + 2);
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


                        SetCursorPosition(x, y + 3);
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


                        SetCursorPosition(x, y + 4);
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

                        SetCursorPosition(x, y + 5);
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

                        SetCursorPosition(x, y + 6);
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

                        SetCursorPosition(x, y + 7);
                        Write("    ██");
                        ForegroundColor = DarkYellow;
                        Write("██████");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");

                        SetCursorPosition(x, y + 8);
                        Write("      ████████      ");

                        SetCursorPosition(x, y + 9);

                        Write("                    ");

                    }
                    break;
                case 5:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(x, y);
                        Write("                    ");

                        SetCursorPosition(x, y + 1);
                        Write("        ████        ");


                        SetCursorPosition(x, y + 2);
                        Write("      ██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        ForegroundColor = White;
                        BackgroundColor = DarkGray;
                        Write("████      ");


                        SetCursorPosition(x, y + 3);
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


                        SetCursorPosition(x, y + 4);
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

                        SetCursorPosition(x, y + 5);
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

                        SetCursorPosition(x, y + 6);
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

                        SetCursorPosition(x, y + 7);
                        Write("      ██");
                        ForegroundColor = DarkYellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██      ");

                        SetCursorPosition(x, y + 8);
                        Write("        ████        ");

                        SetCursorPosition(x, y + 9);

                        Write("                    ");

                    }
                    break;
                case 6:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(x, y);
                        Write("                    ");

                        SetCursorPosition(x, y + 1);
                        Write("        ████        ");

                        for (int i = y + 2; i <= y + 7; i++)
                        {
                            SetCursorPosition(x, i);
                            ForegroundColor = i == y + 7 ? White : Black;
                            Write("      ██");
                            ForegroundColor = i == y + 6 || i == y + 7 ? White : DarkYellow;
                            Write("██");
                            ForegroundColor = i == y + 5 || i == y + 6 ? White : Yellow;
                            BackgroundColor = i == y + 5 || i == y + 6 ? White : DarkGray;
                            Write("▓▓");
                            BackgroundColor = DarkGray;
                            ForegroundColor = i == y + 4 || i == y + 5 ? White : Black;
                            Write("██      ");
                        }

                        SetCursorPosition(x, y + 8);
                        Write("        ████        ");

                        SetCursorPosition(x, y + 9);

                        Write("                    ");

                    }
                    break;
                case 7:
                    {
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        SetCursorPosition(x, y);
                        Write("                    ");

                        SetCursorPosition(x, y + 1);
                        Write("        ████        ");


                        SetCursorPosition(x, y + 2);
                        Write("      ██");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██      ");


                        SetCursorPosition(x, y + 3);
                        Write("    ██");
                        ForegroundColor = Yellow;
                        Write("██");
                        BackgroundColor = Yellow;
                        ForegroundColor = DarkYellow;
                        Write("▓▓▓▓▓▓");
                        BackgroundColor = DarkGray;
                        ForegroundColor = Black;
                        Write("██    ");


                        SetCursorPosition(x, y + 4);
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

                        SetCursorPosition(x, y + 5);
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

                        SetCursorPosition(x, y + 6);
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

                        SetCursorPosition(x, y + 7);
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

                        SetCursorPosition(x, y + 8);
                        Write("        ████        ");

                        SetCursorPosition(x, y + 9);

                        Write("                    ");

                    }
                    break;
            }

            //y + 9
            Print(x, y + 9, Black, "                    ");
            ResetColor();
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
    }
}
