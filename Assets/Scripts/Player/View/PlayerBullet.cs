using System.Collections;
using UnityEngine;
using System;

namespace Game.Asteroids {

    internal sealed class PlayerBullet : MonoBehaviour {

        public static Action<int> OnScoreSent;

        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed;

        private void Start() {
            StartCoroutine(CO_Bullet());
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent<ILiveEnemy>(out var liveEnemy)) {
                liveEnemy.SendLive();
            }

            if (other.TryGetComponent<IEnemy>(out var enemy)) {
                enemy.SendScore();
                enemy.Death();
            }

            if (!other.CompareTag("Border")) {
                Destroy(gameObject);
            }
        }

        private IEnumerator CO_Bullet() {
            Destroy(gameObject, _lifeTime);
            while (true) {
                transform.position += transform.up * _speed * Time.deltaTime;
                yield return null;
            }
        }

    }
}