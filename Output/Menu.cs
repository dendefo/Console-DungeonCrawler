using static System.Console;
using static System.ConsoleColor;

namespace First_Semester_Project.Output
{
    //A lot of visual outputs that are not inGame HUD. All the menus and Shop
    //I don't gonna explain about each thing inside each function, only important points
    static class Menu
    {
        static Dictionary<string, int> Prices = new Dictionary<string, int>() { { "Sword", 3},{"Axe", 7},{"Nunchucks", 12},
            { "Buckler",3},{"Robust Shield",7 },{"Kite Shield",12 },
            {"Small Healing Potion",2 },{"Healing Potion",3 },{"Great Healing Potion",4 },{"Exploasive Potion",5 },
            {"Invisibility Potion",10 },{"Hawk Eye Potion",5 },{"Potion of Accuracy",12 },{"Potion of Invincibility",15 } };

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
            PrintWord("code s crypt", 37, 3);

            SetCursorPosition(105, 8);
            ForegroundColor = Gray;
            Write("Made by Dendefo");

            SetCursorPosition(73, 11);
            Write("Start new game");
            SetCursorPosition(73, 15);
            Write("Continue");
            SetCursorPosition(73, 13);
            Write("Restart the level");
            SetCursorPosition(73, 17);
            Write("Controls");
            SetCursorPosition(73, 19);
            Write("Options");
            SetCursorPosition(73, 21);
            Write("Credits");
            SetCursorPosition(73, 23);
            Write("Exit");
            //It took me too much time and i shouldn't do this pixel arts in console again
            PixelArt.Tiltan();
            PixelArt.Sword(8, 2);
            PixelArt.Shield(132, 2);

        }

        //For menu navigation
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

        public static void Credits()
        {
            Clear();
            ForegroundColor = Blue;
            PrintWord("student", 46, 2);
            PrintWord("teacher", 46, 17);
            ForegroundColor = Green;
            PrintWord("dendefo", 56, 9);
            PrintWord("dor ben dor", 56, 24); //I needed to write something in Credits, but my nickname, didn't i?
            ReadKey(true);
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
            Letters.Button('o', 60, 10);
            Letters.Button('9', 75, 10);
            Letters.Button('e', 60, 21);
            Letters.Button('s', 70, 21);
            Letters.Button('c', 80, 21);
            ResetColor();
            ReadKey(true);
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
            Write("Credits");
            SetCursorPosition(73, 23);
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

        //Best name that i can come up with
        private static void SomeMarketText(Data log)
        {
            lock (log)
            {
                Clear();
                PrintWord("mystery shack", 35, 3);
                ForegroundColor = Green;
                SetCursorPosition(71, 13);
                Write("Welcome, Stranger!");
                SetCursorPosition(45, 15);
                Write("My name is Dor Vendor and i am the only one Merchant in this dungeon. ");
                SetCursorPosition(45, 17);
                Write("I have all sort of weapons shields and potions, if you want some, but ");
                SetCursorPosition(45, 19);
                Write("  I am also looking forward to buy anything you've found down there. ");
                SetCursorPosition(69, 21);
                Write("What you'd like to do?");
                ForegroundColor = White;
                SetCursorPosition(42, 25);
                Write("I want to buy some items");
                SetCursorPosition(92, 25);
                Write("I want to sell something from my bag");
            }
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
                lock (log) ResetColor();


            }, log.CoinCancelToken.Token); //Coin Print

            SomeMarketText(log);

