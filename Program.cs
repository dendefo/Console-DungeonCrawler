global using First_Semester_Project.ActorsNamespace;
global using First_Semester_Project.ItemsNamespace;
global using First_Semester_Project.Output;
global using First_Semester_Project.MapLogic;
global using First_Semester_Project.Core;

namespace First_Semester_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.SetUp();
            Menu.MainMenu();
            Menu.PrintCursor(0,51,11);
            Game game = new();

            game.Run();
        }
    }
}
