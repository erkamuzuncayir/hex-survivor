namespace _Script.PersonalAPI.StateMachine
{
    public interface IState<TStateMachine, TState>
    {
        public void InitState(IStateMachine<TStateMachine, TState> stateMachine);
    
        public void EnterState();
    
        public void UpdateState();

        public void ExitState();
    }
}
