using UnityEngine;

namespace PlayerSystem.PlayerStateSystem
{
    public class ShootingState : PlayerState
    {
        private Player _player;
        private GameObject _bulletPrefab;

        public ShootingState(Player player, GameObject bulletPrefab)
        {
            _player = player;
            _bulletPrefab = bulletPrefab;
        }

        public override void Enter()
        {
            Debug.Log("Enter Shooting State");
        }

        public override void Exit()
        {
            Debug.Log("Exit Shooting State");
        }

        public override void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            GameObject bullet = GameObject.Instantiate(_bulletPrefab, _player.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().linearVelocity = _player.transform.forward * 20f;
        }
    }
}