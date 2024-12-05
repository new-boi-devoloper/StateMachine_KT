using System;
using UnityEngine;

namespace PlayerSystem
{
    public class InputListener : MonoBehaviour
    {
        public event Action<Vector3> OnMove;
        public event Action OnAttack;
        public event Action OnChangeState;
        public event Action OnPause;
        public event Action OnFinal;

        private void Update()
        {
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (moveDirection != Vector3.zero && OnMove != null)
            {
                OnMove(moveDirection);
            }

            if (Input.GetButtonDown("Fire1") && OnAttack != null)
            {
                OnAttack();
            }

            if (Input.GetKeyDown(KeyCode.Return) && OnChangeState != null)
            {
                OnChangeState();
            }

            if (Input.GetKeyDown(KeyCode.Space) && OnPause != null)
            {
                OnPause();
            }

            if (Input.GetKeyDown(KeyCode.Escape) && OnFinal != null)
            {
                OnFinal();
            }
        }
    }
}