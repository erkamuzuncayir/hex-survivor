using UnityEngine;
using UnityEngine.Events;

namespace _Script.PersonalAPI.Event
{
    /// <summary>
    ///     This class listens to the Vector3EventSO.
    ///     When the event raised, it calls the methods assigned to it with a Vector3 parameter.
    /// </summary>
    public class Vector3EventListener : MonoBehaviour
    {
        public Vector3EventSO EventSO;
        public UnityEvent<Vector3> Response;

        private void OnEnable()
        {
            EventSO.RegisterListener(this);
        }

        private void OnDisable()
        {
            EventSO.UnregisterListener(this);
        }

        public void OnEventRaised(Vector3 param)
        {
            Response.Invoke(param);
        }
    }
}