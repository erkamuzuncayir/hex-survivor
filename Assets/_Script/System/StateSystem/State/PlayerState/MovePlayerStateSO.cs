using System.Collections.Generic;
using _Script.Actors;
using _Script.PersonalAPI.Data.RuntimeSet;
using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.StateMachine;
using _Script.Tile;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Script.System.StateSystem.State.PlayerState
{
    [CreateAssetMenu (fileName = "MovePlayerStateSO", menuName = "System/State/Player/Move")]
    public class MovePlayerStateSO : PlayerStateSO
    {
        [Header("Systems")] private PlayerStateMachine _playerStateMachine;
        [SerializeField] private PathfindingSystemSO _so_system_pathfinding;

        [SerializeField] private GameObjectRuntimeSet _so_rs_player;
        [SerializeField] private PlayerDataSO _playerDataSO;

        private Transform _playerTransform; 
        private List<GroundTileData> _movementPath;

        private float _movementTime;
        private float _waitingBetweenMoveTime;
        
        public override void InitState(IStateMachine<PlayerStateMachine, PlayerStateSO> stateMachine)
        {
            _playerStateMachine = (PlayerStateMachine)stateMachine;
            _playerTransform = _so_rs_player.Items[0].transform;
        }

        public override async void EnterState()
        {
            _movementPath =
                _so_system_pathfinding.FindPath(_playerDataSO.PlayerTileDictIndex, _playerDataSO.TargetTileDictIndex);
            foreach (GroundTileData groundTileData in _movementPath)
            {
                ContinuousMove(groundTileData.WorldPosition);
                _playerDataSO.Move();
                _playerDataSO.PlayerTileDictIndex = groundTileData.DictIndex;
                _playerDataSO.TileUnderThePlayer = groundTileData;
                await UniTask.Delay(1000);       
            }
            
            _movementPath.Clear();
            _playerStateMachine.HandleState(_playerStateMachine.so_state_PlayerAttack);
        }

        public override void UpdateState()
        {
        }

        private async UniTask ContinuousMove(Vector3 targetPos)
        {
            while (Vector3.Distance(_playerTransform.position, targetPos) > .05f)
            {
                _playerTransform.position = Vector3.MoveTowards(_playerTransform.position, targetPos, _playerDataSO.StepCountBetweenTwoTile * Time.deltaTime);
                await UniTask.Yield();
            }
        }

        public override void ExitState()
        {
        }
    }
}