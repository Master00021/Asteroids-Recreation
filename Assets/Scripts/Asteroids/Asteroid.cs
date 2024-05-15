using UnityEngine;
using System;

namespace Game.Asteroids {

    internal sealed class Asteroid : MonoBehaviour, IScore {

        public static Action<int> OnScoreSent;
        
        [SerializeField] private int _scoreToGive;

        private void OnEnable() => GameLifeCycle.OnGameStop += AutoDestroy;
        private void OnDisable() => GameLifeCycle.OnGameStop -= AutoDestroy;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                other.GetComponent<PlayerController>().Death();
            }

            // Asteroids have their own layer, so they don't collide between them
            if (other.TryGetComponent<IEnemy>(out var enemy)) {
                enemy.Death();
            }

            if (other.TryGetComponent<IScore>(out var score)) {
                score.SendScore();
            }

            if (!other.GetComponent<Border>()) {
                Destroy(gameObject);
            }
        }

        public void SendScore() {
            OnScoreSent?.Invoke(_scoreToGive);
        }

        private void AutoDestroy() {
            Destroy(gameObject);
        }

    }
}
