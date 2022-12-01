namespace Mission_PrincessRescue
{
    class Bucket : GameObject
    {
        public Bucket(string textureName = "Bucket") : base(textureName, DrawLayer.Middleground)
        { }

        public override void Update()
        {
            if (IsActive)
                base.Update();
        }

    }
}