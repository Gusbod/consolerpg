using System.Numerics;

class Move : IAction
{
    public ActionResult Execute(GameEntity actor, Vector2 targetPosition)
    {
        if (actor.World.TryMoveEntity(actor, targetPosition))
        {
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
