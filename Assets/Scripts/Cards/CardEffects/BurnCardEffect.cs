using BlueRacconGames.MeleeCombat;
using Damageable;
using System.Linq;
using TimeTickSystems;

namespace BlueRacconGames.Cards.Effects
{
    public class BurnCardEffect : PassiveHitCardEffect
    {
        public override void Execute(IDamagableTarget target)
        {
            if(targets.ContainsKey(target)) return;

            if (targets.Count == 0)
                TimeTickSystem.OnTick += OnTick;

            TargetData targetData = new()
            {
                DamagableTarget = target,
                Damageable = target.GameObject.GetComponent<IDamageable>(),
                StartTick = tick
            };

            targets.Add(target, targetData);
        }

        private void OnTick(object sender, OnTickEventArgs e)
        {
            if(targets.Count <= 0) return;

            foreach(var target in targets.Values)
            {
                int tickDifference = tick - target.StartTick;

                if (tickDifference >= tickDuration)
                {
                    targets.Remove(target.DamagableTarget);
                    if(targets.Count <= 0)
                    {
                        TimeTickSystem.OnTick -= OnTick;
                        return;
                    }
                }

                tick++;

                if (tickDifference % tickLoopDuration != 0) continue;

                target.Damageable.TakeDamage(damageAmount);
            }
        }
    }
}