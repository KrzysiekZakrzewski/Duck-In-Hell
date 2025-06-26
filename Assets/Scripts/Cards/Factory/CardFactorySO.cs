using BlueRacconGames.Cards.Effects;
using UnityEngine;

namespace BlueRacconGames.Cards
{
    public abstract class CardFactorySO : ScriptableObject, ICardFactory
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float BasePercentChance { get; private set; }
        [field: SerializeReference, ReferencePicker] public CardEffectBase cardEffect;
        public abstract ICard CreateCard();
    }
}
