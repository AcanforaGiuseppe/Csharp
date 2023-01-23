using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class FiloSpinato : Obstacle
    {
        public FiloSpinato() : base("ostacolino")
        {
            Type = ObstacleType.FILOSPINATO;
            Colliders.AddCollider(new CircleCollider(this, new Vector2(38, -15), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(38, -50), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(38, -85), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(38, -100), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(65, -100), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(100, -100), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(135, -100), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(170, -100), 35));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(173, -100), 35));
        }

        public override void Spawn()
        {
            sprite.position = new Vector2(Game.Win.Width, TerrainMgr.YPos);
        }
    }
}
