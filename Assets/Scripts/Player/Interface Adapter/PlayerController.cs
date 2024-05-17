using UnityEngine;

namespace Asteroids {

    internal sealed class PlayerController : MonoBehaviour {

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
            _playerRotate.GetRotationDirection(direction);
        }

        private void Move() {
            _playerMove.Move();
        }

        private void Attack() {
            _playerAttack.Attack();
        }

    }
}