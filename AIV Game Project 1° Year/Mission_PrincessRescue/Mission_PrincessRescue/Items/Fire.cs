namespace Mission_PrincessRescue
{
    class Fire : GameObject
    {
        private Player player;

        public Fire(string textureName = "Fire") : base(textureName, DrawLayer.Background)
        { }

        public override void Update()
        {
            if (IsActive)
                base.Update();
        }

    }
}