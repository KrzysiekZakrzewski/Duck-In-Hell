using BlueRacconGames.Cards.Effects;

namespace BlueRacconGames.Cards
{
    public class DefaultCard : CardBase
    {
        private CardEffectBase cardEffectBase;
        public DefaultCard(CardEffectBase cardEffectBase)
        {
            this.cardEffectBase = cardEffectBase;
        }

        public override void Execute(CardsController cardsController)
        {
            cardEffectBase.ApplyEffect(cardsController);
        }
    }
}