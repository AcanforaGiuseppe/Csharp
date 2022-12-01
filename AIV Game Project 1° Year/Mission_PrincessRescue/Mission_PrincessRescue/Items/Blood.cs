namespace Mission_PrincessRescue
{
    class Blood : GameObject
    {
        public Blood(string textureName = "Blood") : base(textureName, DrawLayer.Middleground)
        { }

        public override void Update()
        {
            if (IsActive)
                base.Update();
        }

    }
}