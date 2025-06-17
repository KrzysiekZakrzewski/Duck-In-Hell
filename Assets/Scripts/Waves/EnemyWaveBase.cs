using BlueRacconGames.Pool;
using RDG.Platforms;
using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using Units.Implementation;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyWaves.Implementation
{
    public abstract class EnemyWaveBase : IEnemyWave
    {
        private DynamicTimer timer;

        protected readonly float countdownTime = 3;
        protected readonly int maxRandomizeIteration = 10;
        protected readonly List<IUnit> enemyUnitsLUT = new();

        public abstract int TotalEnemy { get; }
        public abstract WaveEnemyUnitData[] WaveEnemyUnitDatas { get; }

        public int EnemyRemain => enemyUnitsLUT.Count;
        public bool IsStarted { get; private set; }
        public bool IsCompleted { get; private set; }

        public event Action<IEnemyWave> OnSetupedE;
        public event Action<IEnemyWave> OnStartedE;
        public event Action<IEnemyWave> OnEndedE;
        public event Action<IEnemyWave> OnCompletedE;
        public event Action<IEnemyWave> OnUpdatedE;

        public void SetupWave(DefaultPooledEmitter defaultPooledEmitter, PooledUnitBase enemyUnitPrefab)
        {
            IsCompleted = false;
            IsStarted = false;

            SpawnEnemies(defaultPooledEmitter, enemyUnitPrefab);

            OnOffEnemiesImmue(true);

            timer = new DynamicTimer();
            timer.OnCountdownE += OnCountdownFinished;

            OnSetupedE?.Invoke(this);

            Debug.Log("Wave Setuped");
        }
        public void StartWave()
        {
            if (IsCompleted || IsStarted) return;

            timer.StartCountdown(countdownTime, 1);
        }
        public void UpdateWave(IUnit unit)
        {
            if (IsCompleted || !IsStarted) return;

            unit.Damageable.OnDeadE -= UpdateWave;

            enemyUnitsLUT.Remove(unit);

            OnUpdatedE?.Invoke(this);

            Debug.Log("Wave Updated");

            if (!CheckIsCompleted()) return;

            CompleteWave();
        }
        public void CompleteWave()
        {
            if (IsCompleted || !IsStarted) return;

            IsCompleted = true;

            OnCompletedE?.Invoke(this);

            Debug.Log("Wave Completed");
        }

        protected virtual void OnCountdownFinished()
        {
            timer.OnCountdownE -= OnCountdownFinished;
            timer = null;

            OnOffEnemiesImmue(false);

            OnStartedE?.Invoke(this);
            IsStarted = true;

            Debug.Log("Wave Started");
        }

        private bool CheckIsCompleted()
        {
            return enemyUnitsLUT.Count <= 0;
        }
        private void SpawnEnemies(DefaultPooledEmitter defaultPooledEmitter, PooledUnitBase enemyUnitPrefab)
        {
            var screenBounds = GetScreenBounds();

            for (int i = 0; i < TotalEnemy; i++)
            {
                var newEnemy = defaultPooledEmitter.EmitItem<PooledUnitBase>(enemyUnitPrefab, GetRandomPoint(screenBounds), Vector3.zero);

                newEnemy.SetUnitData(GetRandomEnemyData());
                
                newEnemy.Damageable.OnDeadE += UpdateWave;

                enemyUnitsLUT.Add(newEnemy);
            }
        }
        private void OnOffEnemiesImmue(bool value)
        {
            foreach(var enemy in enemyUnitsLUT)
            {
                enemy.Damageable.SetImmune(value);
            }
        }
        private Vector2 GetRandomPoint(Vector2 screenBounds)
        {
            return new Vector2(
                Random.Range(-screenBounds.x, screenBounds.x),
                Random.Range(-screenBounds.y, screenBounds.y)
            );
        }
        private Vector2 GetScreenBounds()
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        }
        private PooledEnemyUnitDataSO GetRandomEnemyData()
        {
            int randomizeIteration = 0;

            PooledEnemyUnitDataSO enemyData = null;

            while (randomizeIteration < maxRandomizeIteration && enemyData == null)
            {
                enemyData = RandomizeEnemyData();
                randomizeIteration++;
            }

            return enemyData != null ? enemyData : WaveEnemyUnitDatas[Random.Range(0, WaveEnemyUnitDatas.Length)].EnemyUnitDataSO;
        }
        private PooledEnemyUnitDataSO RandomizeEnemyData()
        {
            float roll = Random.value;
            float cumulative = 0f;

            foreach (WaveEnemyUnitData data in WaveEnemyUnitDatas)
            {
                cumulative += data.PercentChance;

                if (roll > cumulative) continue;

                return data.EnemyUnitDataSO;
            }

            return null;
        }
    }
}