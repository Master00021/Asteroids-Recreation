using System.Collections;
using UnityEngine;
using System;

namespace Game.Asteroids {

    internal sealed class AsteroidWaves : MonoBehaviour {

        public static Action OnWaveStart;
        public static Action OnWaveEnd;

        [SerializeField] private GameObject _asteroid;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private int _firstWaveAsteroids = 4;
        [SerializeField] private int _maxAsteroidsToSpawn = 12;
        [SerializeField] private int _asteroidsWaveIncrease = 2;
        [SerializeField] private float _timeBetweenWaves;

        private int _asteroidsToSpawn;

        private void OnEnable() {
            GameLifeCycle.OnGameStart += StartWave;
            AsteroidDeathHandler.OnDeath += CheckCurrentWave;
        }
        
        private void OnDisable() {
            GameLifeCycle.OnGameStart -= StartWave;
            AsteroidDeathHandler.OnDeath -= CheckCurrentWave;
        }

        private void StartWave() {
            _asteroidsToSpawn = _firstWaveAsteroids;
            StartCoroutine(CO_WaitForNextWave());
        }

        private void CheckCurrentWave() {
            var currentAsteroids = FindObjectsOfType<Asteroid>();
            if (currentAsteroids.Length <= 1) {
                OnWaveEnd?.Invoke();
                StartCoroutine(CO_WaitForNextWave());
            }
        }

        private IEnumerator CO_WaitForNextWave() {
            var timer = _timeBetweenWaves;
            while (timer > 0.0f) {
                timer -= Time.deltaTime;
                yield return null;
            }
            
            NewWave();
        }

        private void NewWave() {
            for (int i = 0; i < _asteroidsToSpawn; i++) {
                var randomSpawnPoint = UnityEngine.Random.Range(0, _spawnPoints.Length);
                Instantiate(_asteroid, _spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            }
            
            _asteroidsToSpawn += _asteroidsWaveIncrease;

            if (_asteroidsToSpawn > _maxAsteroidsToSpawn) {
                _asteroidsToSpawn = _maxAsteroidsToSpawn;
            }
            
            OnWaveStart?.Invoke();
        }

    }
}