namespace Mission_PrincessRescue
{
    class Falo : GameObject
    {
        public Falo(string textureName = "Falo") : base(textureName, DrawLayer.Background)
        { }

        public override void Update()
        {
            if (IsActive)
                base.Update();
        }

    }
}