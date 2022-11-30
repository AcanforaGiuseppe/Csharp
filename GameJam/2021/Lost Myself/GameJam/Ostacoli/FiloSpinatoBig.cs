using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class FiloSpinatoBig : Obstacle
    {
        public FiloSpinatoBig() : base("ostacolone")
        {
            Type = ObstacleType.FILOSPINATOBIG;
            int offset = -130;
            Colliders.AddCollider(new CircleCollider(this, new Vector2(30, -15 + offset), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(65, -15 + offset), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(100, -15 + offset), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(135, -15 + offset), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(170, -15 + offset), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(180, -15 + offset), 35));

            Colliders.AddCollider(new CircleCollider(this, new Vector2(30, -50 + offset), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(30, -85 + offset), 35));

            Colliders.AddCollider(new CircleCollider(this, new Vector2(30, -100 + offset), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(65, -100 + offset), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(100, -100 + offset), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(135, -100 + offset), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(170, -100 + offset), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(180, -100 + offset), 35));
        }

        public override void Spawn()
        {
            sprite.position = new Vector2(Game.Win.Width, TerrainMgr.YPos);
        }
    }
}
