using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Level.Data;
using Tower;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        #region Properties

        [SerializeField] private LevelData levelData;

        [SerializeField] private List<Transform> startPoints;

        #endregion

        #region Variables

        private Transform _enemiesContainer;

        private Queue<EnemyController> _enemiesQueue;


        private int _waveIndex;
        private float _waveStartTime;
        private float _waveTimeout;
        private float _waveEnemyTimeout;
        private float _waveLastEnemyReleaseTime;
        private WaveStatus _waveStatus;

        #endregion

        #region Events

        private void Awake()
        {
            _enemiesContainer = transform.GetChild(0);

            ActiveWave(0);
        }

        private void Update()
        {
            CheckWave();
        }

        #endregion

        #region Set

        private void ActiveWave(int index)
        {
            if (index >= levelData.waves.Count)
            {
                _waveStatus = WaveStatus.Done;
                return;
            }

            var wave = levelData.waves[index];

            _waveIndex = index;
            _waveStatus = WaveStatus.Wait;
            _waveStartTime = Time.time;
            _waveTimeout = wave.timeout;
            _waveEnemyTimeout = wave.enemyTimeout;
            _waveLastEnemyReleaseTime = 0;

            BuildEnemiesQueue(wave);
        }

        private void BuildEnemiesQueue(WaveData wave)
        {
            _enemiesQueue = new Queue<EnemyController>();

            var waveEnemiesCount = wave.enemies.Count;
            var startPointCount = startPoints.Count;
            for (var i = 0; i < wave.enemyCount; i++)
            {
                var prefab = levelData.enemies[wave.enemies[Random.Range(0, waveEnemiesCount)]];
                var startPoint = startPoints[Random.Range(0, startPointCount)];

                var enemy = Instantiate(prefab, startPoint.position, Quaternion.identity, _enemiesContainer);
                enemy.GetComponent<EnemyController>().SetTargetPoint(startPoint);
                enemy.gameObject.SetActive(false);
                _enemiesQueue.Enqueue(enemy);
            }
        }

        #endregion

        #region Get

        public List<TowerController> towers() => levelData.towers;

        public float firstPoint() => levelData.firstPoint;

        #endregion

        #region Check

        private void CheckWave()
        {
            if (_waveStatus.Equals(WaveStatus.Done)) return;

            if (_waveStatus.Equals(WaveStatus.Wait))
            {
                if (_waveStartTime + _waveTimeout > Time.time) return;
                _waveStatus = WaveStatus.Active;
            }
            
            if (_waveLastEnemyReleaseTime + _waveEnemyTimeout > Time.time) return;

            if (_enemiesQueue.Count.Equals(0))
            {
                ActiveWave(_waveIndex + 1);
                return;
            }

            var enemy = _enemiesQueue.Dequeue();
            enemy.gameObject.SetActive(true);
            _waveLastEnemyReleaseTime = Time.time;
        }

        #endregion
    }

    public enum WaveStatus
    {
        Wait,
        Active,
        Done
    }
}