using System.Numerics;

interface IAction
{
    ActionResult Execute(GameEntity actor, Vector2 targetPosition);
}
