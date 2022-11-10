using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledPlugin
{
    class CircleCollider : Collider
    {
        public float Radius;

        public CircleCollider(RigidBody owner, float radius):base(owner)
        {
            Radius = radius;
        }

        public override bool Collides(Collider aCollider)
        {
            return aCollider.Collides(this);
        }

        //Circle vs Circle
        public override bool Collides(CircleCollider other)
        {
            Vector2 dist = other.Position - Position;
            return dist.LengthSquared <= Math.Pow(Radius + other.Radius,2);
        }

        //Circle vs Box
        public override bool Collides(BoxCollider box)
        {
            return box.Collides(this);
        }

        public override bool Contains(Vector2 point)
        {
            Vector2 distFromCenter = point - Position;
            return distFromCenter.Length <= Radius;
        }

    }
}