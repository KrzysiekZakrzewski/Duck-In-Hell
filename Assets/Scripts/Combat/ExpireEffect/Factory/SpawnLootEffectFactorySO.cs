using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    [CreateAssetMenu(fileName = nameof(SpawnLootEffectFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(SpawnLootEffectFactorySO))]
    public class SpawnLootEffectFactorySO : ExpireEffectFactorySO
    {
        [SerializeField] private LootDataSO lootDataSO;
        public override IExpireEffect CreateExpireEffect()
        {
            return new SpawnLootEffect(lootDataSO);
        }
    }
}
