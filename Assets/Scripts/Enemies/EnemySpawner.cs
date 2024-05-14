using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Game.Asteroids {

    internal sealed class EnemySpawner : MonoBehaviour {

        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private GameObject _normalEnemy;
        [SerializeField] private GameObject _liveEnemy;
        [SerializeField] private float _minTimeToSpawn;
        [SerializeField] private float _maxTimeToSpawn;

        private Coroutine _spawnEnemy;
        private bool _leftPointSpawn;

        private void OnEnable() {
            AsteroidWaves.OnNewWave += StartEnemySpawn;
            AsteroidWaves.OnWaveEnd += StopSpawner;
            PlayerData.OnGameOver += StopSpawner;
        }

        private void OnDisable() {
            AsteroidWaves.OnNewWave -= StartEnemySpawn;
            AsteroidWaves.OnWaveEnd -= StopSpawner;
            PlayerData.OnGameOver -= StopSpawner;
        }

        private void StartEnemySpawn() {
            _spawnEnemy = StartCoroutine(CO_SpawnEnemy());
        }

        private IEnumerator CO_SpawnEnemy() {
            while (true) {
                float randomTimeToSpawn = Random.Range(_minTimeToSpawn, _maxTimeToSpawn);
                yield return new WaitForSeconds(randomTimeToSpawn);

                _leftPointSpawn = Random.value > 0.5f;
                float randomPoint;
                
                if (_leftPointSpawn) {
                    randomPoint = Random.Range(0.0f, _spawnPoints.Count / 2);
                }
                else {
                    randomPoint = Random.Range(_spawnPoints.Count / 2, _spawnPoints.Count);
                }

                GameObject enemy;
                bool liveEnemy = Random.value > 0.8f;

                if (liveEnemy) {
                    enemy = Instantiate(_liveEnemy, _spawnPoints[(int)randomPoint].position, Quaternion.identity);
                }
                else {
                    enemy = Instantiate(_normalEnemy, _spawnPoints[(int)randomPoint].position, Quaternion.identity);
                }

                if (_leftPointSpawn) {
                    enemy.GetComponent<EnemyMove>()._spawnLocation = SpawnLocation.Right;
                }
                else {
                    enemy.GetComponent<EnemyMove>()._spawnLocation = SpawnLocation.Left;
                }

                yield return null;
            }
        }

        private void StopSpawner() {
            StopCoroutine(_spawnEnemy);
        }

    }
}