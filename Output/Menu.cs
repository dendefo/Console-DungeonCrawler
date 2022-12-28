using static System.Console;
using static System.ConsoleColor;

namespace First_Semester_Project.Output
{
    internal class Menu
    {
        public static void MainMenu()
        {
            
            Clear();
            SetCursorPosition(35, 1);
            ForegroundColor = DarkRed;
            Write("███████████████████████████████████████████████████████████████████████████████████████");
            for (int i = 2; i < 9; i++)
            {
                SetCursorPosition(35, i);
                Write("█");
                SetCursorPosition(121, i);
                Write("█");
            }
            SetCursorPosition(35, 9);
            Write("███████████████████████████████████████████████████████████████████████████████████████");
            char[] theInsanity = { 't', 'h', 'e', ' ', 'i', 'n', 's', 'a', 'n', 'i', 't', 'y' };
            for (int i = 0; i < 12; i++)
            {
                if (i == 3) continue;
                Letters.Print(theInsanity[i], 37 + 7 * i, 3);
            }
            SetCursorPosition(105, 8);
            ForegroundColor = Gray;
            Write("Made by Dendefo");

            //SetCursorPosition(51, 11 + position * 2);
            //ForegroundColor = Green;
            //Write("►");
            //ForegroundColor = Gray;

            SetCursorPosition(73, 11);
            Write("Start a new game");
            SetCursorPosition(73, 15);
            Write("Continue");
            SetCursorPosition(73, 13);
            Write("Restart the level");
            SetCursorPosition(73, 17);
            Write("Controls");
            SetCursorPosition(73, 19);
            Write("Options");
            SetCursorPosition(73, 21);
            Write("Exit");
            Data.Tiltan();

        }
        public static void PrintCursor(int position,int startX, int startY)
        {
            for (int i = startY; i <= 29; i += 2)
            {
                SetCursorPosition(startX, i);
                Write(" ");
            }
            SetCursorPosition(startX, startY + position * 2);
            ForegroundColor = startX<70?Blue:Green;
            Write("►");
            ForegroundColor = Gray;
        }

        public static void Controls()
        {
            Clear();
            BackgroundColor = White;
            ForegroundColor = Black;
            Letters.Button('a', 8, 10);
            Letters.Button('s', 23, 10);
            Letters.Button('w', 23, 2);
            Letters.Button('d', 38, 10);

            Letters.Button('h', 60, 2);
            Letters.Button('o', 60, 11);
            Letters.Button('9', 75, 11);
            Letters.Button('e', 60, 21);
            Letters.Button('s', 70, 21);
            Letters.Button('c', 80, 21);
            ResetColor();
            ReadKey();
        }

        public static void EndOfGame()
        {
            Clear();
            char[] gameOver = { 'g', 'a', 'm', 'e', ' ', 'o', 'v', 'e', 'r' };
            for (int i = 0; i < gameOver.Length; i++)
            {
                if (gameOver[i] == ' ') continue;
                Letters.Print(gameOver[i], 30 + 7 * i, 11);
            }
        }

