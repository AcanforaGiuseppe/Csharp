namespace Mission_PrincessRescue
{
    class Leaf : GameObject
    {
        public Leaf(string textureName = "Leaf") : base(textureName, DrawLayer.Middleground)
        { }

        public override void Update()
        {
            if (IsActive)
                base.Update();
        }

    }
}