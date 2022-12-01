namespace Mission_PrincessRescue
{
    abstract class State
    {
        virtual public void OnEnter() { }
        virtual public void OnExit() { }
        abstract public void Update();

        protected StateMachine stateMachine;

        public void SetStateMachine(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

    }
}