using BlueRacconGames.Cards.Effects;
using UnityEngine;

namespace BlueRacconGames.Cards
{
    [CreateAssetMenu(fileName = nameof(DefaultCardFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(Cards) + "/" + nameof(DefaultCardFactorySO))]

    public class DefaultCardFactorySO : CardFactorySO
    {
        public override ICard CreateCard()
        {
            return new DefaultCard(cardEffect);
        }
    }
}