        public static void OptionsMenu()
        {
            Clear();
            SetCursorPosition(52, 1);
            ForegroundColor = Blue;
            Write("██████████████████████████████████████████████████████");
            for (int i = 2; i < 9; i++)
            {
                SetCursorPosition(52, i);
                Write("█");
                SetCursorPosition(105, i);
                Write("█");
            }
            SetCursorPosition(52, 9);
            Write("██████████████████████████████████████████████████████");
            char[] options = { 'o','p','t','i','o','n','s' };
            for (int i = 0; i < options.Length; i++)
            {
                Letters.Print(options[i], 55 + 7 * i, 3);
            }
            ForegroundColor = Gray;
            SetCursorPosition(66, 12);
            WriteLine("Chose colour of Player's avatar");
            SetCursorPosition(66, 14);
            WriteLine("Chose colour of Enemy's avatar");
            SetCursorPosition(66, 16);
            WriteLine("Change Player's avatar");
            SetCursorPosition(66, 18);
            WriteLine("Change Enemy's avatar");
            SetCursorPosition(66, 20);
            WriteLine("Set the Difficulty");
            SetCursorPosition(66, 22);
            WriteLine("Reset ");
            SetCursorPosition(66, 24);
            WriteLine("Exit to main menu");
            SetCursorPosition(48, 27);
            WriteLine("Game has to be restarted to apply some changes ('N' in main menu)");

        }
        public static ConsoleColor ColorChose(int position)
        {
            SetCursorPosition(52, 1);
            ForegroundColor = Blue;
            Write("██████████████████████████████████████████████████████");
            for (int i = 2; i < 9; i++)
            {
                SetCursorPosition(52, i);
                Write("█");
                SetCursorPosition(105, i);
                Write("█");
            }
            SetCursorPosition(52, 9);
            Write("██████████████████████████████████████████████████████");
            char[] options = { 'o', 'p', 't', 'i', 'o', 'n', 's' };
            for (int i = 0; i < options.Length; i++)
            {
                Letters.Print(options[i], 55 + 7 * i, 3);
            }
            for (int i = 47; i < 120; i += 13)
            {
                for (int j = 11; j < 18; j += 2)
                {
                    SetCursorPosition(i, j);
                    Write("              ");
                    SetCursorPosition(i, j + 1);
                    Write("              ");
                    SetCursorPosition(i, j + 2);
                    Write("              ");
                }
            }

            SetCursorPosition(48, 12);
            ForegroundColor = White;       Write("White          ");
            ForegroundColor = Green;       Write("Green          ");
            ForegroundColor = Yellow;      Write("Yellow         ");
            ForegroundColor = Magenta;     Write("Magenta        ");
            ForegroundColor = Red;         Write("Red            ");

            SetCursorPosition(48, 14);
            ForegroundColor = Gray;        Write("Gray           ");
            ForegroundColor = DarkGreen;   Write("Dark Green     ");
            ForegroundColor = DarkYellow;  Write("Dark Yellow    ");
            ForegroundColor = DarkMagenta; Write("Dark Magenta   ");
            ForegroundColor = DarkRed;     Write("Dark Red       ");

            SetCursorPosition(48, 16);
            ForegroundColor = DarkGray;    Write("Dark Gray      ");
            ForegroundColor = DarkBlue;    Write("Dark Blue      ");
            ForegroundColor = Blue;        Write("Blue           ");
            ForegroundColor = DarkCyan;    Write("Dark Cyan      ");
            ForegroundColor = Cyan;        Write("Cyan           ");

            ConsoleColor temp = ColorByInt(position);

            SetCursorPosition(47 + ((position % 5) * 15), 11 + ((position) / 5 * 2));
            ForegroundColor = temp;
            Write("▄▄▄▄▄▄▄▄▄▄▄▄▄▄");
            SetCursorPosition(47 + ((position % 5) * 15), 12 + ((position) / 5 * 2));
            Write("█");
            SetCursorPosition(60 + ((position % 5) * 15), 12 + ((position) / 5 * 2));
            Write("█");
            SetCursorPosition(47 + ((position % 5) * 15), 13 + ((position) / 5 * 2));
            Write("▀▀▀▀▀▀▀▀▀▀▀▀▀▀");

            switch (ReadKey(true).Key)
            {
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    return ColorByInt(position);

                case ConsoleKey.Escape:
                    return White;

                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    position -= 5;
                    if (position < 0)
                    {
                        position += 15;
                    }
                    return ColorChose(position);

                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    position += 5;
                    if (position > 14)
                    {
                        position -= 15;
                    }
                    return ColorChose(position);

                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    position--;
                    if (position < 0)
                    {
                        position = 14;
                    }
                    return ColorChose(position);

                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    position++;
                    if (position > 14)
                    {
                        position = 0;
                    }
                    return ColorChose(position);

                default: return ColorChose(position);
            }
        }
        private static ConsoleColor ColorByInt(int position)
        {
            switch (position)
            {
                case 0: return White;
                case 1: return Green;
                case 2: return Yellow;
                case 3: return Magenta;
                case 4: return Red;
                case 5: return Gray;
                case 6: return DarkGreen;
                case 7: return DarkYellow;
                case 8: return DarkMagenta;
                case 9: return DarkRed;
                case 10: return DarkGray;
                case 11: return DarkBlue;
                case 12: return Blue;
                case 13: return DarkCyan;
                case 14: return Cyan;
                default: return White;
            }
        }
    }
}
