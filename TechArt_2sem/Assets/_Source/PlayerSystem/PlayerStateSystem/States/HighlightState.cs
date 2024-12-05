using UnityEngine;

namespace PlayerSystem.PlayerStateSystem.States
{
    public class HighlightState : PlayerState
    {
        private readonly GameObject _highlightCircle;
        private Player _player;

        public HighlightState(Player player, GameObject highlightCircle)
        {
            _player = player;
            _highlightCircle = highlightCircle;
        }

        public override void Enter()
        {
            Debug.Log("Enter Highlight State");
            _highlightCircle.SetActive(false);

        }

        public override void Exit()
        {
            Debug.Log("Exit Highlight State");
            _highlightCircle.SetActive(false);
        }

        public override void Update()
        {
            _highlightCircle.SetActive(!_highlightCircle.activeSelf);
        }
    }
}