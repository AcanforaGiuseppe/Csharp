using System;
using System.Collections.Generic;
using Aiv.Fast2D;
using OpenTK;

namespace Mission_PrincessRescue
{
    class GameObject : IUpdatable, IDrawable
    {
        private DrawLayer layer;

        protected Sprite sprite;
        protected Texture texture;
        protected Dictionary<ComponentType, Component> components;

        public RigidBody RigidBody;
        public bool IsActive;
        public Sprite Sprite { get { return sprite; } }
        public int Width { get { return (int)sprite.Width; } }
        public int Height { get { return (int)sprite.Height; } }
        public float HalfWidth { get; protected set; }
        public float HalfHeight { get; protected set; }
        public DrawLayer Layer { get { return layer; } set { layer = value; } }
        public virtual Vector2 Position
        {
            get { return sprite.position; }
            set { sprite.position = value; }
        }
        public float X
        {
            get { return sprite.position.X; }
            set { sprite.position.X = value; }
        }
        public float Y
        {
            get { return sprite.position.Y; }
            set { sprite.position.Y = value; }
        }
        public Vector2 Forward
        {
            get { return new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation)); }
            set { sprite.Rotation = (float)Math.Atan2(value.Y, value.X); }
        }

        public GameObject(string textureName, DrawLayer layer = DrawLayer.Playground, float w = 0, float h = 0)
        {
            texture = GfxMgr.GetTexture(textureName);
            sprite = new Sprite(w == 0 ? Game.PixelsToUnits(texture.Width) : w, h == 0 ? Game.PixelsToUnits(texture.Height) : h);
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);

            HalfWidth = sprite.Width * 0.5f;
            HalfHeight = sprite.Height * 0.5f;

            Layer = layer;

            UpdateMgr.AddItem(this);
            DrawMgr.AddItem(this);

            components = new Dictionary<ComponentType, Component>();
        }

        public virtual void Update()
        { }

        public virtual void OnCollide(Collision collisionInfo)
        { }

        public virtual void Draw()
        {
            if (IsActive)
                sprite.DrawTexture(texture);
        }

        public Component GetComponent(ComponentType type)
        {
            if (components.ContainsKey(type))
                return components[type];

            return null;
        }

        public virtual void Destroy()
        {
            sprite = null;
            texture = null;

            UpdateMgr.RemoveItem(this);
            DrawMgr.RemoveItem(this);

            if (RigidBody != null)
            {
                RigidBody.Destroy();
                RigidBody = null;
            }
        }

    }
}