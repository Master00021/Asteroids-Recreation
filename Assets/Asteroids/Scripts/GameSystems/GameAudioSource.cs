using UnityEngine;

namespace Game.Asteroids {

    public class GameAudioSource : MonoBehaviour {

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _enemyDeathSound;

        private void OnEnable() {
            Asteroid.OnDeath += PlayAsteroidDeathSound;
            NormalEnemy.OnEnemyDeath += PlayEnemyDeathSound;
        }

        private void OnDisable() {
            Asteroid.OnDeath -= PlayAsteroidDeathSound;
            NormalEnemy.OnEnemyDeath -= PlayEnemyDeathSound;
        }

        private void PlayAsteroidDeathSound(AudioClip audioClip) {
            _audioSource.PlayOneShot(audioClip);
        }

        private void PlayEnemyDeathSound() {
            _audioSource.PlayOneShot(_enemyDeathSound);
        }

    }
}

