using UnityEngine;
using System;

namespace Asteroids {

    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class Asteroid : ScoreProvider, IScore, IKillable {

        public static Action<AudioClip> OnPlayDeathSound;
        public static Action OnAsteroidDeath;

        [SerializeField] private GameObject _asteroidToBecome;
        [SerializeField] private GameObject _deathParticles;
        [SerializeField] private AudioClip _deathSound;
        [SerializeField] private bool _isSmallAsteroid;

        private void OnEnable() => GameLifeCycle.OnGameStop += GameOver;
        private void OnDisable() => GameLifeCycle.OnGameStop -= GameOver;

        private void OnTriggerEnter2D(Collider2D other) {
            // Asteroids have their own layer, so they don't collide between them
            if (other.TryGetComponent<IKillable>(out var killable)) {
                killable.Death();
            }

            if (other.TryGetComponent<IScore>(out var score)) {
                score.SendScore();
            }

            if (other.GetComponent<Player>()) {
                Death();
            }
        }

        public void SendScore() {
            OnScoreSent?.Invoke(_scoreToGive);
        }

        public void Death() {
            OnAsteroidDeath?.Invoke();
            OnPlayDeathSound?.Invoke(_deathSound);

            Instantiate(_deathParticles, transform.position, Quaternion.identity);

            if (!_isSmallAsteroid) {
                Instantiate(_asteroidToBecome, transform.position, Quaternion.identity);
                Instantiate(_asteroidToBecome, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        private void GameOver() {
            Destroy(gameObject);
        }

    }
}