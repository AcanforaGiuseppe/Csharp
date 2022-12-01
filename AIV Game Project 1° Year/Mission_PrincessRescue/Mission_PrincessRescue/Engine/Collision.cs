using OpenTK;

namespace Mission_PrincessRescue
{
    enum CollisionType { None, RectsIntersection, CirclesIntersection, CircleRectIntersection }

    struct Collision
    {
        public GameObject Collider;
        public Vector2 Delta;
        public CollisionType Type;
    }
}