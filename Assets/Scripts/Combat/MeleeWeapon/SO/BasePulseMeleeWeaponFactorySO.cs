using Game.Item;
using Game.Item.Factory.Implementation;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat.Implementation
{
    [CreateAssetMenu(fileName = nameof(BasePulseMeleeWeaponFactorySO), menuName = nameof(MeleeCombat) + "/" + nameof(Implementation) + "/" + nameof(BasePulseMeleeWeaponFactorySO))]
    public class BasePulseMeleeWeaponFactorySO : MeleeWeaponBaseFactorySO
    {
        [field: SerializeField] public float ScaleMultiplayer { get; private set; } = 0.35f;
        [field: SerializeField] public float AnimationDuration { get; private set; } = 0.2f;
        [field: SerializeField] public float PulseDelay { get; private set; } = 0.5f;
        [field: SerializeField] public float PulseSequenceDelay { get; private set; } = 2f;
        [field: SerializeField] public int PulseAmountPerAction { get; private set; } = 1;

        public override IItem CreateItem()
        {
            return new BasePulseMeleeWeapon(this);
        }

        public IMeleeWeapon CreateWeapon()
        {
            return CreateItem() as IMeleeWeapon;
        }
    }
}