            int position = 0;
            while (true)
            {
                lock (log)
                {
                    for (int i = 40; i <= 100; i += 50)
                    {
                        SetCursorPosition(i, 25);
                        Write(" ");
                    }

                    SetCursorPosition(40 + position * 50, 25);
                    ForegroundColor = Green;
                    Write("►");
                    ForegroundColor = Gray;
                }
                switch (ReadKey(true).Key)
                {
                    case ConsoleKey.Escape:
                        log.CoinCancelToken.Cancel();
                        lock (log) Clear();
                        return;

                    case ConsoleKey.D:
                        position++;
                        if (position > 1) position = 0;
                        break;

                    case ConsoleKey.A:
                        position--;
                        if (position < 0) position = 1;
                        break;

                    case ConsoleKey.Enter:
                        switch (position)
                        {
                            case 0:
                                Buy(player, log);
                                break;
                            case 1:
                                Sell(player, log);
                                break;
                        }

                        SomeMarketText(log);
                        break;

                }
            }
        }

        private static void Buy(Player player, Data log)
        {
            lock (log)
            {
                Clear();
                SetCursorPosition(55, 9);
                Write($"You have {player.Coins} money");
                SetCursorPosition(55, 11);
                Write("You looked at the counter and saw this:");
            }
            int x = 40;
            int y = 15;
            foreach (string item in Prices.Keys.ToArray())
            {
                lock (log)
                {
                    SetCursorPosition(x, y);
                    Write($"{item} {Prices[item]*2}$");
                }
                x += 30;
                if (x > 100)
                {
                    y += 2;
                    x = 40;
                }
            }
            int position = 0;
            while (true)
            {


                for (int i = 0; i < Prices.Keys.Count; i++)
                {
                    lock (log)
                    {
                        SetCursorPosition(38 + i % 3 * 30, 15 + i / 3 * 2);
                        ForegroundColor = Green;
                        Write(position == i ? "►" : " ");
                        ForegroundColor = Gray;
                    }
                }

                switch (ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        if (player.Coins < Prices.Values.ToArray()[position]*2) return;
                        player.GiveItem(Item.NameParser(Prices.Keys.ToArray()[position]));
                        player.TakeItem(new Coin(), Prices.Values.ToArray()[position] * 2);
                        return;

                    case ConsoleKey.D:
                        position++;
                        if (position >= Prices.Keys.Count) position = 0;
                        break;
                    case ConsoleKey.A:
                        position--;
                        if (position < 0) position = Prices.Keys.Count - 1;
                        if (position < 0) position = 0;
                        break;
                    case ConsoleKey.W:
                        position -= 3;
                        if (position < 0) position = Prices.Keys.Count + 1 + position;
                        break;
                    case ConsoleKey.S:
                        position += 3;
                        if (position >= Prices.Keys.Count) position -= ((Prices.Keys.Count / 3) + 1) * 3;
                        break;
                    default:
                        return;
                }
                if (position == Prices.Keys.Count) position--;
                if (position < 0) position = 0;
            }
        }
        private static void Sell(Player player, Data log)
        {
            lock (log)
            {
                Clear();
                SetCursorPosition(55, 9);
                Write($"You have {player.Coins} money");
                SetCursorPosition(55, 11);
                Write("You looked in your backpack an found this:");
            }
            int x = 40;
            int y = 15;
            foreach (Item item in player.Inventory.Keys)
            {
                lock (log)
                {
                    SetCursorPosition(x, y);
                    Write($"{player.Inventory[item]} {item.Name} {Prices[item.Name]}$");
                }
                x += 30;
                if (x > 100)
                {
                    y += 2;
                    x = 40;
                }
            }
            int position = 0;
            while (true)
            {
                for (int i = 0; i < player.Inventory.Keys.Count; i++)
                {
                    lock (log)
                    {
                        ForegroundColor = Green;
                        SetCursorPosition(38 + i % 3 * 30, 15 + i / 3 * 2);
                        Write(position==i? "►" : " ");
                        ForegroundColor = Gray;
                    }
                }
                switch (ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        if (player.Inventory.Keys.Count == 0) return;
                        player.GiveItem(new Coin(), Prices[player.Inventory.Keys.ToArray()[position].Name]);
                        player.TakeItem(player.Inventory.Keys.ToArray()[position], 1);
                        return;
                    case ConsoleKey.D:
                        position++;
                        if (position >= player.Inventory.Keys.Count) position = 0;
                        break;
                    case ConsoleKey.A:
                        position--;
                        if (position < 0) position = player.Inventory.Keys.Count - 1;
                        if (position < 0) position = 0;
                        break;
                    case ConsoleKey.W:
                        position -= 3;
                        if (position < 0) position = player.Inventory.Keys.Count + 1 + position;
                        break;
                    case ConsoleKey.S:
                        position += 3;
                        if (position >= player.Inventory.Keys.Count) position -= ((player.Inventory.Keys.Count / 3) + 1) * 3;
                        break;
                    default:
                        return;
                }
                if (position == player.Inventory.Keys.Count) position--;
                if (position < 0) position = 0;
            }
        }

        //I love how this option window looks like.
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
