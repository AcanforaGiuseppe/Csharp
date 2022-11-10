using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledPlugin
{
    class Background : IUpdatable, IDrawable
    {
        private Sprite head;
        private Sprite tail;
        private Texture texture;
        private float speed = -400;
        private DrawLayer layer;

        public DrawLayer Layer { get { return layer; } set { layer = value; } }

        public Background()
        {
            texture = new Texture("Assets/spaceBg.png");
            head = new Sprite(Game.Win.Width, Game.Win.Height);
            tail = new Sprite(Game.Win.Width, Game.Win.Height);
            Layer = DrawLayer.Background;
        }

        public void Update()
        {
            head.position.X += speed * Game.DeltaTime;

            if (head.position.X < -head.Width)
                head.position.X += head.Width;

            tail.position.X = head.position.X + head.Width;
        }

        public void Draw()
        {
            head.DrawTexture(texture);
            tail.DrawTexture(texture);
        }

    }
}