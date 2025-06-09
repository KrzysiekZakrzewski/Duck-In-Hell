using Game.View;
using ViewSystem.Implementation;
using UnityEngine;
using EnemyWaves.UI;

namespace BlueRacconGames.View
{
    public class GameViewController : SingleViewTypeStackController
    {
        [SerializeField] private WaveUIManager waveUIManager;

        protected override void Awake()
        {
            base.Awake();

            TryOpenSafe<GameHud>();
        }
    }
}