using System.Numerics;

//This action will attack any entity that is adjacent to the actor
//Used by enemies in the update phase for example.

class LookForTarget : IAction
{
    public ActionResult Execute(WorldEntity actor, Vector2 targetPosition)
    {
        Vector2[] directions = new Vector2[]
        {
            new Vector2(0, -1), // North
            new Vector2(1, -1), // Northeast
            new Vector2(1, 0),  // East
            new Vector2(1, 1),  // Southeast
            new Vector2(0, 1),  // South
            new Vector2(-1, 1), // Southwest
            new Vector2(-1, 0), // West
            new Vector2(-1, -1) // Northwest
        };

        foreach (Vector2 direction in directions)
        {
            Vector2 adjacentPosition = actor.Position + direction;

            if (actor.World.Player.Position == adjacentPosition)
            {
                Attack attackAction = new Attack();
                return attackAction.Execute(actor, actor.World.Player.Position);
            }
        }

        return new ActionResult(false, $"{actor.Name} found no one to attack.");
    }

}

