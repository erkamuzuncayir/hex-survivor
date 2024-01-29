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
    [CreateAssetMenu(fileName = "MovePlayerStateSO", menuName = "System/State/Player/Move")]
    public class MovePlayerStateSO : PlayerStateSO
    {
        [Header("Systems")] private PlayerStateMachine _playerStateMachine;
        [SerializeField] private PathfindingSystemSO _so_system_pathfinding;

        [SerializeField] private GameObjectRuntimeSet _so_rs_player;
        [SerializeField] private PlayerDataSO _so_playerData;

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
                _so_system_pathfinding.FindPath(_so_playerData.PlayerTileDictIndex, _so_playerData.TargetTileDictIndex);

            int moveCount = _movementPath.Count;
            if (_movementPath.Count > _so_playerData.RemainingMoveCount)
                moveCount = _so_playerData.RemainingMoveCount;

            for (int i = 0; i < moveCount; i++)
            {
                GroundTileData groundTileData = _movementPath[i];
                ContinuousMove(groundTileData.WorldPosition);
                _so_playerData.Move();
                _so_playerData.PlayerTileDictIndex = groundTileData.DictIndex;
                _so_playerData.TileUnderThePlayer = groundTileData;
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
                _playerTransform.position = Vector3.MoveTowards(_playerTransform.position, targetPos,
                    _so_playerData.StepCountBetweenTwoTile * Time.deltaTime);
                await UniTask.Yield();
            }
        }

        public override void ExitState()
        {
        }
    }
}