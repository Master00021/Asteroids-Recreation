using UnityEngine;

namespace Asteroids {

    internal sealed class AsteroidMove : MonoBehaviour {
        
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        private float _speed;
        
        private void Start() {
            float randomRotation = Random.Range(1.0f, 360.0f);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, randomRotation);

            _speed = Random.Range(_minSpeed, _maxSpeed);
        }

        private void Update() {
            transform.position += transform.up * _speed * Time.deltaTime;
        }

    }
}