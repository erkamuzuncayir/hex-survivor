using UnityEngine;
using UnityEngine.Events;

namespace _Script.PersonalAPI.Event
{
    /// <summary>
    ///     This class listens to the GOEventSO.
    ///     When the event raised, it calls the methods assigned to it with a GameObject parameter.
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