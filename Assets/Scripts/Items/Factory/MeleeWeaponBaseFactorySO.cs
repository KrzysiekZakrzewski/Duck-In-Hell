using BlueRacconGames.MeleeCombat;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Item.Factory.Implementation
{
    public abstract class MeleeWeaponBaseFactorySO : ItemFactorySO
    {
        [field: SerializeField] public List<LaunchAttackEffectFactorySO> LaunchAttackEffectFactory { get; set; }
        [field: SerializeField] public List<MeleeTargetEffectFactorySO> MeleeWeaponTargetEffectFactory {  get; set; }
        public abstract override IItem CreateItem();
    }
}