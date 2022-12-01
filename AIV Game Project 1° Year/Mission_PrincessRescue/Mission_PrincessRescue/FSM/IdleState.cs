using OpenTK;

namespace Mission_PrincessRescue
{
    class IdleState : State
    {
        private Player player;

        public IdleState(Player player)
        {
            this.player = player;
            player.RigidBody.Velocity = Vector2.Zero;
        }

        public override void Update()
        { }

    }
}