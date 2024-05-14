using UnityEngine;
using System;

namespace Game.Asteroids {

    internal sealed class PlayerController : MonoBehaviour {

        public static Action OnPlayerDeath;

        [SerializeField] private PlayerAttack _playerAttack;
        [SerializeField] private PlayerRotate _playerRotate;
        [SerializeField] private PlayerMove _playerMove;

        private void OnEnable() {
            GameInput.OnAttackInput += Attack;
            GameInput.OnRotateInput += Rotate;
            GameInput.OnMoveInput += Move;
        }

        private void OnDisable() {
            GameInput.OnAttackInput -= Attack;
            GameInput.OnRotateInput -= Rotate;
            GameInput.OnMoveInput -= Move;
        }

        private void Rotate(float direction) {
            _playerRotate.IsRotating(direction);
        }

        private void Move() {
            _playerMove.Move();
        }

        private void Attack() {
            _playerAttack.Attack();
        }

        internal void Death() {
            OnPlayerDeath?.Invoke();
            gameObject.SetActive(false);
        }

    }
}