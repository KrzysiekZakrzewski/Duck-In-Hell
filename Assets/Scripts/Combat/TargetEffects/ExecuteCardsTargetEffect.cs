namespace BlueRacconGames.MeleeCombat
{
    public class ExecuteCardsTargetEffect : IMeleeTargetEffect
    {
        public void Execute(MeleeCombatControllerBase source, IDamagableTarget target)
        {
            source.ExecuteCards(target);
        }
    }
}
