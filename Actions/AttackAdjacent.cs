using System.Numerics;

class AttackAdjacent : IAction
{
    public ActionResult Execute(GameEntity actor, Vector2 targetPosition)
    {
        bool attacked = false;
        string resultMessage = "";

        // Directions represent the eight adjacent positions
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
                var targetEntity = actor.World.Player;
                attacked = true;
                if (new Random().Next(0, 100) < 75) // Chance to hit
                {
                    int damage = actor.GetAttribute("strength") * 10;
                    targetEntity.TakeDamage(damage);
                    resultMessage += $"{actor.Name} attacks {targetEntity.Name} at ({adjacentPosition.X}, {adjacentPosition.Y})! {targetEntity.Health} HP left.\n";
                }
                else
                {
                    resultMessage += $"{actor.Name} misses {targetEntity.Name} at ({adjacentPosition.X}, {adjacentPosition.Y})!\n";
                }
            }
        }

        if (!attacked)
        {
            return new ActionResult(false, $"{actor.Name} found no one to attack.");
        }

        return new ActionResult(true, resultMessage.TrimEnd());
    }

}

