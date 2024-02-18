using _Script.Tile;
using UnityEngine;

namespace _Script.Enemy
{
    public enum EnemyType
    {
        SkeletonArcher, SkeletonSpearman, SkeletonWarrior, Knight, Archer
    }
    [CreateAssetMenu(menuName = "EnemyController", fileName = "_so_enemy_type_")]
    public class EnemyTypeSO : ScriptableObject
    {
        [Header("Game Object Data")] 
        public GameObject pfb_EnemyParent;
        public EnemyType EType;
        
        [Header("Spawn Data")]
        [field: SerializeField] public int SpawnDistanceFromPlayer { get; private set; }        
        
        [Header("Start Values")]
        [field: SerializeField] public int AttackRange { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
        
        [Header("Default Values")]
        [field: SerializeField] public int MoveRegenEndOfTurn { get; private set; }
        [field: SerializeField] public int HealthRegenEndOfTurn { get; private set; }
        [field: SerializeField] public int MaxMoveCount { get; private set; }
        [field: SerializeField] public int MaxAttackRange { get; private set; }
        [field: SerializeField] public int MaxDamage { get; private set; }
        [field: SerializeField] public int MaxHealth { get; private set; }

        [Header("Movement Data")] 
        public float StepCountBetweenTwoTile = 0.2f;
    }
}
