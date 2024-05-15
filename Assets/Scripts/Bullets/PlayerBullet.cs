using UnityEngine;
using System;

namespace Game.Asteroids {

    internal sealed class PlayerBullet : Bullet {

        public static Action<int> OnScoreSent;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent<ILiveEnemy>(out var liveEnemy)) {
                liveEnemy.SendLive();
            }

            if (other.TryGetComponent<IEnemy>(out var enemy)) {
                enemy.Death();
            }

            if (other.TryGetComponent<IScore>(out var score)) {
                score.SendScore();
            }

            if (!other.CompareTag("Border")) {
                Destroy(gameObject);
            }
        }

    }
}