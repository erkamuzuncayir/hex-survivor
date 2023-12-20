using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.StateMachine;
using UnityEngine;

namespace _Script.System.StateSystem.State.PlayerState
{
    [CreateAssetMenu (fileName = "MovePlayerStateSO", menuName = "System/State/Player/Move")]
    public class MovePlayerStateSO : PlayerStateSO
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

        public override void ExitState()
        {
        }
    }
}