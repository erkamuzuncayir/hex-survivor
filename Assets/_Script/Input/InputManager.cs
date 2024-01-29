using _Script.PersonalAPI.Data.RuntimeSet;
using _Script.PersonalAPI.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

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

        // Layers
        [SerializeField] private LayerMask _tilemapLayer;

        // Cache fields
        [SerializeField] private GameObjectRuntimeSet _so_rs_pfb_tilemap_base;
        private Tilemap _baseTilemap;
        private HoverInputHandler _lastHoveredGOHoverInputHandler;

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
            _ia_mousePosition.performed += OnMouseMoved;
        }

        private void Start()
        {
            _playerCam = _so_rs_playerCam.Items[0].GetComponent<Camera>();
            _baseTilemap = _so_rs_pfb_tilemap_base.Items[0].GetComponent<Tilemap>();
        }

        private void OnDisable()
        {
            _ia_mouseLeftClick.performed -= OnMouseLeftClickPerformed;
            _ia_mousePosition.performed -= OnMouseMoved;
            _ia_mousePosition.Disable();
            _ia_mouseLeftClick.Disable();
        }

        private void OnMouseMoved(InputAction.CallbackContext obj)
        {
            ProcessHover();
        }

        private void ProcessHover()
        {
            Vector2 inputWorldPos = GetInputWorldPosition();
            RaycastHit2D hit = Physics2D.Raycast(inputWorldPos, Vector2.zero);

            if (hit.collider == null) return;
            if (!hit.collider.gameObject.TryGetComponent(out HoverInputHandler hoverInputHandler)) return;

            if (_lastHoveredGOHoverInputHandler != null)
                _lastHoveredGOHoverInputHandler.OnHoverCanceled();

            _lastHoveredGOHoverInputHandler = hoverInputHandler;
            _lastHoveredGOHoverInputHandler.OnHoverPerformed.Invoke(inputWorldPos);
        }

        // Pass inputs to related systems according to game state system.
        private void OnMouseLeftClickPerformed(InputAction.CallbackContext inputData)
        {
            Vector2 inputWorldPos = GetInputWorldPosition();
            RaycastHit2D hit = Physics2D.Raycast(inputWorldPos, Vector2.zero);

            if (hit.collider == null) return;
            if (!hit.collider.gameObject.TryGetComponent(out ClickInputHandler clickInputHandler))
                return;
            if (hit.collider.gameObject.TryGetComponent(out HoverInputHandler hoverInputHandler))
                hoverInputHandler.OnHoverCanceled();

            clickInputHandler.OnClickPerformed.Invoke(inputWorldPos);
        }

        private Vector2 GetInputWorldPosition()
        {
            Vector2 inputScreenPos = _ia_mousePosition.ReadValue<Vector2>();
            Vector2 inputWorldPos = _playerCam.ScreenToWorldPoint(inputScreenPos);
            return inputWorldPos;
        }
    }
}