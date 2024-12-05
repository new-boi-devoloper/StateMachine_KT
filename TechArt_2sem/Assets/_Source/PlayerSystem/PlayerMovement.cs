using UnityEngine;

namespace PlayerSystem
{
    public class PlayerMovement
    {
        public void Move(Rigidbody playerRb, Vector3 direction, float speed)
        {
            var moveDirection = direction.normalized * speed;
            playerRb.linearVelocity = new Vector3(moveDirection.x, playerRb.linearVelocity.y, moveDirection.z);
        }
    }
}