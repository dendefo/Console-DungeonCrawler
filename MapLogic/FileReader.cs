using System.Reflection.Emit;

namespace First_Semester_Project.MapLogic
{
    static internal class FileReader
    {
        static public string[] Read(int Level)
        {
            return File.ReadAllLines($"C:\\Users\\danie\\source\\repos\\First_Semester_Project\\TextFiles\\Level_{Level}.txt");
        }

        static public string[] ReadSpawnConfig(int Level)
        {
            return File.ReadAllLines($"C:\\Users\\danie\\source\\repos\\First_Semester_Project\\TextFiles\\Level_{Level}_SpawnConfig.txt");
        }
        //static public void Write(int stringNumber, char symbol, ConsoleColor color) 
        //{
        //    FileStream file = File.OpenWrite($"C:\\Users\\danie\\source\\repos\\First_Semester_Project\\TextFiles\\Config.txt");
        //    file[stringNumber] = ((int)symbol).ToString();
        //    file[stringNumber+1] = ((int)color).ToString();
        //    file.
        //} TODO config file editor to make customization
    }
}