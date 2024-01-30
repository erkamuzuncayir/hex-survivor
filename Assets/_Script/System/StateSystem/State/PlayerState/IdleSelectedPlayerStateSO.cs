using _Script.Actors;
using _Script.Event;
using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.StateMachine;
using UnityEngine;
using UnityEngine.Events;

namespace _Script.System.StateSystem.State.PlayerState
{
    [CreateAssetMenu(fileName = "IdleSelectedPlayerStateSO", menuName = "System/State/Player/Idle-Selected")]
    public class IdleSelectedPlayerStateSO : PlayerStateSO
    {
        [Header("Systems")] private PlayerStateMachine _playerStateMachine;

        [Header("Events")] [SerializeField] private IntEventSO _so_event_SelectedTileDictIndex;

        private UnityEvent<int> _event_SelectedTileDictIndex;
        [SerializeField] private PlayerDataSO _playerDataSO;

        public override void InitState(IStateMachine<PlayerStateMachine, PlayerStateSO> stateMachine)
        {
            _playerStateMachine = (PlayerStateMachine)stateMachine;
            _event_SelectedTileDictIndex = new UnityEvent<int>();
        }

        public override void EnterState()
        {
            _so_event_SelectedTileDictIndex.RegisterListenerDirectly(_event_SelectedTileDictIndex);
            _event_SelectedTileDictIndex.AddListener(OnTargetTileSelected);
        }


        public override void UpdateState()
        {
        }

        private void OnTargetTileSelected(int selectedTileDictIndex)
        {
            _playerDataSO.TargetTileDictIndex = selectedTileDictIndex;
            _playerStateMachine.HandleState(_playerStateMachine.so_state_PlayerMove);
        }

        public override void ExitState()
        {
            _event_SelectedTileDictIndex.RemoveListener(OnTargetTileSelected);
            _so_event_SelectedTileDictIndex.UnregisterListenerDirectly(_event_SelectedTileDictIndex);
        }
    }
}