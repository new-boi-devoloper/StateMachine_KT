using UnityEngine;

namespace PlayerSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        public Rigidbody PlayerRb { get; private set; }

        private void Start()
        {
            PlayerRb = GetComponent<Rigidbody>();
        }
    }
}