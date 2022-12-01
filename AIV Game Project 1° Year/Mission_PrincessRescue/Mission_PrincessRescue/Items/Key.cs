namespace Mission_PrincessRescue
{
    class Key : GameObject
    {
        public Key(string textureName = "Key") : base(textureName, DrawLayer.Playground)
        {
            RigidBody = new RigidBody(this);
            RigidBody.Type = RigidBodyType.Item;
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.AddCollisionType(RigidBodyType.Player);
        }

        public override void Update()
        {
            if (IsActive)
                base.Update();
        }
        public override void OnCollide(Collision collisionInfo)
        {
            base.OnCollide(collisionInfo);
            IsActive = false;
        }

    }
}