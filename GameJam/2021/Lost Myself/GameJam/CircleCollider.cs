using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class CircleCollider : GameObject
    {
        GameObject Owner;
        Vector2 offsetPosition;
        public float Radious;

        public CircleCollider(GameObject owner, Vector2 offsetPosition, int radious) : base("background", radious, radious)
        {
            Owner = owner;
            Radious = radious * 0.5f;
            this.offsetPosition = offsetPosition;
            sprite.position = Owner.Position + offsetPosition;
            IsActive = false;
            UpdateMgr.AddItem(this);
        }

        public override void Draw()
        {
            if (IsActive)
                sprite.DrawColor(255, 0, 0);
        }

        public override void Update()
        {
            if (IsActive)
            {
                sprite.position = Owner.Position + offsetPosition;
            }
        }
    }
}
