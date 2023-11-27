using System.Numerics;

class Attack : IAction
{
    public ActionResult Execute(GameEntity actor, Vector2 targetPosition)
    {
        var targetEntity = actor.World.GetEntityAt(targetPosition);
        if (targetEntity == null)
        {
            return new ActionResult(false, $"{actor.Name} attack the air!");
        }

        int chanceToHit = 75; //Base this on whatever attributes the actor and target have

        if (new Random().Next(0, 100) < chanceToHit)
        {
            int damagePotential = actor.GetAttribute("strength"); //Calculate max possible damage made by actor
            targetEntity.TakeDamage(damagePotential);
        }
        else
        {
            return new ActionResult(false, $"{actor.Name} attack {targetEntity.Name}!");
        }

        return new ActionResult(true, $"{actor.Name} attack {targetEntity.Name}!");
    }
}
