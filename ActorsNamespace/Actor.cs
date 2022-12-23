namespace First_Semester_Project.ActorsNamespace
{
    //Anything interactable is called Actor.
    //All the Actor Childs are represented in this namespace
    internal class Actor
    {
        //This are used only by Enemy and Player
        public Weapon EquipedWeapon { get; protected set; }
        public Shield EquipedShield { get; protected set; }


        public int MaxHP { get; protected set; }
        public int CurrentHP { get; protected set; }
        public int Evasion { get; protected set; }


        // Square that the Actor is standing on (Not showed)
        public Square StandsOn { get; set; }


        //Square of the Actor (Showing)
        public Square ActorsSquare { get; protected set; }


        //Coordination System and moving
        public int XCoordinate { get; protected set; }
        public int YCoordinate { get; protected set; }
        public void Move(int deltaY, int deltaX, Square stansdOn)
        {
            XCoordinate += deltaX;
            YCoordinate += deltaY;
            StandsOn = stansdOn;
        }


        //Constractor
        public Actor(int xCoordinate, int yCoordinate)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }



        //Damage-Dealing system
        public int Attack(Actor defender)
        {
            Random rand = new Random();

            if (rand.Next(0, 101) > EquipedWeapon.HitChance) return 0; //Checking if Attack is Unsuccesfull
            else return defender.BlockCheck(EquipedWeapon.Damage); // If Succesfull
        } //Checking for Attack Chance
        private int BlockCheck(int dmg)
        {
            Random rand = new Random();
            if (rand.Next(0, 101) < EquipedShield.BlockChance) //Checking if damage is blocked by shield
            {
                if (dmg - EquipedShield.Block <= 0) return 0; //if damage after blocking is less then zero, then no damage
                return GetHit(dmg - EquipedShield.Block); //if damage grater than zero, then:
            }
            else return GetHit(dmg); //If not blocked
        } //Checking for block Chance
        private int GetHit(int dmg)
        {
            Random rand = new Random();
            if (rand.Next(0, 101) < Evasion) return 0; //If Evaded
            int temp = CurrentHP;

            if (CurrentHP <= dmg) CurrentHP = 0;
            else DealDamage(dmg); //Dealing Damage

            return temp - CurrentHP;
        } //Checking for Evading
        private void DealDamage(int damage)
        {
            CurrentHP -= damage;
        }

        public static void Battle(Map map , Enemy enemy, int y, int deltaY, int x, int deltaX, bool isPlayerAttacked)
        {

            if (isPlayerAttacked)
            {
                int userD = map.User.Attack(enemy);
                if (enemy.CurrentHP == 0)
                {
                    map.User.Killed(enemy);
                    map.ChangeSquare(new Square(SquareTypes.Chest, x+deltaX,y+deltaY, enemy.Die()), y + deltaY, x + deltaX);
                    map.Log.action = $"Enemy died and dropped chest with {((Chest)map.MapArray[y + deltaY][x+deltaX].ActorOnSquare).Inside.Name} for you";
                    return;
                }
                map.Log.action = $"You dealed {userD} damage to the enemy. Now his HP is {enemy.CurrentHP}";
                return;
            }

            int enemyD = enemy.Attack(map.User);
            map.Log.action2 = $"Enemy atacked you with his {enemy.EquipedWeapon.Name} and dealed {enemyD} damage.";
        }
    }
}
