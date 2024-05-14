using UnityEngine;
using System;

namespace Game.Asteroids {

    internal sealed class PlayerAttack : MonoBehaviour {

        public static Action OnFire;

        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _spawn;

        public void Attack() {
            Instantiate(_bullet, _spawn.position, transform.rotation);
            OnFire?.Invoke();
        }

    }
}