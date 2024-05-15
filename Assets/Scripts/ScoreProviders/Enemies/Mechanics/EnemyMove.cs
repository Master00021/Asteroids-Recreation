using UnityEngine;

namespace Asteroids {

    internal class EnemyMove : MonoBehaviour {
        
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        internal Vector3 _moveDirection;
        private float _speed;

        protected virtual void Start() {
            _speed = Random.Range(_minSpeed, _maxSpeed);
        }

        private void Update() {
            transform.position += _speed * Time.deltaTime * _moveDirection;
        }

    }
}