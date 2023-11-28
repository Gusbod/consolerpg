using System.Numerics;

class Push : IAction
{
    public ActionResult Execute(WorldEntity actor, Vector2 targetPosition)
    {
        var targetEntity = actor.World.GetEntityAt(targetPosition);
        if (targetEntity != null)
        {
            return new ActionResult(true, $"You push {targetEntity.Name}!");
        }

        return new ActionResult(false, $"You no push!");
    }
}
