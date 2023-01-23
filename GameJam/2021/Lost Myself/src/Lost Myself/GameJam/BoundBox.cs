using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class BoundBox : GameObject
    {
        public BoundBox(Vector2 pos, int w = 0, int h = 0) : base("background", w, h)
        {
            sprite.position = pos;
        }

        public override void Draw()
        {
            sprite.DrawColor(0, 255, 0);
        }
    }
}
