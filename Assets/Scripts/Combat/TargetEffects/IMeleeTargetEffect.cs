namespace BlueRacconGames.MeleeCombat
{
    public interface IMeleeTargetEffect
    {
        void Execute(MeleeCombatControllerBase source, IDamagableTarget target);
    }
}