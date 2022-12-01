namespace Mission_PrincessRescue
{
    class Bow : GameObject
    {
        public Bow(string textureName = "Bow") : base(textureName, DrawLayer.Middleground)
        { }

        public override void Update()
        {
            if (IsActive)
                base.Update();
        }

    }
}