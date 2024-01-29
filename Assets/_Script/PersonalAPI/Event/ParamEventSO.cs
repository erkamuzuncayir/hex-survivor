using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace _Script.PersonalAPI.Event
{
    /// <summary>
    ///     This class inherits scriptable object to create customized event layer for project.
    /// </summary>
    public class ParamEventSO<T> : ScriptableObject
    {
        [SerializeField] private readonly List<UnityEvent<T>> _eventListenerList = new();
        public bool HasAnyDependent;
        [ShowIf("HasAnyDependent")] public List<ParamEventSO<T>> EventsDependOnThis;

        public void Raise(T param)
        {
            for (int i = _eventListenerList.Count - 1; i >= 0; i--)
                _eventListenerList[i].Invoke(param);
        }

        [ShowIf("HasAnyDependent")]
        public void RaiseSelfAndDependents(T param)
        {
            Raise(param);

            if (HasAnyDependent && EventsDependOnThis != null)
                for (int i = 0; i < EventsDependOnThis.Count; i++)
                    EventsDependOnThis[i].Raise(param);
        }

        public void RegisterListener(ParamEventListener<T> listener)
        {
            _eventListenerList.Add(listener.Response);
        }

        public void RegisterListenerDirectly
            (UnityEvent<T> unityListener)
        {
            _eventListenerList.Add(unityListener);
        }
        
        public void UnregisterListener(ParamEventListener<T> listener)
        {
            _eventListenerList.Remove(listener.Response);
        }
        
        public void UnregisterListenerDirectly(UnityEvent<T> unityListener)
        {
            _eventListenerList.Remove(unityListener);
        }
    }
}