using OpenTK;

namespace Mission_PrincessRescue
{
    enum RigidBodyType { Player = 1, Enemy = 2, TileObj = 4, Item = 8 }

    class RigidBody
    {
        protected uint collisionMask;

        public Vector2 Velocity;
        public GameObject GameObject;
        public bool IsGravityAffected;
        public bool IsCollisionsAffected = true;
        public RigidBodyType Type;
        public Collider Collider;
        public float Friction;
        public bool IsActive { get { return GameObject.IsActive; } set { GameObject.IsActive = value; } }
        public Vector2 Position { get { return GameObject.Position; } }

        public RigidBody(GameObject owner)
        {
            GameObject = owner;
            PhysicsMgr.AddItem(this);
        }

        protected void ApplyFriction()
        {
            if (Friction != 0 && Velocity != Vector2.Zero)
            {
                float fAmount = Friction * Game.DeltaTime;
                float newVelocityLength = Velocity.Length - fAmount;

                if (newVelocityLength < 0)
                    newVelocityLength = 0;

                Velocity = Velocity.Normalized() * newVelocityLength;
            }
        }

        public void Update()
        {
            if (IsGravityAffected)
                Velocity.Y += PhysicsMgr.G * Game.DeltaTime;

            ApplyFriction();

            GameObject.Position += Velocity * Game.DeltaTime;
        }

        public void AddCollisionType(RigidBodyType type)
        {
            collisionMask |= (uint)type;
        }

        public void AddCollisionType(uint value)
        {
            collisionMask |= value;
        }

        public bool CollisionTypeMatches(RigidBodyType type)
        {
            return ((uint)type & collisionMask) != 0;
        }

        public bool Collides(RigidBody other, ref Collision collisionInfo)
        {
            return Collider.Collides(other.Collider, ref collisionInfo);
        }

        public virtual void Destroy()
        {
            GameObject = null;
            Collider = null;
            PhysicsMgr.RemoveItem(this);
        }

    }
}