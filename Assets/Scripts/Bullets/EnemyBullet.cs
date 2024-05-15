using UnityEngine;

namespace Asteroids {

    internal sealed class EnemyBullet : Bullet {
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                other.GetComponent<Player>().Death();
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