using System.Numerics;

class Move : IAction
{
    public ActionResult Execute(GameEntity actor, Vector2 targetPosition)
    {
        if (actor.World.CanMoveTo(targetPosition))
        {
            bool success = actor.World.TryMoveEntity(actor, targetPosition);
            if (!success)
            {
                return new ActionResult(false, $"You can't move to {targetPosition}.");
            }
            return new ActionResult(true, "");
        }
        else
        {
            var targetEntity = actor.World.GetEntityAt(targetPosition);
            if (targetEntity != null)
            {
                //When wanting to move to a position with an entity, 
                //we execute the default action of the entity. Attack for enemies,
                //talk for NPCs, push for rocks etc.
                return targetEntity.OnCollideAction.Execute(actor, targetEntity.Position);
            }
            else
            {
                return new ActionResult(false, $"You bump into an invisible wall!");
            }
        }
    }
}
