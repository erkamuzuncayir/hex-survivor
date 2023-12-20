using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.StateMachine;
using UnityEngine;

namespace _Script.System.StateSystem.State.GameState
{
    public abstract class GameStateSO : ScriptableObject, IState<GameStateMachine, GameStateSO>
    {
        public abstract void InitState(IStateMachine<GameStateMachine, GameStateSO> stateMachine);

        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();
    }
}
