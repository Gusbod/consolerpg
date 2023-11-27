using System.Numerics;

class NoAction : IAction
{
    public ActionResult Execute(GameEntity actor, Vector2 targetPosition)
    {
        return new ActionResult(false, "No action performed.");
    }
}