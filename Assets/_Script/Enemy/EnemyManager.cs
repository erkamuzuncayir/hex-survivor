using System.Threading.Tasks;
using _Script.Data.RuntimeSet;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Script.Enemy
{
    [CreateAssetMenu]
    public class EnemyManager : ScriptableObject
    {
        [SerializeField] private EnemyControllerRuntimeSet _so_rs_enemyController;

        public async UniTask OnEnemyTurn()
        {
            await ProcessTurnState(EnemyTurnState.Planning);
            await ReorderTurnOrderBasedOnDistanceToPlayer();
            await ProcessTurnStatesOneAfterAnother(EnemyTurnState.Move, EnemyTurnState.Attack);
         }

        private async UniTask ReorderTurnOrderBasedOnDistanceToPlayer()
        {
            Debug.Log("ordering is start.");
            _so_rs_enemyController.Items.Sort((x,y) => x.BirdsEyeViewDistanceToPlayer.CompareTo(y.BirdsEyeViewDistanceToPlayer));
            await UniTask.Yield();
            Debug.Log("ordering is done.");
        }

        private async UniTask ProcessTurnState(EnemyTurnState enemyTurnState)
        {
            Debug.Log("Planning is start");
            foreach (EnemyController enemyController in _so_rs_enemyController.Items)
            { 
                enemyController.ProcessEnemyTurn(enemyTurnState);
                await UniTask.Yield();
            }
            Debug.Log("Planning is done");
        }
        
        private async UniTask ProcessTurnStatesOneAfterAnother(EnemyTurnState firstStateToProcess, EnemyTurnState secondStateToProcess)
        {
            Debug.Log("move started");
            foreach (EnemyController enemyController in _so_rs_enemyController.Items)
            {
                enemyController.ProcessEnemyTurn(firstStateToProcess);
                enemyController.ProcessEnemyTurn(secondStateToProcess);
                await UniTask.Yield();
            }
            Debug.Log("move done");
        }

    }
}