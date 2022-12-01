namespace Mission_PrincessRescue
{
    class Umbrella : GameObject
    {
        public Umbrella(string textureName = "Umbrella") : base(textureName, DrawLayer.Playground)
        { }

        public override void Update()
        {
            if (IsActive)
                base.Update();
        }

    }
}