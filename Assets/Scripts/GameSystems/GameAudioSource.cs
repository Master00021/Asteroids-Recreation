using UnityEngine;

namespace Asteroids {

    [RequireComponent(typeof(AudioSource))]
    internal sealed class GameAudioSource : MonoBehaviour {

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _enemyDeathSound;

        private void OnEnable() {
            Asteroid.OnPlayDeathSound += PlayAsteroidDeathSound;
            Enemy.OnEnemyDeath += PlayEnemyDeathSound;
        }

        private void OnDisable() {
            Asteroid.OnPlayDeathSound -= PlayAsteroidDeathSound;
            Enemy.OnEnemyDeath -= PlayEnemyDeathSound;
        }

        private void PlayAsteroidDeathSound(AudioClip audioClip) {
            _audioSource.PlayOneShot(audioClip);
        }

        private void PlayEnemyDeathSound() {
            _audioSource.PlayOneShot(_enemyDeathSound);
        }

    }
}