using System;

namespace TiledPlugin
{
    class ColliderFactory
    {
        public static Collider CreateCircleFor(GameObject obj)
        {
            float halfDiagonal = (float)(Math.Sqrt(obj.Width * obj.Width + obj.Height * obj.Height)) * 0.5f;
            return new CircleCollider(obj.RigidBody, halfDiagonal);
        }

        public static Collider CreateBoxFor(GameObject obj)
        {
            return new BoxCollider(obj.RigidBody, obj.Width, obj.Height);
        }

    }
}