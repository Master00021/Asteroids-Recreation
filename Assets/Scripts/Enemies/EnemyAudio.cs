using UnityEngine;

namespace Game.Asteroids {

    internal sealed class EnemyAudio : MonoBehaviour {

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _bulletSound;
        [SerializeField] private AudioClip _enemyMusic;

        private void OnEnable() => EnemyAttack.OnAttack += PlayAttackSound;
        private void OnDisable() => EnemyAttack.OnAttack -= PlayAttackSound;

        private void Start() {
            _audioSource.PlayOneShot(_enemyMusic);
        }

        private void PlayAttackSound() {
            _audioSource.PlayOneShot(_bulletSound);
        }

    }
}   