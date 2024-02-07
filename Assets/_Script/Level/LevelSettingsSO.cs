using _Script.Enemy;
using UnityEngine;

namespace _Script.Level
{
    public class LevelSettingsSO : ScriptableObject
    {
        [Header("General Settings")] 
        public int LevelOrder;

        [Header("SpawnableEnemies Settings")] 
        public SpawnableEnemy[] SpawnableEnemies;
    }

    public class SpawnableEnemy
    {
        public EnemyTypeSO so_Enemy;
        public int Count;
    }
}
