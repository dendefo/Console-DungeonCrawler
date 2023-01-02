namespace First_Semester_Project.MapLogic
{
    static class CollisionLogic
    {
        static public void Collision(Map map, int deltaX, int deltaY, Actor actor)
        {
            if (actor == null) return;

            int y = actor.YCoordinate;
            int x = actor.XCoordinate;

            /*switch (map.MapArray[actor.YCoordinate][actor.XCoordinate].Entity)
            {
                case SquareTypes.Player:
                    PlayerCollision(map, deltaX, deltaY);
                    break;
                case SquareTypes.Enemy:
                    break;
                case SquareTypes.SpykeWall:
                    break;
            }*/


            //This Switch-case block is checking what to do with actor by the entity that he is going to touch
            //I don't know how to write it better, but it looks like i don't repeat myself a lot, so i'm good with it
            switch (map.MapArray[y + deltaY][x + deltaX].Entity)
            {
                case SquareTypes.Empty: //If Actor steps on Empty square
                    map.ActorMoveOnMap(actor, y, deltaY, x, deltaX);
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break; //If it's not the Player

                    map.Log.action = "You moved";
                    break;

                case SquareTypes.Coin:
                    map.MapArray[y + deltaY][x + deltaX].MakeEmpty();
                    map.ActorMoveOnMap(actor,y, deltaY, x, deltaX);

                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;
                    ((Player)actor).GiveItem(new Coin());
                    break;

                case SquareTypes.Exit:
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    map.ActorMoveOnMap(actor, y, deltaY, x, deltaX);
                    map.Log.action = "You moved to the next level! Yay";
                    Task.Run(SoundEffects.NewLevel);
                    break;

                case SquareTypes.Enemy:
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    Enemy enemy = (Enemy)map.MapArray[y + deltaY][x + deltaX].ActorOnSquare;
                    Actor.Battle(map, enemy, y, deltaY, x, deltaX, true);
                    Task.Run(SoundEffects.Attack);
                    break;

                case SquareTypes.Wall:

                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        ((Spike)actor).ChangeDirection(deltaX, deltaY);
                        Collision(map, -deltaX, -deltaY, actor);
                    }
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    map.Log.action = "You can't go there";
                    break;

                case SquareTypes.CrackedWall:
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    map.Log.action = "This wall looks different, maybe it can be destoryed";
                    break;

                case SquareTypes.Entry:
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    map.Log.action = "You came from here, no way back";
                    break;

                case SquareTypes.Player:
                    if (actor.ActorsSquare.Entity == SquareTypes.Enemy)
                    {
                        Actor.Battle(map, (Enemy)actor, y, deltaY, x, deltaX, false);
                    }

                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        map.User.DealDamage(3);
                        map.Log.action2 = "You was damaged by spikes, be careful";
                    }

                    break;

                case SquareTypes.Chest:
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    Chest chest = (Chest)map.MapArray[y + deltaY][x + deltaX].ActorOnSquare;
                    map.Log.action = $"Yay, you got some {chest.Inside.Name}";
                    map.User.GiveItem(chest.Open());
                    map.MapArray[y + deltaY][x + deltaX].MakeEmpty();
                    break;

                case SquareTypes.DamagingTrap:

                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    Trap trap = (Trap)map.MapArray[y + deltaY][x + deltaX].ActorOnSquare;
                    map.MapArray[y + deltaY][x + deltaX] = actor.ActorsSquare;
                    map.MapArray[y][x] = actor.StandsOn;
                    actor.Move(deltaY, deltaX, new Square(SquareTypes.RevealedTrap, x + deltaX, y + deltaY));
                    map.Log.action = "Oh, you just walked on a trap. Something just happened";
                    break;
            }
        }

        //static private void PlayerCollision(Map map, int deltaX, int deltaY)
        //{
        //    int y = map.User.YCoordinate;
        //    int x = map.User.XCoordinate;

        //    switch (map.MapArray[y + deltaY][x + deltaX].Entity)
        //    {
        //        case SquareTypes.Empty:
        //            map.ActorMoveOnMap(map.User, y, deltaY, x, deltaX);
        //            map.Log.action = "You moved";
        //            break;

        //        case SquareTypes.Wall:
        //            break;

        //        case SquareTypes.SpykeWall:
        //            break;

        //        case SquareTypes.CrackedWall:
        //            break;

        //        case SquareTypes.Entry:
        //            break;

        //        case SquareTypes.Exit:
        //            break;

        //        case SquareTypes.Enemy:
        //            break;

        //        case SquareTypes.Chest:
        //            break;

        //        case SquareTypes.RevealedTrap:
        //            break;

        //        case SquareTypes.DamagingTrap:
        //            break;
        //    }
        //}
        //static private void Empty()
    }
}

