using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Events
{
    /// <summary>
    ///     This class listens to the BoolEventSO.
    ///     When the event raised, it can call the methods assigned to it with a boolean parameter.
    /// </summary>
    public class BoolEventListener : MonoBehaviour
    {
        public BoolEventSO EventSO;
        public UnityEvent<bool> Response;

        private void OnEnable()
        {
            EventSO.RegisterListener(this);
        }

        private void OnDisable()
        {
            EventSO.UnregisterListener(this);
        }

        public void OnEventRaised(bool param)
        {
            Response.Invoke(param);
        }
    }
}