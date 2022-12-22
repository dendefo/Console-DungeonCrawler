namespace First_Semester_Project.MapLogic
{
    internal class CollisionLogic
    {
        BlaBla[] bla = {new("baka"),new("sussy"),new("Wolt"),new("JESSY")};


        void Lala()
        {
            BlaBla[][] lalala = { bla,bla,bla };

            for (int i = 0; i< lalala.Length; i++)
            {
                for (int j = 0; j < lalala[i].Length; j++)
                {
                    Console.Write(lalala[i][j].Name);
                }
                Console.WriteLine();
            }
        }
    }
    class BlaBla
    {
        public string Name;
        public string Surname;
        public string Profession;
        public BlaBla(string name)
        {
            Name = name;
        }
    }
}

/*
 * Hello
 * Fucking
 * World
 * !
 * 
 * Console.WriteLine(lalala[i]);
 * 
 * HelloFuckingWorld!
 */