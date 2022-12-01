using OpenTK;

namespace Mission_PrincessRescue
{
    abstract class Collider
    {
        public RigidBody RigidBody;
        public Vector2 Offset;
        public Vector2 Position { get { return RigidBody.Position + Offset; } }

        public Collider(RigidBody owner)
        {
            RigidBody = owner;
            Offset = Vector2.Zero;
        }

        public abstract bool Collides(Collider collider, ref Collision collisionInfo);
        public abstract bool Collides(BoxCollider collider, ref Collision collisionInfo);
        public abstract bool Collides(CircleCollider circle, ref Collision collisionInfo);
        public abstract bool Collides(CompoundCollider compound, ref Collision collisionInfo);
        public abstract bool Contains(Vector2 point);

    }
}