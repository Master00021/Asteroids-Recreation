using UnityEngine;
using System;

namespace Asteroids {

    [RequireComponent(typeof(Rigidbody2D))]
    internal class Enemy : ScoreProvider, IKillable, IScore {

        public static Action OnEnemyDeath;

        [SerializeField] private GameObject _deathParticles;

        private void OnEnable() => GameLifeCycle.OnGameStop += Death;
        private void OnDisable() => GameLifeCycle.OnGameStop -= Death;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.GetComponent<Player>()) {
                Death();
            }

            if (other.CompareTag("LateralBorder")) {
                Destroy(gameObject);
            }
        }

        public void SendScore() {
            OnScoreSent?.Invoke(_scoreToGive);
        }

        public void Death() {
            Instantiate(_deathParticles, transform.position, Quaternion.identity);
            OnEnemyDeath?.Invoke();
            Destroy(gameObject);
        }

    }
}