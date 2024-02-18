using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.StateMachine;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace _Script.System.StateSystem.State.GameState
{
    [CreateAssetMenu(fileName = "so_state_game_MainMenu", menuName = "System/State/Game State/Main Menu")]
    public class MainMenuGameStateSO : GameStateSO
    {
        public override void InitState(IStateMachine<GameStateMachine, GameStateSO> stateMachine)
        {
            throw new NotImplementedException();
        }

        public override void EnterState()
        {
            throw new NotImplementedException();
        }

        public override void UpdateState()
        {
            throw new NotImplementedException();
        }

        public override void ExitState()
        {
            throw new NotImplementedException();
        }
    }
}