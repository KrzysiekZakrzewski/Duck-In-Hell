using BlueRacconGames.UI.Bars;
using UnityEngine;
using UnityEngine.UI;

namespace Game.HUD
{
    public abstract class UnitHUD : MonoBehaviour
    {
        [field: SerializeField] public Image UnitIcon { get; protected set; }
        [field: SerializeField] public HealthBar HealthBar { get; protected set; }
        [field: SerializeField] public ManaBar ManaBar { get; protected set; }
        [field: SerializeField] public StaminaBar StaminaBar { get; protected set; }
    }
}