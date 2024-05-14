using System;
using UnityEngine;

namespace Game.Asteroids {

    internal sealed class NormalEnemy : MonoBehaviour, IEnemy {

        public static Action OnEnemyDeath;
        public static Action<int> OnScoreSent;

        [SerializeField] private GameObject _deathParticles;
        [SerializeField] private int _scoreToGive;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                other.GetComponent<PlayerController>().Death();
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