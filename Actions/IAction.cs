using System.Numerics;

interface IAction
{
    ActionResult Execute(WorldEntity actor, Vector2 targetPosition);
}
