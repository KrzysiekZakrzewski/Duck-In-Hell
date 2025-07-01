namespace BlueRacconGames.MeleeCombat
{
    public class ExecuteCardsTargetEffect : IMeleeTargetEffect
    {
        public void Execute(MeleeCombatControllerBase source, IDamagableTarget target)
        {
            if(!CanExecuteEffects(source)) return;

            source.CardController.ExecutePassiveHitEffects(target);
        }

        protected bool CanExecuteEffects(MeleeCombatControllerBase source) => source.CardController != null;
    }
}
