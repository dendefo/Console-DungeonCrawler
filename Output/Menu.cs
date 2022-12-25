using static System.Console;

namespace First_Semester_Project.Output
{
    internal class Menu
    {
        public static void MainMenu()
        {
            
            Clear();
            SetCursorPosition(15, 1);
            ForegroundColor = ConsoleColor.DarkRed;
            Write("███████████████████████████████████████████████████████████████████████████████████████");
            for (int i = 2; i < 9; i++)
            {
                SetCursorPosition(15, i);
                Write("█");
                SetCursorPosition(101, i);
                Write("█");
            }
            SetCursorPosition(15, 9);
            Write("███████████████████████████████████████████████████████████████████████████████████████");
            char[] theInsanity = { 't', 'h', 'e', ' ', 'i', 'n', 's', 'a', 'n', 'i', 't', 'y' };
            for (int i = 0; i < 12; i++)
            {
                if (i == 3) continue;
                Letters.Print(theInsanity[i], 17 + 7 * i, 3);
            }
            SetCursorPosition(85, 8);
            ForegroundColor = ConsoleColor.Gray;
            Write("Made by Dendefo");
            SetCursorPosition(45, 11);
            Write("To start a NEW game press ");
            ForegroundColor = ConsoleColor.Green;
            Write("'N'");
            ForegroundColor = ConsoleColor.Gray;
            SetCursorPosition(45, 15);
            Write("To CONTINUE press ");
            ForegroundColor = ConsoleColor.Green;
            Write("'C'");
            ForegroundColor = ConsoleColor.Gray;
            SetCursorPosition(45, 13);
            Write("To RESTART current level ");
            ForegroundColor = ConsoleColor.Green;
            Write("'R'");
            ForegroundColor = ConsoleColor.Gray;
            SetCursorPosition(45, 17);
            Write("For controls press ");
            ForegroundColor = ConsoleColor.Green;
            Write("'B'");
            ForegroundColor = ConsoleColor.Gray;
            SetCursorPosition(45, 19);
            Write("For options ");
            ForegroundColor = ConsoleColor.Green;
            Write("'O'");
            ForegroundColor = ConsoleColor.Gray;
            SetCursorPosition(45, 21);
            Write("To Exit press ");
            ForegroundColor = ConsoleColor.Green;
            Write("'Esc'");
            ForegroundColor = ConsoleColor.Gray;
            Data.Tiltan();

        }

        public static void Controls()
        {
            Clear();
            BackgroundColor = ConsoleColor.White;
            ForegroundColor = ConsoleColor.Black;
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
            WriteLine("1. To change Player's avatar");
            WriteLine("2. To change Enemie's avatar");
            WriteLine("3. Set the Difficulty (Medium is standart)");
            WriteLine("4. To chose colour of Player's avatar");
            WriteLine("5. To chose colour of Enemie's avatar");
            WriteLine("6. Reset");
            WriteLine("\nGame has to be restarted to apply the changes ('N' in main menu)\n\n'ESC' to leave this menu");

        }
        public static ConsoleColor ColorChose()
        {
            WriteLine("1. White\n2. Red\n3. Green\n4. Magenta\n5. Gray\n6. Blue\n7. Cyan\n8. Yellow\n9. Dark Green\n0. Dark Red");
            switch (ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    return ConsoleColor.White;
                case ConsoleKey.D2:
                    return ConsoleColor.Red;
                case ConsoleKey.D3:
                    return ConsoleColor.Green;
                case ConsoleKey.D4:
                    return ConsoleColor.Magenta;
                case ConsoleKey.D5:
                    return ConsoleColor.Gray;
                case ConsoleKey.D6:
                    return ConsoleColor.Blue;
                case ConsoleKey.D7:
                    return ConsoleColor.Cyan;
                case ConsoleKey.D8:
                    return ConsoleColor.Yellow;
                case ConsoleKey.D9:
                    return ConsoleColor.DarkGreen;
                case ConsoleKey.D0:
                    return ConsoleColor.DarkRed;
                default: return ConsoleColor.White;
            }
        }
    }
}
