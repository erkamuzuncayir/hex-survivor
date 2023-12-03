namespace _Script.PersonalAPI.StateMachine
{
    public interface IStateMachine<TStateMachine, TState>
    {
        public void InitStates();

        public void HandleState(TState requestedState);

        public void SetState(TState requestedState);
    }
}
