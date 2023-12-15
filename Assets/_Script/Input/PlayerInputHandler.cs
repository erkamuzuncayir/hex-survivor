using _Script.PersonalAPI.Event;
using _Script.System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using NotImplementedException = System.NotImplementedException;

namespace _Script.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        // Camera
        [SerializeField] private Camera _mainCamera;
        
        // Unity's InputSystem related
        public InputActionReference MouseLeftClickReference;
        public InputActionReference MousePositionReference;
        private InputAction IA_mouseLeftClick;
        private InputAction IA_mousePosition;
    
        // SO-Based events
        [SerializeField] private VoidEventSO so_event_onClickPerformed;
        [SerializeField] private VoidEventSO so_event_onClickCanceled;
    
        // Fills InputAction fields.
        private void Awake()
        {
            IA_mouseLeftClick = MouseLeftClickReference.action;
            IA_mousePosition = MousePositionReference.action;
        }

        // Subscribes InputAction methods
        private void OnEnable()
        {
            IA_mousePosition.Enable();
            IA_mouseLeftClick.Enable();
            IA_mouseLeftClick.performed += OnMouseLeftClickPerformed;
            IA_mouseLeftClick.canceled += OnMouseLeftClickCanceled;
        }

        private void OnDisable()
        {
            IA_mousePosition.Disable();
            IA_mouseLeftClick.Disable();
        }

        // Pass inputs to related systems according to game state system.
        private void OnMouseLeftClickPerformed(InputAction.CallbackContext obj)
        { 
            Vector2 mouseScreenPos = IA_mousePosition.ReadValue<Vector2>();
            Vector2 mouseWorldPos = _mainCamera.ScreenToWorldPoint(mouseScreenPos);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            so_event_onClickPerformed.Raise();
        }
        
        private void OnMouseLeftClickCanceled(InputAction.CallbackContext obj)
        {
            so_event_onClickCanceled.Raise();
        }
    }
}
