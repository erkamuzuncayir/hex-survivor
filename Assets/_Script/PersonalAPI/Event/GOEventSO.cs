using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace _Script.PersonalAPI.Event
{
    /// <summary>
    ///     This class inherits scriptable object to create customized event layer for project.
    /// </summary>
    [CreateAssetMenu(fileName = "GameObject Event", menuName = "Events/GameObject Event")]
    public class GOEventSO : ScriptableObject
    {
        private readonly List<GOEventListener> _eventListenerList = new();
        public bool HasAnyDependent;
        [ShowIf("HasAnyDependent")] public List<GOEventSO> EventsDependOnThis;

        public void Raise(GameObject param)
        {
            for (var i = _eventListenerList.Count - 1; i >= 0; i--)
                _eventListenerList[i].OnEventRaised(param);
        }

        [ShowIf("HasAnyDependent")]
        public void RaiseSelfAndDependents(GameObject param)
        {
            Raise(param);

            if (HasAnyDependent && EventsDependOnThis != null)
                for (var i = 0; i < EventsDependOnThis.Count; i++)
                    EventsDependOnThis[i].Raise(param);
        }

        public void RegisterListener(GOEventListener listener)
        {
            _eventListenerList.Add(listener);
        }

        public void UnregisterListener(GOEventListener listener)
        {
            _eventListenerList.Remove(listener);
        }
    }
}