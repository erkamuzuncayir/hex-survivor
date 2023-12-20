using System.Collections.Generic;
using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.State.GameState;

namespace _Script.System.StateSystem.StateMachine
{
    public class GameStateMachine : IStateMachine<GameStateMachine, GameStateSO>
    {
        public List<GameStateSO> CurrentGameStates { get; private set; }
        
        public void InitStates()
        {
        }

        public void HandleState(GameStateSO requestedState)
        {
        }

        private void AddStateToList(GameStateSO stateToBeAdd)
        {
            CurrentGameStates.Add(stateToBeAdd);
            StartState(stateToBeAdd);
        }

        private void ProcessStates()
        {
            for (int i = 0; i < CurrentGameStates.Count; i++)
            { 
                CurrentGameStates[i].UpdateState();
            }
        }

        private void RemoveStateFromList(GameStateSO stateToBeRemove)
        {
            stateToBeRemove.ExitState();
            CurrentGameStates.Remove(stateToBeRemove);
        }
        
        private void RemoveAllStatesFromList()
        {
            for (int i = 0; i < CurrentGameStates.Count; i++)
            { 
                CurrentGameStates[i].ExitState();
            }
            CurrentGameStates.Clear();
        }
        
        private void StartState(GameStateSO stateToBeStart)
        {
            stateToBeStart.EnterState();
        }
        
        public void ProcessState(GameStateSO requestedState)
        {
        }
    }
}
