using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class GameObject : IUpdatable, IDrawable
    {
        protected Texture texture;
        protected Sprite sprite;

        protected RigidBody rigidBody;
        protected Animation[] animations;

        public Vector2 Position
        {
            get { return sprite.position; }
            set { sprite.position = value; }
        }
        public int HalfHeight { get; protected set; }
        public int HalfWidth { get; protected set; }
        public bool IsActive;

        public GameObject(string textureName, int w = 0, int h = 0)
        {
            texture = GfxMgr.GetTexture(textureName);
            sprite = new Sprite(w == 0 ? texture.Width : w, h == 0 ? texture.Height : h);
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);

            HalfHeight = (int)(sprite.Height * 0.5f);
            HalfWidth = (int)(sprite.Width * 0.5f);
            DrawMgr.AddItem(this);
        }

        public virtual void Update()
        {
        }

        public virtual void Draw()
        {
            if (IsActive)
            {
                sprite.DrawTexture(texture);
                //sprite.DrawWireframe(255, 255, 255);
            }
        }
    }
}
