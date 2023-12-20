using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.StateMachine;
using UnityEngine;

namespace _Script.System.StateSystem.State.PlayerState
{
    public abstract class PlayerStateSO : ScriptableObject, IState<PlayerStateMachine, PlayerStateSO>
    {
        public abstract void InitState(IStateMachine<PlayerStateMachine, PlayerStateSO> stateMachine);

        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();
    }
}
