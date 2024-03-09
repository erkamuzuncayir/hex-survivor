using System;
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

        [field: Header("Gameplay Data")] 
        public int RemainingMoveCount { get; private set; }
        public int AttackRange { get; private set; }
        public int Damage { get; private set; }
        public int Health { get; private set; }

        [field: Header("Default Values")] 
        public int DefAttackRange { get; private set; } = 2;
        public int DefMoveCount { get; } = 3;
        public int DefDamage { get; private set; } = 4;
        public int DefHealth { get; } = 5;
        public const int DEATH_THRESHOLD = 0;

        [Header("Movement Values")] public float StepCountBetweenTwoTile = .2f;

        [Header("Regen in Off-Turn Values")] public int MoveRegenEndOfTurn;
        public int HealthRegenEndOfTurn;

        private void OnEnable()
        {
            SetDefaultValuesOfGameplayData();
        }

        private void SetDefaultValuesOfGameplayData()
        {
            RemainingMoveCount = DefMoveCount;
            AttackRange = DefAttackRange;
            Damage = DefDamage;
            Health = DefHealth;
        }

        public bool CanPlayerMove()
        {
            return RemainingMoveCount > 0;
        }

        public void Moved()
        {
            RemainingMoveCount--;
        }

        public void MoveRegen(int regenValue)
        {
            if (RemainingMoveCount + regenValue < DefMoveCount)
                RemainingMoveCount += regenValue;
            else
                RemainingMoveCount = DefMoveCount;
        }

        public void HealthRegen(int regenValue)
        {
            if (Health + regenValue < DefHealth)
                Health += regenValue;
            else
                Health = DefHealth;
        }

        public void DecreaseHealth(int decreaseValue)
        {
            if (Health - decreaseValue < DEATH_THRESHOLD)
                Debug.LogError("Player is death. You should take care of it.");
        }
    }
}