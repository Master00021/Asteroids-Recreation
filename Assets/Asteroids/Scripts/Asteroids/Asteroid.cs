using System.Collections;
using UnityEngine;
using System;

namespace Game.Asteroids {

    internal enum AsteroidType {
        Big,
        Medium,
        Small
    }

    internal sealed class Asteroid : MonoBehaviour, IEnemy {

        public static Action<AudioClip> OnDeath;
        public static Action<int> OnScoreSent;
        
        [SerializeField] private int _scoreToGive;
        [SerializeField] private AsteroidType _asteroidType;
        [SerializeField] private GameObject _asteroidToBecome;
        [SerializeField] private GameObject _deathParticles;
        [SerializeField] private AudioClip _death;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        private void Start() {
            StartCoroutine(CO_Movement());
        }

        private void OnEnable() {
            ScoreMenu.OnScoreShowed += AutoDestroy;
        }

        private void OnDisable() {
            ScoreMenu.OnScoreShowed -= AutoDestroy;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                other.GetComponent<PlayerController>().Death();
            }

            // Asteroids have their own layer, so they don't collide between them
            if (other.TryGetComponent<IEnemy>(out var enemy)) {
                enemy.SendScore();
                enemy.Death();
            }

            if (!other.GetComponent<Border>()) {
                Destroy(gameObject);
            }
        }

        private IEnumerator CO_Movement() {
            var randomRotation = UnityEngine.Random.Range(1.0f, 360.0f);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, randomRotation);
            var speed = UnityEngine.Random.Range(_minSpeed, _maxSpeed);

            while (true) {
                transform.position += transform.up * speed * Time.deltaTime;

                yield return null;
            }
        }

        private void AutoDestroy() {
            Destroy(gameObject);
        }

        public void Death() {
            Instantiate(_deathParticles, transform.position, Quaternion.identity);

            if (_asteroidType != AsteroidType.Small) {
                Instantiate(_asteroidToBecome, transform.position, Quaternion.identity);
                Instantiate(_asteroidToBecome, transform.position, Quaternion.identity);
            }

            OnDeath?.Invoke(_death);
            Destroy(gameObject);
        }

        public void SendScore() {
            OnScoreSent?.Invoke(_scoreToGive);
        }

    }
}
