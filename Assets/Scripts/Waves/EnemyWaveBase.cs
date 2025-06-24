using BlueRacconGames.Pool;
using Game.Difficulty;
using RDG.Platforms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timers;
using Units;
using Units.Implementation;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyWaves.Implementation
{
    public abstract class EnemyWaveBase : IEnemyWave
    {
        private readonly WaveEnemyUnitData[] initialData;
        private readonly float spawnDelayDuration = 0.1f;
        private int totalEnemy;
        private Countdown timer;
        private bool enemySpawned = false;

        protected readonly float countdownTime = 3;
        protected readonly int maxRandomizeIteration = 10;
        protected readonly List<IUnit> enemyUnitsLUT = new();

        public int TotalEnemy => totalEnemy;
        public WaveEnemyUnitData[] WaveEnemyUnitDatas => initialData;

        public int EnemyRemain => enemyUnitsLUT.Count;
        public bool IsStarted { get; private set; }
        public bool IsCompleted { get; private set; }

        public event Action<IEnemyWave> OnSetupedE;
        public event Action<IEnemyWave> OnStartedE;
        public event Action<IEnemyWave> OnEndedE;
        public event Action<IEnemyWave> OnCompletedE;
        public event Action<IEnemyWave> OnUpdatedE;

        public EnemyWaveBase(WaveEnemyUnitData[] initialData)
        {
            this.initialData = initialData;
        }

        public void SetupWave(int waveId, DefaultPooledEmitter defaultPooledEmitter, PooledUnitBase enemyUnitPrefab, IDifficulty difficulty, ICountdownPresentation timerPresentation)
        {
            CorutineSystem.StartSequnce(SetupWaveSequnce(waveId, defaultPooledEmitter, enemyUnitPrefab, difficulty, timerPresentation));
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

            OnOffEnemies(true);

            OnStartedE?.Invoke(this);
            IsStarted = true;
        }
        protected virtual void OnOffEnemies(bool value)
        {
            foreach (var unit in enemyUnitsLUT)
                OnOffEnemy(unit, value);
        }
        protected virtual void OnOffEnemy(IUnit unit, bool value)
        {
            unit.Damageable.SetImmune(!value);
            var enemyUnit = unit as PooledUnitBase;

            enemyUnit.AIController.ForceStartStopSimulate(value);
        }

        private bool CheckIsCompleted()
        {
            return enemyUnitsLUT.Count <= 0;
        }
        private IEnumerator SpawnEnemies(DefaultPooledEmitter defaultPooledEmitter, PooledUnitBase enemyUnitPrefab)
        {
            var screenBounds = GetScreenBounds();

            for (int i = 0; i < TotalEnemy; i++)
            {
                var newEnemy = defaultPooledEmitter.EmitItem<PooledUnitBase>(enemyUnitPrefab, GetRandomPoint(screenBounds), Vector3.zero);
                newEnemy.SetUnitData(GetRandomEnemyData()); 
                newEnemy.Damageable.OnDeadE += UpdateWave;
                OnOffEnemy(newEnemy, false);

                enemyUnitsLUT.Add(newEnemy);

                Debug.Log(enemyUnitsLUT.Count);

                yield return new WaitForSeconds(spawnDelayDuration);
            }

            enemySpawned = true;
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
                cumulative += data.BasePercentChance;

                if (roll > cumulative) continue;

                return data.EnemyUnitDataSO;
            }

            return null;
        }
        private IEnumerator SetupWaveSequnce(int waveId, DefaultPooledEmitter defaultPooledEmitter, PooledUnitBase enemyUnitPrefab, IDifficulty difficulty, ICountdownPresentation timerPresentation)
        {
            IsCompleted = false;
            IsStarted = false;
            enemySpawned = false;

            totalEnemy = difficulty.CalculateTotalEnemyAmount(waveId % 10);

            CorutineSystem.StartSequnce(SpawnEnemies(defaultPooledEmitter, enemyUnitPrefab));

            yield return new WaitUntil(() => enemySpawned);

            yield return new WaitForSeconds(1f);

            timer = new Countdown(timerPresentation);
            timer.OnCountdownE += OnCountdownFinished;

            OnSetupedE?.Invoke(this);

            Debug.Log("Wave Setuped");
        }
    }
}