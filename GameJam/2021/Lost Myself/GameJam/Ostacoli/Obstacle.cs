using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    abstract class Obstacle : GameObject
    {
        public ObstacleType Type;
        public Colliders Colliders;
        public float Speed
        {
            set { rigidBody.Velocity.X = value; }
        }

        public Obstacle(string textureName, int w = 0, int h = 0) : base(textureName, w, h)
        {
            sprite.pivot = new Vector2(0, sprite.Height);
            sprite.scale = new Vector2(0.35f);
            rigidBody = new RigidBody(this);
            rigidBody.Velocity.X = Game.GlobalSpeed;
            IsActive = false;
            Colliders = new Colliders();
            UpdateMgr.AddItem(this);
        }

        public override void Update()
        {
            if (IsActive)
            {
                //Controlla se esce a sinistra
                if (sprite.position.X < 0 - sprite.Width * sprite.scale.X)
                {
                    //Se si, lo rimetto nelle queue
                    ObstacleMgr.RestoreObstacle(this);
                }
            }
        }

        public abstract void Spawn();

    }
}
