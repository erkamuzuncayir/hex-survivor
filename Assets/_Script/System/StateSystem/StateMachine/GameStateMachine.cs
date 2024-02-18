using UnityEngine;
using System.Collections.Generic;
using _Script.Data;
using _Script.PersonalAPI.Event;
using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.State.GameState;

namespace _Script.System.StateSystem.StateMachine
{
    public class GameStateMachine : MonoBehaviour, IStateMachine<GameStateMachine, GameStateSO>
    {
        // State List and States
        private List<GameStateSO> list_state_game_all = new();
        public GameStateSOReference so_ref_state_Game_Current;
        private GameStateSO so_state_game_current;
        public GameStateSO so_state_game_MainMenu;
        public GameStateSO so_state_game_PlayerTurn;
        public GameStateSO so_state_game_EnemyTurn;
        
        // Event
        [SerializeField] private ParamEventSO<GameStateSO> _so_event_state_game_current;
        
        private void Awake()
        {
            FillListWithStates();
        }

        public void FillListWithStates()
        {
            list_state_game_all.Add(so_state_game_MainMenu);
            list_state_game_all.Add(so_state_game_PlayerTurn);
            list_state_game_all.Add(so_state_game_EnemyTurn);
        }

        private void Start()
        {
            InitStates();
            HandleState(so_state_game_PlayerTurn);
        }

        public void InitStates()
        {
            foreach (GameStateSO gameStateSO in list_state_game_all)
                gameStateSO.InitState(this);
        }

        public void HandleState(GameStateSO requestedState)
        {
            if (so_state_game_current == requestedState)
                return;
            so_state_game_current?.ExitState();
            so_state_game_current = requestedState;
            so_ref_state_Game_Current.Value = requestedState;
            so_state_game_current.EnterState();
            _so_event_state_game_current.Raise(so_state_game_current);
        }
        
        public void ProcessState(GameStateSO requestedState)
        {
            so_state_game_current.UpdateState();
        }
    }
}
