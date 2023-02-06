using static System.Console;

namespace First_Semester_Project.Output
{
    internal class GraphicEngine
    {
        private List<ConsoleColor> Colours = new();
        private string _print = "";
        //Add object for print
        public void Push(ConsoleColor printingColor, char symbol)
        {
            Colours.Add(printingColor);
            _print += symbol;
        }
        //Just prints all the objects little bit faster
        public void Print() 
        {
            ClearMap();
            
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

        //Had some visual bug and didn't found the solution, so each turn i clear the map
        private void ClearMap()
        {
            for (int i = 0; i < 10; i++)
            {
                SetCursorPosition(40, 4 + i);
                Write("                  \n");
            }
        }

        //Now Each update looks like this. Much faster and clear but there is a bug, when something may double print on a map

        //Comment above was me At December. Now it is me at February. To implement this function back i need to rewrite half of the Map.Refresh function. 
        //I don't want to do it. Map refreshing don't take too mush time, cause map is now in small window. All the HUD takes more CPU
        static public void RePrint(int x, int y, Square ent)
        {
            SetCursorPosition(x, y);
            ForegroundColor = ent.Color;
            Write(ent.Symbol);

            SetCursorPosition(0, 20);
            ResetColor();
        }
    }
}