using BlueRacconGames.Pool;
using Game.Difficulty;
using Game.Map;
using RDG.Platforms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private Countdown countdown;
        private bool enemySpawned = false;

        protected readonly float countdownTime = 3;
        protected readonly int maxRandomizeIteration = 10;
        protected readonly List<IUnit> enemyUnitsLUT = new();

        public int TotalEnemy => totalEnemy;
        public WaveEnemyUnitData[] WaveEnemyUnitDatas => initialData;

        public int EnemyRemain => enemyUnitsLUT.Count;
        public bool IsReady {  get; private set; }
        public bool IsStarted { get; private set; }
        public bool IsCompleted { get; private set; }

        public event Action<IEnemyWave> OnSetupedE;
        public event Action<IEnemyWave> OnStartedE;
        public event Action<IEnemyWave> OnEndedE;
        public event Action<IEnemyWave> OnCompletedE;
        public event Action<IEnemyWave> OnUpdatedE;

        public EnemyWaveBase(WaveEnemyUnitData[] initialData)
        {
            IsReady = false;

            this.initialData = initialData;
        }

        public void SetupWave(int waveId, UnitPoolEmitter unitSpawner, PooledUnitBase enemyUnitPrefab, MapData mapData, IDifficulty difficulty, ICountdownPresentation timerPresentation)
        {
            CorutineSystem.StartSequnce(SetupWaveSequnce(waveId, unitSpawner, enemyUnitPrefab, mapData, difficulty, timerPresentation));
        }
        public void StartWave()
        {
            if (IsCompleted || IsStarted) return;

            countdown.StartCountdown(countdownTime, 1);
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

            ResetEvent();

            ClearEnemies();

            Debug.Log("Wave Completed");
        }

        protected virtual void OnCountdownFinished()
        {
            countdown = null;

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
            unit.Damageable.SetDamagableOn(value);
            var enemyUnit = unit as PooledEnemyUnit;

            enemyUnit.AIController.ForceStartStopSimulate(value);
        }

        private void ResetEvent()
        {
            OnSetupedE = null;
            OnStartedE = null;
            OnEndedE = null;
            OnCompletedE = null;
            OnUpdatedE = null;
        }
        private bool CheckIsCompleted()
        {
            return enemyUnitsLUT.Count <= 0;
        }
        private IEnumerator SpawnEnemies(UnitPoolEmitter unitSpawner, PooledUnitBase enemyUnitPrefab, MapData mapData)
        {
            for (int i = 0; i < TotalEnemy; i++)
            {
                var enemyData = GetRandomEnemyData();

                var newEnemy = unitSpawner.SpawnUnit(enemyUnitPrefab, enemyData.LaunchVFX, GetSpawnPoint(mapData), Vector3.zero);
                newEnemy.SetUnitData(enemyData); 
                newEnemy.Damageable.OnDeadE += UpdateWave;
                OnOffEnemy(newEnemy, false);

                enemyUnitsLUT.Add(newEnemy);

                Debug.Log(enemyUnitsLUT.Count);

                yield return new WaitForSeconds(spawnDelayDuration);
            }

            enemySpawned = true;
        }
        private Vector2 GetSpawnPoint(MapData mapData)
        {
            int iterationAmount = 100;

            bool isValidPoint = false;

            Vector2 point = Vector2.zero;

            while (!isValidPoint)
            {
                if(iterationAmount <= 0) return Vector2.zero;

                iterationAmount--;

                point = GetRandomPoint(mapData.Bounds);

                if (IsInBounds(point, mapData.BlockedSpawnPoosition)) continue;

                isValidPoint = true;
            }

            return point;
        }
        private bool IsInBounds(Vector2 point, Bounds[] bounds)
        {
            foreach(var bound in bounds)
                if (bound.Contains(point)) return true;

            return false;
        }
        private Vector2 GetRandomPoint(Vector2 bounds)
        {
            return new Vector2(
                Random.Range(-bounds.x, bounds.x),
                Random.Range(-bounds.y, bounds.y)
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
        private IEnumerator SetupWaveSequnce(int waveId, UnitPoolEmitter unitSpawner, PooledUnitBase enemyUnitPrefab, MapData mapData, IDifficulty difficulty, ICountdownPresentation timerPresentation)
        {
            IsCompleted = false;
            IsStarted = false;
            enemySpawned = false;

            totalEnemy = difficulty.CalculateTotalEnemyAmount(waveId % 10);

            CorutineSystem.StartSequnce(SpawnEnemies(unitSpawner, enemyUnitPrefab, mapData));

            yield return new WaitUntil(() => enemySpawned);

            countdown = new Countdown(timerPresentation);
            countdown.OnCountdownE += OnCountdownFinished;

            OnSetupedE?.Invoke(this);

            IsReady = true;

            Debug.Log("Wave Setuped");
        }
        private void ClearEnemies()
        {
            if (enemyUnitsLUT.Count <= 0) return;

            foreach (var unit in enemyUnitsLUT)
            {
                unit.Damageable.TakeDamage(99999999);
            }
        }
    }
}