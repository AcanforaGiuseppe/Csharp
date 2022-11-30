using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Fence : Obstacle
    {
        public Fence() : base("obstacoletto")
        {
            Type = ObstacleType.FENCE;
            sprite.scale.X = 1.05f;
            Colliders.AddCollider(new CircleCollider(this, new Vector2(sprite.Width * 0.5f + 2, -15), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(sprite.Width * 0.5f + 2, -50), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(sprite.Width * 0.5f + 2, -85), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(sprite.Width * 0.5f + 2, -120), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(sprite.Width * 0.5f + 2, -128), 35));
        }

        public override void Spawn()
        {
            sprite.position = new Vector2(Game.Win.Width, TerrainMgr.YPos);
        }
    }
}
