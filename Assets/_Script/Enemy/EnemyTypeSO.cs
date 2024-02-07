using _Script.Tile;
using UnityEngine;

namespace _Script.Enemy
{
    public enum EnemyType
    {
        Skeleton, Knight, Archer
    }
    [CreateAssetMenu(menuName = "EnemyController", fileName = "_so_enemy_type_")]
    public class EnemyTypeSO : ScriptableObject
    {
        [Header("Game Object Data")] 
        public GameObject EnemyPrefab;
        public EnemyType EType;
        
        [Header("Start Values")]
        public int AttackRange { get; private set; }
        public int Damage { get; private set; }
        public int Health { get; private set; }
        
        [Header("Default Values")]
        public int MoveRegenEndOfTurn { get; private set; }
        public int HealthRegenEndOfTurn { get; private set; }
        public int MaxMoveCount { get; private set; }
        public int MaxAttackRange { get; private set; }
        public int MaxDamage { get; private set; }
        public int MaxHealth { get; private set; }

        [Header("Movement Data")] 
        public float StepCountBetweenTwoTile = 0.2f;
    }
}
