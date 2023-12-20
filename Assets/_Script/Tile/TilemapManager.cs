using _Script.PersonalAPI.Input;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace _Script.Tile
{
    public class TilemapManager : MonoBehaviour
    {
        // Input Handler
        [SerializeField] private ClickInputHandler _clickInputHandler;

        private void OnEnable()
        {
            _clickInputHandler.OnClickPerformed += OnMouseClickPerformed;
        }


        private void OnDisable()
        {
            _clickInputHandler.OnClickPerformed -= OnMouseClickPerformed;
        }

        private void OnMouseClickPerformed()
        {
            
        }
    }
}
