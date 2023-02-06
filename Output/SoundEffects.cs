using static System.Console;

namespace First_Semester_Project.Output
{
    //Not too much of a sounds here, but Console.Beep can't allow to play more than one sound at a time, so i'm okay with 3 sounds
    internal class SoundEffects
    {
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
    }
}
