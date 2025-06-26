using UnityEngine;
namespace BlueRacconGames.Cards.Effects
{
    [System.Serializable]
    public abstract class CardEffectBase
    {
        [SerializeField] protected int level = 1;
        public abstract void ApplyEffect(CardsController cardsController);
        public void LevelUp()
        {
            level++;
        }
    }
}