using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Events
{
    /// <summary>
    ///     This class listens to the EventSO.
    ///     When the event raised, it calls the methods assigned to it with a int parameter.
    /// </summary>
    public class GOEventListener : MonoBehaviour
    {
        public GOEventSO EventSO;
        public UnityEvent<GameObject> Response;

        private void OnEnable()
        {
            EventSO.RegisterListener(this);
        }

        private void OnDisable()
        {
            EventSO.UnregisterListener(this);
        }

        public void OnEventRaised(GameObject param)
        {
            Response.Invoke(param);
        }
    }
}