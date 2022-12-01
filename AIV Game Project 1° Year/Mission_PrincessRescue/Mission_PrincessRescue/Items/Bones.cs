namespace Mission_PrincessRescue
{
    class Bones : GameObject
    {
        public Bones(string textureName = "Bones") : base(textureName, DrawLayer.Middleground)
        { }

        public override void Update()
        {
            if (IsActive)
                base.Update();
        }

    }
}