using System.Reflection;

namespace First_Semester_Project.MapLogic
{
    static internal class FileReader
    {
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
            File.WriteAllText(path + "\\TextFiles\\Level_1.txt", "||||||||||||||||||||||||\r\n|                 #    |\r\n|     T        |x||    |\r\n|   ||||       |  | T  |\r\n|   1 X|       |  |    |\r\n|   ||||       |  |    |\r\n|   T  T       | #|  E |\r\n||||||||||||||||||||||||\r\nI3\r\n2<F");
            File.WriteAllText(path + "\\TextFiles\\Level_2.txt", "||||||||||||||||||||||||||||||||||||||||\r\n| E       Sx          1      d         |\r\n|    | |||sx            d  e           |\r\n| #  |    sx1      d      1     #   X  |\r\n||||||||||||||||||||||||||||||||||||||||\r\nIG\r\n2=F2==3<+");
            File.WriteAllText(path + "\\TextFiles\\Level_3.txt", "||||||||||||||||||||||||||\r\n|      E                 |\r\n| xxxx    xxxxxx  xxxxxx |\r\n| xx 1xx  xx  xx  xx#$xx |\r\n| xx# xx  xx# xx  xxxxxx |\r\n| xx  xx  xx 1xx  xx  x  |\r\n| xx||    xxxxxx  xx   x |\r\n|                      e |\r\n| ||xx||  xxxxxx  xx1 xx |\r\n| || 1||  xx1     xxx xx |\r\n| ||x||   xxxx    xxxxxx |\r\n| || X||  xx#     xx xxx |\r\n| ||||||  xxxxxx  xx  xx |\r\n| e                      |\r\n| xxxx    xxxxxx  xxxxxx |\r\n| xx #xx  xx #xx  xx#$xx |\r\n| xx  xx  xx  xx  xxxxxx |\r\n| xx 1xx  xx 1xx  xx  x  |\r\n| xxxx    xxxxxx  xx   x |\r\n||||||||||||||||||||||||||\r\nIIIIIIII\r\n2=+2=+3<+2<I2<+2<+2<+2<+");
            File.WriteAllText(path + "\\TextFiles\\Level_4.txt", "||||||||||||                  ||||||||||\r\n|      E   |                  |        |\r\n|          |                  |   1    |\r\n|e         |                  |       e|\r\n| 1        |                  |   X    |\r\n|e        e|                  |  e     |\r\n|          |                  |        |\r\n|          |                  |        |\r\n|          |                  |        |\r\n|          |                  |        |\r\n|          |                  |        |\r\n|e        e||||||||||||||||||||   e    |\r\n|                d         d           |\r\n|                             1        |\r\n|      1        1                      |\r\n|                d         d           |\r\n||||||||||||||||||||||||||||||||||||||||\r\nI\r\n2=+2=+2=+2=+2=+");
            File.WriteAllText(path + "\\TextFiles\\Level_5.txt", "||||||||||||\r\n| E        |\r\n| x x x x  |\r\n|      ss  |\r\n|  x x xsx |\r\n|sssss  ss |\r\n|sx xsx xsx|\r\n|sss S SssX|\r\n||||||||||||\r\nII\r\nK<2");
        }
    }
}