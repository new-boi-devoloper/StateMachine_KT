using System;
using PlayerSystem;
using PlayerSystem.PlayerStateSystem;
using PlayerSystem.PlayerStateSystem.States;
using UISystem;
using UnityEngine;

namespace Core
{
    public class Boostrapper : MonoBehaviour
    {

        #region View

        [field: SerializeField] private PlayerView playerView;
        
        #endregion
        #region PlayerController

        [field: SerializeField] private Player player;
        [field: SerializeField] private InputListener inputListener;
        [field: SerializeField] private GameObject bulletPrefab;
        [field: SerializeField] private GameObject highlightCircle;

        private PlayerMovement _playerMovement;
        private PlayerInvoker _playerInvoker;

        #endregion

        #region PlayerStates

        private PlayerStateMachine _playerStateMachine;
        private ShootingState _shootingState;
        private HighlightState _highlightState;
        private TransparencyState _transparencyState;

        #endregion

        #region GameStates

        private GameStateMachine<BaseGameState> _gameStateMachine;
        private GameState _gameState;
        private PauseState _pauseState;
        private FinalState _finalState;

        #endregion
        private void Awake()
        {

            PlayerStatesBinds();

            PlayerControllsBinds();

            GameStateBinds();

            UIBinds();
            
            inputListener.OnPause += HandlePause;
            inputListener.OnFinal += HandleFinal;

            StartGame();
        }

        private void PlayerStatesBinds()
        {
            _shootingState = new ShootingState(player, bulletPrefab);
            _highlightState = new HighlightState(player, highlightCircle);
            _transparencyState = new TransparencyState(player);

            _playerStateMachine = new PlayerStateMachine(_shootingState, _highlightState, _transparencyState);
        }

        private void PlayerControllsBinds()
        {
            _playerMovement = new PlayerMovement();
            _playerInvoker = new PlayerInvoker(_playerMovement, player, inputListener, _playerStateMachine);
        }
        
        private void GameStateBinds()
        {
            _gameState = new GameState(inputListener, _playerInvoker);
            _pauseState = new PauseState(inputListener);
            _finalState = new FinalState(player, highlightCircle);

            _gameStateMachine = new GameStateMachine<BaseGameState>(_gameState, _pauseState, _finalState);
        }

        private void UIBinds()
        {
            playerView.Construct(_gameStateMachine, _playerStateMachine);
        }

        private void OnDestroy()
        {
            inputListener.OnPause -= HandlePause;
            inputListener.OnFinal -= HandleFinal;
        }

        private void StartGame()
        {
            _playerStateMachine.ChangeState<ShootingState>();
            _gameStateMachine.ChangeState<PauseState>();
        }
        
        private void HandlePause()
        {
            if (_gameStateMachine.currentState is GameState)
            {
                _gameStateMachine.ChangeState<PauseState>();
            }
            else if (_gameStateMachine.currentState is PauseState)
            {
                _gameStateMachine.ChangeState<GameState>();
            }
        }

        private void HandleFinal()
        {
            _gameStateMachine.ChangeState<FinalState>();
        }
    }
}