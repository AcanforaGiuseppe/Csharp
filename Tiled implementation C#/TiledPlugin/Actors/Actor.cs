using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledPlugin
{
    abstract class Actor : GameObject
    {
        protected float speed = 250;
        protected int energy;
        protected int maxEnergy;
        protected Vector2 cannonOffset;

        public Actor(string textureName) : base(textureName)
        {
            RigidBody = new RigidBody(this);
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);

            Energy = maxEnergy = 100;
        }

        public Vector2 Velocity
        {
            get
            {   // Read
                return RigidBody.Velocity;
            }
            set
            {   // Write
                RigidBody.Velocity = value; // The value passed in player.Velocity
                if (RigidBody.Velocity.Length > speed)
                    // If he's longer than speed, force velocity to speed
                    RigidBody.Velocity = RigidBody.Velocity.Normalized() * speed;
            }
        }

        public virtual void Reset()
        {
            IsActive = true;
            energy = maxEnergy;
        }

        public int Energy { get { return energy; } set { energy = Math.Min(value, maxEnergy); } }
        public bool IsAlive { get { return Energy > 0; } }

        public abstract void OnDie();

        protected virtual void Shoot()
        { }

        public virtual bool AddDamage(int damage)
        {
            Energy -= damage;

            if (energy <= 0)
            {
                OnDie();
                return true;
            }
            return false;
        }

    }
}