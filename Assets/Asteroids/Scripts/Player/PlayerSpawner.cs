using System.Collections;
using UnityEngine;
using System;

namespace Game.Asteroids {

    public class PlayerSpawner : MonoBehaviour {

        public static Action<GameObject> OnPlayerSpawned;
    
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _center;
        [SerializeField] private LayerMask _asteroidsLayer;
        [SerializeField] private float _timeToRespawn;

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

        private void StartRespawn() {
            StartCoroutine(CO_Spawn());
        }

        private IEnumerator CO_Spawn() {
            yield return new WaitForSeconds(_timeToRespawn);

            Collider2D hit = Physics2D.OverlapCircle(_center.position, 2.0f, _asteroidsLayer);

            while (hit) {
                hit = Physics2D.OverlapCircle(_center.position, 1.5f, _asteroidsLayer);
                yield return new WaitForSeconds(1.0f);
            }

            if (_spawnedPlayer == null) {
                _spawnedPlayer = Instantiate(_player, _center.position, Quaternion.identity);
            }
            else {
                _spawnedPlayer.transform.SetPositionAndRotation(_center.position, _playerRotation);
            }

            _spawnedPlayer.SetActive(true);
            OnPlayerSpawned?.Invoke(_spawnedPlayer);
        }

        private void GetPlayerRotation(Quaternion playerRotation) {
            _playerRotation = playerRotation;
        }

    }
}


