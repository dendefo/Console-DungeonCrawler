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
            SetCursorPosition(49, 13);
            Write("To CONTINUE press ");
            ForegroundColor = ConsoleColor.Green;
            Write("'C'");
            ForegroundColor = ConsoleColor.Gray;
            SetCursorPosition(46, 15);
            Write("To RESTART current level ");
            ForegroundColor = ConsoleColor.Green;
            Write("'R'");
            ForegroundColor = ConsoleColor.Gray;
            SetCursorPosition(49, 17);
            Write("For controls press ");
            ForegroundColor = ConsoleColor.Green;
            Write("'B'");
            ForegroundColor = ConsoleColor.Gray;
            SetCursorPosition(48, 19);
            Write("For castomization ");
            ForegroundColor = ConsoleColor.Green;
            Write("'P'");
            ForegroundColor = ConsoleColor.Gray;
            SetCursorPosition(50, 21);
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
            Console.Clear();
            char[] gameOver = { 'g', 'a', 'm', 'e', ' ', 'o', 'v', 'e', 'r' };
            for (int i = 0; i < gameOver.Length; i++)
            {
                if (gameOver[i] == ' ') continue;
                Letters.Print(gameOver[i], 30 + 7 * i, 11);
            }
        }

        public static void Castomization()
        {

        }
    }
}
