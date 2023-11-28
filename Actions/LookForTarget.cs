using System.Numerics;

//This action will attack any entity that is adjacent to the actor
//Used by enemies in the update phase for example.

class LookForTarget : IAction
{
    public ActionResult Execute(WorldEntity actor, Vector2 targetPosition)
    {
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue; // Skip checking actors position

                Vector2 adjacentPosition = actor.Position + new Vector2(dx, dy);

                if (actor.World.Player.Position == adjacentPosition) //TODO this should be configurable somehow, what is it looking for?
                {
                    Attack attackAction = new Attack();
                    return attackAction.Execute(actor, actor.World.Player.Position);
                }
            }
        }

        return new ActionResult(false, $"{actor.Name} found no one to attack.");
    }
}

