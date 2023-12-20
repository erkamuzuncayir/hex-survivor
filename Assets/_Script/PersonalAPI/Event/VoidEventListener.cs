using UnityEngine;
using UnityEngine.Events;

namespace _Script.PersonalAPI.Event
{
    /// <summary>
    ///     This class listens to the VoidEventSO.
    ///     When the event raised, it calls the methods assigned to it.
    /// </summary>
    public class VoidEventListener : MonoBehaviour
    {
        public VoidEventSO EventSO;
        public UnityEvent Response;

        private void OnEnable()
        {
            EventSO.RegisterListener(this);
        }

        private void OnDisable()
        {
            EventSO.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}