using UnityEngine;
using UnityEngine.Events;

namespace _Script.PersonalAPI.Event
{
    /// <summary>
    ///     This class listens to the ParamEventSO<T>.
    ///     When the event raised, it calls the methods assigned to it with a <T> parameter.
    /// </summary>
    public class ParamEventListener<T> : MonoBehaviour
    {
        public ParamEventSO<T> EventSO;
        public UnityEvent<T> Response;

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