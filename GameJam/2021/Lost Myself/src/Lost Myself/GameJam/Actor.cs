using Aiv.Audio;
using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    abstract class Actor : GameObject
    {
        public Actor(string textureName, Vector2 startPosition, int w=0, int h=0) : base(textureName, w, h)
        {
            sprite.position = startPosition;
            rigidBody = new RigidBody(this);
            UpdateMgr.AddItem(this);
        }
    }
}
