using System.Numerics;

class Examine : IAction
{
    public ActionResult Execute(WorldEntity actor, Vector2 targetPosition)
    {
        var targetEntity = actor.World.GetEntityAt(targetPosition);
        if (targetEntity != null)
        {
            return new ActionResult(true, $"You examine {targetEntity.Name}!");
        }

        Random random = new();
        if (random.Next(0, 1000) < 1)
        {
            actor.AddInventory(new Thing() { Name = "Gold", Value = 1 });
            return new ActionResult(true, $"You found a gold coin!");
        }

        return new ActionResult(false, $"You see nothing of value.");
    }
}
