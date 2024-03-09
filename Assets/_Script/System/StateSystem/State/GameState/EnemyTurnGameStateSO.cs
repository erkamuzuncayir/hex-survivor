using _Script.Enemy;
using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.StateMachine;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace _Script.System.StateSystem.State.GameState
{
    [CreateAssetMenu(fileName = "so_state_game_EnemyTurn", menuName = "System/State/Game State/Enemy Turn")]
    public class EnemyTurnGameStateSO : GameStateSO
    { 
        private GameStateMachine _so_stateMachine_game;
        [SerializeField] private EnemyManager _so_enemyManager;
        
        public override void InitState(IStateMachine<GameStateMachine, GameStateSO> stateMachine)
        { 
            _so_stateMachine_game = (GameStateMachine)stateMachine;
        }

        public override async void EnterState()
        {
            await _so_enemyManager.OnEnemyTurn();
            _so_stateMachine_game.HandleState(_so_stateMachine_game.so_state_game_PlayerTurn);
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
        }
    }
}