using System;
using System.Collections.Generic;

namespace PlayerSystem.PlayerStateSystem
{
    public class PlayerStateMachine
    {
        public PlayerState currentState;
        private readonly Dictionary<Type, PlayerState> _states = new();
        public event Action<PlayerState> OnStateChanged;

        public PlayerStateMachine(params PlayerState[] states)
        {
            foreach (var state in states)
            {
                _states.Add(state.GetType(), state);
            }
        }

        public void ChangeState<T>() where T : PlayerState
        {
            if (currentState != null) currentState.Exit();

            currentState = _states[typeof(T)];
            currentState.Enter();
            OnStateChanged?.Invoke(currentState);

        }

        public void Update()
        {
            if (currentState != null) currentState.Update();
            OnStateChanged?.Invoke(currentState);

        }
    }
}