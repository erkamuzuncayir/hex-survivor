using System.Collections.Generic;
using _Script.Actors;
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
        public PlayerStateSO so_state_PlayerIdle_Unselected;
        public PlayerStateSO so_state_PlayerIdle_Selected;
        public PlayerStateSO so_state_PlayerOffTurn;
        public PlayerStateSO so_state_PlayerMove;
        public PlayerStateSO so_state_PlayerAttack;

        // Input Handler
        [SerializeField] private ClickInputHandler _clickInputHandler;
        
        // Cache fields
        [SerializeField] private GameObject _go_player;
        [SerializeField] private PlayerDataSO _playerDataSO;
        
        private void Awake()
        {
            FillListWithStates();
            _playerDataSO.PlayerCoord = Vector3Int.zero;
        }
        
        private void FillListWithStates()
        {
            AllPlayerStates.Add(so_state_PlayerIdle_Unselected);
            AllPlayerStates.Add(so_state_PlayerIdle_Selected);
            AllPlayerStates.Add(so_state_PlayerOffTurn);
            AllPlayerStates.Add(so_state_PlayerMove);
            AllPlayerStates.Add(so_state_PlayerAttack);
        }
        
        public void InitStates()
        {
            foreach (IState<PlayerStateMachine, PlayerStateSO> state in AllPlayerStates)
                state.InitState(this);
        }

        private void Start()
        {
            InitStates();
            HandleState(so_state_PlayerIdle_Unselected);
        }

        private void OnEnable()
        {
            _clickInputHandler.OnClickPerformed += OnMouseClickPerformed;
        }

        private void OnDisable()
        {
            _clickInputHandler.OnClickPerformed -= OnMouseClickPerformed;
        }
        
        public void OnMouseClickPerformed(Vector2 inputWorldPos)
        {
            switch (CurrentPlayerState)
            {
                case IdleSelectedPlayerStateSO:
                {
                    HandleState(so_state_PlayerIdle_Unselected);
                    break;
                }
                case IdleUnselectedPlayerStateSO:
                {
                    HandleState(so_state_PlayerIdle_Selected);
                    break;
                }
            }
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