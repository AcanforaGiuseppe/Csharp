namespace Mission_PrincessRescue
{
    class WalkState : State
    {
        private Player player;

        public WalkState(Player player)
        {
            this.player = player;
        }

        public override void OnEnter()
        {
            player.ComputePlayerPoint();
        }

        public override void Update()
        {
            if (player.target != null)
            {
                player.ComputePlayerPoint();
                player.HeadToPoint();
            }
        }

    }
}