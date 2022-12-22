using static System.Console;

namespace First_Semester_Project.Output
{
    static internal class Letters
    {
        public static void Print(char letter, int x, int y)
        {
            switch (letter)
            {
                case '9':
                    Number9(x, y); 
                    break;
                case 'a':
                    LetterA(x, y);
                    break;
                case 'b':
                    LetterB(x, y);
                    break;
                case 'c':
                    LetterC(x, y);
                    break;
                case 'd':
                    LetterD(x, y);
                    break;
                case 'e':
                    LetterE(x, y);
                    break;
                case 'f':
                    LetterF(x, y);
                    break;
                case 'g':
                    LetterG(x, y);
                    break;
                case 'h':
                    LetterH(x, y);
                    break;
                case 'i':
                    LetterI(x, y);
                    break;
                case 'j':
                    LetterJ(x, y);
                    break;
                case 'k':
                    LetterK(x, y);
                    break;
                case 'l':
                    LetterL(x, y);
                    break;
                case 'm':
                    LetterM(x, y);
                    break;
                case 'n':
                    LetterN(x, y);
                    break;
                case 'o':
                    LetterO(x, y);
                    break;
                case 'p':
                    LetterP(x, y);
                    break;
                case 'q':
                    LetterQ(x, y);
                    break;
                case 'r':
                    LetterR(x, y);
                    break;
                case 's':
                    LetterS(x, y);
                    break;
                case 't':
                    LetterT(x, y);
                    break;
                case 'u':
                    LetterV(x, y);
                    break;
                case 'v':
                    LetterV(x, y);
                    break;
                case 'w':
                    LetterW(x, y);
                    break;
                case 'x':
                    LetterX(x, y);
                    break;
                case 'y':
                    LetterY(x, y);
                    break;
                case 'Z':
                    LetterZ(x, y);
                    break;
            }
        }
        public static void Button(char letter, int x, int y)
        {

            for (int i = 0; i < 7; i++)
            {
                SetCursorPosition(x - 2, y + i);
                Write("              ");
            }

            Print(letter, x + 2, y + 1);
        }
        static void Number9(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██████");
            SetCursorPosition(x, y + 1);
            Write("██  ██");
            SetCursorPosition(x, y + 2);
            Write("██████");
            SetCursorPosition(x, y + 3);
            Write("    ██");
            SetCursorPosition(x, y + 4);
            Write("██████");
        }
        static void LetterA(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██████");
            SetCursorPosition(x, y + 1);
            Write("██  ██");
            SetCursorPosition(x, y + 2);
            Write("██████");
            SetCursorPosition(x, y + 3);
            Write("██  ██");
            SetCursorPosition(x, y + 4);
            Write("██  ██");
        }
        static void LetterB(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██████");
            SetCursorPosition(x, y + 1);
            Write("██  ██");
            SetCursorPosition(x, y + 2);
            Write("████");
            SetCursorPosition(x, y + 3);
            Write("██  ██");
            SetCursorPosition(x, y + 4);
            Write("██████");
        }
        static void LetterC(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██████");
            SetCursorPosition(x, y + 1);
            Write("██");
            SetCursorPosition(x, y + 2);
            Write("██");
            SetCursorPosition(x, y + 3);
            Write("██");
            SetCursorPosition(x, y + 4);
            Write("██████");
        }
        static void LetterD(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("████");
            SetCursorPosition(x, y + 1);
            Write("██  ██");
            SetCursorPosition(x, y + 2);
            Write("██  ██");
            SetCursorPosition(x, y + 3);
            Write("██  ██");
            SetCursorPosition(x, y + 4);
            Write("████");
        }
        static void LetterE(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██████");
            SetCursorPosition(x, y + 1);
            Write("██");
            SetCursorPosition(x, y + 2);
            Write("██████");
            SetCursorPosition(x, y + 3);
            Write("██");
            SetCursorPosition(x, y + 4);
            Write("██████");
        }
        static void LetterF(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██████");
            SetCursorPosition(x, y + 1);
            Write("██");
            SetCursorPosition(x, y + 2);
            Write("████");
            SetCursorPosition(x, y + 3);
            Write("██");
            SetCursorPosition(x, y + 4);
            Write("██");
        }
        static void LetterG(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██████");
            SetCursorPosition(x, y + 1);
            Write("██");
            SetCursorPosition(x, y + 2);
            Write("██  ██");
            SetCursorPosition(x, y + 3);
            Write("██   █");
            SetCursorPosition(x, y + 4);
            Write("██████");
        }
        static void LetterH(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██  ██");
            SetCursorPosition(x, y + 1);
            Write("██  ██");
            SetCursorPosition(x, y + 2);
            Write("██████");
            SetCursorPosition(x, y + 3);
            Write("██  ██");
            SetCursorPosition(x, y + 4);
            Write("██  ██");
        }
        static void LetterI(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██████");
            SetCursorPosition(x, y + 1);
            Write("  ██");
            SetCursorPosition(x, y + 2);
            Write("  ██");
            SetCursorPosition(x, y + 3);
            Write("  ██");
            SetCursorPosition(x, y + 4);
            Write("██████");
        }
        static void LetterJ(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("  ████");
            SetCursorPosition(x, y + 1);
            Write("    ██");
            SetCursorPosition(x, y + 2);
            Write("    ██");
            SetCursorPosition(x, y + 3);
            Write("██  ██");
            SetCursorPosition(x, y + 4);
            Write("██████");
        }
        static void LetterK(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██   █");
            SetCursorPosition(x, y + 1);
            Write("██  █");
            SetCursorPosition(x, y + 2);
            Write("████");
            SetCursorPosition(x, y + 3);
            Write("██  ██");
            SetCursorPosition(x, y + 4);
            Write("██   █");
        }
        static void LetterL(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██");
            SetCursorPosition(x, y + 1);
            Write("██");
            SetCursorPosition(x, y + 2);
            Write("██");
            SetCursorPosition(x, y + 3);
            Write("██");
            SetCursorPosition(x, y + 4);
            Write("██████");
        }
        static void LetterM(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██  ██");
            SetCursorPosition(x, y + 1);
            Write("█ ██ █");
            SetCursorPosition(x, y + 2);
            Write("█ ██ █");
            SetCursorPosition(x, y + 3);
            Write("█    █");
            SetCursorPosition(x, y + 4);
            Write("█    █");
        }
        static void LetterN(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██  ██");
            SetCursorPosition(x, y + 1);
            Write("███  █");
            SetCursorPosition(x, y + 2);
            Write("██ █ █");
            SetCursorPosition(x, y + 3);
            Write("██ ███");
            SetCursorPosition(x, y + 4);
            Write("██  ██");
        }
        static void LetterO(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██████");
            SetCursorPosition(x, y + 1);
            Write("██  ██");
            SetCursorPosition(x, y + 2);
            Write("██  ██");
            SetCursorPosition(x, y + 3);
            Write("██  ██");
            SetCursorPosition(x, y + 4);
            Write("██████");
        }
        static void LetterP(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██████");
            SetCursorPosition(x, y + 1);
            Write("██  ██");
            SetCursorPosition(x, y + 2);
            Write("██████");
            SetCursorPosition(x, y + 3);
            Write("██    ");
            SetCursorPosition(x, y + 4);
            Write("██    ");
        }
        static void LetterQ(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██████");
            SetCursorPosition(x, y + 1);
            Write("██  ██");
            SetCursorPosition(x, y + 2);
            Write("██  ██");
            SetCursorPosition(x, y + 3);
            Write("██████");
            SetCursorPosition(x, y + 4);
            Write("    ██");
        }
        static void LetterR(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██████");
            SetCursorPosition(x, y + 1);
            Write("██  ██");
            SetCursorPosition(x, y + 2);
            Write("████  ");
            SetCursorPosition(x, y + 3);
            Write("██  ██");
            SetCursorPosition(x, y + 4);
            Write("██  ██");
        }
        static void LetterS(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██████");
            SetCursorPosition(x, y + 1);
            Write("██");
            SetCursorPosition(x, y + 2);
            Write("██████");
            SetCursorPosition(x, y + 3);
            Write("    ██");
            SetCursorPosition(x, y + 4);
            Write("██████");
        }
        static void LetterT(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██████");
            SetCursorPosition(x, y + 1);
            Write("  ██  ");
            SetCursorPosition(x, y + 2);
            Write("  ██  ");
            SetCursorPosition(x, y + 3);
            Write("  ██  ");
            SetCursorPosition(x, y + 4);
            Write("  ██  ");
        }
        static void LetterU(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██  ██");
            SetCursorPosition(x, y + 1);
            Write("██  ██");
            SetCursorPosition(x, y + 2);
            Write("██  ██");
            SetCursorPosition(x, y + 3);
            Write("██  ██");
            SetCursorPosition(x, y + 4);
            Write("██████");
        }
        static void LetterV(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("█    █");
            SetCursorPosition(x, y + 1);
            Write("█    █");
            SetCursorPosition(x, y + 2);
            Write(" █  █");
            SetCursorPosition(x, y + 3);
            Write(" █  █");
            SetCursorPosition(x, y + 4);
            Write("  ██");
        }
        static void LetterW(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("█    █");
            SetCursorPosition(x, y + 1);
            Write("█    █");
            SetCursorPosition(x, y + 2);
            Write("█ ██ █");
            SetCursorPosition(x, y + 3);
            Write("█ ██ █");
            SetCursorPosition(x, y + 4);
            Write(" █  █ ");
        }
        static void LetterX(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██  ██");
            SetCursorPosition(x, y + 1);
            Write(" ████");
            SetCursorPosition(x, y + 2);
            Write("  ██");
            SetCursorPosition(x, y + 3);
            Write(" ████");
            SetCursorPosition(x, y + 4);
            Write("██  ██");
        }
        static void LetterY(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██  ██");
            SetCursorPosition(x, y + 1);
            Write(" ████");
            SetCursorPosition(x, y + 2);
            Write("  ██");
            SetCursorPosition(x, y + 3);
            Write("  ██");
            SetCursorPosition(x, y + 4);
            Write("  ██");
        }
        static void LetterZ(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("██████");
            SetCursorPosition(x, y + 1);
            Write("    ██");
            SetCursorPosition(x, y + 2);
            Write("  ██");
            SetCursorPosition(x, y + 3);
            Write("██");
            SetCursorPosition(x, y + 4);
            Write("██████");
        }
    }
}
