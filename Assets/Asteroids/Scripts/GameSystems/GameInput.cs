using UnityEngine.InputSystem;
using UnityEngine;
using System;

namespace Game.Asteroids {

    internal sealed class GameInput : MonoBehaviour, ShipInput.IAsteroidShipActions, ShipInput.IStartGameActions, ShipInput.IEndGameActions {
        
        public static Action<float> OnRotateInput;
        public static Action OnAttackInput;
        public static Action OnMoveInput;
        public static Action OnGameStart;
        public static Action OnGameStop;
        public static Action OnGameEnded;

        private ShipInput _shipInput;

        private void Awake() {
            _shipInput = new();
            ChangeInputMap(_shipInput.StartGame, _shipInput.AsteroidShip);
        }

        private void OnEnable() {
            _shipInput.StartGame.SetCallbacks(this);
            _shipInput.AsteroidShip.SetCallbacks(this);
            _shipInput.EndGame.SetCallbacks(this);

            ScoreMenu.OnScoreShowed += OnScoreMenu;
            PlayerController.OnPlayerDeath += DisableCurrentInputMap;
            PlayerSpawner.OnPlayerSpawned += EnableCurrentInputMap;
        }

        private void OnDisable() {
            _shipInput.StartGame.RemoveCallbacks(this);
            _shipInput.AsteroidShip.RemoveCallbacks(this);
            _shipInput.EndGame.RemoveCallbacks(this);
            _shipInput = null;

            ScoreMenu.OnScoreShowed -= OnScoreMenu;
            PlayerController.OnPlayerDeath -= DisableCurrentInputMap;
            PlayerSpawner.OnPlayerSpawned -= EnableCurrentInputMap;
        }

        internal static void ChangeInputMap(InputActionMap newActionMap, InputActionMap oldActionMap) {
            if (!newActionMap.enabled) {
                newActionMap.Enable();
                oldActionMap.Disable();
            }   
        }

        public void OnFire(InputAction.CallbackContext context) {
            if (context.started) {
                OnAttackInput?.Invoke();
            }
        }

        public void OnMove(InputAction.CallbackContext context) {
            if (context.started || context.canceled) {
                OnMoveInput?.Invoke();
            }
        }

        public void OnRotate(InputAction.CallbackContext context) {
            if (context.started || context.canceled) {
                OnRotateInput?.Invoke(context.ReadValue<float>());
            }
        }

        public void OnPressToStart(InputAction.CallbackContext context) {
            if (context.started) {
                ChangeInputMap(_shipInput.AsteroidShip, _shipInput.StartGame);
                OnGameStart?.Invoke();
            }
        }
        
        public void OnPressToEnd(InputAction.CallbackContext context) {
            if (context.started) {
                ChangeInputMap(_shipInput.StartGame, _shipInput.EndGame);
                OnGameEnded?.Invoke();
            }
        }

        private void OnScoreMenu() {
            ChangeInputMap(_shipInput.EndGame, _shipInput.AsteroidShip);
            OnGameStop?.Invoke();
        }

        private void DisableCurrentInputMap() {
            if (_shipInput.AsteroidShip.enabled) {
                _shipInput.AsteroidShip.Disable();
            }
        }

        private void EnableCurrentInputMap(GameObject _) {
            if (!_shipInput.AsteroidShip.enabled) {
                _shipInput.AsteroidShip.Enable();
            }
        }

    }
}

