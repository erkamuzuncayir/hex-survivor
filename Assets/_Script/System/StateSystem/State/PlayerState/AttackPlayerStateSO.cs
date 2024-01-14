using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.StateMachine;
using UnityEngine;

namespace _Script.System.StateSystem.State.PlayerState
{
    [CreateAssetMenu (fileName = "AttackPlayerStateSO", menuName = "System/State/Player/Attack")]
    public class AttackPlayerStateSO : PlayerStateSO
    {
        private PlayerStateMachine _stateMachine;
        public override void InitState(IStateMachine<PlayerStateMachine, PlayerStateSO> stateMachine)
        {
            _stateMachine = (PlayerStateMachine)stateMachine;
        }

        public override void EnterState()
        {
            _stateMachine.HandleState(_stateMachine.so_state_PlayerOffTurn);
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
        }
    }
}