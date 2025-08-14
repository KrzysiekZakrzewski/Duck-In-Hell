using UnityEngine;

namespace BlueRacconGames.AI.Factory
{
    public class ShootAIModuleFactory : IAIModuleFactory
    {
        [field: SerializeReference, ReferencePicker] public IShootType ShootType { get; private set; }
        [field: SerializeField] public int ShootTickCountdown { get; private set; } = 10;
        public IAIModule Create()
        {
            return new ShootAIModule(this);
        }
    }
}
