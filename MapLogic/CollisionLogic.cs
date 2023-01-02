namespace First_Semester_Project.MapLogic
{
    static class CollisionLogic
    {
        static public void Collision(Map map, Coordinates delta, Actor actor)
        {
            if (actor == null) return;

            Coordinates coor = actor.Coor;
            Coordinates newCoor = coor + delta;


            //This Switch-case block is checking what to do with actor by the entity that he is going to touch


            switch ((newCoor^map.MapArray).Entity)
            {
                case SquareTypes.Empty: //If Actor steps on Empty square
                    map.ActorMoveOnMap(actor, coor,delta);
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break; //If it's not the Player

                    map.Log.action = "You moved";
                    break;

                case SquareTypes.Coin:
                    (newCoor ^ map.MapArray).MakeEmpty();
                    map.ActorMoveOnMap(actor,coor, delta);

                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;
                    ((Player)actor).GiveItem(new Coin());
                    break;

                case SquareTypes.Exit:
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    map.ActorMoveOnMap(actor, coor, delta);
                    map.Log.action = "You moved to the next level! Yay";
                    Task.Run(SoundEffects.NewLevel);
                    break;

                case SquareTypes.Enemy:
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    Enemy enemy = (Enemy)(newCoor ^ map.MapArray).ActorOnSquare;
                    Actor.Battle(map, enemy, coor+delta, true);
                    Task.Run(SoundEffects.Attack);
                    break;

                case SquareTypes.Wall:

                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        ((Spike)actor).ChangeDirection();
                        Collision(map, -delta, actor);
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
                        Actor.Battle(map, (Enemy)actor, newCoor, false);
                    }

                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        map.User.DealDamage(3);
                        map.Log.action2 = "You was damaged by spikes, be careful";
                    }

                    break;

                case SquareTypes.Chest:
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    Chest chest = (Chest)(newCoor ^ map.MapArray).ActorOnSquare;
                    map.Log.action = $"Yay, you got some {chest.Inside.Name}";
                    map.User.GiveItem(chest.Open());
                    (newCoor ^ map.MapArray).MakeEmpty();
                    break;

                case SquareTypes.DamagingTrap:

                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    Trap trap = (Trap)(newCoor ^ map.MapArray).ActorOnSquare;
                    map.MapArray[coor.Y + delta.Y][coor.X + delta.X] = actor.ActorsSquare;
                    map.MapArray[coor.Y][coor.X] = actor.StandsOn;
                    actor.Move(coor, new Square(SquareTypes.RevealedTrap, coor+delta));
                    map.Log.action = "Oh, you just walked on a trap. Something just happened";
                    break;
            }
        }
    }
}

