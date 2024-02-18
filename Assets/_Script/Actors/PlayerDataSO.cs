using _Script.Tile;
using UnityEngine;

namespace _Script.Actors
{
    [CreateAssetMenu(menuName = "Player/Data", fileName = "PlayerDataSO")]
    public class PlayerDataSO : ScriptableObject
    {
        [Header("Navigation Data")]
        public Vector3Int PlayerCoord;
        public int PlayerTileDictIndex;
        public int TargetTileDictIndex;
        public GroundTileData TileUnderThePlayer;

        [Header("Gameplay Data")] 
        public int RemainingMoveCount;
        public int AttackRange;
        public int Damage { get; }
        public int Health { get; private set; }

        [Header("MinMax Values")] 
        public int MaxAttackRange { get; private set; } = 2;
        public int MaxMoveCount { get; } = 3;
        public int MaxDamage { get; private set; } = 4;
        public int MaxHealth { get; } = 5;
        public const int DEATH_THRESHOLD = 0;

        [Header("Movement Values")] public float StepCountBetweenTwoTile = .2f;

        [Header("Regen in Off-Turn Values")] public int MoveRegenEndOfTurn;
        public int HealthRegenEndOfTurn;

        public bool CanPlayerMove()
        {
            return RemainingMoveCount > 0;
        }

        public void Move(GroundTileData tileUnderThePlayer)
        {
            TileUnderThePlayer = tileUnderThePlayer;
            PlayerTileDictIndex = tileUnderThePlayer.DictIndex;
            PlayerCoord = tileUnderThePlayer.Coord;
            RemainingMoveCount--;
        }

        public void MoveRegen(int regenValue)
        {
            if (RemainingMoveCount + regenValue < MaxMoveCount)
                RemainingMoveCount += regenValue;
            else
                RemainingMoveCount = MaxMoveCount;
        }

        public void HealthRegen(int regenValue)
        {
            if (Health + regenValue < MaxHealth)
                Health += regenValue;
            else
                Health = MaxHealth;
        }

        public void DecreaseHealth(int decreaseValue)
        {
            if (Health - decreaseValue < DEATH_THRESHOLD)
                Debug.LogError("Player is death. You should take care of it.");
        }
    }
}