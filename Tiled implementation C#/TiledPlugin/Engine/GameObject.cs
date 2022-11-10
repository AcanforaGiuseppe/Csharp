using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledPlugin
{
    class GameObject : IUpdatable, IDrawable
    {
        protected Sprite sprite;
        protected Texture texture;

        public RigidBody RigidBody;
        public bool IsActive;

        private DrawLayer layer;

        public DrawLayer Layer { get { return layer; } set { layer = value; } }
        public Vector2 Position { get { return sprite.position; } set { sprite.position = value; } }
        public int Width { get { return (int)sprite.Width; } }
        public int Height { get { return (int)sprite.Height; } }
        public int X { get { return (int)sprite.position.X; } set { sprite.position.X = value; } }
        public int Y { get { return (int)sprite.position.Y; } set { sprite.position.Y = value; } }

        public GameObject(string textureName)
        {
            Layer = DrawLayer.Playground;
            texture = GfxMgr.GetTexture(textureName);
            sprite = new Sprite(texture.Width, texture.Height);
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);

            UpdateMgr.AddItem(this);
            DrawMgr.AddItem(this);
        }

        public virtual void Update()
        { }

        public virtual void Draw()
        {
            if (IsActive)
                sprite.DrawTexture(texture);
        }

        public virtual void OnCollide(GameObject other)
        { }

    }
}