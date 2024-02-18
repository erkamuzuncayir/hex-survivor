using _Script.Tile;
using UnityEngine;

namespace _Script.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public EnemyTypeSO so_Type;
        public EnemyType EType;
        
        [Header("Spawn Data")]
        public int SpawnDistanceFromPlayer { get; private set; }
        
        [Header("Navigation Data")]
        public Vector3Int Coord; // It'll be filled first from EnemySpawner.cs
        public int CurTileDictIndex;
        public int PlayerTileDictIndex;
        public GroundTileData TileUnderTheEnemy;

        [field: Header("Gameplay Data")] public int RemainingMoveCount { get; set; }
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

        public void Initialize()
        {
            GetDataFromEnemyType();
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
    }
}