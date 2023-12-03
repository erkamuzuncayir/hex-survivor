using UnityEngine;

namespace _Script.System
{
    /// <summary>
    ///     This class inherits scriptable object to create an event based state machine for project.
    /// </summary>

    public enum GameState
    {
        MainMenu,
        PlayerTurn,
        EnemyTurn
    }
    
    [CreateAssetMenu(fileName = "GameStateMachine", menuName = "Scriptable Object/System/Game State Machine")]
    public class GameStateMachineSO : ScriptableObject
    {
        public GameState CurrentGameState;
        
        public void InitSystem()
        {
            CurrentGameState = GameState.PlayerTurn;
        }
        
    }
}
