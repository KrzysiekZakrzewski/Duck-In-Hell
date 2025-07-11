using Game.Item.Factory;
using TimeTickSystems;
using Units;

namespace Game.Item
{
    public abstract class BuffStatMultiplierItem : ActionItem
    {
        public float Multiplier { get; private set; }
        protected int duration;

        public BuffStatMultiplierItem(BuffStatMultiplierItemFactorySO initialData) : base(initialData)
        {
            duration = initialData.Duration;
            Multiplier = initialData.Multiplier;
        }

        public override bool Use(IUnit source)
        {
            ApplyBuff();

            return true;
        }
        protected virtual void ApplyBuff()
        {
            TimeTickSystem.OnTick += OnTick;
        }
        protected virtual void RemoveBuff()
        {
            TimeTickSystem.OnTick -= OnTick;
        }
        private void OnTick(object sender, OnTickEventArgs e)
        {
            if (duration <= 0) return;

            duration--;

            if (duration > 0) return;

            RemoveBuff();
        }
    }
}
