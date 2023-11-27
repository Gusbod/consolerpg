using System.Numerics;

interface IAction
{
    ActionResult Execute(GameEntity actor, Vector2 targetPosition);
}

class AttackAction : IAction
{
    public ActionResult Execute(GameEntity actor, Vector2 targetPosition)
    {
        var targetEntity = actor.World.GetEntityAt(targetPosition);
        if (targetEntity != null)
        {

            return new ActionResult(true, $"You attack {targetEntity.Name}!");
        }

        return new ActionResult(false, $"{actor.Name} attack the air!");
    }
}

class PushAction : IAction
{
    public ActionResult Execute(GameEntity actor, Vector2 targetPosition)
    {
        var targetEntity = actor.World.GetEntityAt(targetPosition);
        if (targetEntity != null)
        {
            return new ActionResult(true, $"You push {targetEntity.Name}!");
        }

        return new ActionResult(false, $"You no push!");
    }
}

class ExamineAction : IAction
{
    public ActionResult Execute(GameEntity actor, Vector2 targetPosition)
    {
        var targetEntity = actor.World.GetEntityAt(targetPosition);
        if (targetEntity != null)
        {
            return new ActionResult(true, $"You examine {targetEntity.Name}!");
        }

        if (actor is Player player)
        {
            Random random = new();
            if (random.Next(0, 1000) < 1)
            {
                player.AddGold(1);
                return new ActionResult(true, $"You found a gold coin!");
            }
        }

        return new ActionResult(false, $"You see nothing of value.");
    }
}

class ActionResult
{
    public bool Success { get; private set; } = true;
    public string Message { get; private set; } = "";

    public ActionResult(bool success = true, string message = "")
    {
        Message = message;
        Success = success;
    }
}
