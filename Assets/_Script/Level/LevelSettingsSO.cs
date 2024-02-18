using System;
using _Script.Enemy;
using UnityEngine;

namespace _Script.Level
{
    [CreateAssetMenu(fileName = "so_LevelSettings_*", menuName = "LevelSettings")]
    public class LevelSettingsSO : ScriptableObject
    {
        [Header("General Settings")] 
        public int LevelOrder;

        [Header("SpawnableEnemies Settings")] 
        public SpawnableEnemy[] SpawnableEnemies;
    }

    [Serializable]
    public class SpawnableEnemy
    {
        public EnemyTypeSO so_Enemy;
        public int Count;
    }
}
