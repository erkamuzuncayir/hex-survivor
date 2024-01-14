using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace _Script.PersonalAPI.Event
{
    /// <summary>
    ///     This class inherits scriptable object to create customized event layer for project.
    /// </summary>
    [CreateAssetMenu(fileName = "Int Event", menuName = "Events/Int Event")]
    public class IntEventSO : ScriptableObject
    {
        [SerializeField] private readonly List<UnityEvent<int>> _eventListenerList = new();
        public bool HasAnyDependent;
        [ShowIf("HasAnyDependent")] public List<IntEventSO> EventsDependOnThis;

        public void Raise(int param)
        {
            for (int i = _eventListenerList.Count - 1; i >= 0; i--)
                _eventListenerList[i].Invoke(param);
        }

        [ShowIf("HasAnyDependent")]
        public void RaiseSelfAndDependents(int param)
        {
            Raise(param);

            if (HasAnyDependent && EventsDependOnThis != null)
                for (int i = 0; i < EventsDependOnThis.Count; i++)
                    EventsDependOnThis[i].Raise(param);
        }

        public void RegisterListener(IntEventListener listener)
        {
            _eventListenerList.Add(listener.Response);
        }

        public void RegisterListenerDirectly
            (UnityEvent<int> unityListener)
        {
            _eventListenerList.Add(unityListener);
        }
        
        public void UnregisterListener(IntEventListener listener)
        {
            _eventListenerList.Remove(listener.Response);
        }
        
        public void UnregisterListenerDirectly(UnityEvent<int> unityListener)
        {
            _eventListenerList.Remove(unityListener);
        }
    }
}