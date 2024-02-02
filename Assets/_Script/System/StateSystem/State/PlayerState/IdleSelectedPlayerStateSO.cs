using _Script.Actors;
using _Script.Event;
using _Script.PersonalAPI.Data.RuntimeSet;
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

        [SerializeField] private GameObject pfb_gfx_player_selected_underlay;
        private GameObject gfx_player_selected_underlay;
        private static readonly Vector3 _SELECTED_UNDERLAY_OFFSET = new Vector3(0, -0.05f, 0);
        
        private UnityEvent<int> _event_SelectedTileDictIndex;        
        [SerializeField] private GameObjectRuntimeSet _so_rs_player;
        [SerializeField] private PlayerDataSO _so_playerData;
        
        // Cache Fields
        private Transform _playerTransform;


        public override void InitState(IStateMachine<PlayerStateMachine, PlayerStateSO> stateMachine)
        {
            _playerStateMachine = (PlayerStateMachine)stateMachine;
            _playerTransform = _so_rs_player.Items[0].transform;
            _event_SelectedTileDictIndex = new UnityEvent<int>();
            gfx_player_selected_underlay = Instantiate(pfb_gfx_player_selected_underlay, _playerTransform, true);
            gfx_player_selected_underlay.SetActive(false);
        }

        public override void EnterState()
        {
            _so_event_SelectedTileDictIndex.RegisterListenerDirectly(_event_SelectedTileDictIndex);
            _event_SelectedTileDictIndex.AddListener(OnTargetTileSelected);
            gfx_player_selected_underlay.transform.position = _SELECTED_UNDERLAY_OFFSET + _playerTransform.position;
            gfx_player_selected_underlay.SetActive(true);
        }


        public override void UpdateState()
        {
        }

        private void OnTargetTileSelected(int selectedTileDictIndex)
        {
            _so_playerData.TargetTileDictIndex = selectedTileDictIndex;
            _playerStateMachine.HandleState(_playerStateMachine.so_state_PlayerMove);
        }

        public override void ExitState()
        {
            _event_SelectedTileDictIndex.RemoveListener(OnTargetTileSelected);
            _so_event_SelectedTileDictIndex.UnregisterListenerDirectly(_event_SelectedTileDictIndex);
            gfx_player_selected_underlay.SetActive(false);
        }
    }
}