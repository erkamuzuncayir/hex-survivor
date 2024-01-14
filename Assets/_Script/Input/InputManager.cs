using _Script.PersonalAPI.Data.RuntimeSet;
using _Script.PersonalAPI.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Script.Input
{
    public class InputManager : MonoBehaviour
    {
        // Camera
        [SerializeField] private GameObjectRuntimeSet _so_rs_playerCam;
        private Camera _playerCam;
        
        // Unity's InputSystem related
        public InputActionReference MouseLeftClickReference;
        public InputActionReference MousePositionReference;
        private InputAction _ia_mouseLeftClick;
        private InputAction _ia_mousePosition;
    
        // Cache fields
        private GameObject _targetedGameObject;
        
        // Fills InputAction fields.
        private void Awake()
        {
            _ia_mouseLeftClick = MouseLeftClickReference.action;
            _ia_mousePosition = MousePositionReference.action;
        }

        // Subscribes InputAction methods
        private void OnEnable()
        {
            _ia_mousePosition.Enable();
            _ia_mouseLeftClick.Enable();
            _ia_mouseLeftClick.performed += OnMouseLeftClickPerformed;
            _ia_mouseLeftClick.canceled += OnMouseLeftClickCanceled;
        }

        private void Start()
        {
            _playerCam = _so_rs_playerCam.Items[0].GetComponent<Camera>();
        }

        private void OnDisable()
        {
            _ia_mousePosition.Disable();
            _ia_mouseLeftClick.Disable();
        }

        // Pass inputs to related systems according to game state system.
        private void OnMouseLeftClickPerformed(InputAction.CallbackContext inputData)
        { 
            Vector2 mouseScreenPos = _ia_mousePosition.ReadValue<Vector2>();
            Vector2 mouseWorldPos = _playerCam.ScreenToWorldPoint(mouseScreenPos);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            
            if (hit.collider.gameObject == null) return;
            if(hit.collider.gameObject.GetComponent<ClickInputHandler>() == null) return;
            hit.collider.gameObject.GetComponent<ClickInputHandler>().OnClickPerformed.Invoke(mouseWorldPos);
        }
        
        private void OnMouseLeftClickCanceled(InputAction.CallbackContext obj)
        {
        }
    }
}
