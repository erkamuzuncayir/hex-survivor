using System.Collections.Generic;
using _Script.PersonalAPI.Input;
using _Script.PersonalAPI.StateMachine;
using _Script.System.StateSystem.State.PlayerState;
using UnityEngine;

namespace _Script.System.StateSystem.StateMachine
{ 
    public class PlayerStateMachine : MonoBehaviour, IStateMachine<PlayerStateMachine, PlayerStateSO>
    {
        // State List and States
        public List<IState<PlayerStateMachine, PlayerStateSO>> AllPlayerStates = new();
        public IState<PlayerStateMachine, PlayerStateSO> CurrentPlayerState;
        public PlayerStateSO so_state_PlayerUnselected;
        public PlayerStateSO so_state_PlayerSelected;
        public PlayerStateSO so_state_PlayerIdle;
        public PlayerStateSO so_state_PlayerMove;
        public PlayerStateSO so_state_PlayerAttack;

        // Input Handler
        [SerializeField] private ClickInputHandler _clickInputHandler;
        
        // Cache fields
        [SerializeField] private GameObject _go_player;
        
        private void Awake()
        {
            FillListWithStates();
            InitStates();
        }

        private void FillListWithStates()
        {
            AllPlayerStates.Add(so_state_PlayerIdle);
            AllPlayerStates.Add(so_state_PlayerMove);
            AllPlayerStates.Add(so_state_PlayerAttack);
        }
        
        public void InitStates()
        {
            for (int i = 0; i < AllPlayerStates.Count; i++)
            {
                AllPlayerStates[i].InitState(this);
            }
        }

        public void OnMouseClickPerformed()
        {
            
        }
        
        public void HandleState(PlayerStateSO requestedState)
        {
            if (CurrentPlayerState == requestedState)
                return;
            CurrentPlayerState?.ExitState();
            CurrentPlayerState = requestedState;
            CurrentPlayerState.EnterState();
        }

        public void ProcessState(PlayerStateSO requestedState)
        {
            CurrentPlayerState.UpdateState();
        }
    }
}