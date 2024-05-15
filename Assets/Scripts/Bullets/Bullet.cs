using UnityEngine;

namespace Asteroids {

    [RequireComponent(typeof(Rigidbody2D))]
    internal class Bullet : MonoBehaviour {
        
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed;

        private void Start() {
            Destroy(gameObject, _lifeTime);
        }

        private void Update() {
            transform.position += transform.up * _speed * Time.deltaTime;
        }

    }
}