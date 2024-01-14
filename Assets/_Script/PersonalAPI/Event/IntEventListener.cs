using UnityEngine;
using UnityEngine.Events;

namespace _Script.PersonalAPI.Event
{
    /// <summary>
    ///     This class listens to the IntEventSO.
    ///     When the event raised, it calls the methods assigned to it with a int parameter.
    /// </summary>
    public class IntEventListener : MonoBehaviour
    {
        public IntEventSO EventSO;
        public UnityEvent<int> Response;

        private void OnEnable()
        {
            EventSO.RegisterListener(this);
        }

        private void OnDisable()
        {
            EventSO.UnregisterListener(this);
        }
    }
}