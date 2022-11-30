using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Font : GameObject
    {
        protected float counter;
        protected int method;
        protected Vector2 endPos;
        protected bool isTimed;
        public FontType Type;

        public Font(string textureName, int w = 0, int h = 0) : base(textureName, w, h)
        {
            rigidBody = new RigidBody(this);
            IsActive = false;
            UpdateMgr.AddItem(this);
        }

        public void Spawn(int method, Vector2 pos, Vector2 endPos, Vector2 velocity, bool isTimed) //0: Scorrimento laterale/verticale - 1: Effetto dissolvenza
        {
            sprite.position = pos;
            this.method = method;
            this.endPos = endPos;
            this.isTimed = isTimed;
            switch (method)
            {
                case 0:
                    rigidBody.Velocity = velocity;
                    break;
                case 1:
                    counter = 0;
                    break;
                default:
                    break;
            }
        }

        public override void Update()
        {
            if (IsActive)
            {
                switch (method)
                {
                    case 0:
                        float dist = (endPos - sprite.position).Length;
                        if (dist <= 3)
                        {
                            sprite.position = endPos;
                            rigidBody.Velocity = Vector2.Zero;
                        }
                        break;
                    case 1:
                        counter += Game.DeltaTime;
                        sprite.SetAdditiveTint((int)(counter * 255), (int)(counter * 255), (int)(counter * 255), 0);
                        break;
                } 
            }
        }
    }
}
