using _Script.Actors;
using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.StateMachine;
using UnityEngine;

namespace _Script.System.StateSystem.State.PlayerState
{
    [CreateAssetMenu (fileName = "EndOfTurnPlayerStateSO", menuName = "System/State/Player/End of Turn")]
    public class EndOfTurnPlayerStateSO : PlayerStateSO
    {
        [SerializeField] private PlayerDataSO _playerDataSO;
        private PlayerStateMachine _stateMachine;
        
        public override void InitState(IStateMachine<PlayerStateMachine, PlayerStateSO> stateMachine)
        {
            _stateMachine = (PlayerStateMachine)stateMachine;
        }

        public override void EnterState()
        {
            RegenPlayerAttributes();
            _stateMachine.HandleState(_stateMachine.so_state_PlayerIdle_Unselected);
        }

        private void RegenPlayerAttributes()
        {
            _playerDataSO.MoveRegen(_playerDataSO.MoveRegenEndOfTurn);
            _playerDataSO.HealthRegen(_playerDataSO.HealthRegenEndOfTurn);
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
        }
    }
}