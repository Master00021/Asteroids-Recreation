using UnityEngine;
using System;

namespace Asteroids {

    internal sealed class PlayerAttack : MonoBehaviour {

        public static Action OnAttack;

        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _spawn;

        public void Attack() {
            Instantiate(_bullet, _spawn.position, transform.rotation);
            OnAttack?.Invoke();
        }

    }
}