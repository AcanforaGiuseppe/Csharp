using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledPlugin
{
    abstract class Collider
    {
        public Vector2 Offset;
        public RigidBody RigidBody;

        public Vector2 Position { get { return RigidBody.Position + Offset; } }

        public Collider(RigidBody owner)
        {
            Offset = Vector2.Zero;
            RigidBody = owner;
        }

        public abstract bool Collides(Collider aCollider);
        public abstract bool Collides(CircleCollider circle);
        public abstract bool Collides(BoxCollider box);
        public abstract bool Contains(Vector2 point);

    }
}