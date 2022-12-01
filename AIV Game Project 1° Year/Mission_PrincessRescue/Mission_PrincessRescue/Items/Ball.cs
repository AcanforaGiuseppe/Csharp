namespace Mission_PrincessRescue
{
    class Ball : GameObject
    {
        public Ball(string textureName = "Ball") : base(textureName, DrawLayer.Middleground)
        { }

        public override void Update()
        {
            if (IsActive)
                base.Update();
        }

    }
}