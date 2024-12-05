using UnityEngine;

namespace PlayerSystem.PlayerStateSystem.States
{
    public class TransparencyState : PlayerState
    {
        private bool _isTransparent;
        private readonly Player _player;

        public TransparencyState(Player player)
        {
            _player = player;
        }

        public override void Enter()
        {
            Debug.Log("Enter Transparency State");
            SetTransparency(false);
        }

        public override void Exit()
        {
            Debug.Log("Exit Transparency State");
            SetTransparency(false);
        }

        public override void Update()
        {
            _isTransparent = !_isTransparent;
            SetTransparency(_isTransparent);
        }

        private void SetTransparency(bool transparent)
        {
            _player.GetComponent<MeshRenderer>().enabled = !transparent;
        }
    }
}