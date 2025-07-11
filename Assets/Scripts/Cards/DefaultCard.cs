using BlueRacconGames.Cards.Effects;
using Units;

namespace BlueRacconGames.Cards
{
    public class DefaultCard : CardBase
    {
        private CardEffectBase cardEffectBase;
        public DefaultCard(CardEffectBase cardEffectBase)
        {
            this.cardEffectBase = cardEffectBase;
        }

        public override void Execute(CardsController cardsController, IUnit source)
        {
            cardEffectBase.ApplyEffect(cardsController, source);
        }

        protected override void LevelUpInternal()
        {
            base.LevelUpInternal();

            cardEffectBase.LevelUp();
        }
    }
}