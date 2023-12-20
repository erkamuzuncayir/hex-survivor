using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.StateMachine;
using UnityEngine;

namespace _Script.System.StateSystem.State.UIState
{
    public abstract class UIStateSO : ScriptableObject, IState<UIStateMachine, UIStateSO>
    {
        public abstract void InitState(IStateMachine<UIStateMachine, UIStateSO> stateMachine);

        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();
    }
}