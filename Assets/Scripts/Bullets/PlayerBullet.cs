using UnityEngine;
using System;

namespace Asteroids {

    internal sealed class PlayerBullet : Bullet {

        public static Action<int> OnScoreSent;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent<IGiveLive>(out var liveEnemy)) {
                liveEnemy.GiveLive();
            }

            if (other.TryGetComponent<IScore>(out var score)) {
                score.SendScore();
            }

            if (other.TryGetComponent<IKillable>(out var killable)) {
                killable.Death();
            }

            if (!other.CompareTag("Border") && !other.CompareTag("LateralBorder")) {
                Destroy(gameObject);
            }
        }

    }
}