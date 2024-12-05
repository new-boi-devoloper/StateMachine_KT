using System;
using Core;
using PlayerSystem.PlayerStateSystem;
using TMPro;
using UnityEngine;

namespace UISystem
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] private TextMeshProUGUI playerStateText;
        [field: SerializeField] private TextMeshProUGUI gameStateText;
        
        private GameStateMachine<BaseGameState> _gameStateMachine;
        private PlayerStateMachine _playerStateMachine;
        
        public void Construct(GameStateMachine<BaseGameState> gameStateMachine, PlayerStateMachine playerStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _playerStateMachine = playerStateMachine;
            
            _gameStateMachine.OnStateChanged += UpdateGameStateText;
            _playerStateMachine.OnStateChanged += UpdatePlayerStateText;
        }

        private void OnDestroy()
        {
            _gameStateMachine.OnStateChanged -= UpdateGameStateText;
            _playerStateMachine.OnStateChanged -= UpdatePlayerStateText;
        }

        private void UpdateGameStateText(BaseGameState state)
        {
            gameStateText.text = $"Состояние игры: {state.GetType().Name}";
        }

        private void UpdatePlayerStateText(PlayerState state)
        {
            playerStateText.text = $"Состояние игрока: {state.GetType().Name}";
        }
    }
}