using static System.Console;

namespace First_Semester_Project.Output
{
    internal class GraphicEngine
    {
        private List<ConsoleColor> Colours = new();
        private string _print = "";
        public void Push(ConsoleColor printingColor, char symbol)
        {
            Colours.Add(printingColor);
            _print += symbol;
        }
        public void Print() //I tried to find a way for more smooth printing. 
        {
            string toPrint = "";
            int newline = 4;
            SetCursorPosition(40, newline);
            for (int i = 0; i < Colours.Count - 1; i++)
            {
                if (Colours[i] == ConsoleColor.Black)
                {
                    newline++;
                    SetCursorPosition(40, newline);
                    continue;
                }
                
                if (Colours[i] == Colours[i + 1])
                {
                    toPrint += _print[i];
                }
                else
                {
                    toPrint += _print[i];
                    ForegroundColor = Colours[i];
                    Write(toPrint);
                    toPrint = "";
                }
            }
            ForegroundColor = Colours[Colours.Count - 1];
            Write(toPrint);

        }

        static public void RePrint(int x, int y, Square ent)//Now Each update looks like this. Much faster and clear but there is a bug, when something may double print on a map
        {
            SetCursorPosition(x, y);
            ForegroundColor = ent.Color;
            Write(ent.Symbol);

            SetCursorPosition(0, 20);
            ResetColor();
        }
    }
}