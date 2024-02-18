using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.StateMachine;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace _Script.System.StateSystem.State.GameState
{
    [CreateAssetMenu(fileName = "so_state_game_EnemyTurn", menuName = "System/State/Game State/Enemy Turn")]
    public class EnemyTurnGameStateSO : GameStateSO
    {
        public override void InitState(IStateMachine<GameStateMachine, GameStateSO> stateMachine)
        {
        }

        public override void EnterState()
        {
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
        }
    }
}