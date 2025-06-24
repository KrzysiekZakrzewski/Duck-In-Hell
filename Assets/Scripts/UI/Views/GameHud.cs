using Game.HUD;
using UnityEngine;
using ViewSystem.Implementation;

namespace Game.View
{
    public class GameHud : BasicView
    {
        [field: SerializeField] public PlayerHUD PlayerHUD { get; private set; }
        public override bool Absolute => false;
    }
}