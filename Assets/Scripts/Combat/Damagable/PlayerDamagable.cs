namespace Damageable.Implementation
{
    public class PlayerDamagable : DamageableBase
    {
        public override void Launch(DamagableDataSO initialData)
        {
            this.initialData = initialData as PlayerDamagableDataSO;

            base.Launch(initialData);
        }
    }
}