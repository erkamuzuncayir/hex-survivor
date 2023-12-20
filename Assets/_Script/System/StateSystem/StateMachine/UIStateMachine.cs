using System.Collections.Generic;
using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.State.UIState;

namespace _Script.System.StateSystem.StateMachine
{
    public class UIStateMachine : IStateMachine<UIStateMachine, UIStateSO>
    {
        public List<UIStateSO> CurrentUIStates { get; private set; }
        
        public void InitStates()
        {
        }

        public void HandleState(UIStateSO requestedState)
        {
        }

        private void AddStateToList(UIStateSO stateToBeAdd)
        {
            CurrentUIStates.Add(stateToBeAdd);
            StartState(stateToBeAdd);
        }

        private void ProcessStates()
        {
            for (int i = 0; i < CurrentUIStates.Count; i++)
            { 
                CurrentUIStates[i].UpdateState();
            }
        }

        private void RemoveStateFromList(UIStateSO stateToBeRemove)
        {
            stateToBeRemove.ExitState();
            CurrentUIStates.Remove(stateToBeRemove);
        }
        
        private void RemoveAllStatesFromList()
        {
            for (int i = 0; i < CurrentUIStates.Count; i++)
            { 
                CurrentUIStates[i].ExitState();
            }
            CurrentUIStates.Clear();
        }
        
        private void StartState(UIStateSO stateToBeStart)
        {
            stateToBeStart.EnterState();
        }
        
        public void ProcessState(UIStateSO requestedState)
        {
        }
    }
}