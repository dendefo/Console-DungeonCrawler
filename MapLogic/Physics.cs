using First_Semester_Project.ActorsNamespace;

namespace First_Semester_Project.MapLogic
{
    static class Physics
    {
        static public void CollisionCheck(Map map, Coordinates delta, Actor actor)
        {
            if (actor == null) return;

            Coordinates coor = actor.Coor;
            Coordinates newCoor = coor + delta;


            //This Switch-case block is checking what to do with actor by the entity that he is going to touch

            switch (map[newCoor].Entity)
            {
                case SquareTypes.Empty: //If Actor steps on Empty square
                    map.MoveActorOnMap(actor, coor, delta); // Moving the actor
                    break;

                case SquareTypes.Coin:
                    map[newCoor].MakeEmpty(); //Getting coin
                    map.MoveActorOnMap(actor, coor, delta); //Moving the actor

                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break; //if it is player
                    ((Player)actor).GiveItem(new Coin()); // Give him coin
                    break;

                case SquareTypes.Exit:
                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall) // If this is spyke
                    {
                        ((Spike)actor).ChangeDirection(); 
                        CollisionCheck(map, -delta, actor);
                    }
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break; //If it is player


                    //Most ugly way to do it, but i needed 6-th Easy feature
                    foreach (Enemy enemy in map.Enemies)
                    {
                        if (enemy != null)
                        {
                            if (enemy.CurrentHP != 0) { map.Log.Logs.Enqueue(new("You must kill every enemy", ConsoleColor.Red)); return; }
                        }
                    }
                    foreach(Snake snake in map.SnakesHeads)
                    {
                        if (snake != null)
                        {
                            if (snake.CurrentHP != 0) { map.Log.Logs.Enqueue(new("You must kill every enemy", ConsoleColor.Red)); return; }
                        }
                    }


                    map.MoveActorOnMap(actor, coor, delta); //Move player on map
                    map.Log.Logs.Enqueue(new("You moved to the next level! Yay")); //Output about 
                    Task.Run(SoundEffects.NewLevel);
                    break;

                case SquareTypes.Enemy:
                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall) //If this is spyke
                    {
                        ((Spike)actor).ChangeDirection(); 
                    }
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break; //If it is player

                    Unit.Battle(map, (Enemy)map[newCoor].ActorOnSquare, coor + delta, true); //Battle between teo of these
                    Task.Run(SoundEffects.Attack);
                    break;

                case SquareTypes.Wall:

                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        ((Spike)actor).ChangeDirection();
                        CollisionCheck(map, -delta, actor);
                    }
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;


                    map.Log.Logs.Enqueue(new("You can't go there", ConsoleColor.Red));
                    break;

                case SquareTypes.CrackedWall:
                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        ((Spike)actor).ChangeDirection();
                        CollisionCheck(map, -delta, actor);
                    }
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    map.Log.Logs.Enqueue(new("This wall looks different, maybe it can be destoryed", ConsoleColor.White));
                    break;

                case SquareTypes.Entry:
                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        ((Spike)actor).ChangeDirection();
                        CollisionCheck(map, -delta, actor);
                    }
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    map.Log.Logs.Enqueue(new("You came from here, no way back", ConsoleColor.White));
                    break;

                case SquareTypes.Player:

                    if (map.User.CurrentEffect == EffectType.Invulnerbl) break;

                    if (actor.ActorsSquare.Entity == SquareTypes.Enemy || actor.ActorsSquare.Entity == SquareTypes.SnakeHead)
                    {
                        Unit.Battle(map, (Unit)actor, newCoor, false); //Enemies fight the player
                    }

                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        map.User.DealDamage(3); //Spykewall give instaDamage
                        map.Log.Logs.Enqueue(new("Spikes Damaged you by 3 hp, be careful", ConsoleColor.DarkRed));
                    }

                    break;

                case SquareTypes.Chest:
                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        ((Spike)actor).ChangeDirection();
                        CollisionCheck(map, -delta, actor);
                    }
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    Chest chest = (Chest)map[newCoor].ActorOnSquare;
                    map.Log.Logs.Enqueue(new($"Yay, you got {chest.ItemToDrop.Name} from chest"));
                    map.User.GiveItem(chest.Open()); //Opening the chest
                    map[newCoor].MakeEmpty(); //Deleting the chest
                    break;

                case SquareTypes.DamagingTrap:

                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;
                    if (map.User.CurrentEffect == EffectType.Invulnerbl) break;

                    Trap trap = (Trap)map[newCoor].ActorOnSquare;
                    map[newCoor] = actor.ActorsSquare;
                    map[coor] = actor.StandsOn;
                    actor.Move(delta, new Square(SquareTypes.RevealedTrap, newCoor)); //Reveal the trap
                    map.User.DealDamage(3); //Damage the player
                    map.Log.Logs.Enqueue(new("You walked on a trap and took 3 Damage", ConsoleColor.DarkRed));
                    break;

                case SquareTypes.SpykeWall:
                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        ((Spike)actor).ChangeDirection();
                    }
                    break;
                case SquareTypes.SnakeSegment:
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    ((Snake)map[newCoor].ActorOnSquare).Die(map); //Delete Snake Segment and create new snake
                    Task.Run(SoundEffects.Attack);
                    break;

                case SquareTypes.SnakeHead:
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    Unit.Battle(map, (Unit)map[newCoor].ActorOnSquare, coor + delta, true); //Attack the head
                    Task.Run(SoundEffects.Attack);
                    break;
            }
        }

        //I finally made it work right and almost without any bugs
        static public bool Raycast(Map map, Coordinates start, Coordinates target, List<SquareTypes> objectsToCollide, int maxDistance)
        {
            if (start == target) return true; //if raycast target and start point are the same
            float distance = Coordinates.Distance(start, target); 
            if (distance > maxDistance) return false; //If distance between points are less than max distance

            //For this algorithm i need Normalized "Coordinates", but since i use int, i can't make it normalized
            //But Normalized Vector2 is just sin and cos of triangle where two points are "start" and "target" and third point has 90 degrees angle
            //So instead of using normalized vector, i use sin and cos
            float cos = (target.X - start.X) / distance; 
            float sin = (target.Y - start.Y) / distance;

            float x = start.X;
            float y = start.Y;

            while (true)
            {
                x += cos / 2; //i move on x-axis for cos/2 value each time
                y += sin / 2; //i move in y-axis for sin/2 value each time

                //That means, that x and y is the points on straight line between "start" and "target". Also called Hypotenuse

                //I check here if x and y are already at the target position or over it (Over is depends on cos and sin positivity or negativity)
                //If i don't add sin and cos to checks, than player can't see objects from objectsToCollide
                if (cos <= 0 && sin <= 0)
                {
                    if (x + cos < target.X || y + sin < target.Y) return true;
                }
                else if (cos >= 0 && sin <= 0)
                {
                    if (x + cos > target.X || y + sin < target.Y) return true;
                }
                else if (sin >= 0 && cos <= 0)
                {
                    if (x + cos < target.X || y + sin > target.Y) return true;
                }
                else
                {
                    if (x + cos > target.X || y + sin > target.Y) return true;
                }
                //If ray found something that listed in objectsToCollide, than return false, ray can't get to asked point
                if (objectsToCollide.Contains(map[new((int)MathF.Round(x), (int)MathF.Round(y))].Entity)) return false;
            }
        }
    }
}

