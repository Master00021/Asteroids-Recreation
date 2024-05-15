using System.Collections;
using UnityEngine;

namespace Game.Asteroids {

    public class EnemyBullet : MonoBehaviour {
        
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed;

        private void Start() {
            StartCoroutine(CO_Bullet());
        }

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

        private IEnumerator CO_Bullet() {
            Destroy(gameObject, _lifeTime);
            while (true) {
                transform.position += transform.up * _speed * Time.deltaTime;
                yield return null;
            }
        }

    }
}

