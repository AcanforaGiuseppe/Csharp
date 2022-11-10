using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace TiledPlugin
{
    class BoxCollider : Collider
    {
        protected float halfWidth;
        protected float halfHeight;

        public float Height { get { return halfHeight * 2; } }
        public float Width { get { return halfWidth * 2; } }

        public BoxCollider(RigidBody owner, int w, int h) : base(owner)
        {
            halfWidth = w * 0.5f;
            halfHeight = h * 0.5f;
        }

        public override bool Collides(Collider aCollider)
        {
            return aCollider.Collides(this);
        }

        //Box vs Circle
        public override bool Collides(CircleCollider circle)
        {
            float deltaX = circle.Position.X - Math.Max(Position.X - halfWidth, Math.Min(circle.Position.X, Position.X + halfWidth));
            float deltaY = circle.Position.Y - Math.Max(Position.Y - halfHeight, Math.Min(circle.Position.Y, Position.Y + halfHeight));

            return (deltaX * deltaX + deltaY * deltaY) < (circle.Radius * circle.Radius);
        }

        //Box vs Box
        public override bool Collides(BoxCollider box)
        {
            float deltaX = box.Position.X - Position.X;
            float deltaY = box.Position.Y - Position.Y;

            return (Math.Abs(deltaX) <= halfWidth + box.halfWidth) && (Math.Abs(deltaY) <= halfHeight + box.halfHeight);
        }

        public override bool Contains(Vector2 point)
        {
            return
                point.X >= Position.X - halfWidth &&
                point.X <= Position.X + halfWidth &&
                point.Y >= Position.Y - halfHeight &&
                point.Y <= Position.Y + halfHeight;
        }

    }
}