namespace Mission_PrincessRescue
{
    class Fish : GameObject
    {
        public Fish(string textureName = "Fish") : base(textureName, DrawLayer.Middleground)
        { }

        public override void Update()
        {
            if (IsActive)
                base.Update();
        }

    }
}