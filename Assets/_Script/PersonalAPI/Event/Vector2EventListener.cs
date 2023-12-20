using UnityEngine;
using UnityEngine.Events;

namespace _Script.PersonalAPI.Event
{
    /// <summary>
    ///     This class listens to the Vector2EventSO.
    ///     When the event raised, it calls the methods assigned to it with a Vector2 parameter.
    /// </summary>
    public class Vector2EventListener : MonoBehaviour
    {
        public Vector2EventSO EventSO;
        public UnityEvent<Vector2> Response;

        private void OnEnable()
        {
            EventSO.RegisterListener(this);
        }

        private void OnDisable()
        {
            EventSO.UnregisterListener(this);
        }

        public void OnEventRaised(Vector2 param)
        {
            Response.Invoke(param);
        }
    }
}