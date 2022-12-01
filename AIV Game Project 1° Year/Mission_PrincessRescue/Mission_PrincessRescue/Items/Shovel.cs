namespace Mission_PrincessRescue
{
    class Shovel : GameObject
    {
        public Shovel(string textureName = "Shovel") : base(textureName, DrawLayer.Middleground)
        { }

        public override void Update()
        {
            if (IsActive)
                base.Update();
        }

    }
}