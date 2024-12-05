using System;
using PlayerSystem.PlayerStateSystem;
using PlayerSystem.PlayerStateSystem.States;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerInvoker
    {
        private readonly InputListener _inputListener;
        private readonly Player _player;
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerStateMachine _stateMachine;

        public PlayerInvoker(PlayerMovement playerMovement, Player player, InputListener inputListener, PlayerStateMachine stateMachine)
        {
            _playerMovement = playerMovement;
            _player = player;
            _inputListener = inputListener;
            _stateMachine = stateMachine;
        }

        public void HandleMove(Vector3 direction)
        {
            _playerMovement.Move(_player.PlayerRb, direction, _player.MoveSpeed);
        }

        public void HandleChangeState()
        {
            if (_stateMachine.currentState is ShootingState)
            {
                _stateMachine.ChangeState<HighlightState>();
            }
            else if (_stateMachine.currentState is HighlightState)
            {
                _stateMachine.ChangeState<TransparencyState>();
            }
            else if (_stateMachine.currentState is TransparencyState)
            {
                _stateMachine.ChangeState<ShootingState>();
            }
        }

        public void HandleAttack()
        {
            _stateMachine.Update();
        }
    }
}