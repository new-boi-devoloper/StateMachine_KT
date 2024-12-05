using System;
using System.Collections.Generic;

namespace Core
{
    public class GameStateMachine<T> : IStateMachine where T : BaseGameState
    {
        public BaseGameState currentState;
        private readonly Dictionary<Type, T> _states;
        public event Action<BaseGameState> OnStateChanged;

        public GameStateMachine(params T[] states)
        {
            _states = new Dictionary<Type, T>();
            foreach (var state in states)
            {
                _states.Add(state.GetType(), state);
            }

            InitState();
        }

        public bool ChangeState<T>() where T : BaseGameState
        {
            if (_states.ContainsKey(typeof(T)))
            {
                currentState?.ExitState();
                currentState = _states[typeof(T)];
                currentState.EnterState();
                
                OnStateChanged?.Invoke(currentState);
                return true;
            }

            return false;
        }

        public void Update()
        {
            currentState.UpdateState();
            OnStateChanged?.Invoke(currentState);
        }

        private void InitState()
        {
            foreach (var state in _states) state.Value.InjectOwner(this);
        }
    }

    public interface IStateMachine
    {
        bool ChangeState<T>() where T : BaseGameState;
        void Update();
    }
}