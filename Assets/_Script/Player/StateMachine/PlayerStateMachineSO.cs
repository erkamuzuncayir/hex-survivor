using _Script.PersonalAPI.StateMachine;
using UnityEngine;

namespace _Script.Player.StateMachine
{
    public class PlayerStateMachineSO : ScriptableObject, IStateMachine<PlayerStateMachineSO, PlayerStateSO>
    {
        /// <summary>
        ///     This class inherits scriptable object to create an event based state machine for project.
        /// </summary>
        
        public void InitStates()
        {
            throw new global::System.NotImplementedException();
        }

        public void HandleState(PlayerStateSO requestedState)
        {
            throw new global::System.NotImplementedException();
        }

        public void SetState(PlayerStateSO requestedState)
        {
            throw new global::System.NotImplementedException();
        }
    }
}