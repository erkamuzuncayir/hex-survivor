using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Script.Actors;
using _Script.System;
using _Script.Tile;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

namespace _Script.Enemy
{
    public enum EnemyTurnState
    {
        Planning,
        Move,
        Attack,
        Die,
        OffTurn
    }
    
    public class EnemyController : MonoBehaviour
    {
        [Header("Systems")] 
        [SerializeField] private PathfindingSystemSO _so_system_pathfinding;
        
        [Header("Game Object")] 
        public GameObject GO_EnemyParent;
        private Transform _parentTransform;
        
        [Header("Enemy Type")]
        public EnemyTypeSO so_Type;
        public EnemyType EType;
        
        [field: Header("Spawn Data"), SerializeField]
        public int SpawnDistanceFromPlayer { get; private set; }
        
        [Header("Navigation Data")]
        public Vector3Int Coord; // It'll be filled first from EnemySpawner.cs
        public int CurTileDictIndex;
        public GroundTileData TileUnderTheEnemy;

        [field: Header("Gameplay Data")] 
        public int RemainingMoveCount { get; set; }
        public int AttackRange;
        public int Damage;
        public int Health;
        
        [Header("Default Values")]
        public int MoveRegenEndOfTurn;
        public int HealthRegenEndOfTurn;
        public int MaxMoveCount { get; private set; }
        public int MaxAttackRange { get; private set; }
        public int MaxDamage { get; private set; }
        public int MaxHealth { get; private set; }
        
        [Header("Movement Data")] 
        public float StepCountBetweenTwoTile;
        public int BirdsEyeViewDistanceToPlayer;

        [Header("Player Data")]
        [SerializeField] private PlayerDataSO _so_playerData;

        // Turn Info
        private List<GroundTileData> _movementPath = new();
        private bool _canAttack;
        private bool _willAttackDirectly;
        private int _moveCount;
        
        // Animation
        [SerializeField] private Animator _animator;
        private int _animIDIsWalk;
        private int _animIDIsAttack;
        private int _animIDIsHit;
        private int _animIDIsDead;

        public void Initialize()
        {
            _parentTransform = GO_EnemyParent.transform;
            SetAnimIDValues();
            GetDataFromEnemyType();
        }

        private void SetAnimIDValues()
        {
            _animIDIsWalk = Animator.StringToHash("IsWalk");
            _animIDIsAttack = Animator.StringToHash("IsAttack");
            _animIDIsHit = Animator.StringToHash("IsHit");
            _animIDIsDead = Animator.StringToHash("IsDead");
        }

        private void GetDataFromEnemyType()
        {
            MoveRegenEndOfTurn = so_Type.MoveRegenEndOfTurn;
            HealthRegenEndOfTurn = so_Type.HealthRegenEndOfTurn;
            MaxMoveCount = so_Type.MaxMoveCount;
            MaxAttackRange = so_Type.MaxAttackRange;
            MaxDamage = so_Type.MaxDamage;
            MaxHealth = so_Type.MaxHealth;
            AttackRange = so_Type.AttackRange;
            Damage = so_Type.Damage;
            Health = so_Type.Health;
            RemainingMoveCount = MaxMoveCount;
            SpawnDistanceFromPlayer = so_Type.SpawnDistanceFromPlayer;
        }

        public async Task ProcessEnemyTurn(EnemyTurnState enemyTurnState)
        {
            switch (enemyTurnState)
            {
                case EnemyTurnState.Planning: 
                    await Task.Run(Planning);
                    break;
                case EnemyTurnState.Move when !_willAttackDirectly:
                {
                    Move(_moveCount);
                    break;
                }
                case EnemyTurnState.Attack when _canAttack:
                    Attack();
                    break;
                case EnemyTurnState.Die:
                    Die();
                    break;
                case EnemyTurnState.OffTurn:
                    break;
            }
        }

        [Button()]
        private async void Planning()
        {
            BirdsEyeViewDistanceToPlayer = _so_system_pathfinding.DistanceCheckToPlayer(CurTileDictIndex, _so_playerData.PlayerTileDictIndex,
                    true);

            if (AttackRange >= BirdsEyeViewDistanceToPlayer)
            {
                MarkToDirectAttack();
                return;
            }
            else
                _willAttackDirectly = false;
            
            bool isPathFound;
            (isPathFound, _movementPath) = _so_system_pathfinding.FindPath(CurTileDictIndex, _so_playerData.PlayerTileDictIndex, excludeActors: false);

            if (!isPathFound)
            {
                Debug.LogError($"Path not found from {Coord}");
                return;
            }

            int moveCount = _movementPath.Count;
            
            if (moveCount > AttackRange)
            {
                int needCountForAttack = moveCount - AttackRange;
                _canAttack = needCountForAttack <= RemainingMoveCount;
                moveCount = _canAttack ? needCountForAttack : RemainingMoveCount;
                _moveCount = moveCount;
                MarkToMove(moveCount);
            }
        }

        private void MarkToMove(int moveCount)
        {
            TileUnderTheEnemy.ThisIsOnIt = WhatIsOnIt.Nothing;
            _movementPath[moveCount - 1].ThisIsOnIt = WhatIsOnIt.Enemy;
        }

        private void MarkToDirectAttack()
        {
            _willAttackDirectly = true;
            _canAttack = true;
        }
        
        private async void Move(int moveCount)
        {
            _animator.SetBool(_animIDIsWalk, true);
            for (int i = 0; i < moveCount; i++)
            {
                GroundTileData tileUnderEnemy = _movementPath[i];
                ContinuousMove(tileUnderEnemy.WorldPosition);
                TileUnderTheEnemy = tileUnderEnemy;
                CurTileDictIndex = tileUnderEnemy.DictIndex;
                Coord = tileUnderEnemy.Coord;
                await UniTask.Delay(1000);
            }
            _animator.SetBool(_animIDIsWalk, false);
        }

        private async UniTask ContinuousMove(Vector3 targetPos)
        {
            while (Vector3.Distance(_parentTransform.position, targetPos) > .05f)
            {
                _parentTransform.position = Vector3.MoveTowards(_parentTransform.position, targetPos,
                    1 * Time.deltaTime);
                await UniTask.Yield();
            }
        }
        
        private void Attack()
        {
            _willAttackDirectly = false;
            _canAttack = false;
            _animator.SetTrigger(_animIDIsAttack);
        }

        private void Hit()
        {
            _animator.SetTrigger(_animIDIsHit);
        }
        
        private void Die()
        {
            _animator.SetTrigger(_animIDIsDead);
        }
    }
}