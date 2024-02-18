using _Script.Actors;
using _Script.Data;
using _Script.PersonalAPI.Data.RuntimeSet;
using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.StateMachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Script.System.StateSystem.State.GameState
{
    [CreateAssetMenu(fileName = "so_state_game_PlayerTurn", menuName = "System/State/Game State/Player Turn")]
    public class PlayerTurnGameStateSO : GameStateSO
    {
        // Cache Fields
        [SerializeField] private PlayerStateMachineReference _so_ref_stateMachine_Player;
        [SerializeField] private PlayerDataSO _so_playerData;
        [SerializeField] private RuntimeSet<GameObject> _so_rs_go_player;
        private GameObject _go_player;
        
        public override void InitState(IStateMachine<GameStateMachine, GameStateSO> stateMachine)
        {
            _go_player = _so_rs_go_player.Items[0];
            SetPlayerInitialCoordToPlayerData();
        }

        private void SetPlayerInitialCoordToPlayerData()
        {
            var position = _go_player.transform.position;
            _so_playerData.PlayerCoord = new Vector3Int((int)position.x, (int)position.y, (int)position.z);
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
