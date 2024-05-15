using System.Collections;
using UnityEngine;

namespace Asteroids {

    [RequireComponent(typeof(Rigidbody2D))]
    internal class Bullet : MonoBehaviour {
        
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed;

        private void Start() {
            StartCoroutine(CO_Bullet());
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