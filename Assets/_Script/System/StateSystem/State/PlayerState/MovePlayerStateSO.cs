using System.Collections.Generic;
using _Script.Actors;
using _Script.PersonalAPI.Data.RuntimeSet;
using _Script.PersonalAPI.Event;
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
        [Header("Systems")] 
        [SerializeField] private PathfindingSystemSO _so_system_pathfinding;
        private PlayerStateMachine _playerStateMachine;
        
        [Header("Runtime Sets")]
        [SerializeField] private GameObjectRuntimeSet _so_rs_player;
        [SerializeField] private PlayerDataSO _so_playerData;

        [Header("Events")] 
        [SerializeField] private VoidEventSO _so_event_player_moved;
       
        // Cache Fields
        private Transform _playerTransform;
        private List<GroundTileData> _movementPath;

        public override void InitState(IStateMachine<PlayerStateMachine, PlayerStateSO> stateMachine)
        {
            _playerStateMachine = (PlayerStateMachine)stateMachine;
            _playerTransform = _so_rs_player.Items[0].transform;
        }

        public override async void EnterState()
        {
            bool isPathFound;
            (isPathFound, _movementPath) =
                _so_system_pathfinding.FindPath(_so_playerData.PlayerTileDictIndex, _so_playerData.TargetTileDictIndex, excludeActors: false);

            if (!isPathFound)
            {
                _playerStateMachine.HandleState(_playerStateMachine.so_state_PlayerAttack);
                return;
            }

                
            int moveCount = _movementPath.Count;
            if (moveCount > _so_playerData.RemainingMoveCount)
                moveCount = _so_playerData.RemainingMoveCount;
            
            _so_playerData.TileUnderThePlayer.ThisIsOnIt = WhatIsOnIt.Nothing;
            for (int i = 0; i < moveCount; i++)
            {
                GroundTileData tileUnderThePlayer = _movementPath[i];
                ContinuousMove(tileUnderThePlayer.WorldPosition);
                _so_playerData.Moved();
                _so_playerData.TileUnderThePlayer = tileUnderThePlayer;
                _so_playerData.PlayerTileDictIndex = tileUnderThePlayer.DictIndex;
                _so_playerData.PlayerCoord = tileUnderThePlayer.Coord;
                await UniTask.Delay(1000);
                _so_event_player_moved.Raise();
            }
            _so_playerData.TileUnderThePlayer.ThisIsOnIt = WhatIsOnIt.Player;

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