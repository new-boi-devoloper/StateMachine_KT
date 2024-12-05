namespace PlayerSystem.PlayerStateSystem
{
    public abstract class PlayerState
    {
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }
}