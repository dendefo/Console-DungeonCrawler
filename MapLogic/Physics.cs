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
                    map.MoveActorOnMap(actor, coor, delta);
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break; //If it's not the Player

                    map.Log.GreenAction = "You moved";
                    break;

                case SquareTypes.Coin:
                    map[newCoor].MakeEmpty();
                    map.MoveActorOnMap(actor, coor, delta);

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

                    map.MoveActorOnMap(actor, coor, delta);
                    map.Log.GreenAction = "You moved to the next level! Yay";
                    Task.Run(SoundEffects.NewLevel);
                    break;

                case SquareTypes.Enemy:
                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        ((Spike)actor).ChangeDirection();
                    }
                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;

                    Unit.Battle(map, (Enemy)map[newCoor].ActorOnSquare, coor + delta, true);
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

                    if (map.User.CurrentEffect == EffectType.Invulnerbl) break;

                    if (actor.ActorsSquare.Entity == SquareTypes.Enemy)
                    {
                        Unit.Battle(map, (Enemy)actor, newCoor, false);
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

                    Chest chest = (Chest)map[newCoor].ActorOnSquare;
                    map.Log.GreenAction = $"Yay, you got some {chest.ItemToDrop.Name}";
                    map.User.GiveItem(chest.Open());
                    map[newCoor].MakeEmpty();
                    break;

                case SquareTypes.DamagingTrap:

                    if (actor.ActorsSquare.Entity != SquareTypes.Player) break;
                    if (map.User.CurrentEffect == EffectType.Invulnerbl) break;

                    Trap trap = (Trap)map[newCoor].ActorOnSquare;
                    map[newCoor] = actor.ActorsSquare;
                    map[coor] = actor.StandsOn;
                    actor.Move(delta, new Square(SquareTypes.RevealedTrap, newCoor));
                    map.User.DealDamage(3);
                    map.Log.GreenAction = "You walked on a trap and took 3 Damage";
                    break;

                case SquareTypes.SpykeWall:
                    if (actor.ActorsSquare.Entity == SquareTypes.SpykeWall)
                    {
                        ((Spike)actor).ChangeDirection();
                    }
                    break;
            }
        }
        static public bool Raycast(Map map, Coordinates start, Coordinates target, List<SquareTypes> objectsToCollide,int maxDistance)
        {
            if (start == target) return true;
            float distance = Coordinates.Distance(start, target);
            if (distance > maxDistance) return false;

            float cos = (target.X - start.X) / distance;
            float sin = (target.Y - start.Y) / distance;

            double summ = cos * cos + sin * sin;
            float x = start.X;
            float y = start.Y;

            while (true)
            {
                x += cos;
                y += sin;

                if (Math.Round(x) == target.X && Math.Round(y) == target.Y) return true;
                else if (Math.Floor(x) == target.X && Math.Round(y) == target.Y) return true;
                else if (Math.Round(x) == target.X && Math.Floor(y) == target.Y) return true;
                else if (Math.Floor(x) == target.X && Math.Floor(y) == target.Y) return true;



                if (!objectsToCollide.Contains(map[new((int)Math.Round(x), (int)Math.Round(y))].Entity)) continue;
                else return false;
            }
        }
    }
}

