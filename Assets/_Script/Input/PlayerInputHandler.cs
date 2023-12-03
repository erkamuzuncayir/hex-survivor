using _Script.System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Script.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        // Input related
        public PlayerInput PlayerInput;
        public InputAction MouseLeftClick;
        public InputAction MousePosition;
    
        // State System
        [SerializeField] private GameStateMachineSO _gameStateMachineSO;
    
        // Fills InputAction fields.
        private void Awake()
        {
            MouseLeftClick = PlayerInput.actions["MouseLeftClick"];
            MousePosition = PlayerInput.actions["MousePosition"];
        }

        // Subscribes InputAction methods
        private void Start()
        {
            MouseLeftClick.performed += OnMouseLeftClickPerformed;
        }

        // Pass inputs to related systems according to game state system.
        private void OnMouseLeftClickPerformed(InputAction.CallbackContext obj)
        {
            switch (_gameStateMachineSO.CurrentGameState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.PlayerTurn:
                    break;
                case GameState.EnemyTurn:
                    break;
                default:
                    Debug.Log($"{_gameStateMachineSO.CurrentGameState} is not implemented!");
                    break;
            }
        }
    }
}
