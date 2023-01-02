using System.Reflection;

namespace First_Semester_Project.MapLogic
{
    static internal class FileReader
    {
        static string[] levels = { };
        static string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName).FullName).FullName;

        //This function reads levels from files
        static public string[] Read(int Level)
        {
            if (!Directory.Exists(path + "\\TextFiles"))
            {
                CreateFiles();
            }
            return File.ReadAllLines($"{path}\\TextFiles\\Level_{Level}.txt");
        }
        //If somehow there is no files, this one creates them
        static private void CreateFiles()
        {
            Directory.CreateDirectory(path + "\\TextFiles");
            File.WriteAllText(path + "\\TextFiles\\Level_1.txt", "||||||||||||||||||||||||\r\n|                 #   $|\r\n|          d   |x||    |\r\n|   ||||       |  | T  |\r\n|   1 X|       |  |    |\r\n|   ||||       |  |    |\r\n|              | #|  E |\r\n||||||||||||||||||||||||\r\nI3\r\n3<+");
            File.WriteAllText(path + "\\TextFiles\\Level_2.txt", "||||||||||||||||||||||||||||||||||||||||\r\n| E     #  x      x              1     |\r\n|    | ||| x     1x                    |\r\n| #  |     x1     x         1       X  |\r\n||||||||||||||||||||||||||||||||||||||||\r\nII\r\n2=+2==3<+2<+");
            File.WriteAllText(path + "\\TextFiles\\Level_3.txt", "||||||||||||||||||||||||||||||||||||||||\r\n|      E                               |\r\n| xxxx    xxxxxx  xxxxxx               |\r\n| xx 1xx  xx  xx  xx# xx               |\r\n| xx# xx  xx# xx  xxxxxx           X   |\r\n| xx  xx  xx 1xx  xx  x                |\r\n| xxxx    xxxxxx  xx   x               |\r\n|                                      |\r\n| xxxxxx  xxxxxx  xx1 xx               |\r\n| xx #xx  xx1     xxx xx               |\r\n| xxxxx   xxxx    xxxxxx               |\r\n| xx 1xx  xx#     xx xxx               |\r\n| xxxxxx  xxxxxx  xx  xx               |\r\n|                                      |\r\n| xxxx    xxxxxx  xxxxxx               |\r\n| xx #xx  xx #xx  xx# xx               |\r\n| xx  xx  xx  xx  xxxxxx               |\r\n| xx 1xx  xx 1xx  xx  x                |\r\n| xxxx    xxxxxx  xx   x               |\r\n||||||||||||||||||||||||||||||||||||||||\r\nIIIIIIII\r\n2=+2=+3<+2<+2<+2<+2<+2<+");
            File.WriteAllText(path + "\\TextFiles\\Level_4.txt", "||||||||||||||||||||||||||||||||||||||||\r\n|      E                               |\r\n|              #           1           |\r\n| 1                                    |\r\n| ||                               X   |\r\n||||||||||||||||||||||||||||||||||||||||\r\nI\r\n2=+2=+");
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