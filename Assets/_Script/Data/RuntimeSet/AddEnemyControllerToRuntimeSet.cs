using _Script.Enemy;
using UnityEngine;

namespace _Script.Data.RuntimeSet
{
    public class AddEnemyControllerToRuntimeSet : MonoBehaviour
    {
        public EnemyControllerRuntimeSet EnemyControllerRuntimeSet;
        private EnemyController _enemyController;
        
        private void OnEnable()
        {
            _enemyController = GetComponent<EnemyController>();
            EnemyControllerRuntimeSet.AddToList(_enemyController);
        }

        private void OnDisable()
        {
            EnemyControllerRuntimeSet.RemoveFromList(_enemyController);
        }
    }
}