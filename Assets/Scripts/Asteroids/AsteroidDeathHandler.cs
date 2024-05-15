using UnityEngine;
using System;

namespace Game.Asteroids {

    internal sealed class AsteroidDeathHandler : MonoBehaviour, IEnemy {

        public static Action<AudioClip> OnPlayDeathSound;
        public static Action OnDeath;

        [SerializeField] private GameObject _asteroidToBecome;
        [SerializeField] private GameObject _deathParticles;
        [SerializeField] private AudioClip _deathSound;
        [SerializeField] private bool _isSmallAsteroid;

        public void Death() {
            OnDeath?.Invoke();
            OnPlayDeathSound?.Invoke(_deathSound);

            Instantiate(_deathParticles, transform.position, Quaternion.identity);

            if (!_isSmallAsteroid) {
                Instantiate(_asteroidToBecome, transform.position, Quaternion.identity);
                Instantiate(_asteroidToBecome, transform.position, Quaternion.identity);
            }
        }

    }
}

