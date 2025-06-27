using BlueRacconGames.Animation;

namespace Damageable
{
    public interface IDamagableDataSO
    {
        int MaxHealth { get; }
        bool ExpireOnDead { get; }
        AnimationDataSO GetHitAnimation {  get; }
    }
}
