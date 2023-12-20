using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.StateMachine;
using UnityEngine;

namespace _Script.System.StateSystem.State.PlayerState
{
    [CreateAssetMenu (fileName = "SelectedPlayerStateSO", menuName = "System/State/Player/Selected")]
    public class SelectedPlayerStateSO : PlayerStateSO
    {
        public override void InitState(IStateMachine<PlayerStateMachine, PlayerStateSO> stateMachine)
        {
        }

        public override void EnterState()
        {
        }

        public override void UpdateState()
        {
        }

        private void OnTargetTileSelect()
        {
            
        }

        public override void ExitState()
        {
        }
    }
}
