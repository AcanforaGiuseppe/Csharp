using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class RigidBody
    {
        public Vector2 Velocity;
        public GameObject Owner;
        public bool IsGravityAffected;
        public bool IsCollisionAffected;
        public float Gravity;
        public bool IsActive
        {
            get { return Owner.IsActive; }
        }

        public RigidBody(GameObject Owner, bool gravityAffected = false, bool collisionAffected = false)
        {
            this.Owner = Owner;
            Velocity = Vector2.Zero;
            IsGravityAffected = gravityAffected;
            IsCollisionAffected = collisionAffected;

            PhysicsMgr.AddItem(this);

            Gravity = 2000;
        }

        public void Update()
        {
            if (IsGravityAffected)
                Velocity.Y += Gravity * Game.DeltaTime;
            Owner.Position += Velocity * Game.DeltaTime;
        }
    }
}
