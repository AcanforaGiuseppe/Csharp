namespace Mission_PrincessRescue
{
    class Crown : GameObject
    {
        public Crown(string textureName = "Crown") : base(textureName, DrawLayer.Playground)
        { }

        public override void Update()
        {
            if (IsActive)
                base.Update();
        }

    }
}