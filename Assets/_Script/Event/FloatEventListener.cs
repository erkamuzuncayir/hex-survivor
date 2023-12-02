using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Events
{
    /// <summary>
    ///     This class listens to the FloatEventSO.
    ///     When the event raised, it calls the methods assigned to it with a float parameter.
    /// </summary>
    public class FloatEventListener : MonoBehaviour
    {
        public FloatEventSO EventSO;
        public UnityEvent<float> Response;

        private void OnEnable()
        {
            EventSO.RegisterListener(this);
        }

        private void OnDisable()
        {
            EventSO.UnregisterListener(this);
        }

        public void OnEventRaised(float param)
        {
            Response.Invoke(param);
        }
    }
}