using UnityEngine;

namespace Game.Asteroids {

    internal sealed class EnemyBullet : Bullet {
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                other.GetComponent<PlayerController>().Death();
                Destroy(gameObject);
            }

            if (other.TryGetComponent<IEnemy>(out var enemy)) {
                enemy.Death();
                Destroy(gameObject);
            }
        }
        
    }
}