using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Tile : GameObject
    {
        public TileType Type;

        public float PositionX
        {
            get { return sprite.position.X; }
            set { sprite.position.X = value; }
        }
        public float PositionY
        {
            get { return sprite.position.Y; }
            set { sprite.position.Y = value; }
        }
        public float Speed
        {
            set { rigidBody.Velocity.X = value; }
        }

        public Tile(string nameTexture) : base(nameTexture)
        {
            sprite.pivot = new Vector2(0, sprite.Height * 0.9f);
            sprite.scale = new Vector2(0.75f);
            rigidBody = new RigidBody(this);
            rigidBody.Velocity.X = Game.GlobalSpeed;
            UpdateMgr.AddItem(this);
            IsActive = false;
        }

        public override void Update()
        {
            if (IsActive)
            {
                if (sprite.position.X < 0 - sprite.Width)
                {
                    TerrainMgr.RestoreTile(this);
                }
            }
        }
    }
}
