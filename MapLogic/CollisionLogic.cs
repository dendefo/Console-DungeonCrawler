namespace First_Semester_Project.MapLogic
{
    static class CollisionLogic
    {
        static public void CollisionCheck(Map map, Coordinates delta, Actor actor)
        {
            if (actor == null) return;

            Coordinates coor = actor.Coor;
            Coordinates newCoor = coor + delta;


            //This Switch-case block is checking what to do with actor by the entity that he is going to touch

            switch (map.GetFromMap(newCoor).Entity)
            {
                case SquareTypes.Empty: //If Actor steps on Empty square
                    map.ActorMoveOnMap(actor, coor, delta);
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break; //If it's not the Player

                    map.Log.GreenAction = "You moved";
                    break;

                case SquareTypes.Coin:
                    map.GetFromMap(newCoor).MakeEmpty();
                    map.ActorMoveOnMap(actor, coor, delta);

                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;
                    ((Player)actor).GiveItem(new Coin());
                    break;

                case SquareTypes.Exit:
                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        ((Spike)actor).ChangeDirection();
                        CollisionCheck(map, -delta, actor);
                    }
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    map.ActorMoveOnMap(actor, coor, delta);
                    map.Log.GreenAction = "You moved to the next level! Yay";
                    Task.Run(SoundEffects.NewLevel);
                    break;

                case SquareTypes.Enemy:
                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        ((Spike)actor).ChangeDirection();
                    }
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    Enemy enemy = (Enemy)map.GetFromMap(newCoor).ActorOnSquare;
                    Actor.Battle(map, enemy, coor + delta, true);
                    Task.Run(SoundEffects.Attack);
                    break;

                case SquareTypes.Wall:

                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        ((Spike)actor).ChangeDirection();
                        CollisionCheck(map, -delta, actor);
                    }
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    map.Log.GreenAction = "You can't go there";
                    break;

                case SquareTypes.CrackedWall:
                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        ((Spike)actor).ChangeDirection();
                        CollisionCheck(map, -delta, actor);
                    }
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    map.Log.GreenAction = "This wall looks different, maybe it can be destoryed";
                    break;

                case SquareTypes.Entry:
                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        ((Spike)actor).ChangeDirection();
                        CollisionCheck(map, -delta, actor);
                    }
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    map.Log.GreenAction = "You came from here, no way back";
                    break;

                case SquareTypes.Player:
                    if (actor.ActorsSquare.Entity == SquareTypes.Enemy)
                    {
                        Actor.Battle(map, (Enemy)actor, newCoor, false);
                    }

                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        map.User.DealDamage(3);
                        map.Log.RedAction = "You have been damaged by spikes, be careful";
                    }

                    break;

                case SquareTypes.Chest:
                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        ((Spike)actor).ChangeDirection();
                        CollisionCheck(map, -delta, actor);
                    }
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    Chest chest = (Chest)map.GetFromMap(newCoor).ActorOnSquare;
                    map.Log.GreenAction = $"Yay, you got some {chest.Inside.Name}";
                    map.User.GiveItem(chest.Open());
                    map.GetFromMap(newCoor).MakeEmpty();
                    break;

                case SquareTypes.DamagingTrap:

                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    Trap trap = (Trap)map.GetFromMap(newCoor).ActorOnSquare;
                    map.SetToMap(newCoor, actor.ActorsSquare);
                    map.SetToMap(coor, actor.StandsOn);
                    actor.Move(delta, new Square(SquareTypes.RevealedTrap, newCoor));
                    map.Log.GreenAction = "Oh, you just walked on a trap. Something just happened";
                    break;

                case SquareTypes.SpykeWall:
                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        ((Spike)actor).ChangeDirection();
                    }
                    break;
            }
        }
    }
}

