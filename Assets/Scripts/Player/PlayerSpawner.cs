using System.Collections;
using UnityEngine;
using System;

namespace Asteroids {

    internal sealed class PlayerSpawner : MonoBehaviour {

        public static Action OnPlayerSpawned;
    
        [SerializeField] private GameObject _player;
        [SerializeField] private LayerMask _asteroidsLayer;
        [SerializeField] private float _timeToRespawn;
        [SerializeField] private float _spawnRadius = 2.0f;

        private GameObject _spawnedPlayer;
        private Quaternion _playerRotation;

        private void OnEnable() {
            GameLifeCycle.OnGameStart += OnGameStart;
            PlayerData.OnLivesDecreased += StartRespawn;
            PlayerRotate.OnPlayerDeath += GetPlayerRotation;
        }

        private void OnDisable() {
            GameLifeCycle.OnGameStart -= OnGameStart;
            PlayerData.OnLivesDecreased -= StartRespawn;
            PlayerRotate.OnPlayerDeath -= GetPlayerRotation;
        }

        private void OnGameStart() {
            _playerRotation = Quaternion.identity;
            StartCoroutine(CO_Spawn());
        }

        private void GetPlayerRotation(Quaternion playerRotation) {
            _playerRotation = playerRotation;
        }

        private void StartRespawn() {
            StartCoroutine(CO_Spawn());
        }

        private IEnumerator CO_Spawn() {
            yield return new WaitForSeconds(_timeToRespawn);

            Collider2D hit = Physics2D.OverlapCircle(transform.position, _spawnRadius, _asteroidsLayer);

            while (hit) {
                hit = Physics2D.OverlapCircle(transform.position, _spawnRadius, _asteroidsLayer);
                yield return new WaitForSeconds(0.1f);
            }

            if (_spawnedPlayer == null) {
                _spawnedPlayer = Instantiate(_player, transform.position, Quaternion.identity);
            }
            else {
                _spawnedPlayer.transform.SetPositionAndRotation(transform.position, _playerRotation);
            }

            _spawnedPlayer.SetActive(true);
            OnPlayerSpawned?.Invoke();
        }

    }
}