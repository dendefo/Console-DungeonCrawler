﻿using static System.Console;
using static System.ConsoleColor;

namespace First_Semester_Project.Output
{
    static class Menu
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
            PrintWord("the insanity", 37, 3);

            SetCursorPosition(105, 8);
            ForegroundColor = Gray;
            Write("Made by Dendefo");

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
            PixelArt.Tiltan();

        }
        public static void PrintCursor(int position, int startX, int startY)
        {
            for (int i = startY; i <= 29; i += 2)
            {
                SetCursorPosition(startX, i);
                Write(" ");
            }
            SetCursorPosition(startX, startY + position * 2);
            ForegroundColor = startX < 70 ? Blue : Green;
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
            PrintWord("game over", 55, 3);

            PrintCursor(0, 71, 11);
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
        }

        public static void OptionsMenu()
        {
            Clear();
            SetCursorPosition(54, 1);
            ForegroundColor = Blue;
            Write("██████████████████████████████████████████████████████");
            for (int i = 2; i < 9; i++)
            {
                SetCursorPosition(54, i);
                Write("█");
                SetCursorPosition(107, i);
                Write("█");
            }
            SetCursorPosition(54, 9);
            Write("██████████████████████████████████████████████████████");

            PrintWord("options", 57, 3);

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
            WriteLine("Game has to be restarted to apply some changes ('Start new game' in main menu)");

        }
        public static void Market(Player player, Data log)
        {

            Clear();
            log.CoinCancelToken = new();
            Task.Factory.StartNew(() =>
            {
                int count = 0;
                while (true)
                {
                    lock (log)
                    {
                        if (log.CoinCancelToken.IsCancellationRequested)
                        {
                            
                            break;
                        }

                        PixelArt.Coin(5, 3, count);
                        PixelArt.Coin(135, 3, count);

                    }
                    count++;
                    count %= 8;
                    Thread.Sleep(150);

                }
                lock (log)ResetColor();


            }, log.CoinCancelToken.Token);

            lock (log)
            {
                PrintWord("mystery shack", 35, 3);
                PixelArt.Sword(36, 13);
                PixelArt.Shield(70, 12);
                PixelArt.Potion(106, 12);
            }



            ReadKey(true);

            log.CoinCancelToken.Cancel();
            lock (log) Clear();

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
            ForegroundColor = White; Write("White          ");
            ForegroundColor = Green; Write("Green          ");
            ForegroundColor = Yellow; Write("Yellow         ");
            ForegroundColor = Magenta; Write("Magenta        ");
            ForegroundColor = Red; Write("Red            ");

            SetCursorPosition(48, 14);
            ForegroundColor = Gray; Write("Gray           ");
            ForegroundColor = DarkGreen; Write("Dark Green     ");
            ForegroundColor = DarkYellow; Write("Dark Yellow    ");
            ForegroundColor = DarkMagenta; Write("Dark Magenta   ");
            ForegroundColor = DarkRed; Write("Dark Red       ");

            SetCursorPosition(48, 16);
            ForegroundColor = DarkGray; Write("Dark Gray      ");
            ForegroundColor = DarkBlue; Write("Dark Blue      ");
            ForegroundColor = Blue; Write("Blue           ");
            ForegroundColor = DarkCyan; Write("Dark Cyan      ");
            ForegroundColor = Cyan; Write("Cyan           ");

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
        private static void PrintWord(string word, int startX, int startY)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == ' ') continue;
                Letters.Print(word[i], startX + 7 * i, startY);
            }
        }
    }
}
