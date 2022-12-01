namespace Mission_PrincessRescue
{
    class Skull : GameObject
    {
        public Skull(string textureName = "Skull") : base(textureName, DrawLayer.Playground)
        { }

        public override void Update()
        {
            if (IsActive)
                base.Update();
        }

    }
}