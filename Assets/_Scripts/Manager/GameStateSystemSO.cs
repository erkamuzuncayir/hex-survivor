using UnityEngine;

namespace _Scripts.Manager
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
    
    [CreateAssetMenu(fileName = "GameStateSystem", menuName = "Scriptable Object/System/Game State System")]
    public class GameStateSystemSO : ScriptableObject
    {
        public GameState CurrentGameState;
        
        public void InitSystem()
        {
            CurrentGameState = GameState.PlayerTurn;
        }
        
    }
}
