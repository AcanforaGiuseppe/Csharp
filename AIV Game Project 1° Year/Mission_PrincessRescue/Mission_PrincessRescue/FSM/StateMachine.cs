using System.Collections.Generic;

namespace Mission_PrincessRescue
{
    class StateMachine
    {
        private Dictionary<StateEnum, State> states;
        private State current;
        private StateEnum initialState;
        private bool firstTime;

        public StateMachine()
        {
            states = new Dictionary<StateEnum, State>();
            current = null;
            firstTime = true;
        }

        public void AddState(StateEnum key, State state)
        {
            states[key] = state;
            state.SetStateMachine(this);
        }

        public void GoTo(StateEnum key)
        {
            if (current != null)
                current.OnExit();

            current = states[key];
            current.OnEnter();
        }

        public void Update()
        {
            if (firstTime)
            {
                firstTime = false;
                GoTo(initialState);
            }
            current.Update();
        }

        public void SetFirstState(StateEnum initial)
        {
            initialState = initial;
        }

    }
}