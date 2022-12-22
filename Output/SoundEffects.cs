using static System.Console;

namespace First_Semester_Project.Output
{

    internal class SoundEffects
    {
        const int c = 261;
        const int d = 294;
        const int e = 329;
        const int f = 349;
        const int g = 391;
        const int gS = 415;
        const int a = 440;
        const int aS = 455;
        const int b = 466;
        const int cH = 523;
        const int cSH = 554;
        const int dH = 587;
        const int dSH = 622;
        const int eH = 659;
        const int fH = 698;
        const int fSH = 740;
        const int gH = 784;
        const int gSH = 830;
        const int aH = 880;
        public static void Heal()
        {
            Beep(440, 75);
            Beep(480, 75);
            Beep(520, 75);
            Beep(560, 75);
            Beep(600, 75);
        }
        public static void Attack() { Beep(150, 75); }
        public static void NewLevel() { Beep(220, 150); Beep(550, 300); }
        public static void MainTheme()
        {
            /*
            Beep(a, 500);
            Beep(a, 500);
            Beep(a, 500);
            Beep(f, 350);
            Beep(cH, 150);

            Beep(a, 500);
            Beep(f, 350);
            Beep(cH, 150);
            Beep(a, 500);
            //Thread.Sleep(1000);

            Beep(eH, 500);
            Beep(eH, 500);
            Beep(eH, 500);
            Beep(fH, 350);
            Beep(cH, 150);

            Beep(gS, 500);
            Beep(f, 350);
            Beep(cH, 150);
            Beep(a, 500);
            //Thread.Sleep(1000);

            Beep(aH, 500);
            Beep(a, 350);
            Beep(a, 150);
            Beep(aH, 500);
            Beep(gSH, 350);
            Beep(gH, 125);

            Beep(fSH, 125);
            Beep(fH, 125);
            Beep(fSH, 250);
            Thread.Sleep(500);
            Beep(aS, 250);
            Beep(dSH, 500);
            Beep(dH, 350);
            Beep(cSH, 125);
            */
        }
    }
}
