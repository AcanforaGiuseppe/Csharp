using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;

namespace TiledPlugin
{
    class TileObj : GameObject
    {
        private int xOff;
        private int yOff;

        public TileObj(string texture, int posX, int posY, int tOffX, int tOffY, int width, int height) : base(texture)
        {
            sprite = new Sprite(width, height);
            sprite.position.X = posX;
            sprite.position.Y = posY;
            xOff = tOffX;
            yOff = tOffY;
            IsActive = true;
        }

        public override void Draw()
        {
            if (!IsActive)
                return;

            sprite.DrawTexture(texture, xOff, yOff, Width, Height);
        }

    }
}