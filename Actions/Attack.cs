using System.Numerics;

class Attack : IAction
{
    public ActionResult Execute(WorldEntity actor, Vector2 targetPosition)
    {
        var targetEntity = actor.World.GetEntityAt(targetPosition);
        if (targetEntity == null)
        {
            return new ActionResult(false, $"{actor.Name} attack the air!");
        }

        int chanceToHit = 75; //Base this on whatever attributes the actor and target have

        if (new Random().Next(0, 100) < chanceToHit)
        {
            int damagePotential = actor.GetAttribute("strength") * 10; //Calculate max possible damage made by actor
            targetEntity.TakeDamage(damagePotential);
            return new ActionResult(true, $"{actor.Name} attack {targetEntity.Name}! {targetEntity.Health} HP left.");
        }

        return new ActionResult(false, $"{actor.Name} miss {targetEntity.Name}!");

    }
}
