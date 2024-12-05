using PlayerSystem;
using UnityEngine;

namespace Core
{
    public abstract class BaseGameState
    {
        protected IStateMachine owner;

        public void InjectOwner(IStateMachine ownerInput)
        {
            owner = ownerInput;
        }

        public virtual void EnterState()
        {
        }

        public virtual void UpdateState()
        {
        }

        public virtual void ExitState()
        {
        }
    }

    public class FinalState : BaseGameState
    {
        private readonly Player _player;
        private readonly GameObject _highlightCircle;

        public FinalState(Player player, GameObject highlightCircle)
        {
            _player = player;
            _highlightCircle = highlightCircle;
        }

        public override void EnterState()
        {
            Debug.Log("Enter Final State");
            _player.GetComponent<MeshRenderer>().material.color = Color.green;
            _player.GetComponent<MeshRenderer>().enabled = true;
            _highlightCircle.SetActive(false);
            Time.timeScale = 0f;
        }

        public override void ExitState()
        {
            Debug.Log("Exit Final State");
        }
    }


    public class PauseState : BaseGameState
    {
        private readonly InputListener _inputListener;

        public PauseState(InputListener inputListener)
        {
            _inputListener = inputListener;
        }

        public override void EnterState()
        {
            Debug.Log("Enter Pause State");
            Time.timeScale = 0f;
            _inputListener.OnPause += HandlePause;
        }

        public override void ExitState()
        {
            Debug.Log("Exit Pause State");
            _inputListener.OnPause -= HandlePause;
        }

        private void HandlePause()
        {
            owner.ChangeState<GameState>();
        }
    }
    
    public class GameState : BaseGameState
    {
        private readonly InputListener _inputListener;
        private readonly PlayerInvoker _playerInvoker;

        public GameState(InputListener inputListener, PlayerInvoker playerInvoker)
        {
            _inputListener = inputListener;
            _playerInvoker = playerInvoker;
        }

        public override void EnterState()
        {
            Debug.Log("Enter Game State");
            Time.timeScale = 1f;
            _inputListener.OnMove += _playerInvoker.HandleMove;
            _inputListener.OnAttack += _playerInvoker.HandleAttack;
            _inputListener.OnChangeState += _playerInvoker.HandleChangeState;
        }

        public override void ExitState()
        {
            Debug.Log("Exit Game State");
            _inputListener.OnMove -= _playerInvoker.HandleMove;
            _inputListener.OnAttack -= _playerInvoker.HandleAttack;
            _inputListener.OnChangeState -= _playerInvoker.HandleChangeState;
        }
    }
}