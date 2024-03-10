using _Script.Actors;
using _Script.Data;
using _Script.Event;
using _Script.PersonalAPI.Data.RuntimeSet;
using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.State.PlayerState;
using _Script.System.StateSystem.StateMachine;
using UnityEngine;
using UnityEngine.Events;

namespace _Script.System.StateSystem.State.GameState
{
    [CreateAssetMenu(fileName = "so_state_game_PlayerTurn", menuName = "System/State/Game State/Player Turn")]
    public class PlayerTurnGameStateSO : GameStateSO
    {
        [Header("Systems")] 
        private GameStateMachine _so_stateMachine_game;
        private PlayerStateMachine _so_stateMachine_player;
        
        [Header("Events")]
        [SerializeField] private PlayerStateSOEventSO _so_event_playerStateSO_changed;
        private UnityEvent<PlayerStateSO> _bridge_event_playerStateSO_changed;
        
        [Header("Cache fields")]
        [SerializeField] private PlayerStateMachineReference _so_ref_stateMachine_Player;
        [SerializeField] private OffTurnPlayerStateSO _so_state_PlayerOffTurn;
        [SerializeField] private PlayerDataSO _so_playerData;
        
        public override void InitState(IStateMachine<GameStateMachine, GameStateSO> stateMachine)
        {
            _so_stateMachine_game = (GameStateMachine)stateMachine;
            _so_stateMachine_player = _so_ref_stateMachine_Player.Value;
        }

        public override void EnterState()
        {
            _so_event_playerStateSO_changed.RegisterListenerDirectly(_bridge_event_playerStateSO_changed);
            _bridge_event_playerStateSO_changed.AddListener(OnPlayerStateChanged);
            _so_stateMachine_player.HandleState(_so_stateMachine_player.so_state_PlayerIdle_Unselected);
        }

        private void OnPlayerStateChanged(PlayerStateSO currentPlayerStateSO)
        {
            if(currentPlayerStateSO == _so_state_PlayerOffTurn)
                _so_stateMachine_game.HandleState(_so_stateMachine_game.so_state_game_EnemyTurn);
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
            _bridge_event_playerStateSO_changed.RemoveListener(OnPlayerStateChanged);
            _so_event_playerStateSO_changed.UnregisterListenerDirectly(_bridge_event_playerStateSO_changed);
        }
    }
}
