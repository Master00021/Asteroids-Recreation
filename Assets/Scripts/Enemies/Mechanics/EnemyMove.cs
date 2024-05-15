using System.Collections;
using UnityEngine;

namespace Game.Asteroids {

    internal class EnemyMove : MonoBehaviour {
        
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        internal Vector3 _moveDirection;
        private float _speed;

        protected virtual void Start() {
            _speed = Random.Range(_minSpeed, _maxSpeed);

            StartCoroutine(CO_HorizontalMove());
        }

        private IEnumerator CO_HorizontalMove() {
            while (true) {
                transform.position += _speed * Time.deltaTime * _moveDirection;
                yield return null;
            }
        }

    }
}